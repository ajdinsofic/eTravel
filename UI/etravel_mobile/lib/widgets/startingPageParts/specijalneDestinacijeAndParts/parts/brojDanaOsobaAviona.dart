import 'package:flutter/material.dart';

Widget BrojDanaOsobaAviona(
    double screenWidth,
    double screenHeight,
    Map<String, dynamic> detalj,
  ) {
    String tip = detalj["tip"];
    String tekst = detalj["tekst"];
    String ikonaPath = "";

    switch (tip) {
      case "brojDana":
        ikonaPath = "assets/images/clock.png";
        break;
      case "osobe":
        ikonaPath = "assets/images/person.png";
        break;
      case "nacinPrevoza":
        ikonaPath = "assets/images/plane.png";
        break;
    }

    return Padding(
      padding: EdgeInsets.only(left: screenWidth * 0.032),
      child: SizedBox(
        width: screenWidth * 0.25,
        child: Row(
          children: [
            Image.asset(ikonaPath, width: 25, height: 25),
            SizedBox(width: 2),
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
    );
  }