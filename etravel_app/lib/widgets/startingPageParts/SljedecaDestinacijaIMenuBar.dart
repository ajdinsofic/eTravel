import 'package:etravel_app/providers/navigationOpenProvider.dart';
import 'package:etravel_app/screens/NavigationPage.dart';
import 'package:etravel_app/screens/startingPage.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class SljedecaDestinacijaIMenuBar extends StatefulWidget
    implements PreferredSizeWidget {
  const SljedecaDestinacijaIMenuBar({super.key});

  @override
  State<SljedecaDestinacijaIMenuBar> createState() =>
      _SljedecaDestinacijaIMenuBarState();

  @override
  Size get preferredSize => Size.fromHeight(screenHeight * 0.1);
}

class _SljedecaDestinacijaIMenuBarState
    extends State<SljedecaDestinacijaIMenuBar> {
  bool daLijeKliknuo =
      false; // Ovdje postavljamo daLijeKliknuo kao varijablu klase

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);
    // final daLiJeOtvorenaNavigacija = Provider.of<AppState>(context); NAUCI JEBENE PROVIDERE STA SU KAKO RADE JEBO IH TATA
    return AppBar(
      toolbarHeight: screenHeight * 0.1,
      backgroundColor: const Color(0xFF67B1E5),
      automaticallyImplyLeading: false,
      elevation: 0,
      flexibleSpace: Center(
        child: Container(
          height: screenHeight * 0.09,
          margin: EdgeInsets.only(top: screenHeight * 0.045),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              // Ovaj container je za prikazivanje naziva destinacije
              GestureDetector(
                onTap: () {
                  // Ovdje se koristi pushAndRemoveUntil za resetovanje svih prethodnih ekrana
                  Navigator.pushAndRemoveUntil(
                    context,
                    MaterialPageRoute(builder: (context) => StartingPage()),
                    (Route<dynamic> route) => false, // Briše sve prethodne rute
                  );
                },
                child: Container(
                  height: screenHeight * 0.05,
                  width: screenWidth * 0.6,
                  margin: EdgeInsets.only(left: screenWidth * 0.025),
                  decoration: BoxDecoration(
                    border: Border.all(color: Colors.white, width: 2),
                    borderRadius: BorderRadius.circular(20),
                  ),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Image.asset('assets/images/world.png'),
                      const SizedBox(width: 6),
                      Text(
                        'SLJEDECA DESTINACIJA?',
                        style: TextStyle(
                          color: Colors.white,
                          fontWeight: FontWeight.bold,
                          fontSize: screenWidth * 0.03,
                          fontFamily: 'AROneSans',
                        ),
                      ),
                    ],
                  ),
                ),
              ),
              // Ovaj container je za sliku koja se menja (ikona koja se klika)
              Container(
                height: screenHeight * 0.045,
                width: screenWidth * 0.12,
                margin: EdgeInsets.only(right: screenWidth * 0.025),
                child: GestureDetector(
                  onTap: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(builder: (context) => NavigationPage()),
                    );
                  },
                  child: Center(child: Image.asset('assets/images/lines.png')),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
