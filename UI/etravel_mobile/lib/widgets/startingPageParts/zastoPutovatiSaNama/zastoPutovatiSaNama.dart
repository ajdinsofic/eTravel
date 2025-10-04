import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class zastoPutovatiSaNama extends StatefulWidget {
  const zastoPutovatiSaNama({super.key});

  @override
  State<zastoPutovatiSaNama> createState() => _zastoPutovatiSaNamaState();
}

class _zastoPutovatiSaNamaState extends State<zastoPutovatiSaNama> {
  final List<Map<String, dynamic>> zastoPutovatiSaNama = [
    {
      "naslov": "Povoljne cijene",
      "text":
          "Putujte povoljno uz naše ekskluzivne ponude i uštedite na smještaju, letovima i turama",
      "logo": "assets/images/moneyIcon.png",
    },
    {
      "naslov": "Egzotične destinacije",
      "text":
          "Otkrijte rajske plaže, gradove i skrivena blaga širom svijeta uz naše odabrane aranžmane",
      "logo": "assets/images/locationIcon.png",
    },
    {
      "naslov": "Podrška",
      "text":
          "Naša stručna podrška dostupna je 24/7 kako bismo vaše putovanje učinili sigurnim i bezbrižnim",
      "logo": "assets/images/supportIcon.png",
    },
  ];

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);
    return Container(
      child: Column(
        children: [
          Container(
            width: screenWidth,
            height: screenHeight * 0.1,
            margin: EdgeInsets.only(bottom: screenHeight * 0.02),
            alignment: Alignment(0, 0),
            child: Text(
              'Zašto putovati sa nama',
              style: TextStyle(
                fontSize: 20,
                fontFamily: 'AROneSans',
                fontWeight: FontWeight.bold,
              ),
            ),
          ),
          Column(
            children: List.generate(zastoPutovatiSaNama.length, (index) {
              final stavka = zastoPutovatiSaNama[index];
              return Container(
                margin: EdgeInsets.only(bottom: screenHeight * 0.05 + 20),
                height: screenHeight * 0.4,
                width: screenWidth * 0.85,
                padding: EdgeInsets.all(16),
                decoration: BoxDecoration(
                  color: Colors.white, // čista bijela pozadina unutra
                  border: Border.all(color: Colors.black, width: 1),
                  borderRadius: BorderRadius.circular(10),
                  boxShadow: [
                    BoxShadow(
                      color: Colors.black.withOpacity(0.15),
                      spreadRadius: 1,
                      blurRadius: 8,
                      offset: Offset(0, 5),
                    ),
                  ],
                ),

                child: Column(
                  mainAxisAlignment: MainAxisAlignment.end,
                  children: [
                    Image.asset(
                      width: screenWidth * 0.9,
                      height: 150,
                      stavka["logo"] as String,
                    ), // Widget ikonica
                    SizedBox(height: screenHeight * 0.01 + 5),
                    Text(
                      stavka["naslov"] as String,
                      style: TextStyle(
                        fontSize: 20,
                        fontWeight: FontWeight.bold,
                        fontFamily: 'AROneSans',
                      ),
                    ),
                    SizedBox(height: screenHeight * 0.01),
                    Text(
                      stavka["text"] as String,
                      style: TextStyle(fontSize: 15),
                      textAlign: TextAlign.center,
                    ),
                  ],
                ),
              );
            }),
          ),
        ],
      ),
    );
  }
}
