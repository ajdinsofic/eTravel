import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:etravel_app/providers/offer_provider.dart';
import 'package:etravel_app/providers/offer_hotel_provider.dart';
import 'package:etravel_app/models/offer.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeAndParts/parts/specijalneDestinacijeKontejneri.dart';

class UniversalDestinations extends StatefulWidget {
  const UniversalDestinations({super.key});

  @override
  State<UniversalDestinations> createState() => _UniversalDestinationsState();
}

class _UniversalDestinationsState extends State<UniversalDestinations> {
  late OfferProvider offerProvider;
  late OfferHotelProvider offerHotelProvider;

  late PageController _pageController;

  /// Sve stranice koje PageView prikazuje
  final Map<int, List<Offer>> _pageOffers = {};

  /// ML lista (frontend paginacija)
  List<Offer> _recommendedOffers = [];

  /// Cache datuma po offeru
  final Map<int, List<String>> _datesCache = {};

  bool isLoading = true;
  bool useRecommended = false;

  int currentPage = 0;
  final int pageSize = 1;
  int totalPages = 1;

  @override
  void initState() {
    super.initState();

    offerProvider = Provider.of<OfferProvider>(context, listen: false);
    offerHotelProvider = Provider.of<OfferHotelProvider>(context, listen: false);

    _pageController = PageController(initialPage: 0);

    _initData();
  }

  @override
  void dispose() {
    _pageController.dispose();
    super.dispose();
  }

  // ======================================================
  // ====================== INIT ==========================
  // ======================================================

  Future<void> _initData() async {
    setState(() => isLoading = true);

    // Ako je user logovan → ML
    if (Session.userId != null) {
      final ok = await _loadRecommended();
      if (ok) {
        setState(() => isLoading = false);
        return;
      }
    }

    // fallback → specijalne destinacije
    await _loadSpecial();
    setState(() => isLoading = false);
  }

  // ======================================================
  // ================= RECOMMENDED (ML) ===================
  // ======================================================

  Future<bool> _loadRecommended() async {
    try {
      _recommendedOffers =
          await offerProvider.getRecommendedForUser(Session.userId!);

      if (_recommendedOffers.length < 3) return false;

      totalPages = (_recommendedOffers.length / pageSize).ceil();
      _prepareRecommendedPages();
      await _preloadDates(_recommendedOffers);

      useRecommended = true;
      currentPage = 0;
      return true;
    } catch (_) {
      return false;
    }
  }

  void _prepareRecommendedPages() {
    _pageOffers.clear();

    for (int i = 0; i < totalPages; i++) {
      final start = i * pageSize;
      final end =
          (start + pageSize).clamp(0, _recommendedOffers.length);

      _pageOffers[i] = _recommendedOffers.sublist(start, end);
    }
  }

  // ======================================================
  // ================= SPECIAL (BACKEND) ==================
  // ======================================================

  Future<void> _loadSpecial() async {
    final all = await offerProvider.get(filter: {"RetrieveAll": true});

    totalPages = (all.items.length / pageSize).ceil();
    _pageOffers.clear();

    for (int i = 0; i < totalPages; i++) {
      final start = i * pageSize;
      final end = (start + pageSize).clamp(0, all.items.length);

      _pageOffers[i] = all.items.sublist(start, end);
    }

    await _preloadDates(all.items);
    useRecommended = false;
    currentPage = 0;
  }

  // ======================================================
  // ====================== DATUMI ========================
  // ======================================================

  Future<void> _preloadDates(List<Offer> offers) async {
    for (final offer in offers) {
      if (_datesCache.containsKey(offer.offerId)) continue;

      final result = await offerHotelProvider.get(
        filter: {"offerDetailsId": offer.offerId},
      );

      final datumi = result.items
          .map((h) => h.departureDate)
          .map(
            (d) =>
                "${d.day.toString().padLeft(2, '0')}.${d.month.toString().padLeft(2, '0')}.${d.year}",
          )
          .toSet()
          .toList()
        ..sort((a, b) {
          final da = DateTime.parse(a.split('.').reversed.join('-'));
          final db = DateTime.parse(b.split('.').reversed.join('-'));
          return da.compareTo(db);
        });

      _datesCache[offer.offerId] = datumi;
    }
  }

  // ======================================================
  // ======================== UI ==========================
  // ======================================================

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    return Column(
      children: [
        Text(
          useRecommended
              ? "Destinacije po vašem ukusu"
              : "Specijalne destinacije",
          style: _naslovStyle(),
        ),
        SizedBox(height: screenHeight * 0.03),

        SizedBox(
          height: screenHeight * 0.47,
          child: isLoading
              ? const Center(
                  child: CircularProgressIndicator(
                    color: Color(0xFF67B1E5),
                  ),
                )
              : PageView.builder(
                  controller: _pageController,
                  itemCount: totalPages,
                  onPageChanged: (index) {
                    setState(() => currentPage = index);
                  },
                  itemBuilder: (context, pageIndex) {
                    final offers = _pageOffers[pageIndex]!;
                    final offer = offers.first;

                    return Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        destinationContainers(
                          naziv: offer.title,
                          glavnaslikaPath: _getMainImage(offer),
                          slike: _getOtherImages(offer),
                          cijena: "${offer.minimalPrice.toInt()}\$",
                          detalji: {
                            "brojDana": "${offer.daysInTotal} dana",
                            "nacinPrevoza": offer.wayOfTravel ?? "N/A",
                          },
                          offerId: offer.offerId,
                          datumi: _datesCache[offer.offerId] ?? [],
                        ),
                      ],
                    );
                  },
                ),
        ),

        const SizedBox(height: 12),
        Text(
          "Stranica ${currentPage + 1} / $totalPages",
          style: const TextStyle(fontWeight: FontWeight.bold),
        ),
      ],
    );
  }

  // ======================================================
  // ====================== HELPERS =======================
  // ======================================================

  String _getMainImage(Offer offer) {
    if (offer.offerImages.isNotEmpty) {
      final main = offer.offerImages.firstWhere(
        (i) => i.isMain == true,
        orElse: () => offer.offerImages.first,
      );
      return main.imageUrl ?? "default.jpg";
    }
    return "default.jpg";
  }

  List<String> _getOtherImages(Offer offer) {
    return offer.offerImages
        .where((i) => i.isMain == false)
        .map((i) => i.imageUrl ?? "default.jpg")
        .toList();
  }

  TextStyle _naslovStyle() {
    return TextStyle(
      color: Colors.black,
      fontWeight: FontWeight.bold,
      fontFamily: 'AROneSans',
      fontSize: screenWidth * 0.06,
    );
  }
}
