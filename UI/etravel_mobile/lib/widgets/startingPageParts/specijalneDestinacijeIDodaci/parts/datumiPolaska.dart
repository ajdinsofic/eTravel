import 'package:flutter/material.dart';

Widget DatumiPolaska(
  double screenWidth,
  double screenHeight,
  String putanjaIkone,
  String tekst,
) {
  return SizedBox(
    width: screenWidth * 0.9,
    child: Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        // Naslov sa ikonom
        Container(
          padding: EdgeInsets.only(left: screenWidth * 0.032),
          margin: EdgeInsets.only(top: screenHeight * 0.015),
          child: Row(
            children: [
              Image.asset(putanjaIkone, width: 25, height: 25),
              SizedBox(width: 6),
              Text(
                tekst,
                style: TextStyle(
                  fontFamily: 'AROneSans',
                  fontWeight: FontWeight.bold,
                  fontSize: 14,
                ),
              ),
            ],
          ),
        ),

        // Datumi polaska
        Padding(
          padding: EdgeInsets.only(top: screenHeight * 0.015, left: screenWidth * 0.032),
          child: Row(
            children: [
              datumTag(screenWidth, screenHeight, '28.5.2025'),
              SizedBox(width: screenWidth * 0.03),
              datumTag(screenWidth, screenHeight, '7.6.2025'),
              SizedBox(width: screenWidth * 0.03),
              datumTag(screenWidth, screenHeight, '8.7.2025'),
            ],
          ),
        ),
      ],
    ),
  );
}

// Pomoćna funkcija za "datum tag"
Widget datumTag(double screenWidth, double screenHeight, String datum) {
  return Container(
    alignment: Alignment.center,
    width: 70,
    height: 20,
    decoration: BoxDecoration(
      color: Color(0xFF67B1E5),
      borderRadius: BorderRadius.circular(20),
    ),
    child: Text(
      datum,
      style: TextStyle(
        color: Colors.white,
        fontSize: screenWidth * 0.03,
        fontWeight: FontWeight.bold,
      ),
    ),
  );
}
