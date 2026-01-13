import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/helper/image_helper.dart';
import 'package:etravel_app/models/offer.dart';
import 'package:etravel_app/providers/offer_hotel_provider.dart';
import 'package:etravel_app/providers/offer_provider.dart';
import 'package:etravel_app/screens/destinationPage.dart';
import 'package:etravel_app/widgets/startingPageParts/popularneDestinacijeAndParts/parts/dotSlider.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class PopularDestinations extends StatefulWidget {
  const PopularDestinations({super.key});

  @override
  State<PopularDestinations> createState() => _PopularDestinationsState();
}

class _PopularDestinationsState extends State<PopularDestinations> {
  final PageController _pageController = PageController(viewportFraction: 0.7);
  int _currentIndex = 0;

  late OfferProvider _offerProvider;
  late OfferHotelProvider _offerHotelProvider;

  List<Offer> _offers = [];
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    _offerProvider = Provider.of<OfferProvider>(context, listen: false);
    _offerHotelProvider =
        Provider.of<OfferHotelProvider>(context, listen: false);

    _loadPopularOffers();
  }

  Future<void> _loadPopularOffers() async {
    try {
      final result = await _offerProvider.get(
        filter: {
          "isPopularOffers": true, // ✅ samo ovo, bez isMainImage
        },
      );

      setState(() {
        _offers = result.items;
        _isLoading = false;
      });
    } catch (e) {
      debugPrint("Greška pri učitavanju popularnih destinacija → $e");
      setState(() => _isLoading = false);
    }
  }

  Future<List<String>> _ucitajDatume(int offerId) async {
    try {
      final result = await _offerHotelProvider.get(
        filter: {"offerDetailsId": offerId},
      );

      final datumi = result.items
          .map((h) => h.departureDate)
          .map(
            (d) =>
                "${d.day.toString().padLeft(2, '0')}.${d.month.toString().padLeft(2, '0')}.${d.year}",
          )
          .toList();

      final unique = datumi.toSet().toList()
        ..sort((a, b) {
          final da = DateTime.parse(a.split('.').reversed.join('-'));
          final db = DateTime.parse(b.split('.').reversed.join('-'));
          return da.compareTo(db);
        });

      return unique;
    } catch (e) {
      debugPrint("Ne mogu učitati datume → $e");
      return [];
    }
  }

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    if (_isLoading) {
      return const Center(
        child: CircularProgressIndicator(color: Color(0xFF67B1E5)),
      );
    }

    if (_offers.isEmpty) {
      return const SizedBox();
    }

    return Column(
      children: [
        Text(
          'Popularne destinacije',
          style: TextStyle(
            color: Colors.black,
            fontWeight: FontWeight.bold,
            fontFamily: 'AROneSans',
            fontSize: screenWidth * 0.06,
          ),
        ),
        SizedBox(height: screenHeight * 0.02),

        SizedBox(
          height: screenWidth * 0.5,
          child: PageView.builder(
            controller: _pageController,
            itemCount: _offers.length,
            onPageChanged: (index) {
              setState(() => _currentIndex = index);
            },
            itemBuilder: (context, index) {
              final offer = _offers[index];
              final isActive = index == _currentIndex;

              // ✅ GLAVNA SLIKA (isMain == true), fallback: prva, fallback: null
              String? mainImage;
              if (offer.offerImages != null && offer.offerImages!.isNotEmpty) {
                final mainImg = offer.offerImages!.firstWhere(
                  (img) => img.isMain == true,
                  orElse: () => offer.offerImages!.first,
                );
                mainImage = mainImg.imageUrl;
              }

              // ✅ SPOREDNE SLIKE (isMain == false)
              final List<String> sporedneSlike =
                  offer.offerImages != null
                      ? offer.offerImages!
                          .where((img) => img.isMain == false)
                          .map((img) => img.imageUrl ?? "default.jpg")
                          .toList()
                      : [];

              // ✅ brojDana string
              final brojDana = "${offer.daysInTotal} dana";

              // ✅ cijena
              final cijena = "${offer.minimalPrice.toInt()}\$";

              return FutureBuilder<List<String>>(
                future: _ucitajDatume(offer.offerId),
                builder: (context, snapshot) {
                  final datumi = snapshot.data ?? [];

                  return GestureDetector(
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => destinationPage(
                            naziv: offer.title,
                            cijena: cijena,
                            brojDana: brojDana,
                            glavnaSlikaPath: mainImage ?? "default.jpg",
                            slike: sporedneSlike,
                            datumi: datumi,
                            offerId: offer.offerId,
                          ),
                        ),
                      );
                    },
                    child: Transform.scale(
                      scale: isActive ? 1 : 0.9,
                      child: Stack(
                        children: [
                          ClipRRect(
                            borderRadius: BorderRadius.circular(20),
                            child: mainImage != null
                                ? Image.network(
                                    resolveOfferImageUrl(mainImage),
                                    width: screenWidth * 0.8,
                                    height: screenHeight * 0.5,
                                    fit: BoxFit.cover,
                                  )
                                : Container(
                                    color: Colors.grey[300],
                                    width: screenWidth * 0.8,
                                    height: screenHeight * 0.5,
                                  ),
                          ),

                          if (isActive)
                            Container(
                              decoration: BoxDecoration(
                                color: Colors.black.withOpacity(0.3),
                                borderRadius: BorderRadius.circular(20),
                              ),
                            ),

                          if (isActive)
                            Center(
                              child: Text(
                                offer.title ?? '',
                                textAlign: TextAlign.center,
                                style: TextStyle(
                                  color: Colors.white,
                                  fontSize: screenWidth * 0.06,
                                  fontWeight: FontWeight.bold,
                                  shadows: [
                                    Shadow(
                                      color: Colors.black.withOpacity(0.7),
                                      blurRadius: 10,
                                      offset: const Offset(0, 2),
                                    ),
                                  ],
                                ),
                              ),
                            ),
                        ],
                      ),
                    ),
                  );
                },
              );
            },
          ),
        ),

        SizedBox(height: screenHeight * 0.02),

        SizedBox(
          width: screenWidth * 0.35,
          height: screenHeight * 0.05,
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            children: List.generate(
              _offers.length,
              (index) => dotSlider(_currentIndex == index),
            ),
          ),
        ),
      ],
    );
  }
}
