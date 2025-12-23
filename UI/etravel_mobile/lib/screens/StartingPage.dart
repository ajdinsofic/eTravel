import 'package:etravel_app/helper/adding_universal_width_height.dart';
import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:etravel_app/widgets/headerIFooterAplikacije/eTravelFooter.dart';
import 'package:etravel_app/widgets/startingPageParts/osjetiteSvakiMjesec/osjetiteMjesec.dart';
import 'package:etravel_app/widgets/startingPageParts/popularneDestinacijeAndParts/popularneDestinacije.dart';
import 'package:etravel_app/widgets/startingPageParts/putujemoZajednoSilverBar/putujemoZajednoNaslovISearchBar.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeAndParts/specijalneDestinacije.dart';
import 'package:etravel_app/widgets/startingPageParts/zastoPutovatiSaNama/zastoPutovatiSaNama.dart';
import 'package:flutter/material.dart';

class StartingPage extends StatefulWidget {
  const StartingPage({super.key});

  @override
  State<StartingPage> createState() => _StartingPageState();
}

class _StartingPageState extends State<StartingPage> {
  String searchQuery = "";

  @override
  Widget build(BuildContext context) {
    addingUniversalWidthAndHeight(context);

    return Scaffold(
      backgroundColor: Colors.white,
      body: CustomScrollView(
        slivers: [
          SljedecaDestinacijaIMenuBar(daLijeKliknuo: false,),
          TravelTogetherHeadlineISearchBar(),

          SliverToBoxAdapter(
            child: Container(
              margin: EdgeInsets.only(top: screenHeight * 0.08),
              child: Column(
                children: [
                  PopularDestinations(),
                  SizedBox(height: screenHeight * 0.06),
                  UniversalDestinations(),
                  SizedBox(height: screenHeight * 0.06),
                  osjetiteMjesec(),
                  SizedBox(height: screenHeight * 0.03),
                  zastoPutovatiSaNama(),
                  SizedBox(height: screenHeight * 0.03),
                  eTravelFooter(),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
