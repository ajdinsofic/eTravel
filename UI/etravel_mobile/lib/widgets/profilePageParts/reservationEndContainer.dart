import 'package:flutter/material.dart';

import 'package:etravel_app/providers/reservationsProvider.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart'; // prilagodi putanju ako treba

class ReservationEndContainer extends StatelessWidget {
  final int index;
  final String slika;


  const ReservationEndContainer({super.key, required this.index, required this.slika});

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);
    return Consumer<ReservationsProvider>(
      builder: (context, provider, child) {
        return Container(
          width: screenWidth * 0.9,
          padding: EdgeInsets.symmetric(vertical: screenHeight * 0.015),
          decoration: BoxDecoration(
            color: Color(0xFFF5F5F5),
            border: Border.all(color: Color(0xFF67B1E5)),
            borderRadius: BorderRadius.circular(20),
          ),
          child: Column(
            children: [
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceAround,
                children: [
                  CircleAvatar(
                    backgroundImage: AssetImage('assets/images/${slika}'),
                    radius: screenWidth * 0.08,
                  ),
                  SizedBox(
                    width: screenWidth * 0.38,
                    height: screenHeight * 0.1,
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(
                          'FIRENCE',
                          style: TextStyle(
                            fontWeight: FontWeight.bold,
                            fontSize: screenWidth * 0.04,
                          ),
                        ),
                        Row(
                          mainAxisAlignment: MainAxisAlignment.spaceBetween,
                          children: [
                            Row(
                              children: [
                                Image.asset(
                                  'assets/images/clock.png',
                                  width: 15,
                                  height: 15,
                                ),
                                SizedBox(width: 4),
                                Text(
                                  '5 dana',
                                  style: TextStyle(fontWeight: FontWeight.bold),
                                ),
                              ],
                            ),
                            Row(
                              children: [
                                Image.asset(
                                  'assets/images/person.png',
                                  width: 15,
                                  height: 15,
                                ),
                                SizedBox(width: 4),
                                Text(
                                  '3 osobe',
                                  style: TextStyle(fontWeight: FontWeight.bold),
                                ),
                              ],
                            ),
                          ],
                        ),
                      ],
                    ),
                  ),
                  TextButton(
                    onPressed: () {
                      provider.togglePrikaziZavrseneDetalje(index);
                    },
                    child: Container(
                      width: screenWidth * 0.2,
                      height: screenHeight * 0.05,
                      alignment: Alignment.center,
                      decoration: BoxDecoration(
                        color: Color(0xFF67B1E5),
                        borderRadius: BorderRadius.circular(10),
                      ),
                      child: Text(
                        provider.prikaziZavrseneDetalje(index)
                            ? 'Manje detalja'
                            : 'Više detalja',
                        style: TextStyle(
                          fontSize: screenWidth * 0.03,
                          color: Colors.white,
                        ),
                        textAlign: TextAlign.center,
                      ),
                    ),
                  ),
                ],
              ),
              if (provider.prikaziZavrseneDetalje(index))
                Padding(
                  padding: EdgeInsets.symmetric(
                    horizontal: screenWidth * 0.04,
                    vertical: screenHeight * 0.015,
                  ),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Divider(color: Color(0xFF67B1E5)),
                      Text(
                        'Nakon putovanja, vaš utisak o celokupnom iskustvu nam je izuzetno važan, jer nam pomaže da unaprijedimo našu ponudu i uslugu ',
                        style: TextStyle(
                          fontWeight: FontWeight.bold,
                          fontSize: 15,
                          fontFamily: 'AROneSans',
                        ),
                        textAlign: TextAlign.center,
                      ),
                      SizedBox(height: screenHeight * 0.01),
                      TextField(
                        maxLines: 5, // Povećaj broj linija da bude veće polje
                        decoration: InputDecoration(
                          hintText: 'Ovdje možete ostaviti svoj komentar...',
                          border: OutlineInputBorder(),
                          fillColor: Colors.white, // daje mu lijepi okvir
                          contentPadding: EdgeInsets.symmetric(
                            horizontal: 12,
                            vertical: 16,
                          ),
                        ),
                      ),
                      SizedBox(height: 20),
                      Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [
                          // Tekst "Broj zvijezda"
                          Column(
                            children: [
                              Text(
                                'Broj zvijezda:',
                                style: TextStyle(
                                  fontSize: 18,
                                  fontWeight: FontWeight.bold,
                                ),
                              ),
                              
                              
                              // Row sa zvjezdama
                              Row(
                                children: List.generate(5, (index) {
                                  return Icon(
                                    Icons.star,
                                    color: Color(0xFFDAB400), // Žuta boja zvijezde
                                    size: 28, // Veličina zvijezde
                                  );
                                }),
                              ),
                            ],
                          ),
                          SizedBox(height: 20),

                          // ElevatedButton
                          ElevatedButton(
                            onPressed: () {
                              // logika za "posalji" dugme
                            },
                            style: ElevatedButton.styleFrom(
                              backgroundColor: Color(0xFF67B1E5), // Plava boja pozadine
                            ),
                            child: Text(
                              'Pošalji',
                              style: TextStyle(
                                color: Colors.white, // Bijela boja teksta
                              ),
                            ),
                          ),
                        ],
                      ),
                    ],
                  ),
                ),
            ],
          ),
        );
      },
    );
  }
}
