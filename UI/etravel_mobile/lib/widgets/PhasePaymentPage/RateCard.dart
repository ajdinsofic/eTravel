import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

class RateCard extends StatefulWidget {
  final String title;
  final String amountText;
  final bool checked;
  final bool enabled;
  final bool showPaymentOptions;
  final String? pendingMessage;
  final void Function(String paymentMethod)? onPay;

  const RateCard({
    super.key,
    required this.title,
    required this.amountText,
    required this.checked,
    required this.enabled,
    required this.showPaymentOptions,
    this.pendingMessage,
    this.onPay,
  });

  @override
  State<RateCard> createState() => _RateCardState();
}

class _RateCardState extends State<RateCard> {
  String? _paymentMethod; // 'karticno' | 'uplatnica'

  void _handlePay() {
    // ❌ validacija
    if (_paymentMethod == null) {
      _showError("Molimo odaberite način plaćanja.");
      return;
    }

    // ✅ javi parentu
    widget.onPay?.call(_paymentMethod!);
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

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    return Container(
      width: screenWidth * 0.9,
      margin: const EdgeInsets.only(bottom: 20),
      decoration: BoxDecoration(
        color: const Color(0xFFF5F5F5),
        border: Border.all(color: const Color(0xFF67B1E5)),
        borderRadius: BorderRadius.circular(40),
      ),
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            //-----------------------------------------
            // GORNJI RED
            //-----------------------------------------
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Row(
                  children: [
                    Checkbox(value: widget.checked, onChanged: null),
                    Text(
                      widget.title,
                      style: const TextStyle(
                        fontSize: 15,
                        fontFamily: 'AROneSans',
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ],
                ),

                //-----------------------------------------
                // UPLATI
                //-----------------------------------------
                SizedBox(
                  width: screenWidth * 0.23,
                  height: screenHeight * 0.05,
                  child: ElevatedButton(
                    onPressed: widget.enabled ? _handlePay : null,
                    style: ElevatedButton.styleFrom(
                      backgroundColor: const Color(0xFF67B1E5),
                      disabledBackgroundColor: const Color(
                        0xFF67B1E5,
                      ).withOpacity(0.4),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(20),
                      ),
                      elevation: 0,
                    ),
                    child: const Text(
                      'uplati',
                      style: TextStyle(color: Colors.white, fontSize: 14),
                    ),
                  ),
                ),
              ],
            ),

            //-----------------------------------------
            // IZNOS
            //-----------------------------------------
            Padding(
              padding: const EdgeInsets.only(left: 14, top: 6),
              child: Text(
                'Iznos: ${widget.amountText}',
                style: const TextStyle(
                  fontSize: 15,
                  fontFamily: 'AROneSans',
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),

            //-----------------------------------------
            // PENDING PORUKA
            //-----------------------------------------
            if (widget.pendingMessage != null) ...[
              const SizedBox(height: 10),
              Padding(
                padding: const EdgeInsets.only(left: 14),
                child: Text(
                  "${widget.pendingMessage!} ",
                  style: const TextStyle(
                    color: Colors.red,
                    fontWeight: FontWeight.bold,
                  ),
                ),
              ),
            ],

            //-----------------------------------------
            // NAČIN PLAĆANJA
            //-----------------------------------------
            if (widget.showPaymentOptions) ...[
              const SizedBox(height: 12),
              const Padding(
                padding: EdgeInsets.only(left: 14),
                child: Text(
                  'Odaberi način plaćanja:',
                  style: TextStyle(fontSize: 14, fontWeight: FontWeight.w600),
                ),
              ),
              Padding(
                padding: const EdgeInsets.only(left: 14),
                child: Row(
                  children: [
                    Radio<String>(
                      value: 'karticno',
                      groupValue: _paymentMethod,
                      onChanged:
                          widget.enabled
                              ? (value) {
                                setState(() {
                                  _paymentMethod = value;
                                });
                              }
                              : null,
                    ),

                    const Text("Paypal"),
                    Radio<String>(
                      value: 'uplatnica',
                      groupValue: _paymentMethod,
                      onChanged:
                          widget.enabled
                              ? (value) {
                                setState(() {
                                  _paymentMethod = value;
                                });
                              }
                              : null,
                    ),

                    const Text("Uplatnicom"),
                  ],
                ),
              ),
            ],
          ],
        ),
      ),
    );
  }
}
