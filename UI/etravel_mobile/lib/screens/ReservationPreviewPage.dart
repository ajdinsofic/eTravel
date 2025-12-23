import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/helper/date_converter.dart';
import 'package:etravel_app/models/reservation_preview.dart';
import 'package:etravel_app/models/user.dart';
import 'package:etravel_app/providers/offer_provider.dart';
import 'package:etravel_app/providers/payment_provider.dart';
import 'package:etravel_app/providers/paypal_provider.dart';
import 'package:etravel_app/providers/reservation_preview_provider.dart';
import 'package:etravel_app/providers/reservation_provider.dart';
import 'package:etravel_app/providers/user_provider.dart';
import 'package:etravel_app/providers/user_voucher_provider.dart';
import 'package:etravel_app/providers/voucher_provider.dart';
import 'package:etravel_app/screens/PayPalWebViewPage.dart';
import 'package:etravel_app/screens/StartingPage.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:etravel_app/widgets/ReservationIMenuBar.dart';
import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:flutter/services.dart';
import 'package:provider/provider.dart';

class ReservationPreviewScreen extends StatefulWidget {
  final int offerId;
  final int hotelId;
  final int roomId;
  final double basePrice;
  final DateTime departureDate;
  final DateTime returnDate;
  final String imageUrl;

  const ReservationPreviewScreen({
    super.key,
    required this.offerId,
    required this.hotelId,
    required this.roomId,
    required this.basePrice,
    required this.departureDate,
    required this.returnDate,
    required this.imageUrl,
  });

  @override
  State<ReservationPreviewScreen> createState() =>
      _ReservationPreviewScreenState();
}

class _ReservationPreviewScreenState extends State<ReservationPreviewScreen> {
  late ReservationPreviewProvider _previewProvider;
  late UserProvider _userProvider;
  late ReservationProvider _reservationProvider;
  late PaymentProvider _paymentProvider;
  late OfferProvider _offerProvider;


  ReservationPreview? preview;
  bool isPreviewLoading = true;

  bool includeInsurance = true;
  bool firstInstallment = true;
  bool payWithPaypal = true;
  bool acceptTerms = true;

  final TextEditingController _voucherController = TextEditingController();
  String? voucherCode;

  double? appliedDiscount; // npr 0.20, 0.50...
  String? appliedVoucherCode;
  int? appliedVoucherId;

  double? discountedBasePrice;
  double? discountedTotalPrice;

  String? _paypalOrderId;

  User? _user;
  bool _isUserLoading = true;

  bool _isProcessing = false;

  @override
  void initState() {
    super.initState();
    _previewProvider = Provider.of<ReservationPreviewProvider>(
      context,
      listen: false,
    );
    _userProvider = Provider.of<UserProvider>(context, listen: false);
    _reservationProvider = Provider.of<ReservationProvider>(
      context,
      listen: false,
    );
    _paymentProvider = Provider.of<PaymentProvider>(context, listen: false);
    _offerProvider = Provider.of<OfferProvider>(context, listen: false);

    _loadPreview();
    _loadUser();
  }

  Future<void> _loadUser() async {
    try {
      final user = await _userProvider.getById(Session.userId!);

      setState(() {
        _user = user;
        _isUserLoading = false;
      });
    } catch (e) {
      _isUserLoading = false;
    }
  }

  void _show(String message) {
    ScaffoldMessenger.of(
      context,
    ).showSnackBar(SnackBar(content: Text(message)));
  }

  Future<void> _confirmPaypalPayment() async {
    if (_paypalOrderId == null) return;

    final paypalProvider = Provider.of<PayPalProvider>(context, listen: false);

    // 2️⃣ CAPTURE
    final success = await paypalProvider.capturePayPalOrder(_paypalOrderId!);

    if (!success) {
      _show("PayPal uplata nije potvrđena");
      return;
    }

    // 3️⃣ SNIMI REZERVACIJU + PAYMENT
    await _createReservationWithPayment(
      isConfirmed: true,
      rateId: firstInstallment ? 1 : 4,
    );

    await _offerProvider.increaseTotalReservation(widget.offerId);

    // 4️⃣ UPOZORENJE ZA VAUČER
    _showVoucherWarning();
  }

  Future<void> _createReservationWithSlip() async {
    await _createReservationWithPayment(
      isConfirmed: false,
      rateId: firstInstallment ? 1 : 4,
    );

    _showVoucherWarning();
  }

  double _getPayAmount() {
    if (firstInstallment) {
      return 100.0; // ✅ I RATA = 100$
    }

    return discountedTotalPrice ?? preview!.totalPrice; // ✅ PUNI IZNOS
  }

  Future<void> _createReservationWithPayment({
    required bool isConfirmed,
    required int rateId,
  }) async {
    if (preview == null) return;

    // 1️⃣ SNIMI REZERVACIJU
    final reservation = await _reservationProvider.insert({
      "userId": Session.userId,
      "offerId": widget.offerId,
      "hotelId": widget.hotelId,
      "roomId": widget.roomId,
      "isActive": true,
      "includeInsurance": includeInsurance,
      "isFirstRatePaid": isConfirmed && rateId == 1,
      "isFullPaid": isConfirmed && rateId == 4,
      "totalPrice": discountedTotalPrice ?? preview!.totalPrice,
      "priceLeftToPay":
          isConfirmed ? 0 : (discountedTotalPrice ?? preview!.totalPrice),
      "createdAt": DateConverter.toUtcIsoFromDate(DateTime.now()),
      "addedNeeds": "", // ili iz TextField-a
    });

    // 2️⃣ SNIMI PAYMENT
    await _paymentProvider.insert({
      "reservationId": reservation.id,
      "rateId": rateId,
      "amount": discountedTotalPrice ?? preview!.totalPrice,
      "paymentDate": DateConverter.toUtcIsoFromDate(DateTime.now()),
      "paymentMethod": payWithPaypal ? "kartica" : "uplatnica",
      "paymentDeadline":
          payWithPaypal
              ? DateConverter.toUtcIsoFromDate(DateTime.now())
              : DateConverter.toUtcIsoFromDate(
                DateTime.now().add(const Duration(days: 3)),
              ),
      "deadlineExtended": false,
      "isConfirmed": isConfirmed,
    });

    // 3️⃣ OZNAČI VAUČER KAO ISKORIŠTEN
    if (appliedVoucherId != null) {
      await Provider.of<UserVoucherProvider>(
        context,
        listen: false,
      ).markAsUsed(appliedVoucherId!);
    }
  }

  void _showVoucherWarning() {
    showDialog(
      context: context,
      barrierDismissible: false, // ⛔ ne može se zatvoriti klikom vani
      builder:
          (_) => AlertDialog(
            title: const Text("Rezervacija uspješna"),
            content: Text(
              appliedVoucherId != null
                  ? "Rezervacija je uspješno izvršena.\n\n"
                      "Napomena: Iskorišteni vaučer neće biti refundiran u slučaju otkazivanja rezervacije."
                  : "Rezervacija je uspješno izvršena.",
            ),
            actions: [
              TextButton(
                onPressed: () {
                  Navigator.of(context).pop(); // zatvori dialog

                  // 🔁 vrati korisnika na početnu stranicu
                  Navigator.of(context).pushAndRemoveUntil(
                    MaterialPageRoute(builder: (_) => const StartingPage()),
                    (route) => false,
                  );
                },
                child: const Text("U redu"),
              ),
            ],
          ),
    );
  }

  Future<void> _startPaypalFlow() async {
    final amount = _getPayAmount();

    final paypalProvider = Provider.of<PayPalProvider>(context, listen: false);

    // 1️⃣ CREATE ORDER
    final order = await paypalProvider.createPayPalOrder(amount);

    if (order == null) {
      _show("Greška pri PayPal uplati");
      return;
    }

    final approveUrl = order.links.firstWhere((l) => l.rel == 'approve').href;

    _paypalOrderId = order.id;

    // 2️⃣ OTVORI PAYPAL
    final approved = await Navigator.push<bool>(
      context,
      MaterialPageRoute(
        builder:
            (_) => PayPalWebViewPage(approveUrl: approveUrl, orderId: order.id),
      ),
    );

    if (approved == true) {
      await _confirmPaypalPayment();
    } else {
      _show("PayPal uplata otkazana");
    }
  }

  void _recalculateDiscounts() {
    if (preview == null) return;

    if (appliedDiscount == null) {
      discountedBasePrice = null;
      discountedTotalPrice = null;
      return;
    }

    final d = appliedDiscount!;

    discountedBasePrice = preview!.basePrice * (1 - d);
    // ukupan: samo popust na putovanje, ostalo ostaje isto
    discountedTotalPrice =
        discountedBasePrice! + preview!.residenceTaxTotal + preview!.insurance;
  }

  Future<void> _applyVoucher() async {
    final code = _voucherController.text.trim().toUpperCase();

    if (appliedVoucherId != null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Vaučer je već primijenjen")),
      );
      return;
    }

    // 1) prazno
    if (code.isEmpty) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text("Nevalidan kod")));
      return;
    }

    try {
      // 2) da li kod postoji u Voucher tabeli?
      //    OVO PRETPOSTAVLJA da backend podržava filter voucherCode
      //    GET /api/Voucher?voucherCode=ABC123
      final voucherResult = await Provider.of<VoucherProvider>(
        context,
        listen: false,
      ).get(filter: {"code": code});

      if (voucherResult.items.isEmpty) {
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(const SnackBar(content: Text("Nevalidan kod")));
        return;
      }

      final voucher = voucherResult.items.first;

      // 3) provjeri da li user posjeduje taj voucher
      final userVouchers = await Provider.of<UserVoucherProvider>(
        context,
        listen: false,
      ).get(filter: {"userId": Session.userId});

      final owned = userVouchers.items.any((uv) => uv.voucherId == voucher.id);

      if (!owned) {
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(const SnackBar(content: Text("Nevalidan kod")));
        return;
      }

      // 4) provjeri isUsed
      final uv = userVouchers.items.firstWhere(
        (x) => x.voucherId == voucher.id,
      );
      if (uv.isUsed == true) {
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(const SnackBar(content: Text("Kod iskorišten")));
        return;
      }

      // ✅ validan i nije iskorišten -> primijeni popust (ali NE mijenjaj isUsed)
      setState(() {
        appliedVoucherCode = code;
        appliedDiscount = voucher.discount;
        appliedVoucherId = voucher.id; // ⬅️ BITNO
      });

      _recalculateDiscounts();

      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text("Vaučer primijenjen")));
    } catch (e) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text("Greška: $e")));
    }
  }

  Future<void> _loadPreview() async {
    setState(() => isPreviewLoading = true);

    final result = await _previewProvider.generatePreview(
      userId: Session.userId!,
      offerId: widget.offerId,
      hotelId: widget.hotelId,
      roomId: widget.roomId,
      basePrice: widget.basePrice,
      includeInsurance: includeInsurance,
    );

    setState(() {
      preview = result;
      isPreviewLoading = false;
    });

    // nakon što preview stigne, ako je već neki voucher primijenjen, preračunaj
    _recalculateDiscounts();
  }

  Widget _buildLoadingOverlay() {
    return Container(
      color: Colors.black.withOpacity(0.4), // zatamni ekran
      width: double.infinity,
      height: double.infinity,
      child: Center(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            const SizedBox(
              width: 80,
              height: 80,
              child: CircularProgressIndicator(
                strokeWidth: 6,
                color: Color(0xFF67B1E5),
              ),
            ),
            const SizedBox(height: 20),
            Text(
              "Obrada rezervacije...",
              style: GoogleFonts.openSans(
                color: Colors.white,
                fontSize: 16,
                fontWeight: FontWeight.bold,
              ),
            ),
          ],
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      body: Stack(
        children: [
          GestureDetector(
            behavior: HitTestBehavior.translucent,
            onTap: () {
              FocusScope.of(context).unfocus();
            },
            child: CustomScrollView(
              slivers: [
                // ===== TVOJ SLIVER APP BAR =====
                ReservationBackIMenuBar(daLijeKliknuo: false),

                // ===== SADRŽAJ =====
                SliverToBoxAdapter(
                  child: Column(
                    children: [
                      _offerCard(),
                      const SizedBox(height: 16),
                      _sectionTitle("Način uplate"),
                      _paymentMethod(),
                      const SizedBox(height: 16),
                      _sectionTitle(""),
                      _personalInfo(),
                      const SizedBox(height: 30),
                    ],
                  ),
                ),
              ],
            ),
          ),
          if (_isProcessing) _buildLoadingOverlay(),
        ],
      ),
    );
  }

  // ================= OFFER CARD =================

  Widget _offerCard() {
    if (isPreviewLoading || preview == null) {
      return const Padding(
        padding: EdgeInsets.all(40),
        child: Center(
          child: CircularProgressIndicator(color: Color(0xFF67B1E5)),
        ),
      );
    }

    return Padding(
      padding: const EdgeInsets.all(12),
      child: Card(
        color: Color(0xFFF5F5F5),
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            ClipRRect(
              borderRadius: const BorderRadius.vertical(
                top: Radius.circular(16),
              ),
              child: Image.network(
                "${ApiConfig.imagesHotels}/${widget.imageUrl}",
                height: 200,
                width: double.infinity,
                fit: BoxFit.cover,
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(14),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    preview?.offerTitle ?? "",
                    style: GoogleFonts.openSans(
                      fontSize: 16,
                      fontWeight: FontWeight.bold,
                    ),
                  ),

                  const SizedBox(height: 4),
                  Row(
                    children: [
                      Text(
                        preview!.hotelTitle,
                        style: GoogleFonts.openSans(
                          fontWeight: FontWeight.bold,
                        ),
                      ),

                      const SizedBox(width: 6),
                      Row(
                        children: List.generate(
                          int.tryParse(preview?.hotelStars ?? "0") ?? 0,
                          (_) => const Icon(
                            Icons.star,
                            color: Colors.amber,
                            size: 16,
                          ),
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 12),
                  Text(
                    preview!.roomType,
                    style: GoogleFonts.openSans(fontWeight: FontWeight.bold),
                  ),

                  const SizedBox(height: 12),

                  Text(
                    "Termin putovanja",
                    style: GoogleFonts.openSans(fontWeight: FontWeight.bold),
                  ),
                  Text(
                    "${widget.departureDate.day}.${widget.departureDate.month}.${widget.departureDate.year} - "
                    "${widget.returnDate.day}.${widget.returnDate.month}.${widget.returnDate.year}",
                    style: GoogleFonts.openSans(color: const Color(0xFF67B1E5)),
                  ),

                  // ===== USLUGE =====
                  Text(
                    "Usluga:",
                    style: GoogleFonts.openSans(fontWeight: FontWeight.bold),
                  ),
                  const SizedBox(height: 8),

                  _serviceRow(
                    "Putovanje",
                    "${(discountedBasePrice ?? preview!.basePrice).toStringAsFixed(2)}\$",
                  ),

                  _serviceRow(
                    "Boravišna taksa",
                    "${preview?.residenceTaxTotal.toStringAsFixed(2)}\$",
                  ),

                  _insuranceRow(),

                  const SizedBox(height: 16),

                  // ===== VAUČER =====
                  Theme(
                    data: Theme.of(
                      context,
                    ).copyWith(dividerColor: Colors.transparent),
                    child: Container(
                      decoration: BoxDecoration(
                        color: const Color(0xFFD9D9D9),
                        borderRadius: BorderRadius.circular(12),
                      ),
                      child: ExpansionTile(
                        tilePadding: EdgeInsets.zero,
                        title: Padding(
                          padding: const EdgeInsets.only(left: 20),
                          child: Text(
                            "Iskoristite vaučer?",
                            style: GoogleFonts.openSans(
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                        ),

                        children: [
                          Padding(
                            padding: const EdgeInsets.only(
                              left: 12,
                              right: 12,
                              bottom: 12,
                            ),
                            child: Row(
                              children: [
                                Expanded(
                                  child: TextField(
                                    controller: _voucherController,
                                    inputFormatters: [
                                      TextInputFormatter.withFunction((
                                        oldValue,
                                        newValue,
                                      ) {
                                        return newValue.copyWith(
                                          text: newValue.text.toUpperCase(),
                                          selection: newValue.selection,
                                        );
                                      }),
                                    ],
                                    decoration: InputDecoration(
                                      hintText: "unesite kod vaučera",
                                      filled: true,
                                      fillColor: const Color(0xFFEFEFEF),
                                      border: OutlineInputBorder(
                                        borderRadius: BorderRadius.circular(20),
                                        borderSide: BorderSide.none,
                                      ),
                                    ),
                                  ),
                                ),
                                const SizedBox(width: 8),
                                ElevatedButton(
                                  style: ElevatedButton.styleFrom(
                                    backgroundColor: const Color(0xFF67B1E5),
                                    shape: RoundedRectangleBorder(
                                      borderRadius: BorderRadius.circular(20),
                                    ),
                                    padding: const EdgeInsets.symmetric(
                                      horizontal: 18,
                                      vertical: 12,
                                    ),
                                  ),
                                  onPressed: _applyVoucher,

                                  child: Text(
                                    "primijeni",
                                    style: GoogleFonts.openSans(
                                      color: Colors.white,
                                      fontWeight: FontWeight.bold,
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),

                  // ===== UKUPNO =====
                  Align(
                    alignment: Alignment.centerRight,
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.end,
                      children: [
                        Text(
                          "UKUPNO:",
                          style: GoogleFonts.openSans(
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                        if (isPreviewLoading)
                          const CircularProgressIndicator(
                            color: Color(0xFF67B1E5),
                          )
                        else
                          Text(
                            "${(discountedTotalPrice ?? preview!.totalPrice).toStringAsFixed(2)}\$",

                            style: GoogleFonts.openSans(
                              fontSize: 26,
                              fontWeight: FontWeight.bold,
                              color: const Color(0xFF67B1E5),
                            ),
                          ),
                      ],
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget _serviceRow(String title, String price) {
    return Container(
      padding: const EdgeInsets.symmetric(vertical: 6),
      decoration: const BoxDecoration(
        border: Border(
          bottom: BorderSide(color: Color(0xFF67B1E5), width: 0.8),
        ),
      ),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Text(
            title,
            style: GoogleFonts.openSans(
              color: const Color(0xFF67B1E5),
              fontSize: 13,
            ),
          ),
          Text(
            price,
            style: GoogleFonts.openSans(
              color: const Color(0xFF67B1E5),
              fontWeight: FontWeight.bold,
            ),
          ),
        ],
      ),
    );
  }

  Widget _insuranceRow() {
    return Container(
      padding: const EdgeInsets.symmetric(vertical: 6),
      decoration: const BoxDecoration(
        border: Border(
          bottom: BorderSide(color: Color(0xFF67B1E5), width: 0.8),
        ),
      ),
      child: Row(
        children: [
          SizedBox(
            width: 28,
            height: 28,
            child: Transform.scale(
              scale: 0.9, // mijenjaj 0.8–1.1 po potrebi
              child: Checkbox(
                value: includeInsurance,
                activeColor: const Color(0xFF67B1E5),
                materialTapTargetSize: MaterialTapTargetSize.shrinkWrap,
                visualDensity: VisualDensity.compact,
                onChanged: (value) {
                  setState(() {
                    includeInsurance = value ?? true;
                  });
                  _loadPreview();
                },
              ),
            ),
          ),

          Expanded(
            child: Text(
              "Putničko zdravstveno osiguranje",
              style: GoogleFonts.openSans(
                color: const Color(0xFF67B1E5),
                fontSize: 12,
              ),
            ),
          ),
          Text(
            "${preview?.insurance.toStringAsFixed(2)}\$",
            style: GoogleFonts.openSans(
              color: const Color(0xFF67B1E5),
              fontWeight: FontWeight.bold,
            ),
          ),
        ],
      ),
    );
  }

  // ================= PAYMENT =================

  Widget _paymentMethod() {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 12),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          _paymentCard(
            title: "UPLATA PUTEM PAYPAL RAČUNA",
            description:
                "Uplata putem PayPala omogućava brzo i sigurno korištenje međunarodnih kartica.",
            selected: payWithPaypal,
            onTap: () => setState(() => payWithPaypal = true),
          ),
          _paymentCard(
            title: "UPLATA PUTEM UPLATNICE",
            description:
                "Uplata putem uplatnice može se izvršiti direktno na šalteru banke.",
            selected: !payWithPaypal,
            onTap: () => setState(() => payWithPaypal = false),
          ),
        ],
      ),
    );
  }

  Widget _paymentCard({
    required String title,
    required String description,
    required bool selected,
    required VoidCallback onTap,
  }) {
    return Card(
      color: const Color(0xFFF5F5F5),
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(14)),
      elevation: 0,
      child: Padding(
        padding: const EdgeInsets.all(30),
        child: InkWell(
          borderRadius: BorderRadius.circular(14),
          onTap: onTap,
          child: Row(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Checkbox(
                value: selected,
                onChanged: (_) => onTap(),
                activeColor: const Color(0xFF67B1E5),
              ),
              const SizedBox(width: 8),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      title,
                      style: const TextStyle(fontWeight: FontWeight.bold),
                    ),
                    const SizedBox(height: 6),
                    Text(description),
                  ],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  // ================= PERSONAL =================

  Widget _personalInfo() {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 12),
      child: Card(
        color: Color(0xFFF5F5F5),
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
        elevation: 2,
        child: Padding(
          padding: const EdgeInsets.all(14),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              // ===== NASLOV =====
              Container(
                width: double.infinity,
                padding: const EdgeInsets.all(10),
                margin: const EdgeInsets.only(bottom: 14),
                child: Text(
                  "OSOBNE INFORMACIJE",
                  textAlign: TextAlign.center,
                  style: GoogleFonts.openSans(
                    fontSize: 14,
                    fontWeight: FontWeight.bold,
                    color: Colors.black,
                  ),
                ),
              ),

              // ===== PRIJAVLJENI PROFIL =====
              Text(
                "Prijavljeni profil",
                style: GoogleFonts.openSans(fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 10),

              // ===== USER CARD =====
              Container(
                padding: const EdgeInsets.all(14),
                decoration: BoxDecoration(
                  color: Color(0xFFD9D9D9),
                  borderRadius: BorderRadius.circular(40),
                  border: Border.all(color: const Color(0xFF67B1E5)),
                ),
                child: Row(
                  children: [
                    CircleAvatar(
                      radius: 26,
                      backgroundImage:
                          _isUserLoading
                              ? const AssetImage("assets/images/default.jpg")
                              : (_user?.imageUrl != null
                                      ? NetworkImage(
                                        "${ApiConfig.imagesUsers}/${_user!.imageUrl}",
                                      )
                                      : const AssetImage(
                                        "assets/images/default.jpg",
                                      ))
                                  as ImageProvider,
                    ),

                    const SizedBox(width: 12),

                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: [
                          Text(
                            _isUserLoading
                                ? "Učitavanje..."
                                : "${_user?.firstName ?? ""} ${_user?.lastName ?? ""}",
                            textAlign: TextAlign.center,
                            style: GoogleFonts.openSans(
                              fontWeight: FontWeight.bold,
                            ),
                          ),

                          const SizedBox(height: 4),

                          Text(
                            "svi neophodni podaci biće preuzeti s profila",
                            textAlign: TextAlign.center,
                            style: GoogleFonts.openSans(
                              fontWeight: FontWeight.bold,
                              fontSize: 12,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
              ),

              const SizedBox(height: 20),

              // ===== DODATNI ZAHTJEVI =====
              Text(
                "Dodatni zahtjevi",
                style: GoogleFonts.openSans(fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 8),
              TextField(
                maxLines: 4,
                decoration: InputDecoration(
                  filled: true,
                  fillColor: Colors.white,
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(12),
                  ),
                ),
              ),

              const SizedBox(height: 20),

              // ===== RATE (U JEDNOJ LINIJI) =====
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceAround,
                children: [
                  Expanded(
                    child: InkWell(
                      onTap: () => setState(() => firstInstallment = true),
                      child: Row(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          SizedBox(
                            width: 28,
                            height: 28,
                            child: Transform.scale(
                              scale: 0.9,
                              child: Checkbox(
                                value: firstInstallment,
                                activeColor: const Color(0xFF67B1E5),
                                materialTapTargetSize:
                                    MaterialTapTargetSize.shrinkWrap,
                                visualDensity: VisualDensity.compact,
                                onChanged:
                                    (_) =>
                                        setState(() => firstInstallment = true),
                              ),
                            ),
                          ),
                          const SizedBox(width: 6),
                          Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: const [
                              Text(
                                "I RATA",
                                style: TextStyle(fontWeight: FontWeight.bold),
                              ),
                              SizedBox(height: 2),
                              Text(
                                "iznos: 100\$",
                                style: TextStyle(fontSize: 12),
                              ),
                            ],
                          ),
                        ],
                      ),
                    ),
                  ),

                  const SizedBox(width: 90),

                  Expanded(
                    child: InkWell(
                      onTap: () => setState(() => firstInstallment = false),
                      child: Row(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          SizedBox(
                            width: 28,
                            height: 28,
                            child: Transform.scale(
                              scale: 0.9,
                              child: Checkbox(
                                value: !firstInstallment,
                                activeColor: const Color(0xFF67B1E5),
                                materialTapTargetSize:
                                    MaterialTapTargetSize.shrinkWrap,
                                visualDensity: VisualDensity.compact,
                                onChanged:
                                    (_) => setState(
                                      () => firstInstallment = false,
                                    ),
                              ),
                            ),
                          ),
                          const SizedBox(width: 6),
                          Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Text(
                                "Puni iznos",
                                style: TextStyle(fontWeight: FontWeight.bold),
                              ),
                              SizedBox(height: 2),
                            ],
                          ),
                        ],
                      ),
                    ),
                  ),
                ],
              ),

              const SizedBox(height: 8),

              InkWell(
                onTap: () => setState(() => acceptTerms = !acceptTerms),
                child: Row(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    SizedBox(
                      width: 28,
                      height: 28,
                      child: Transform.scale(
                        scale: 0.9,
                        child: Checkbox(
                          value: acceptTerms,
                          activeColor: const Color(0xFF67B1E5),
                          materialTapTargetSize:
                              MaterialTapTargetSize.shrinkWrap,
                          visualDensity: VisualDensity.compact,
                          onChanged: (v) => setState(() => acceptTerms = v!),
                        ),
                      ),
                    ),
                    const SizedBox(width: 8),
                    Expanded(
                      child: RichText(
                        text: TextSpan(
                          style: GoogleFonts.openSans(
                            fontSize: 14,
                            fontWeight: FontWeight.bold,
                            color: Colors.black,
                          ),
                          children: const [
                            TextSpan(text: "Slažem se sa "),
                            TextSpan(
                              text: "opštim uslovima putovanja",
                              style: TextStyle(color: Color(0xFF67B1E5)),
                            ),
                            TextSpan(text: " i "),
                            TextSpan(
                              text: "programom putovanja",
                              style: TextStyle(color: Color(0xFF67B1E5)),
                            ),
                          ],
                        ),
                      ),
                    ),
                  ],
                ),
              ),

              const SizedBox(height: 20),

              // ===== DUGME =====
              SizedBox(
                width: double.infinity,
                child: ElevatedButton(
                  style: ElevatedButton.styleFrom(
                    backgroundColor: const Color(0xFF67B1E5),
                    padding: const EdgeInsets.symmetric(vertical: 14),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(30),
                    ),
                  ),
                  onPressed: () async {
                    if (!acceptTerms) {
                      _show("Morate prihvatiti uslove");
                      return;
                    }

                    if (preview == null) return;

                    setState(() => _isProcessing = true); // 🔥 START

                    try {
                      if (payWithPaypal) {
                        await _startPaypalFlow();
                      } else {
                        await _createReservationWithSlip();
                      }
                    } catch (e) {
                      _show("Greška pri obradi rezervacije");
                    } finally {
                      if (mounted) {
                        setState(
                          () => _isProcessing = false,
                        ); // 🔥 STOP (fallback)
                      }
                    }
                  },

                  child: Text(
                    "PLATITE",
                    style: GoogleFonts.openSans(
                      color: Colors.white,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _sectionTitle(String title) {
    return Container(
      width: double.infinity,
      padding: const EdgeInsets.all(10),
      margin: const EdgeInsets.only(bottom: 8),
      color: const Color(0xFF67B1E5),
      child: Text(
        title,
        textAlign: TextAlign.center,
        style: GoogleFonts.openSans(
          fontSize: 14,
          fontWeight: FontWeight.bold,
          color: Colors.white,
        ),
      ),
    );
  }
}
