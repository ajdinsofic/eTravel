import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/helper/image_helper.dart';
import 'package:etravel_app/providers/offer_hotel_provider.dart';
import 'package:etravel_app/providers/offer_provider.dart';
import 'package:etravel_app/screens/DestinationPage.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeAndParts/parts/brojDanaOsobaAviona.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeAndParts/parts/datumiPolaska.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class destinationContainers extends StatefulWidget {
  final String naziv;
  final String glavnaslikaPath;
  final String cijena;
  final Map<String, String> detalji;
  final List<String> slike;
  final List<String> datumi;
  final int offerId;

  const destinationContainers({
    super.key,
    required this.naziv,
    required this.glavnaslikaPath,
    required this.cijena,
    required this.detalji,
    required this.slike,
    required this.datumi,
    required this.offerId,
  });

  @override
  State<destinationContainers> createState() =>
      _SpecijalneDestinacijeKontejnerState();
}

class _SpecijalneDestinacijeKontejnerState
    extends State<destinationContainers> {
  late OfferProvider _offerProvider;
  late OfferHotelProvider _offerHotelProvider;

  @override
  void initState() {
    super.initState();
    _offerProvider = Provider.of<OfferProvider>(context, listen: false);
  }

  @override
  Widget build(BuildContext context) {
    // Poziv funkcije koja postavlja širinu i visinu
    postaviWidthIHeight(context);
    return GestureDetector(
      onTap: () {
        Navigator.push(
          context,
          MaterialPageRoute(
            builder:
                (context) => destinationPage(
                  naziv: widget.naziv,
                  cijena: widget.cijena,
                  brojDana: widget.detalji["brojDana"],
                  glavnaSlikaPath: widget.glavnaslikaPath,
                  slike: widget.slike,
                  datumi: widget.datumi,
                  offerId: widget.offerId,
                  //slike
                ),
          ),
        );
      },
      child: Container(
        height: screenHeight * 0.44,
        width: screenWidth * 0.9,
        margin: EdgeInsets.only(bottom: screenHeight * 0.03),
        child: Column(
          children: [
            // Slika (gornji dio)
            ClipRRect(
              borderRadius: BorderRadius.vertical(top: Radius.circular(18)),
              child: SizedBox(
                height: screenHeight * 0.2415,
                width: double.infinity,
                child: Stack(
                  children: [
                    AspectRatio(
                      aspectRatio: 16 / 9,
                      child: Image.network(
                        resolveOfferImageUrl(widget.glavnaslikaPath),
                        fit: BoxFit.cover,
                        errorBuilder: (context, error, stackTrace) {
                          return Image.asset(
                            "assets/images/default.jpg",
                            fit: BoxFit.cover,
                          );
                        },
                      ),
                    ),
                    Container(
                      height: screenHeight * 0.1,
                      width: screenWidth * 0.9,
                      alignment: Alignment.bottomRight,
                      child: Container(
                        height: screenHeight * 0.05,
                        width: screenWidth * 0.27,
                        decoration: BoxDecoration(
                          color: Color(0xFF67B1E5),
                          borderRadius: BorderRadius.only(
                            topLeft: Radius.circular(10),
                            bottomLeft: Radius.circular(10),
                          ),
                        ),
                        child: Center(
                          child: Text(
                            widget.cijena,
                            style: TextStyle(
                              color: Colors.white,
                              fontSize: 24,
                              fontFamily: 'AROneSans',
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ),
            // Separator line
            Container(
              height: screenHeight * 0.02,
              width: screenWidth * 0.9,
              decoration: BoxDecoration(color: Color(0xFF67B1E5)),
            ),
            // Naziv i detalji (donji dio)
            Container(
              height: screenHeight * 0.17,
              width: screenWidth * 0.9,
              decoration: BoxDecoration(
                color: Colors.white,
                borderRadius: BorderRadius.only(
                  bottomLeft: Radius.circular(20),
                  bottomRight: Radius.circular(20),
                ),
                boxShadow: [
                  BoxShadow(
                    color: Colors.black.withOpacity(0.2),
                    offset: Offset(0, 4),
                    blurRadius: 8,
                    spreadRadius: 1,
                  ),
                ],
              ),
              child: Container(
                margin: EdgeInsets.only(top: screenHeight * 0.01),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Padding(
                      padding: EdgeInsets.only(left: screenWidth * 0.04),
                      child: Text(
                        widget.naziv,
                        style: TextStyle(
                          fontSize: screenWidth * 0.045,
                          fontWeight: FontWeight.w600,
                        ),
                        textAlign: TextAlign.center,
                      ),
                    ),
                    // Detalji
                    SizedBox(
                      width: screenWidth * 0.9,
                      child: Row(
                        children:
                            widget.detalji.entries.map((entry) {
                              return BrojDanaOsobaAviona(
                                screenWidth,
                                screenHeight,
                                {"tip": entry.key, "tekst": entry.value},
                              );
                            }).toList(),
                      ),
                    ),
                    // Datumi polaska
                    DatumiPolaska(
                      datumi: widget.datumi,
                      screenWidth: screenWidth,
                      screenHeight: screenHeight,
                    ),
                  ],
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
