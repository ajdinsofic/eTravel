import 'package:etravel_app/providers/reservationsProvider.dart';
import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:etravel_app/widgets/headerIFooterAplikacije/eTravelFooter.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class PhasePaymentPage extends StatelessWidget {
  final Map<String, dynamic> fazePlacanja = {
    "PrvaRata": ["I RATA", "100KM"],
    "DrugaRata": ["II RATA", "200KM"],
    "TrecaRata": ["III RATA", "100KM"],
    "PreostaliIznos": ["Preostali iznos", "679KM"],
  };

  PhasePaymentPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: SljedecaDestinacijaIMenuBar(),
      body: SingleChildScrollView(
        child: Column(
          children: [
            Container(
              alignment: Alignment(-1, 0),
              margin: EdgeInsets.only(top: 20),
              child: TextButton(
                onPressed: () {
                  Navigator.pop(context);
                },
                style: TextButton.styleFrom(
                  backgroundColor: Color(0xFF67B1E5), // Ispravno upisana boja
                  shape:
                      CircleBorder(), // Ako hoćeš kružno dugme, može i RoundedRectangleBorder
                ),
                child: Icon(Icons.arrow_back, color: Colors.white),
              ),
            ),

            Container(
              width: screenWidth,
              height: screenHeight * 0.05,
              alignment: Alignment.center,
              child: Text(
                'Plačanje u ratama',
                style: TextStyle(
                  fontSize: 24,
                  fontFamily: 'AROneSans',
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
            SizedBox(height: screenHeight * 0.04),

            Column(
              children:
                  fazePlacanja.entries.map((entry) {
                    return Consumer<ReservationsProvider>(
                      builder: (context, provider, child) {
                        bool value;
                        VoidCallback toggle;

                        switch (entry.key) {
                          case "PrvaRata":
                            value = provider.daLiJePlacenaRataI;
                            toggle = provider.togglePlacenaRataI;
                            break;
                          case "DrugaRata":
                            value = provider.daLiJePlacenaRataII;
                            toggle = provider.togglePlacenaRataII;
                            break;
                          case "TrecaRata":
                            value = provider.daLiJePlacenaRataIII;
                            toggle = provider.togglePlacenaRataIII;
                            break;
                          case "PreostaliIznos":
                            value = provider.daLiJePlacenaPreostala;
                            toggle = provider.togglePlacenaPreostala;
                            break;
                          default:
                            value = false;
                            toggle = () {};
                        }

                        return Container(
                          width: screenWidth * 0.9,
                          margin: EdgeInsets.only(bottom: 20),
                          decoration: BoxDecoration(
                            color: Color(0xFFF5F5F5),
                            border: Border.all(color: Color(0xFF67B1E5)),
                            borderRadius: BorderRadius.circular(40),
                          ),
                          child: Padding(
                            padding: EdgeInsets.all(16),
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                Row(
                                  mainAxisAlignment:
                                      MainAxisAlignment.spaceBetween,
                                  children: [
                                    Row(
                                      children: [
                                        Checkbox(
                                          value: value,
                                          onChanged: (newValue) {
                                            toggle();
                                          },
                                        ),
                                        Text(
                                          entry.value[0],
                                          style: TextStyle(
                                            fontSize: 15,
                                            fontFamily: 'AROneSans',
                                            fontWeight: FontWeight.bold,
                                          ),
                                        ),
                                      ],
                                    ),
                                    Container(
                                      width: screenWidth * 0.20,
                                      height: screenHeight * 0.05,
                                      margin: EdgeInsets.only(top: 10),
                                      decoration: BoxDecoration(
                                        color:
                                            value
                                                ? Colors.grey
                                                : Color(
                                                  0xFF67B1E5,
                                                ), // sivo ako je deaktivirano
                                        borderRadius: BorderRadius.circular(20),
                                      ),
                                      child: TextButton(
                                        onPressed:
                                            value == false
                                                ? () {
                                                  Navigator.push(
                                                    context,
                                                    MaterialPageRoute(
                                                      builder:
                                                          (context) =>
                                                              PhasePaymentPage(),
                                                    ),
                                                  );
                                                }
                                                : null,
                                        child: Text(
                                          'uplati',
                                          style: TextStyle(
                                            color: Colors.white,
                                            fontSize: 14,
                                          ),
                                        ),
                                      ),
                                    ),
                                  ],
                                ),
                                Padding(
                                  padding: EdgeInsets.only(left: 14),
                                  child: Text(
                                    'Iznos: ${entry.value[1]}',
                                    style: TextStyle(
                                      fontSize: 15,
                                      fontFamily: 'AROneSans',
                                      fontWeight: FontWeight.bold,
                                    ),
                                  ),
                                ),
                                if (!value) ...[
                                  SizedBox(height: 12),
                                  Padding(
                                    padding: EdgeInsets.only(left: 14),
                                    child: Column(
                                      crossAxisAlignment:
                                          CrossAxisAlignment.start,
                                      children: [
                                        Text(
                                          'Odaberi način plaćanja:',
                                          style: TextStyle(
                                            fontSize: 14,
                                            fontWeight: FontWeight.w600,
                                          ),
                                        ),
                                        SizedBox(height: 6),
                                        Row(
                                          children: [
                                            Radio(
                                              value: 'karticno',
                                              groupValue:
                                                  null, // Dodaj kontrolu ako želiš da se prati selekcija
                                              onChanged: (value) {
                                                // Handle kartično
                                              },
                                            ),
                                            Text("Kartično"),
                                            SizedBox(width: 16),
                                            Radio(
                                              value: 'uplatnica',
                                              groupValue:
                                                  null, // Dodaj kontrolu ako želiš da se prati selekcija
                                              onChanged: (value) {
                                                // Handle uplatnicom
                                              },
                                            ),
                                            Text("Uplatnicom"),
                                          ],
                                        ),
                                      ],
                                    ),
                                  ),
                                ],
                              ],
                            ),
                          ),
                        );
                      },
                    );
                  }).toList(),
            ),
            eTravelFooter(),
          ],
        ),
      ),
    );
  }
}
