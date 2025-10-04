import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:etravel_app/widgets/headerIFooterAplikacije/eTravelFooter.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeIDodaci/parts/AllLists.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeIDodaci/parts/specijalneDestinacijeKontejneri.dart';
import 'package:flutter/material.dart';

class Universalofferpage extends StatelessWidget {
  final String mjesec; // Non-nullable required parameter

  // Constructor now requires mjesec to be passed
  Universalofferpage({required this.mjesec});

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);
    return Scaffold(
      appBar: SljedecaDestinacijaIMenuBar(),
      backgroundColor: Colors.white,
      body: SingleChildScrollView(
        child: Column(
          children: [
            Container(
              width: screenWidth,
              height: screenHeight * 0.1,
              alignment: Alignment.center,
              child: Text(
                mjesec,
                style: TextStyle(
                  fontFamily: 'AROneSans',
                  fontWeight: FontWeight.bold,
                  fontSize: screenWidth * 0.06,
                ),
              ),
            ),
            ...destinacije.map((destinacija) {
              return SpecijalneDestinacijeKontejneri(
                naziv: destinacija["naziv"].toString(),
                slikaPath: destinacija["slika"].toString(),
                cijena: destinacija["cijena"].toString(),
                detalji: Map<String, String>.from(
                  destinacija["detalji"] as Map,
                ),
              );
            }).toList(),
            eTravelFooter()
          ],
        ),
      ),
    );
  }
}
