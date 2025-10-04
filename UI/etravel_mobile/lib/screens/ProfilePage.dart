import 'package:etravel_app/providers/reservationsProvider.dart';
import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:etravel_app/widgets/headerIFooterAplikacije/eTravelFooter.dart';
import 'package:etravel_app/widgets/profilePageParts/reservationActiveContainer.dart';
import 'package:etravel_app/widgets/profilePageParts/reservationUniversalView.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class profilePage extends StatefulWidget {
  const profilePage({super.key});

  @override
  State<profilePage> createState() => _profilePageState();
}

class _profilePageState extends State<profilePage> {
  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    return Scaffold(
      appBar: SljedecaDestinacijaIMenuBar(),
      backgroundColor: Colors.white,
      body: SingleChildScrollView(
        child: Column(
          children: [
            // Naslov
            Container(
              width: screenWidth,
              height: screenHeight * 0.13,
              alignment: Alignment.center,
              child: Text(
                'Moj profil',
                style: TextStyle(
                  fontFamily: 'AROneSans',
                  fontWeight: FontWeight.bold,
                  fontSize: screenWidth * 0.06,
                ),
              ),
            ),

            // Profilna slika
            Container(
              alignment: Alignment.center,
              child: CircleAvatar(
                backgroundImage: AssetImage('assets/images/me.jpg'),
                radius: screenWidth * 0.2,
              ),
            ),

            SizedBox(height: screenHeight * 0.02),

            // Dugme za unos slike
            Container(
              width: screenWidth * 0.5,
              height: screenHeight * 0.06,
              alignment: Alignment.center,
              color: Color(0xFF67B1E5),
              child: Text(
                'Unesite sliku',
                style: TextStyle(
                  color: Colors.white,
                  fontFamily: 'AROneSans',
                  fontWeight: FontWeight.bold,
                  fontSize: screenWidth * 0.04,
                ),
              ),
            ),

            SizedBox(height: screenHeight * 0.04),

            // Travel tokeni
            Container(
              width: screenWidth,
              height: screenHeight * 0.05,
              alignment: Alignment.center,
              child: Text(
                'UKUPAN BROJ TRAVEL TOKENA:',
                style: TextStyle(
                  fontFamily: 'AROneSans',
                  fontWeight: FontWeight.bold,
                  fontSize: screenWidth * 0.045,
                ),
                textAlign: TextAlign.center,
              ),
            ),
            Container(
              width: screenWidth,
              height: screenHeight * 0.1,
              alignment: Alignment.center,
              color: Color(0xFF67B1E5),
              child: Text(
                '20',
                style: TextStyle(
                  color: Colors.white,
                  fontFamily: 'AROneSans',
                  fontWeight: FontWeight.bold,
                  fontSize: screenWidth * 0.12,
                ),
              ),
            ),

            SizedBox(height: screenHeight * 0.04),

            // Glavni kontejner za podatke
            Container(
              width: screenWidth * 0.9,
              padding: EdgeInsets.all(screenWidth * 0.04),
              decoration: BoxDecoration(
                color: Color(0xFFF5F5F5),
                borderRadius: BorderRadius.circular(screenWidth * 0.05),
              ),
              child: Column(
                children: [
                  // Osobne informacije
                  Text(
                    'OSOBNE INFORMACIJE',
                    style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: screenWidth * 0.05,
                    ),
                  ),
                  SizedBox(height: screenHeight * 0.02),

                  _buildTextField('Ime', 'Ajdin'),
                  _buildTextField('Prezime', 'Sofić'),
                  _buildTextField(
                    'Broj telefona',
                    '0603458476',
                    keyboardType: TextInputType.phone,
                  ),
                  _buildTextField(
                    'Datum rođenja',
                    '24/6/2003',
                    keyboardType: TextInputType.datetime,
                  ),

                  SizedBox(height: screenHeight * 0.025),

                  // Promjena lozinke - bez ExpansionTile
                  Container(
                    width: double.infinity,
                    padding: EdgeInsets.all(screenWidth * 0.04),
                    decoration: BoxDecoration(
                      color: Color(0xFF67B1E5),
                      borderRadius: BorderRadius.circular(screenWidth * 0.05),
                    ),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(
                          'Želite li promijeniti lozinku?',
                          style: TextStyle(
                            color: Colors.white,
                            fontWeight: FontWeight.bold,
                            fontSize: screenWidth * 0.045,
                          ),
                        ),
                        SizedBox(height: screenHeight * 0.015),
                        _buildTextField('Stara lozinka', '', obscureText: true),
                        _buildTextField('Nova lozinka', '', obscureText: true),
                        SizedBox(height: screenHeight * 0.015),
                        Row(
                          children: [
                            SizedBox(
                              width: screenWidth * 0.45,
                              child: Text(
                                'zaboravili ste staru lozinku?',
                                style: TextStyle(
                                  color: Colors.white,
                                  fontSize: screenWidth * 0.03,
                                ),
                              ),
                            ),
                            Spacer(),
                            ElevatedButton(
                              onPressed: () {},
                              style: ElevatedButton.styleFrom(
                                backgroundColor: Color(0xFF67B1E5),
                                foregroundColor: Colors.white,
                                side: BorderSide(color: Colors.white, width: 2),
                              ),
                              child: Text(
                                'ažuriraj',
                                style: TextStyle(fontSize: screenWidth * 0.035),
                              ),
                            ),
                          ],
                        ),
                      ],
                    ),
                  ),

                  SizedBox(height: screenHeight * 0.025),

                  // Ažuriraj podatke dugme
                  ElevatedButton(
                    onPressed: () {},
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Color(0xFF67B1E5),
                      shape: StadiumBorder(),
                      padding: EdgeInsets.symmetric(
                        horizontal: screenWidth * 0.1,
                        vertical: screenHeight * 0.015,
                      ),
                    ),
                    child: Text(
                      'ažuriraj podatke',
                      style: TextStyle(
                        color: Colors.white,
                        fontWeight: FontWeight.bold,
                        fontSize: screenWidth * 0.04,
                      ),
                    ),
                  ),
                ],
              ),
            ),

            SizedBox(height: screenHeight * 0.05),

            Container(
              width: screenWidth,
              height: screenHeight * 0.08,
              alignment: Alignment.center,
              color: Color(0xFF67B1E5),
            ),

            SizedBox(height: screenHeight * 0.05),

            Container(
              width: screenWidth * 0.95,
              decoration: BoxDecoration(
                border: Border.all(color: Color(0xFF67B1E5)),
                borderRadius: BorderRadius.circular(20),
              ),
              child: Column(
                children: [
                  ReservationUniversalView(imeRezervacija: 'Aktivne rezervacije', daliJeActive: true),
                  SizedBox(height: screenHeight * 0.05),
                  ReservationUniversalView(imeRezervacija: 'Završena putovanja', daliJeActive: false),
                ],
              ),
            ),
            SizedBox(height: screenHeight * 0.05),
            eTravelFooter()
          ],
        ),
      ),
    );
  }

  Widget _buildTextField(
    String label,
    String hint, {
    bool obscureText = false,
    TextInputType keyboardType = TextInputType.text,
  }) {
    return Padding(
      padding: EdgeInsets.symmetric(vertical: screenHeight * 0.01),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            '*$label',
            style: TextStyle(
              fontWeight: FontWeight.bold,
              fontSize: screenWidth * 0.035,
            ),
          ),
          SizedBox(height: screenHeight * 0.005),
          TextField(
            obscureText: obscureText,
            keyboardType: keyboardType,
            decoration: InputDecoration(
              hintText: hint,
              hintStyle: TextStyle(fontSize: screenWidth * 0.035),
              filled: true,
              fillColor: Colors.white,
              border: OutlineInputBorder(
                borderRadius: BorderRadius.circular(screenWidth * 0.02),
              ),
              contentPadding: EdgeInsets.symmetric(
                horizontal: screenWidth * 0.03,
                vertical: screenHeight * 0.015,
              ),
            ),
          ),
        ],
      ),
    );
  }
}
