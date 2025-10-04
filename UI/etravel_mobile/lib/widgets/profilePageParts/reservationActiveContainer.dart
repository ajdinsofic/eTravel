import 'package:etravel_app/providers/reservationsProvider.dart';
import 'package:etravel_app/screens/PhasePaymentPage.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart'; // prilagodi putanju ako treba

class ReservationActiveContainer extends StatelessWidget {
  final int index;
  final String slika;

  final Map<String, dynamic> aktivneRezervacije = {
    "hotel": "Hotel Paradiso",
    "tipSobe": "Dvokrevetna soba",
    "datumPolaska": "15.8.2025",
    "brojOdraslih": 2,
    "usluga": {
      "putovanje": ["Putovanje", "599KM"],
      "doplata": ["Doplata za dodatak krevet", "140KM"],
      "boravisnaTaksa": ["Boravišna taksa", "20KM"],
      "zdravstveno": ["Putničko zdravstveno osiguranje", "20KM"],
    },
  };

  ReservationActiveContainer({
    super.key,
    required this.index,
    required this.slika,
  });

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
                      provider.togglePrikaziAktivneDetalje(index);
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
                        provider.prikaziAktivneDetalje(index)
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
              if (provider.prikaziAktivneDetalje(index))
                Padding(
                  padding: EdgeInsets.symmetric(
                    horizontal: screenWidth * 0.04,
                    vertical: screenHeight * 0.015,
                  ),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Divider(color: Color(0xFF67B1E5)),
                      Row(
                        mainAxisAlignment: MainAxisAlignment.spaceAround,
                        children: [
                          Container(
                            child: Text(
                              aktivneRezervacije["tipSobe"],
                              style: TextStyle(
                                fontFamily: 'AROneSans',
                                fontSize: 15,
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                          ),
                          Container(
                            height: 20,
                            decoration: BoxDecoration(
                              border: Border.all(color: Colors.black),
                            ),
                          ),
                          Container(
                            child: Text(
                              aktivneRezervacije["hotel"],
                              style: TextStyle(
                                fontFamily: 'AROneSans',
                                fontSize: 15,
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                          ),
                        ],
                      ),
                      Row(
                        children: [
                          Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Container(
                                width: screenWidth * 0.4,
                                padding: EdgeInsets.only(left: 7),
                                margin: EdgeInsets.only(top: 10),
                                child: Text(
                                  'Vrijeme polaska: ',
                                  style: TextStyle(
                                    fontFamily: 'AROneSans',
                                    fontSize: 15,
                                    fontWeight: FontWeight.bold,
                                  ),
                                ),
                              ),
                              Container(
                                width: screenWidth * 0.6,
                                padding: EdgeInsets.only(left: 7),
                                child: Text(
                                  "${aktivneRezervacije['datumPolaska']} - 20.8.2025",
                                  style: TextStyle(
                                    fontFamily: 'AROneSans',
                                    fontSize: 15,
                                    fontWeight: FontWeight.bold,
                                  ),
                                ),
                              ),
                            ],
                          ),
                          Container(
                            width: screenWidth * 0.20,
                            height: screenHeight * 0.05,
                            margin: EdgeInsets.only(top: 10),
                            decoration: BoxDecoration(
                              color: Color(0xFF67B1E5),
                              borderRadius: BorderRadius.circular(20),
                            ),
                            child: TextButton(
                              onPressed: () {
                                Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                    builder: (context) => PhasePaymentPage(),
                                  ),
                                );
                              },
                              child: Text(
                                'detalji rata',
                                style: TextStyle(
                                  color: Colors.white,
                                  fontSize: 10,
                                ), // Da se tekst vidi na plavoj
                              ),
                            ),
                          ),
                        ],
                      ),
                      Container(
                        width: screenWidth * 0.6,
                        padding: EdgeInsets.only(left: 7),
                        margin: EdgeInsets.only(top: 20),
                        child: Text(
                          "Odraslih: ${aktivneRezervacije['brojOdraslih']}",
                          style: TextStyle(
                            fontFamily: 'AROneSans',
                            fontSize: 15,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ),
                      SizedBox(height: screenHeight * 0.05),
                      Container(
                        width: screenWidth * 0.9,
                        padding: EdgeInsets.only(left: 7),
                        child: Text(
                          'Usluga:',
                          style: TextStyle(
                            fontFamily: 'AROneSans',
                            fontSize: 20,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ),
                      ...aktivneRezervacije["usluga"].entries.map((entry) {
                        final String naziv = entry.value[0];
                        final String cijena = entry.value[1];

                        return Container(
                          width: screenWidth * 0.9,
                          height: screenHeight * 0.03,
                          padding: EdgeInsets.only(left: 7),
                          margin: EdgeInsets.only(top: 20),
                          decoration: BoxDecoration(
                            border: Border(
                              bottom: BorderSide(
                                color: Color(
                                  0xFF67B1E5,
                                ), // ili Color(0xFF67B1E5) ako želiš istu plavu
                                width: 2.0, // možeš podesiti debljinu
                              ),
                            ),
                          ),
                          child: Row(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            children: [
                              Text(
                                "$naziv",
                                style: TextStyle(
                                  color: Color(0xFF67B1E5),
                                  fontSize: 15,
                                ),
                              ),
                              Text(
                                "$cijena",
                                style: TextStyle(
                                  color: Color(0xFF67B1E5),
                                  fontSize: 15,
                                ),
                              ),
                            ],
                          ),
                        );
                      }).toList(),
                      SizedBox(height: screenHeight * 0.03),
                      Container(
                        width: screenWidth * 0.9,
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.end,
                          children: [
                            Text(
                              'UKUPNO:',
                              style: TextStyle(
                                fontFamily: 'AROneSans',
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                            Text(
                              '779KM',
                              style: TextStyle(
                                color: Color(0xFF67B1E5),
                                fontFamily: 'AROneSans',
                                fontWeight: FontWeight.bold,
                                fontSize: 30,
                              ),
                            ),
                          ],
                        ),
                      ),
                      SizedBox(height: screenHeight * 0.03),
                      Container(
                        alignment: Alignment(1, 0),
                        child: ElevatedButton(
                          onPressed: () {
                            // logika za otkazivanje
                          },
                          style: ElevatedButton.styleFrom(
                            backgroundColor: Color(
                              0xFFD62929,
                            ), // pozadina bijela (ili neka druga)
                            foregroundColor: Colors.white, // tekst crvene boje
                            // ako hoćeš crveni rub
                          ),
                          child: Text(
                            'Otkazi rezervaciju',
                            style: TextStyle(fontWeight: FontWeight.bold),
                          ),
                        ),
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
