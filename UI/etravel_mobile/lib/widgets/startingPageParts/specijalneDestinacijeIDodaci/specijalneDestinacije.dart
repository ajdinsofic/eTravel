import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeIDodaci/parts/AllLists.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeIDodaci/parts/specijalneDestinacijeKontejneri.dart';
import 'package:flutter/material.dart';

class specijalneDestinacije extends StatelessWidget {
 

  const specijalneDestinacije({
    super.key,
  });

  

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
                  return SpecijalneDestinacijeKontejneri(
                    naziv: destinacija["naziv"].toString(),
                    slikaPath: destinacija["slika"].toString(),
                    cijena: destinacija["cijena"].toString(),
                    detalji: Map<String, String>.from(destinacija["detalji"] as Map),
                  );
                }).toList(),
          ),
        ),
      ],
    );
  }
}
