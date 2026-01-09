import 'dart:io';
import 'dart:typed_data';

import 'package:etravel_desktop/models/payment.dart';
import 'package:etravel_desktop/models/rate.dart';
import 'package:etravel_desktop/providers/payment_provider.dart';
import 'package:etravel_desktop/providers/rate_provider.dart';
import 'package:etravel_desktop/providers/reservation_provider.dart';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

import '../models/reservations.dart';
import '../models/user.dart';
import '../models/offer.dart';
import '../models/hotel.dart';
import '../models/room.dart';
import '../models/hotel_room.dart';

import '../providers/offer_provider.dart';
import '../providers/hotel_provider.dart';
import '../providers/room_provider.dart';
import '../providers/hotel_room_provider.dart';

class ReservationDetailsDialog extends StatefulWidget {
  final Reservation reservation;
  final User user;

  const ReservationDetailsDialog({
    super.key,
    required this.reservation,
    required this.user,
  });

  @override
  State<ReservationDetailsDialog> createState() =>
      _ReservationDetailsDialogState();
}

class _ReservationDetailsDialogState extends State<ReservationDetailsDialog> {
  late OfferProvider _offerProvider;
  late HotelProvider _hotelProvider;
  late RoomProvider _roomProvider;
  late HotelRoomProvider _hotelRoomProvider;
  late PaymentProvider _paymentProvider;
  late RateProvider _rateProvider;
  late ReservationProvider _reservationProvider;

  bool isLoading = true;

  int? selectedRateId;

  double? refreshedLeftToPay;

  Offer? offer;
  Hotel? hotel;
  Room? room;
  HotelRoom? hotelRoom;

  List<Rate> allRates = [];
  List<Payment> payments = [];

  @override
  void initState() {
    super.initState();

    _offerProvider = Provider.of<OfferProvider>(context, listen: false);
    _hotelProvider = Provider.of<HotelProvider>(context, listen: false);
    _roomProvider = Provider.of<RoomProvider>(context, listen: false);
    _hotelRoomProvider = Provider.of<HotelRoomProvider>(context, listen: false);
    _paymentProvider = Provider.of<PaymentProvider>(context, listen: false);
    _rateProvider = Provider.of<RateProvider>(context, listen: false);
    _reservationProvider = Provider.of<ReservationProvider>(
      context,
      listen: false,
    );

    _loadData();
  }

  // ===========================================================
  // LOAD ALL NECESSARY DATA
  // ===========================================================
  Future<void> _loadData() async {
    try {
      final offerResult = await _offerProvider.get(
        filter: {"offerId": widget.reservation.offerId},
      );
      if (offerResult.items.isNotEmpty) offer = offerResult.items.first;

      hotel = await _hotelProvider.getById(widget.reservation.hotelId);

      room = await _roomProvider.getById(widget.reservation.roomId);

      final hrResult = await _hotelRoomProvider.get(
        filter: {
          "hotelId": widget.reservation.hotelId,
          "roomId": widget.reservation.roomId,
        },
      );
      if (hrResult.items.isNotEmpty) hotelRoom = hrResult.items.first;

      final rateResult = await _rateProvider.get();
      allRates = rateResult.items;

      final paymentResult = await _paymentProvider.get(
        filter: {"reservationId": widget.reservation.id},
      );
      payments = paymentResult.items;
    } catch (e) {
      debugPrint("Popup error: $e");
    }

    setState(() => isLoading = false);
  }

  Future<void> _refreshReservationPrice() async {
    final reservationProvider = Provider.of<ReservationProvider>(
      context,
      listen: false,
    );

    final updated = await reservationProvider.getById(widget.reservation.id);

    setState(() {
      refreshedLeftToPay = updated.priceLeftToPay;
    });
  }

  Future<void> _confirmPayment() async {
    if (selectedRateId == null) return;

    try {
      // 1. Nađemo payment za tu ratu
      Payment? payment;
      try {
        payment = payments.firstWhere((p) => p.rateId == selectedRateId);
      } catch (_) {
        payment = null;
      }

      if (payment == null) {
        throw Exception("Payment zapis nije pronađen!");
      }

      await _paymentProvider.updatePayment(
        widget.reservation.id,
        selectedRateId!,
        {
          "amount": payment.amount, // ← UZIMAMO AMOUNT IZ PAYMENTA
          "paymentDate": DateTime.now().toUtc().toIso8601String(),
          "paymentMethod":
              payment.paymentMethod, // Ostavimo originalni način plaćanja
          "isConfirmed": true,
        },
      );

      await _loadData(); // osvježi popup kompletno
      await _refreshReservationPrice();

      showDialog(
        context: context,
        builder:
            (_) => AlertDialog(
              title: const Text("Uspjeh"),
              content: Text(
                "Uplata za '${payment!.paymentMethod}' je uspješno potvrđena.",
              ),
              actions: [
                TextButton(
                  onPressed: () => Navigator.pop(context),
                  child: const Text("OK"),
                ),
              ],
            ),
      );

      setState(() {
        selectedRateId = null;
      });
    } catch (e) {
      showDialog(
        context: context,
        builder:
            (_) => AlertDialog(
              title: const Text("Greška"),
              content: Text("Potvrda uplate nije uspjela: $e"),
              actions: [
                TextButton(
                  onPressed: () => Navigator.pop(context),
                  child: const Text("Zatvori"),
                ),
              ],
            ),
      );
    }
  }

  // ===========================================================
  // PAYMENT DEADLINE LOGIC
  // ===========================================================

  Payment? getCurrentUnconfirmedPayment() {
    for (final p in payments) {
      if (!p.isConfirmed) return p;
    }
    return null;
  }

  String formatRateDeadlineText(Rate rate) {
    switch (rate.name.toLowerCase()) {
      case "prva rata":
        return "prve rate";
      case "druga rata":
        return "druge rate";
      case "treća rata":
        return "treće rate";
      case "preostali iznos":
        return "preostalog plaćanja";
      default:
        return rate.name.toLowerCase();
    }
  }

  void showDeadlineWarningPopup(BuildContext context) {
    showDialog(
      context: context,
      builder: (_) {
        return AlertDialog(
          title: const Text("Upozorenje"),
          content: const Text(
            "Korisnik je već probio prvobitni rok plaćanja!\n"
            "Rok je produžen, ali uplatu treba hitno izvršiti.",
          ),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: const Text("OK"),
            ),
          ],
        );
      },
    );
  }

  // ===========================================================
  // FULLY PAID LOGIC
  // ===========================================================

  bool isFullyPaid() {
    final fullAmount = allRates.firstWhere((r) => r.name == "Puni iznos");
    final remainingAmount = allRates.firstWhere(
      (r) => r.name == "Preostali iznos",
    );

    final pFull = _getPaymentForRate(fullAmount.id);
    final pRemaining = _getPaymentForRate(remainingAmount.id);

    if (pFull?.isConfirmed == true) return true;
    if (pRemaining?.isConfirmed == true) return true;

    final standardRates =
        allRates
            .where((r) => r.orderNumber != null && r.orderNumber! > 0)
            .toList();

    for (final r in standardRates) {
      final p = _getPaymentForRate(r.id);
      if (p == null || p.isConfirmed == false) return false;
    }

    return true;
  }

  int? getNextPaymentRateId() {
    // 1) Ako je potvrđen puni iznos → NEMA iduće rate
    final fullAmount = allRates.firstWhere((r) => r.name == "Puni iznos");
    final preostali = allRates.firstWhere((r) => r.name == "Preostali iznos");

    if (_getPaymentForRate(fullAmount.id)?.isConfirmed == true) return null;
    if (_getPaymentForRate(preostali.id)?.isConfirmed == true) return null;

    // 2) Ako postoji čekajući puni iznos (waiting == true)
    final pFull = _getPaymentForRate(fullAmount.id);
    if (pFull != null && pFull.isConfirmed == false) {
      return fullAmount.id;
    }

    // 3) Ako postoji čekajući preostali iznos
    final pRem = _getPaymentForRate(preostali.id);
    if (pRem != null && pRem.isConfirmed == false) {
      return preostali.id;
    }

    // 4) Inače standardne rate redom: I → II → III
    final standard =
        allRates.where((r) => r.orderNumber != null).toList()
          ..sort((a, b) => a.orderNumber!.compareTo(b.orderNumber!));

    for (final r in standard) {
      final p = _getPaymentForRate(r.id);
      if (p == null || p.isConfirmed == false) {
        return r.id;
      }
    }

    // Sve je plaćeno
    return null;
  }

  // ===========================================================
  // HELPERS
  // ===========================================================
  Payment? _getPaymentForRate(int rateId) {
    try {
      return payments.firstWhere((p) => p.rateId == rateId);
    } catch (_) {
      return null;
    }
  }

  bool _isNextRate(int rateId) {
    return getNextPaymentRateId() == rateId;
  }

  // ===========================================================
  // RATE ITEM
  // ===========================================================

  Widget _rateItem(Rate r) {
    final payment = _getPaymentForRate(r.id);

    final bool confirmed = payment?.isConfirmed == true;
    final bool waiting = payment != null && payment.isConfirmed == false;

    // Ako ova rata već postoji ali nije potvrđena — to je aktivna rata
    final bool isActiveRate = waiting;

    // Ako je payment nepostojeći → možda je sljedeća rata (dozvoliti potvrdu)
    final bool isNext = _isNextRate(r.id);

    // CHECKBOX VALUE
    // - confirmed  → TRUE
    // - waiting    → FALSE (admin treba tek čekirati!)
    // - null       → FALSE
    bool checkValue = confirmed || selectedRateId == r.id;

    return Row(
      children: [
        Checkbox(
          value: checkValue,
          onChanged:
              (isNext || isActiveRate)
                  ? (value) {
                    setState(() {
                      selectedRateId = value == true ? r.id : null;
                    });
                  }
                  : null,
        ),

        Text(
          r.name,
          style: GoogleFonts.openSans(
            fontSize: 20,
            fontWeight: FontWeight.bold,
          ),
        ),
      ],
    );
  }

  // ===========================================================
  // RATE BLOCK
  // ===========================================================

  Widget _ratesBlock() {
    final standardRates =
        allRates
            .where((r) => r.orderNumber != null && r.orderNumber! > 0)
            .toList()
          ..sort((a, b) => a.orderNumber!.compareTo(b.orderNumber!));

    final fullAmount = allRates.firstWhere((r) => r.name == "Puni iznos");
    final remainingAmount = allRates.firstWhere(
      (r) => r.name == "Preostali iznos",
    );

    final firstRatePayment = _getPaymentForRate(standardRates.first.id);
    final bool isFirstRatePaid = firstRatePayment?.isConfirmed == true;

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        ...standardRates.map((r) => _rateItem(r)).toList(),

        const SizedBox(height: 15),

        Center(
          child: Text(
            "ili",
            style: GoogleFonts.openSans(
              fontSize: 18,
              fontWeight: FontWeight.bold,
              color: Colors.black54,
            ),
          ),
        ),

        const SizedBox(height: 15),

        if (!isFirstRatePaid) _rateItem(fullAmount),
        if (isFirstRatePaid) _rateItem(remainingAmount),
      ],
    );
  }

  // ===========================================================
  // UI BUILD
  // ===========================================================

  @override
  Widget build(BuildContext context) {
    return Dialog(
      insetPadding: const EdgeInsets.symmetric(horizontal: 140, vertical: 40),
      backgroundColor: Colors.transparent,
      child:
          isLoading
              ? const Center(
                child: CircularProgressIndicator(color: Colors.white),
              )
              : _buildContent(context),
    );
  }

  Widget _buildContent(BuildContext context) {
    return ConstrainedBox(
      constraints: const BoxConstraints(maxWidth: 1100, maxHeight: 750),
      child: ClipRRect(
        borderRadius: BorderRadius.circular(16),
        child: Material(
          color: const Color(0xffD9D9D9),
          child: Column(children: [_header(context), _body(context)]),
        ),
      ),
    );
  }

  // ===========================================================
  // HEADER
  // ===========================================================
  Widget _header(BuildContext context) {
    return Container(
      height: 70,
      color: const Color(0xff67B1E5),
      padding: const EdgeInsets.symmetric(horizontal: 25),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          const SizedBox(width: 28),
          Expanded(
            child: Center(
              child: Text(
                "${widget.user.firstName} ${widget.user.lastName}",
                style: GoogleFonts.openSans(
                  fontSize: 30,
                  fontWeight: FontWeight.bold,
                  color: Colors.white,
                ),
              ),
            ),
          ),
          InkWell(
            onTap: () => Navigator.pop(context),
            child: const Icon(Icons.close, size: 28, color: Colors.white),
          ),
        ],
      ),
    );
  }

  // ===========================================================
  // BODY
  // ===========================================================

  Widget _body(BuildContext context) {
    return Expanded(
      child: SingleChildScrollView(
        child: Container(
          width: 950,
          padding: const EdgeInsets.all(30),
          decoration: BoxDecoration(
            color: const Color(0xffF5F5F5),
            borderRadius: BorderRadius.circular(14),
          ),
          child: Column(
            children: [
              _destinationSection(),
              const SizedBox(height: 20),
              _hotelSection(),
              const SizedBox(height: 30),
            ],
          ),
        ),
      ),
    );
  }

  // ===========================================================
  // DESTINATION SECTION
  // ===========================================================

  Widget _destinationSection() {
    final Payment? activePayment = getCurrentUnconfirmedPayment();

    Rate? activeRate;
    if (activePayment != null) {
      activeRate = allRates.firstWhere((r) => r.id == activePayment!.rateId);
    }

    final bool deadlineExtended = activePayment?.deadlineExtended == true;

    final String deadlineDate =
        activePayment != null
            ? "${activePayment.paymentDeadline.day}.${activePayment.paymentDeadline.month}.${activePayment.paymentDeadline.year}"
            : "-";

    final String deadlineLabel =
        activeRate != null ? formatRateDeadlineText(activeRate!) : "";

    if (deadlineExtended && activePayment != null) {
      WidgetsBinding.instance.addPostFrameCallback((_) {
        showDeadlineWarningPopup(context);
      });
    }

    return Container(
      width: double.infinity,
      padding: const EdgeInsets.all(30),
      decoration: BoxDecoration(
        color: const Color(0xffF5F5F5),
        borderRadius: BorderRadius.circular(14),
        border: Border.all(color: const Color(0xff67B1E5), width: 2),
      ),
      child: Column(
        children: [
          Text(
            "Destinacija",
            style: GoogleFonts.openSans(
              fontSize: 40,
              fontWeight: FontWeight.bold,
            ),
          ),

          const SizedBox(height: 30),

          Row(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    _labelValue("Naslov", offer?.title ?? "-"),

                    Text(
                      "Datum i vrijeme rezervacije",
                      style: GoogleFonts.openSans(
                        fontSize: 24,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                    const SizedBox(height: 8),

                    Row(
                      children: [
                        Expanded(
                          child: _valueBox(
                            _formatDate(widget.reservation.createdAt),
                          ),
                        ),
                        const SizedBox(width: 12),
                        Expanded(
                          child: _valueBox(
                            _formatTime(widget.reservation.createdAt),
                          ),
                        ),
                      ],
                    ),

                    const SizedBox(height: 20),

                    _labelValue("Email", widget.user.email ?? "-"),
                    _labelValue(
                      "Broj telefona",
                      widget.user.phoneNumber ?? "-",
                    ),
                    _labelValue("Datum rodjenja", "23.12.2003"),
                  ],
                ),
              ),

              const SizedBox(width: 40),

              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      "Dodatni zahtjevi",
                      style: GoogleFonts.openSans(
                        fontSize: 24,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                    const SizedBox(height: 8),

                    Container(
                      width: 500,
                      height: 150,
                      padding: const EdgeInsets.all(12),
                      decoration: BoxDecoration(
                        color: const Color(0xffF3ECF8),
                        borderRadius: BorderRadius.circular(8),
                      ),
                      child: Text(
                        widget.reservation.userNeeds ??
                            "Nema dodatnih zahtjeva",
                        style: const TextStyle(
                          fontSize: 20,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),

                    const SizedBox(height: 20),

                    Text(
                      "NAPOMENA ZA RADNIKA",
                      style: GoogleFonts.openSans(
                        fontSize: 24,
                        fontWeight: FontWeight.bold,
                      ),
                    ),

                    const SizedBox(height: 12),

                    // Ako nema aktivne uplate → zelena napomena
                    if (activePayment == null)
                      Text(
                        "Nema napomena",
                        style: GoogleFonts.openSans(
                          fontSize: 18,
                          fontWeight: FontWeight.bold,
                          color: Colors.green,
                        ),
                      )
                    else
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          if (deadlineExtended) ...[
                            Text(
                              "UPOZORENJE: VEĆ PREKORAČEN ROK!",
                              style: GoogleFonts.openSans(
                                fontSize: 18,
                                fontWeight: FontWeight.bold,
                                color: Colors.red,
                              ),
                            ),
                            const SizedBox(height: 6),
                            Text(
                              "Novo vrijeme isteka $deadlineLabel: $deadlineDate",
                              style: GoogleFonts.openSans(
                                fontSize: 16,
                                fontWeight: FontWeight.bold,
                                color: Colors.red,
                              ),
                            ),
                          ] else ...[
                            Text(
                              "POTVRDI UPLATU",
                              style: GoogleFonts.openSans(
                                fontSize: 16,
                                fontWeight: FontWeight.bold,
                                color: Colors.red,
                              ),
                            ),
                            Text(
                              "Vrijeme isteka $deadlineLabel: $deadlineDate",
                              style: GoogleFonts.openSans(
                                fontSize: 16,
                                fontWeight: FontWeight.bold,
                                color: Colors.red,
                              ),
                            ),
                          ],
                        ],
                      ),

                    const SizedBox(height: 20),

                    Container(
                      padding: const EdgeInsets.all(12),
                      decoration: BoxDecoration(
                        color: const Color(0xffF3ECF8),
                        borderRadius: BorderRadius.circular(8),
                      ),
                      child: _ratesBlock(),
                    ),

                    const SizedBox(height: 20),

                    _confirmBillButtons(context),
                  ],
                ),
              ),
            ],
          ),

          const SizedBox(height: 30),

          Row(
            children: [
              Expanded(
                child: _labelValue(
                  "Ukupan iznos",
                  "${widget.reservation.totalPrice} KM",
                ),
              ),
              const SizedBox(width: 40),
              Expanded(
                child: _labelValue(
                  "Preostali iznos",
                  "${refreshedLeftToPay ?? widget.reservation.priceLeftToPay} KM",
                ),
              ),
            ],
          ),
        ],
      ),
    );
  }

  // ===========================================================
  // HOTEL SECTION
  // ===========================================================

  Widget _hotelSection() {
    return Container(
      width: double.infinity,
      padding: const EdgeInsets.all(30),
      decoration: BoxDecoration(
        color: const Color(0xffF5F5F5),
        borderRadius: BorderRadius.circular(14),
        border: Border.all(color: const Color(0xff67B1E5), width: 2),
      ),
      child: Column(
        children: [
          Text(
            "Hotel",
            style: GoogleFonts.openSans(
              fontSize: 40,
              fontWeight: FontWeight.bold,
            ),
          ),

          const SizedBox(height: 30),

          Row(
            children: [
              Expanded(child: _labelValue("Naslov", hotel?.name ?? "-")),
              const SizedBox(width: 40),
              Expanded(child: _labelValue("Tip sobe", room?.roomType ?? "-")),
            ],
          ),

          const SizedBox(height: 30),

          _labelValue(
            "Broj preostalih soba",
            hotelRoom?.roomsLeft.toString() ?? "-",
          ),
        ],
      ),
    );
  }

  // ===========================================================
  // CONFIRM/GREEN BUTTON
  // ===========================================================

  Future<void> _generateInvoice() async {
  try {
    final reservation = widget.reservation;

    // 1️⃣ Povuci dodatne podatke
    final offer = await _offerProvider.getById(reservation.offerId);
    final hotel = await _hotelProvider.getById(reservation.hotelId);
    final room = await _roomProvider.getById(reservation.roomId);

    // 2️⃣ Poziv API-ja (SADA ŠALJEMO VIŠE PODATAKA)
    final Uint8List bytes =
        await _reservationProvider.generateInvoice(
      {
        "reservationId": reservation.id,

        "userFullName":
            "${widget.user.firstName} ${widget.user.lastName}",

        "offerTitle": offer.title,

        "hotelName": hotel.name,
        "hotelStars": hotel.stars,

        "roomType": room.roomType,
      },
    );

    // 3️⃣ Save dialog
    final String? filePath = await FilePicker.platform.saveFile(
      dialogTitle: "Sačuvaj račun",
      fileName: "racun_${reservation.id}.pdf",
      type: FileType.custom,
      allowedExtensions: ['pdf'],
    );

    if (filePath == null) return;

    final file = File(filePath);
    await file.writeAsBytes(bytes, flush: true);
  } catch (e) {
    showDialog(
      context: context,
      builder: (_) => AlertDialog(
        title: const Text("Greška"),
        content: Text("Generisanje računa nije uspjelo: $e"),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text("OK"),
          ),
        ],
      ),
    );
  }
}



  Widget _confirmBillButtons(BuildContext context) {
    final fullyPaid = isFullyPaid();

    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        ElevatedButton(
          onPressed: fullyPaid
            ? () async {
                debugPrint("GENERISI RACUN");
                await _generateInvoice(); // ⬅️ NOVO
              }
            : (selectedRateId != null ? () => _confirmPayment() : null),
          style: ElevatedButton.styleFrom(
            backgroundColor:
                fullyPaid
                    ? Colors.green
                    : (selectedRateId != null
                        ? const Color(0xff67B1E5)
                        : Colors.grey),
            padding: const EdgeInsets.symmetric(horizontal: 45, vertical: 18),
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(30),
            ),
          ),
          child: Text(
            fullyPaid ? "generiši račun" : "potvrdi uplatu",
            style: GoogleFonts.openSans(
              color: Colors.white,
              fontSize: 18,
              fontWeight: FontWeight.bold,
            ),
          ),
        ),
      ],
    );
  }

  // ===========================================================
  // UTILITY WIDGETS
  // ===========================================================

  Widget _labelValue(String label, String value) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 20),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            label,
            style: GoogleFonts.openSans(
              fontSize: 24,
              fontWeight: FontWeight.bold,
            ),
          ),
          const SizedBox(width: 8),
          Container(
            height: 48,
            alignment: Alignment.centerLeft,
            padding: const EdgeInsets.symmetric(horizontal: 14),
            decoration: BoxDecoration(
              color: const Color(0xffF3ECF8),
              borderRadius: BorderRadius.circular(8),
            ),
            child: Text(
              value,
              style: GoogleFonts.openSans(
                fontSize: 20,
                fontWeight: FontWeight.bold,
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _valueBox(String value) {
    return Container(
      height: 48,
      alignment: Alignment.centerLeft,
      padding: const EdgeInsets.symmetric(horizontal: 14),
      decoration: BoxDecoration(
        color: const Color(0xffF3ECF8),
        borderRadius: BorderRadius.circular(8),
      ),
      child: Text(
        value,
        style: GoogleFonts.openSans(fontSize: 20, fontWeight: FontWeight.bold),
      ),
    );
  }

  Widget _actionButton() {
    final fullyPaid = isFullyPaid();

    // 🟢 SVE JE PLAĆENO → GENERIŠI RAČUN
    if (fullyPaid) {
      return ElevatedButton(
        onPressed: () {
          debugPrint("GENERISI RACUN");
          // TODO: poziv API-ja za generisanje računa
        },
        style: ElevatedButton.styleFrom(
          backgroundColor: Colors.green,
          padding: const EdgeInsets.symmetric(horizontal: 26, vertical: 14),
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(12),
          ),
        ),
        child: Text(
          "generiši račun",
          style: GoogleFonts.openSans(
            fontSize: 16,
            color: Colors.white,
            fontWeight: FontWeight.bold,
          ),
        ),
      );
    }

    // 🔵 NIJE SVE PLAĆENO → POTVRDI UPLATU
    return ElevatedButton(
      onPressed: selectedRateId != null ? () => _confirmPayment() : null,
      style: ElevatedButton.styleFrom(
        backgroundColor:
            selectedRateId != null ? const Color(0xff67B1E5) : Colors.grey,
        padding: const EdgeInsets.symmetric(horizontal: 26, vertical: 14),
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
      ),
      child: Text(
        "potvrdi uplatu",
        style: GoogleFonts.openSans(
          fontSize: 16,
          color: Colors.white,
          fontWeight: FontWeight.bold,
        ),
      ),
    );
  }

  String _formatDate(DateTime? dt) {
    if (dt == null) return "-";
    return "${dt.day}.${dt.month}.${dt.year}";
  }

  String _formatTime(DateTime? dt) {
    if (dt == null) return "-";
    return "${dt.hour}:${dt.minute.toString().padLeft(2, '0')}";
  }
}
