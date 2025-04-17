import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeIDodaci/parts/specijalneDestinacijeKontejneri.dart';
import 'package:flutter/material.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeIDodaci/specijalneDestinacije.dart';

class specijalneDestinacije extends StatelessWidget {
 

  specijalneDestinacije({
    super.key,
  });

  final destinacije = [
    {
      "naziv": "Pariz – Grad svjetlosti",
      "slika": "assets/images/coverSantorini.jpg",
      "detalji": {
        "brojDana": "5 dana",
        "osobe": "3 osobe",
        "nacinPrevoza": "Avion",
      },
      "cijena": "599\$",
    },
    {
      "naziv": "New York – Velika jabuka",
      "slika": "assets/images/firencaPonuda.jpg",
      "detalji": {
        "brojDana": "5 dana",
        "osobe": "3 osobe",
        "nacinPrevoza": "Avion",
      },
      "cijena": "499\$",
    },
    {
      "naziv": "Tokio – Tehnološko čudo",
      "slika": "assets/images/santoriniPonuda.jpg",
      "detalji": {
        "brojDana": "5 dana",
        "osobe": "3 osobe",
        "nacinPrevoza": "Avion",
      },
      "cijena": "799\$",
    },
  ];

  @override
  Widget build(BuildContext context) {

    postaviWidthIHeight(context);
    
    return Column(
      children: [
        Text(
          'Specijalne destinacije',
          style: TextStyle(
            color: Colors.black,
            fontWeight: FontWeight.bold,
            fontFamily: 'AROneSans',
            fontSize: screenWidth * 0.06,
          ),
        ),
        Container(
          margin: EdgeInsets.only(top: screenHeight * 0.03),
          child: Column(
            children:
                destinacije.map((destinacija) {
                  return specijalneDestinacijeKontejneri(
                    screenHeight,
                    screenWidth,
                    destinacija["naziv"].toString(),
                    destinacija["slika"].toString(),
                    destinacija["cijena"].toString(),
                    Map<String, String>.from(destinacija["detalji"] as Map),
                  );
                }).toList(),
          ),
        ),
      ],
    );
  }
}
