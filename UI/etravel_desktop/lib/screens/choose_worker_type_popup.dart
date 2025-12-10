import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class ChooseWorkerTypePopup extends StatelessWidget {
  const ChooseWorkerTypePopup({super.key});

  @override
  Widget build(BuildContext context) {
    return Dialog(
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
      insetPadding: const EdgeInsets.symmetric(horizontal: 220, vertical: 200),
      child: SizedBox(
        height: 240,
        width: 500,
        child: Stack(
          children: [
            // -------------- CLOSE BUTTON (X) -------------

            Column(
              children: [
                // ---------------- HEADER ----------------
                Container(
  height: 55,
  decoration: const BoxDecoration(
    color: Color(0xff67B1E5),
    borderRadius: BorderRadius.vertical(top: Radius.circular(16)),
  ),
  child: Row(
    mainAxisAlignment: MainAxisAlignment.spaceBetween,
    children: [
      // 🟦 Nevidljivi placeholder – čuva centar
      const Opacity(
        opacity: 0,
        child: Padding(
          padding: EdgeInsets.only(left: 12),
          child: Icon(Icons.close, size: 24, color: Colors.white),
        ),
      ),

      // 🟦 Naslov u centru
      Text(
        "dodavanje radnika",
        style: GoogleFonts.openSans(
          color: Colors.white,
          fontSize: 20,
          fontWeight: FontWeight.w700,
        ),
      ),

      // 🟦 X dugme
      Padding(
        padding: const EdgeInsets.only(right: 12),
        child: GestureDetector(
          onTap: () => Navigator.pop(context, null),
          child: const Icon(Icons.close, size: 24, color: Colors.white),
        ),
      ),
    ],
  ),
),

                const SizedBox(height: 25),

                // ------------ QUESTION TEXT ------------
                Text(
                  "Da li osoba koju želite zaposliti već ima korisnički račun\nu našoj aplikaciji?",
                  textAlign: TextAlign.center,
                  style: GoogleFonts.openSans(fontSize: 16),
                ),

                const SizedBox(height: 32),

                // ------------ BUTTONS ------------
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  children: [
                    // ---------- EXISTING USER ----------
                    ElevatedButton(
                      onPressed: () => Navigator.pop(context, "existing"),
                      style: ElevatedButton.styleFrom(
                        backgroundColor: const Color(0xff67B1E5), // PLAVA
                        padding: const EdgeInsets.symmetric(
                          horizontal: 32,
                          vertical: 14,
                        ),
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(30),
                        ),
                      ),
                      child: Text(
                        "Da, već je korisnik",
                        style: GoogleFonts.openSans(
                          fontSize: 15,
                          color: Colors.white,
                          fontWeight: FontWeight.w600,
                        ),
                      ),
                    ),

                    // ---------- NEW USER ----------
                    ElevatedButton(
                      onPressed: () => Navigator.pop(context, "new"),
                      style: ElevatedButton.styleFrom(
                        backgroundColor: Colors.redAccent, // CRVENA
                        padding: const EdgeInsets.symmetric(
                          horizontal: 32,
                          vertical: 14,
                        ),
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(30),
                        ),
                      ),
                      child: Text(
                        "Ne, dodajem novog",
                        style: GoogleFonts.openSans(
                          fontSize: 15,
                          color: Colors.white,
                          fontWeight: FontWeight.w600,
                        ),
                      ),
                    ),
                  ],
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
