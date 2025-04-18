import 'package:etravel_app/widgets/destinationPageParts/roomofferGenerator.dart';
import 'package:etravel_app/widgets/startingPageParts/SljedecaDestinacijaIMenuBar.dart';
import 'package:etravel_app/widgets/startingPageParts/popularneDestinacijeIDodaci/parts/sliderTackeZaPopularneDestinacije.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class destinationPage extends StatefulWidget {
  final String naziv;
  final String cijena;
  final String brojDana;

  const destinationPage({
    super.key,
    required this.naziv,
    required this.cijena,
    required this.brojDana,
  });

  @override
  State<destinationPage> createState() => _DestinationPageState();
}

class _DestinationPageState extends State<destinationPage> {
  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);
    return Scaffold(
      appBar: SljedecaDestinacijaIMenuBar(),
      body: SingleChildScrollView(
        child: Column(
          children: [
            SizedBox(
              width: screenWidth,
              height: screenHeight * 0.08,
              child: Center(
                child: Text(
                  widget.naziv,
                  style: TextStyle(
                    color: Colors.black,
                    fontWeight: FontWeight.bold,
                    fontFamily: 'AROneSans',
                    fontSize: screenWidth * 0.06,
                  ),
                ),
              ),
            ),
            SizedBox(height: screenHeight * 0.02),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: [
                Transform.translate(
                  offset: Offset(-screenWidth * 0.08, 0),
                  child: ClipRRect(
                    borderRadius: BorderRadius.circular(screenWidth * 0.05),
                    child: SizedBox(
                      height: screenWidth * 0.5,
                      width: screenWidth * 0.3,
                      child: AspectRatio(
                        aspectRatio: 13 / 18,
                        child: Image.asset(
                          'assets/images/istambulPonuda.jpg',
                          fit: BoxFit.cover,
                        ),
                      ),
                    ),
                  ),
                ),
                ClipRRect(
                  borderRadius: BorderRadius.circular(screenWidth * 0.05),
                  child: SizedBox(
                    height: screenWidth * 0.5,
                    width: screenWidth * 0.35,
                    child: AspectRatio(
                      aspectRatio: 13 / 18,
                      child: Image.asset(
                        'assets/images/coverSantorini.jpg',
                        fit: BoxFit.cover,
                      ),
                    ),
                  ),
                ),
                Transform.translate(
                  offset: Offset(screenWidth * 0.08, 0),
                  child: ClipRRect(
                    borderRadius: BorderRadius.circular(screenWidth * 0.05),
                    child: SizedBox(
                      height: screenWidth * 0.5,
                      width: screenWidth * 0.3,
                      child: AspectRatio(
                        aspectRatio: 13 / 18,
                        child: Image.asset(
                          'assets/images/santoriniPonuda.jpg',
                          fit: BoxFit.cover,
                        ),
                      ),
                    ),
                  ),
                ),
              ],
            ),
            SizedBox(height: screenHeight * 0.02),
            SizedBox(
              width: screenWidth * 0.35,
              height: screenHeight * 0.05,
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: [
                  sliderTackeZaPopularneDestinacije(false),
                  sliderTackeZaPopularneDestinacije(true),
                  sliderTackeZaPopularneDestinacije(false),
                ],
              ),
            ),
            Container(
              width: screenWidth,
              height: screenHeight * 0.05,
              margin: EdgeInsets.only(top: screenHeight * 0.04),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Image.asset('assets/images/clock.png'),
                  SizedBox(width: screenWidth * 0.02),
                  Text(
                    widget.brojDana,
                    style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: screenWidth * 0.05,
                    ),
                  ),
                ],
              ),
            ),
            Container(
              width: screenWidth * 0.7,
              height: screenHeight * 0.09,
              margin: EdgeInsets.only(top: screenHeight * 0.025),
              alignment: Alignment.center,
              decoration: BoxDecoration(
                border: Border.all(color: Color(0xFF67B1E5), width: 2),
                borderRadius: BorderRadius.circular(screenWidth * 0.05),
              ),
              child: RichText(
                text: TextSpan(
                  children: [
                    TextSpan(
                      text: 'od ',
                      style: TextStyle(
                        color: Colors.black,
                        fontSize: screenWidth * 0.045,
                        fontWeight: FontWeight.bold,
                        fontFamily: 'AROneSans',
                      ),
                    ),
                    TextSpan(
                      text: widget.cijena,
                      style: TextStyle(
                        color: Color(0xFF67B1E5),
                        fontSize: screenWidth * 0.08,
                        fontWeight: FontWeight.bold,
                        fontFamily: 'AROneSans',
                      ),
                    ),
                    TextSpan(
                      text: ' po osobi',
                      style: TextStyle(
                        color: Colors.black,
                        fontSize: screenWidth * 0.045,
                        fontWeight: FontWeight.bold,
                        fontFamily: 'AROneSans',
                      ),
                    ),
                  ],
                ),
              ),
            ),
            Column(
              children: [
                Container(
                  width: screenWidth * 0.95,
                  height: screenHeight * 0.1,
                  alignment: Alignment.centerLeft,
                  margin: EdgeInsets.only(top: screenHeight * 0.01),
                  child: Text(
                    'O destinaciji',
                    style: TextStyle(
                      fontFamily: 'AROneSans',
                      fontSize: screenWidth * 0.06,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
                Container(
                  width: screenWidth * 0.95,
                  height: screenHeight * 0.4,
                  alignment: Alignment.center,
                  child: Text(
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
                    style: TextStyle(
                      color: Color(0xFF67B1E5),
                      fontFamily: 'AROneSans',
                      fontSize: screenWidth * 0.04,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
              ],
            ),
            Container(
              width: screenWidth * 0.9,
              height: screenHeight * 0.15,
              margin: EdgeInsets.only(top: screenHeight * 0.02),
              alignment: Alignment.center,
              child: Text(
                'Izaberite datum putovanja',
                style: TextStyle(
                  fontFamily: 'AROneSans',
                  fontSize: screenWidth * 0.06,
                  fontWeight: FontWeight.bold,
                ),
                textAlign: TextAlign.center,
              ),
            ),
            Container(
              width: screenWidth,
              padding: EdgeInsets.symmetric(
                vertical: screenHeight * 0.02,
                horizontal: screenWidth * 0.04,
              ),
              decoration: BoxDecoration(
                color: Color(0xFFF5F5F5),
                border: Border.all(color: Color(0xFF67B1E5), width: 2),
              ),
              child: Column(
                children: [
                  ...[
                    '28 Maj 2025',
                    '13 April 2025',
                    '28 Juli 2025',
                    '28 August 2025',
                  ].map((datum) {
                    return Container(
                      margin: EdgeInsets.symmetric(
                        vertical: screenHeight * 0.015,
                      ),
                      width: double.infinity,
                      height: screenHeight * 0.12,
                      child: ElevatedButton(
                        onPressed: () {},
                        style: ElevatedButton.styleFrom(
                          backgroundColor: Colors.white,
                          foregroundColor: Color(0xFF67B1E5),
                          elevation: 3,
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(
                              screenWidth * 0.03,
                            ),
                            side: BorderSide(color: Color(0xFF67B1E5)),
                          ),
                          padding: EdgeInsets.symmetric(
                            vertical: screenHeight * 0.015,
                          ),
                        ),
                        child: Text(
                          datum,
                          style: TextStyle(
                            fontWeight: FontWeight.bold,
                            fontSize: screenWidth * 0.06,
                            fontFamily: 'AROneSans',
                          ),
                        ),
                      ),
                    );
                  }).toList(),
                  SizedBox(height: screenHeight * 0.02),
                  Container(
                    padding: EdgeInsets.symmetric(
                      horizontal: screenWidth * 0.03,
                    ),
                    decoration: BoxDecoration(
                      border: Border.all(color: Color(0xFF67B1E5), width: 2),
                      borderRadius: BorderRadius.circular(screenWidth * 0.03),
                    ),
                    child: DropdownButtonFormField<String>(
                      decoration: InputDecoration.collapsed(hintText: ''),
                      hint: Text(
                        'odaberite tip sobe',
                        style: TextStyle(color: Color(0xFFC7C7C7)),
                      ),
                      value: null,
                      onChanged: (value) {},
                      items:
                          ['Jednokrevetna', 'Dvokrevetna', 'Apartman']
                              .map(
                                (tip) => DropdownMenuItem(
                                  value: tip,
                                  child: Text(tip),
                                ),
                              )
                              .toList(),
                    ),
                  ),
                  SizedBox(height: screenHeight * 0.015),
                  Container(
                    padding: EdgeInsets.symmetric(
                      horizontal: screenWidth * 0.03,
                    ),
                    decoration: BoxDecoration(
                      border: Border.all(color: Color(0xFF67B1E5), width: 2),
                      borderRadius: BorderRadius.circular(screenWidth * 0.03),
                    ),
                    child: DropdownButtonFormField<String>(
                      decoration: InputDecoration.collapsed(hintText: ''),
                      hint: Text(
                        'sortirajte cijenu',
                        style: TextStyle(color: Color(0xFFC7C7C7)),
                      ),
                      value: null,
                      onChanged: (value) {},
                      items:
                          ['Najjeftinije', 'Najskuplje']
                              .map(
                                (opcija) => DropdownMenuItem(
                                  value: opcija,
                                  child: Text(opcija),
                                ),
                              )
                              .toList(),
                    ),
                  ),
                ],
              ),
            ),
            SizedBox(height: screenHeight * 0.1 - 20),
            Column(
              children: [
                Roomoffergenerator(
                  cijena: widget.cijena,
                  slikaPath: 'assets/images/hotel.jpg',
                ),
                Roomoffergenerator(
                  cijena: widget.cijena,
                  slikaPath: 'assets/images/hotel2.jpg',
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
