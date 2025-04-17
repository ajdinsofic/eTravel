import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeIDodaci/parts/brojDanaOsobaAviona.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeIDodaci/parts/datumiPolaska.dart';
import 'package:flutter/material.dart';

Widget specijalneDestinacijeKontejneri(
  double screenHeight,
  double screenWidth,
  String naziv,
  String slikaPath,
  String cijena,
  Map<String, String> detalji,
) {
  return Container(
    height: screenHeight * 0.5,
    width: screenWidth * 0.9,
    margin: EdgeInsets.only(bottom: screenHeight * 0.03),
    child: Column(
      children: [
        // Slika (gornji dio)
        ClipRRect(
          borderRadius: BorderRadius.vertical(top: Radius.circular(18)),
          child: SizedBox(
            height: screenHeight * 0.2415,
            width: double.infinity,
            child: Stack(
              children: [
                AspectRatio(
                  aspectRatio: 16 / 9,
                  child: Image.asset(slikaPath, fit: BoxFit.cover),
                ),
                Container(
                  height: screenHeight * 0.1,
                  width: screenWidth * 0.9,
                  alignment: Alignment.bottomRight,
                  child: Container(
                    height: screenHeight * 0.05,
                    width: screenWidth * 0.27,
                    decoration: BoxDecoration(
                      color: Color(0xFF67B1E5),
                      borderRadius: BorderRadius.only(
                        topLeft: Radius.circular(10),
                        bottomLeft: Radius.circular(10),
                      ),
                    ),
                    child: Container(
                      alignment: Alignment(0, 0),
                      child: Text(
                        cijena,
                        style: TextStyle(
                          color: Colors.white,
                          fontSize: 24,
                          fontFamily: 'AROneSans',
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),
                  ),
                ),
              ],
            ),
          ),
        ),

        Container(
          height: screenHeight * 0.02,
          width: screenWidth * 0.9,
          decoration: BoxDecoration(color: Color(0xFF67B1E5)),
        ),
        // Naziv (donji dio)
        Container(
          height: screenHeight * 0.17,
          width: screenWidth * 0.9,
          decoration: BoxDecoration(
            color: Colors.white,
            borderRadius: BorderRadius.only(
              bottomLeft: Radius.circular(20),
              bottomRight: Radius.circular(20),
            ),
            boxShadow: [
              BoxShadow(
                color: Colors.black.withOpacity(0.2), // boja sjene
                offset: Offset(0, 4), // pomak u X i Y osi
                blurRadius: 8, // zamućenost
                spreadRadius: 1, // širenje sjene
              ),
            ],
          ),
          child: Container(
            margin: EdgeInsets.only(top: screenHeight * 0.01),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Padding(
                  padding: EdgeInsets.only(left: screenWidth * 0.04),
                  child: Text(
                    naziv,
                    style: TextStyle(
                      fontSize: screenWidth * 0.045,
                      fontWeight: FontWeight.w600,
                    ),
                    textAlign: TextAlign.center,
                  ),
                ),

                Container(
                  width: screenWidth * 0.9,
                  child: Row(
                    children:
                        detalji.entries.map((entry) {
                          return BrojDanaOsobaAviona(
                            screenWidth,
                            screenHeight,
                            {"tip": entry.key, "tekst": entry.value},
                          );
                        }).toList(),
                  ),
                ),

                DatumiPolaska(
                  screenWidth,
                  screenHeight,
                  'assets/images/date.png',
                  'Datumi polaska',
                ),
              ],
            ),
          ),
        ),
      ],
    ),
  );
}
