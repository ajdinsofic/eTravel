import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class KomentariKontejner extends StatelessWidget {
  final String korisnik;
  final String komentar;
  final String slika;
  final int brojZvjezda;

  const KomentariKontejner({
    super.key,
    required this.korisnik,
    required this.komentar,
    required this.slika,
    required this.brojZvjezda,
  });

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);
    return Container(
      width: screenWidth * 0.85,
      margin: EdgeInsets.symmetric(vertical: screenHeight * 0.02),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(16),
        border: Border.all(color: Colors.blue.shade100, width: 1),
      ),
      child: Column(
        children: [
          // Slika korisnika
          Container(
            padding: EdgeInsets.only(top: 20),
            child: CircleAvatar(
              backgroundImage: AssetImage(slika),
              radius: screenWidth * 0.08,
            ),
          ),
          SizedBox(height: screenHeight * 0.015),
          // Ime korisnika
          Text(
            korisnik,
            style: TextStyle(
              fontWeight: FontWeight.bold,
              fontSize: screenWidth * 0.045,
            ),
          ),
          SizedBox(height: screenHeight * 0.02),
          // Plavi container: Komentar + Zvjezdice
          Container(
            width: screenWidth * 0.85,
            padding: EdgeInsets.symmetric(
              vertical: screenHeight * 0.025,
              horizontal: screenWidth * 0.04,
            ),
            decoration: BoxDecoration(
              color: Color(0xFF67B1E5),
              borderRadius: BorderRadius.circular(16),
            ),
            child: Column(
              children: [
                Text(
                  komentar,
                  textAlign: TextAlign.center,
                  style: TextStyle(
                    fontSize: screenWidth * 0.038,
                    color: Colors.white,
                  ),
                ),
                SizedBox(height: screenHeight * 0.015),
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: List.generate(5, (index) {
                    return Icon(
                      index < brojZvjezda ? Icons.star : Icons.star_border,
                      color: Color(0xFFDAB400),
                      size: screenWidth * 0.05,
                    );
                  }),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
