import 'package:etravel_app/helper/date_converter.dart';
import 'package:etravel_app/models/payment_summary.dart';
import 'package:etravel_app/models/reservations.dart';
import 'package:etravel_app/providers/payment_provider.dart';
import 'package:etravel_app/providers/paypal_provider.dart';
import 'package:etravel_app/screens/PayPalWebViewPage.dart';
import 'package:etravel_app/widgets/PhasePaymentPage/RateCard.dart';
import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:etravel_app/widgets/headerIFooterAplikacije/eTravelFooter.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class PhasePaymentPage extends StatefulWidget {
  final Reservation reservation;

  const PhasePaymentPage({super.key, required this.reservation});

  @override
  State<PhasePaymentPage> createState() => _PhasePaymentPageState();
}

class _PhasePaymentPageState extends State<PhasePaymentPage> {
  late PaymentProvider _paymentProvider;
  late PayPalProvider _payPalProvider;
  PaymentSummary? summary;
  bool isLoading = true;

  @override
  void initState() {
    super.initState();
    _paymentProvider = Provider.of<PaymentProvider>(context, listen: false);
    _payPalProvider = Provider.of<PayPalProvider>(context, listen: false);
    _loadSummary();
  }

  double _calculateRemainingAmount() {
    double total = widget.reservation.totalPrice.toDouble();
    double paid = 0;

    if (summary!.isFirstRatePending == true) {
      paid += 100;
    }

    if (summary!.isSecondRatePending == true) {
      paid += 200;
    }

    // Ako je FULL ili REMAINING već potvrđen,
    // backend ionako neće dozvoliti plaćanje,
    // ali sigurnosno:
    // if (summary!.isFullAmountPending == true ||
    //     summary!.isRemainingPending == true) {
    //   return 0;
    // }

    double remaining = total - paid;
    return remaining < 0 ? 0 : remaining;
  }

  Future<void> _pay({
    required int rateId,
    required String method, // 'karticno' | 'uplatnica'
    required double amount,
  }) async {
    try {
      // =========================
      // UPLATNICA
      // =========================
      if (method == 'uplatnica') {
        final request = {
          "reservationId": widget.reservation.id,
          "rateId": rateId,
          "amount": amount,
          "paymentDate": DateConverter.toUtcIsoFromDate(DateTime.now()),
          "paymentMethod": "uplatnica",
          "isConfirmed": false,
        };

        await _paymentProvider.insert(request);

        _showSuccess(
          "Uplata putem uplatnice je zaprimljena.\n"
          "Molimo izvršite uplatu do navedenog roka.",
        );
      }
      // =========================
      // KARTIČNO / PAYPAL
      // =========================
      if (method == 'karticno') {
        // 1️⃣ Kreiraj PayPal order
        final order = await _payPalProvider.createPayPalOrder(amount);

        if (order == null) {
          _showError("Greška pri kreiranju PayPal naloga.");
          return;
        }

        // 2️⃣ Izvuci approve URL (ISPRAVNO)
        final approveUrl =
            order.links.firstWhere((l) => l.rel == 'approve').href;

        // 3️⃣ Otvori PayPal WebView
        final success = await Navigator.push<bool>(
          context,
          MaterialPageRoute(
            builder:
                (_) => PayPalWebViewPage(
                  approveUrl: approveUrl,
                  orderId: order.id,
                ),
          ),
        );

        if (success != true) {
          _showError("Plaćanje je otkazano.");
          return;
        }

        // 4️⃣ Capture order
        final captured = await _payPalProvider.capturePayPalOrder(order.id);

        if (!captured) {
          _showError("Greška pri potvrdi PayPal plaćanja.");
          return;
        }

        // 5️⃣ Snimi payment u bazu
        await _paymentProvider.insert({
          "reservationId": widget.reservation.id,
          "rateId": rateId,
          "amount": amount,
          "paymentDate": DateConverter.toUtcIsoFromDate(DateTime.now()),
          "paymentMethod": "kartica",
          "isConfirmed": true,
        });

        _showSuccess("Uspješno ste platili putem PayPal-a.");
      }

      // =========================
      // REFRESH SUMMARY
      // =========================
      await _loadSummary();
    } catch (e) {
      debugPrint("${e}");
      _showError("Došlo je do greške pri slanju uplate.");
    }
  }

  Future<bool> _simulatePaypalPayment() async {
    await Future.delayed(const Duration(seconds: 2));
    return true; // kasnije ide pravi PayPal SDK
  }

  void _showSuccess(String message) {
    showDialog(
      context: context,
      builder:
          (_) => AlertDialog(
            title: const Text("Uspjeh", textAlign: TextAlign.center),
            content: Text(message, textAlign: TextAlign.center),
            actions: [
              TextButton(
                onPressed: () => Navigator.pop(context),
                child: const Text("OK"),
              ),
            ],
          ),
    );
  }

  void _showError(String message) {
    showDialog(
      context: context,
      builder:
          (_) => AlertDialog(
            title: const Text("Greška"),
            content: Text(message),
            actions: [
              TextButton(
                onPressed: () => Navigator.pop(context),
                child: const Text("OK"),
              ),
            ],
          ),
    );
  }

  Future<void> _loadSummary() async {
    try {
      summary = await _paymentProvider.getPaymentSummary(widget.reservation.id);
    } catch (e) {
      debugPrint('Greška pri učitavanju PaymentSummary: $e');
    }

    if (!mounted) return;
    setState(() => isLoading = false);
  }

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    return Scaffold(
      backgroundColor: Colors.white,
      body: CustomScrollView(
        slivers: [
          // =========================
          // MENU BAR
          // =========================
          SljedecaDestinacijaIMenuBar(daLijeKliknuo: false),

          SliverToBoxAdapter(
            child: Column(
              children: [
                // =========================
                // BACK BUTTON
                // =========================
                Container(
                  alignment: const Alignment(-1, 0),
                  margin: const EdgeInsets.only(top: 20, left: 10),
                  child: TextButton(
                    onPressed: () => Navigator.pop(context),
                    style: TextButton.styleFrom(
                      backgroundColor: const Color(0xFF67B1E5),
                      shape: const CircleBorder(),
                    ),
                    child: const Icon(Icons.arrow_back, color: Colors.white),
                  ),
                ),

                // =========================
                // NASLOV
                // =========================
                SizedBox(
                  width: screenWidth,
                  height: screenHeight * 0.05,
                  child: const Center(
                    child: Text(
                      'Plaćanje u ratama',
                      style: TextStyle(
                        fontSize: 24,
                        fontFamily: 'AROneSans',
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),
                ),

                SizedBox(height: screenHeight * 0.04),

                // =========================
                // CONTENT
                // =========================
                if (isLoading)
                  const Padding(
                    padding: EdgeInsets.all(24),
                    child: CircularProgressIndicator(),
                  )
                else if (summary == null)
                  const Padding(
                    padding: EdgeInsets.all(24),
                    child: Text(
                      'Nije moguće učitati podatke o ratama.',
                      style: TextStyle(fontWeight: FontWeight.bold),
                    ),
                  )
                else
                  Column(
                    children: [
                      // =========================
                      // I RATA
                      // =========================
                      if (summary!.isFirstRateVisible)
                        _rate(
                          title: 'I RATA',
                          amount: '100 KM',
                          disabled: summary!.isFirstRateDisabled,
                          pending: summary!.isFirstRatePending,
                          onPay:
                              (method) =>
                                  _pay(rateId: 1, method: method, amount: 100),
                        ),

                      // =========================
                      // II RATA
                      // =========================
                      if (summary!.isSecondRateVisible)
                        _rate(
                          title: 'II RATA',
                          amount: '200 KM',
                          disabled: summary!.isSecondRateDisabled,
                          pending: summary!.isSecondRatePending,
                          onPay:
                              (method) =>
                                  _pay(rateId: 2, method: method, amount: 200),
                        ),

                      // =========================
                      // III RATA
                      // =========================
                      if (summary!.isThirdRateVisible)
                        _rate(
                          title: 'III RATA',
                          amount: '--- KM',
                          disabled: summary!.isThirdRateDisabled,
                          pending: summary!.isThirdRatePending,
                          onPay:
                              (method) =>
                                  _pay(rateId: 3, method: method, amount: 0),
                        ),

                      // =========================
                      // PREOSTALI IZNOS
                      // =========================
                      if (summary!.isRemainingVisible)
                        _rate(
                          title: 'PREOSTALI IZNOS',
                          amount:
                              '${_calculateRemainingAmount().toStringAsFixed(0)} KM',
                          disabled: summary!.isRemainingDisabled,
                          pending: summary!.isRemainingPending,
                          onPay:
                              (method) => _pay(
                                rateId: 5,
                                method: method,
                                amount: _calculateRemainingAmount(),
                              ),
                        ),

                      // =========================
                      // PUNI IZNOS
                      // =========================
                      if (summary!.isFullAmountVisible)
                        _rate(
                          title: 'PUNI IZNOS',
                          amount: '${widget.reservation.totalPrice}KM',
                          disabled: summary!.isFullAmountDisabled,
                          pending: summary!.isFullAmountPending,
                          onPay:
                              (method) => _pay(
                                rateId: 4,
                                method: method,
                                amount: widget.reservation.totalPrice,
                              ),
                        ),
                    ],
                  ),

                // =========================
                // FOOTER
                // =========================
                eTravelFooter(),
              ],
            ),
          ),
        ],
      ),
    );
  }

  // =========================
  // UNIVERZALNI BUILDER RATE
  // =========================
  Widget _rate({
    required String title,
    required String amount,
    required bool disabled,
    required bool? pending,
    required void Function(String paymentMethod) onPay,
  }) {
    final bool isConfirmed = pending == true;
    final bool isPending = pending == false;

    return RateCard(
      title: title,
      amountText: amount,
      checked: isConfirmed,
      enabled: !disabled,
      showPaymentOptions: !disabled,
      pendingMessage:
          isPending
              ? 'Uplatnicu mozete predatu u najblizu poslovnicu do: ${DateConverter.fromUtcIsoToDate(summary!.paymentDeadline)}'
              : null,
      onPay: onPay,
    );
  }
}
