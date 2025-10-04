import 'package:etravel_app/screens/UniversalOfferPage.dart';
import 'package:etravel_app/widgets/startingPageParts/osjetiteSvakiMjesec/parts/AllLists.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class osjetiteMjesec extends StatefulWidget {
  const osjetiteMjesec({super.key});

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
          SizedBox(
            width: screenWidth * 0.9,
            height: screenHeight * 0.465,
            child: Stack(
              children: [
                Container(
                  margin: EdgeInsets.only(top: 20),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.end,
                    children: List.generate(mjeseciKolone.length, (index) {
                      return GestureDetector(
                        onTap: () {
                          Navigator.pushAndRemoveUntil(
                            context,
                            MaterialPageRoute(
                              builder:
                                  (context) => Universalofferpage(
                                    mjesec: mjeseciKolone[index],
                                  ), // Passing the required mjesec
                            ),
                            (Route<dynamic> route) =>
                                false, // Clears previous routes
                          );
                        },
                        child: Container(
                          width: screenWidth * sirine[index],
                          height: screenHeight * 0.05,
                          margin: const EdgeInsets.only(top: 10),
                          decoration: BoxDecoration(
                            border: Border.all(color: Colors.white, width: 2),
                          ),
                          alignment: Alignment(0, 0),
                          child: Text(
                            mjeseciKolone[index],
                            style: const TextStyle(
                              color: Colors.white,
                              fontFamily: 'AROneSans',
                              fontWeight: FontWeight.bold,
                              fontSize: 12,
                            ),
                          ),
                        ),
                      );
                    }),
                  ),
                ),
                ...List.generate(mjeseciRedove.length, (index) {
                  double leftOffset =
                      index * (screenWidth * 0.1 + 10); // širina + margina
                  return Positioned(
                    left: leftOffset,
                    bottom: 0,
                    child: GestureDetector(
                      onTap: () {
                          Navigator.pushAndRemoveUntil(
                            context,
                            MaterialPageRoute(
                              builder:
                                  (context) => Universalofferpage(
                                    mjesec: mjeseciRedove[index],
                                  ), // Passing the required mjesec
                            ),
                            (Route<dynamic> route) =>
                                false, // Clears previous routes
                          );
                        },
                      child: Container(
                        width: screenWidth * 0.1,
                        height: screenHeight * sirine2[index],
                        decoration: BoxDecoration(
                          border: Border.all(color: Colors.white, width: 2),
                        ),
                        alignment: Alignment(0, 0),
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children:
                              mjeseciRedove[index].split('').map((char) {
                                bool isOktobar =
                                    mjeseciRedove[index] == "OKTOBAR";
                                return Text(
                                  char,
                                  style: TextStyle(
                                    color: Colors.white,
                                    fontSize: isOktobar ? 7.5 : 14,
                                    fontFamily: 'AROneSans',
                                    fontWeight: FontWeight.bold,
                                  ),
                                );
                              }).toList(),
                        ),
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
