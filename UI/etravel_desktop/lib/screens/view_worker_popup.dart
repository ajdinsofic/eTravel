import 'dart:io';
import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/user.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class ViewWorkerPopup extends StatelessWidget {
  final User worker;

  const ViewWorkerPopup({super.key, required this.worker});

  // ------------------ DATE FORMAT ------------------
  String formatDate(String? iso) {
    if (iso == null || iso.isEmpty) return "-";
    try {
      final dt = DateTime.parse(iso).toLocal();
      return "${dt.day.toString().padLeft(2, '0')}"
          ".${dt.month.toString().padLeft(2, '0')}"
          ".${dt.year}";
    } catch (_) {
      return iso;
    }
  }

  Future<bool?> _showConfirmPopup(BuildContext context, User w) {
    return showDialog<bool>(
      context: context,
      barrierDismissible: false,
      builder: (_) {
        return AlertDialog(
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(12),
          ),
          title: const Text(
            "Otpustiti radnika?",
            textAlign: TextAlign.center,
            style: TextStyle(fontWeight: FontWeight.bold),
          ),
          content: Text(
            "Da li ste sigurni da želite otpustiti radnika:\n\n"
            "👤 ${w.firstName} ${w.lastName}",
            textAlign: TextAlign.center,
          ),
          actionsAlignment: MainAxisAlignment.center,
          actions: [
            OutlinedButton(
              onPressed: () => Navigator.pop(context, false),
              style: OutlinedButton.styleFrom(
                foregroundColor: Colors.blue,
                side: const BorderSide(color: Colors.blue, width: 2),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: const Text("Otkaži"),
            ),
            ElevatedButton(
              onPressed: () => Navigator.pop(context, true),
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.redAccent,
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: const Text("Da, otpusti"),
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Dialog(
      insetPadding: const EdgeInsets.symmetric(horizontal: 180, vertical: 80),
      backgroundColor: Colors.white,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(18)),
      child: SizedBox(
        height: 580,
        width: 760,
        child: Column(
          children: [
            // ------------------ HEADER ------------------
            Container(
              height: 48,
              decoration: const BoxDecoration(
                color: Color(0xff67B1E5),
                borderRadius: BorderRadius.vertical(top: Radius.circular(18)),
              ),
              child: Stack(
                children: [
                  Center(
                    child: Text(
                      "pregled radnika",
                      style: GoogleFonts.openSans(
                        fontSize: 18,
                        color: Colors.white,
                        fontWeight: FontWeight.w600,
                      ),
                    ),
                  ),
                  Positioned(
                    right: 12,
                    top: 10,
                    child: GestureDetector(
                      onTap: () => Navigator.pop(context),
                      child: const Icon(
                        Icons.close,
                        color: Colors.white,
                        size: 24,
                      ),
                    ),
                  ),
                ],
              ),
            ),

            // ------------------ CONTENT ------------------
            Expanded(
              child: Padding(
                padding: const EdgeInsets.symmetric(
                  horizontal: 30,
                  vertical: 20,
                ),
                child: Row(
                  children: [
                    // ------------------ LEFT IMAGE ------------------
                    SizedBox(
                      width: 220,
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.spaceAround,
                        children: [
                          Container(
                            width: 180,
                            height: 180,
                            decoration: BoxDecoration(
                              shape: BoxShape.circle,
                              border: Border.all(
                                color: Colors.black54,
                                width: 2,
                              ),
                            ),
                            child: ClipOval(
                              child:
                                  (worker.imageUrl == null ||
                                          worker.imageUrl!.isEmpty)
                                      ? const Icon(
                                        Icons.account_circle_outlined,
                                        size: 120,
                                        color: Colors.black87,
                                      )
                                      : Image.network(
                                        "${ApiConfig.imagesUsers}/${worker.imageUrl}",
                                        fit: BoxFit.cover,
                                        errorBuilder:
                                            (_, __, ___) => const Icon(
                                              Icons.account_circle_outlined,
                                              size: 120,
                                            ),
                                      ),
                            ),
                          ),

                          Padding(
                            padding: const EdgeInsets.only(bottom: 20),
                            child: SizedBox(
                              width: 180,
                              height: 35,
                              child: ElevatedButton(
                                onPressed: () async {
                                  final confirmed = await _showConfirmPopup(
                                    context,
                                    worker,
                                  );
                                  if (confirmed == true) {
                                    // vraća "fire" parent screenu
                                    Navigator.pop(context, "fire");
                                  }
                                },
                                style: ElevatedButton.styleFrom(
                                  backgroundColor: Colors.red,
                                  shape: RoundedRectangleBorder(
                                    borderRadius: BorderRadius.circular(12),
                                  ),
                                ),
                                child: Text(
                                  "otpusti radnika",
                                  style: GoogleFonts.openSans(
                                    fontSize: 15,
                                    color: Colors.white,
                                    fontWeight: FontWeight.w600,
                                  ),
                                ),
                              ),
                            ),
                          ),
                        ],
                      ),
                    ),

                    const SizedBox(width: 30),

                    // ------------------ RIGHT SIDE ------------------
                    Expanded(
                      child: SingleChildScrollView(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            _readonly("Ime", worker.firstName),
                            const SizedBox(height: 12),

                            _readonly("Prezime", worker.lastName),
                            const SizedBox(height: 12),

                            _readonly("Email", worker.email),
                            const SizedBox(height: 12),

                            _readonly("Username", worker.username),
                            const SizedBox(height: 12),

                            _readonly("Broj telefona", worker.phoneNumber),
                            const SizedBox(height: 12),

                            _readonly(
                              "Datum rođenja",
                              formatDate(
                                formatDate(worker.dateBirth?.toIso8601String()),
                              ),
                            ),
                          ],
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }

  // ------------------ READONLY FIELD BUILDER ------------------
  Widget _readonly(String title, String value) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          title,
          style: GoogleFonts.openSans(
            fontWeight: FontWeight.w600,
            fontSize: 14,
          ),
        ),
        const SizedBox(height: 3),
        Container(
          width: double.infinity,
          padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 12),
          decoration: BoxDecoration(
            color: const Color(0xffEDEDED),
            borderRadius: BorderRadius.circular(8),
          ),
          child: Text(value, style: GoogleFonts.openSans(fontSize: 14)),
        ),
      ],
    );
  }
}
