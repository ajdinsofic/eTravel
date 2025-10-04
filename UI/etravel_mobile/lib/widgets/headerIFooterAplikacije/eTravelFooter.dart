import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class eTravelFooter extends StatefulWidget {
  const eTravelFooter({super.key});

  @override
  State<eTravelFooter> createState() => _eTravelFooterState();
}

class _eTravelFooterState extends State<eTravelFooter> {
  final lokacije = [
    {
      "naslovLokacije": "Sarajevo",
      "adresa": "Ulica Mladih 15, 71000 Sarajevo",
      "brojTelefona": "+387 33 456 789",
    },
    {
      "naslovLokacije": "Banjaluka",
      "adresa": "Bulevar Kralja Petra 45, 78000 Banja Luka",
      "brojTelefona": "+387 33 234 567",
    },
    {
      "naslovLokacije": "Travnik",
      "adresa": "Ulica Sunca 22, 72270 Travnik",
      "brojTelefona": "+387 33 987 654",
    },
  ];

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);
    return Container(
      color: const Color(0xFF67B1E5),
      padding: EdgeInsets.all(screenWidth * 0.06), // skaliran padding
      width: double.infinity,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            "eTravel",
            style: TextStyle(
              fontFamily: 'LeckerliOne',
              fontSize: screenWidth * 0.095, // skaliran font
              color: Colors.white,
            ),
          ),
          SizedBox(height: screenHeight * 0.02),
          Text(
            "Krenite sa nama u jedna od nezaboravnih putovanja",
            style: TextStyle(
              fontFamily: 'AROneSans',
              fontSize: screenWidth * 0.035,
              fontWeight: FontWeight.bold,
              color: Colors.white,
            ),
          ),
          SizedBox(height: screenHeight * 0.02),
          Text(
            "infoeTravel@gmail.com\nbugeTravel@gmail.com",
            style: TextStyle(
              fontSize: screenWidth * 0.035,
              color: Colors.white,
            ),
          ),
          SizedBox(height: screenHeight * 0.03),

          // Lokacije
          ...lokacije.map(
            (lokacija) => Padding(
              padding: EdgeInsets.only(bottom: screenHeight * 0.02),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    lokacija["naslovLokacije"]!,
                    style: TextStyle(
                      fontSize: screenWidth * 0.04,
                      fontWeight: FontWeight.bold,
                      color: Colors.white,
                    ),
                  ),
                  Text(
                    lokacija["adresa"]!,
                    style: TextStyle(
                      fontSize: screenWidth * 0.035,
                      color: Colors.white,
                    ),
                  ),
                  Text(
                    lokacija["brojTelefona"]!,
                    style: TextStyle(
                      fontSize: screenWidth * 0.035,
                      color: Colors.white,
                    ),
                  ),
                ],
              ),
            ),
          ),

          // Ikone društvenih mreža
          Row(
            children: [
              Icon(
                Icons.facebook,
                color: Colors.white,
                size: screenWidth * 0.07,
              ),
              SizedBox(width: screenWidth * 0.04),
              Icon(
                Icons.camera_alt,
                color: Colors.white,
                size: screenWidth * 0.07,
              ),
              SizedBox(width: screenWidth * 0.04),
              Icon(
                Icons.whatshot,
                color: Colors.white,
                size: screenWidth * 0.07,
              ),
            ],
          ),

          // Platne kartice (mjesta za slike)
          Container(
            margin: EdgeInsets.only(top:20),
            child: Row(
              children: [
                Container(
                  width: screenWidth / 6,
                  decoration: BoxDecoration(
                    color: Colors.white, // pozadina unutar bordera
                    borderRadius: BorderRadius.circular(15),
                  ),
                  child: Image.asset(
                    "assets/images/mastercard.png",
                    height: screenHeight * 0.04,
                  ),
                ),
                SizedBox(width: screenWidth * 0.015),
                Container(
                  width: screenWidth / 6,
                  decoration: BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.circular(15),
                  ),
                  child: Image.asset(
                    "assets/images/visa.png",
                    height: screenHeight * 0.04,
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
