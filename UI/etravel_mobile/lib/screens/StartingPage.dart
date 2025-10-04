import 'package:etravel_app/widgets/headerIFooterAplikacije/eTravelFooter.dart';
import 'package:etravel_app/widgets/startingPageParts/osjetiteSvakiMjesec/osjetiteMjesec.dart';
import 'package:etravel_app/widgets/startingPageParts/popularneDestinacijeIDodaci/popularneDestinacije.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeIDodaci/specijalneDestinacije.dart';
import 'package:etravel_app/widgets/startingPageParts/zastoPutovatiSaNama/zastoPutovatiSaNama.dart';
import 'package:flutter/material.dart';
import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:etravel_app/widgets/startingPageParts/putujemoZajednoSilverBar/putujemoZajednoNaslovISearchBar.dart';


class StartingPage extends StatefulWidget {
  const StartingPage({super.key});

  @override
  State<StartingPage> createState() => _StartingPageState();
}

class _StartingPageState extends State<StartingPage> {
  @override
  Widget build(BuildContext context) {
    
    postaviWidthIHeight(context);

    return Scaffold(
      backgroundColor: Colors.white,
      appBar: SljedecaDestinacijaIMenuBar(),
      body: CustomScrollView(
        slivers: [
          PutujemoZajednoNaslovISearchBar(),
          SliverToBoxAdapter(
            child: Container(
              margin: EdgeInsets.only(top: screenHeight * 0.08),
              child: Column(
                children: [
                  popularneDestinacije(),
                  SizedBox(height: screenHeight * 0.06),
                  specijalneDestinacije(),
                  SizedBox(height: screenHeight * 0.06),
                  osjetiteMjesec(),
                  SizedBox(height: screenHeight * 0.03),
                  zastoPutovatiSaNama(),
                  SizedBox(height: screenHeight * 0.03),
                  eTravelFooter()
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
