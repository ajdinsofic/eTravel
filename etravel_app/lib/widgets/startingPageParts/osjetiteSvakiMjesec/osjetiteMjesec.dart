import 'package:etravel_app/widgets/startingPageParts/osjetiteSvakiMjesec/mjeseciUKoloni.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class osjetiteMjesec extends StatefulWidget {
  final mjeseciKolone = [
    'JANUAR',
    'MART',
    'MAJ',
    'JULI',
    'SEPTEMBAR',
    'NOVEMBAR',
    'DECEMBAR',
  ];

  final mjeseciRedove = ['FEBRUAR', 'APRIL', 'JUNI', 'AUGUST', 'OKTOBAR'];

  final sirine = [0.9, 0.769, 0.64, 0.515, 0.39, 0.27, 0.27];
  final sirine2 = [0.363, 0.3, 0.238, 0.178, 0.1135];

  osjetiteMjesec({super.key});

  @override
  State<osjetiteMjesec> createState() => _osjetiteMjesecState();
}

class _osjetiteMjesecState extends State<osjetiteMjesec> {
  double smallerWidth = 0;

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(
      context,
    ); // Pretpostavljamo da postavlja screenWidth i screenHeight

    return Container(
      width: screenWidth,
      height: screenHeight * 0.7,
      color: const Color(0xFF67B1E5),
      child: Column(
        children: [
          Container(
            width: screenWidth,
            height: screenHeight * 0.15,
            alignment: const Alignment(0, 0),
            child: Text(
              'Osjetite svaki mjesec',
              style: TextStyle(
                color: Colors.white,
                fontSize: screenWidth * 0.06,
                fontFamily: 'AROneSans',
                fontWeight: FontWeight.bold,
              ),
            ),
          ),
          Container(
            width: screenWidth * 0.9,
            height: screenHeight * 0.465,
            child: Stack(
              children: [
                Container(
                  margin: EdgeInsets.only(top: 20),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.end,
                    children: List.generate(widget.mjeseciKolone.length, (index) {
                      return Container(
                        width: screenWidth * widget.sirine[index],
                        height: screenHeight * 0.05,
                        margin: const EdgeInsets.only(top: 10),
                        decoration: BoxDecoration(
                          border: Border.all(color: Colors.white, width: 2),
                        ),
                        alignment: Alignment(0, 0),
                        child: Text(
                          widget.mjeseciKolone[index],
                          style: const TextStyle(color: Colors.white, fontFamily: 'AROneSans', fontWeight: FontWeight.bold, fontSize: 12),
                        ),
                      );
                    }),
                  ),
                ),
                ...List.generate(widget.mjeseciRedove.length, (index) {
                  double leftOffset =
                      index * (screenWidth * 0.1 + 10); // širina + margina
                  return Positioned(
                    left: leftOffset,
                    bottom: 0,
                    child: Container(
                      width: screenWidth * 0.1,
                      height: screenHeight * widget.sirine2[index],
                      decoration: BoxDecoration(
                        border: Border.all(color: Colors.white, width: 2),
                      ),
                      alignment: Alignment(0, 0),
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children:
                            widget.mjeseciRedove[index].split('').map((char) {
                              bool isOktobar = widget.mjeseciRedove[index] == "OKTOBAR";
                              return Text(
                                char,
                                style: TextStyle(
                                  color: Colors.white,
                                  fontSize: isOktobar ? 7.5 : 14,
                                  fontFamily: 'AROneSans', fontWeight: FontWeight.bold
                                ),
                              );
                            }).toList(),
                      ),
                    ),
                  );
                }),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
