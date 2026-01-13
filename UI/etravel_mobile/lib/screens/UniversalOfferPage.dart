import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:etravel_app/models/offer.dart';
import 'package:etravel_app/providers/offer_provider.dart';
import 'package:etravel_app/providers/offer_hotel_provider.dart';
import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:etravel_app/widgets/headerIFooterAplikacije/eTravelFooter.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeAndParts/parts/specijalneDestinacijeKontejneri.dart';

class Universalofferpage extends StatefulWidget {
  int? subCategoryId;
  String? title;
  String? searchQuery;

  Universalofferpage({
    super.key,
    this.title,
    this.subCategoryId,
    this.searchQuery,
  });

  @override
  State<Universalofferpage> createState() => _UniversalofferpageState();
}

class _UniversalofferpageState extends State<Universalofferpage> {
  late OfferProvider offerProvider;
  late OfferHotelProvider offerHotelProvider;

  PageController? _pageController;
  final Map<int, List<Offer>> _pageOffers = {};

  bool isLoading = true;
  int currentPage = 0;
  final int pageSize = 1;
  int totalPages = 1;

  @override
  void initState() {
    super.initState();

    offerProvider = Provider.of<OfferProvider>(context, listen: false);
    offerHotelProvider = Provider.of<OfferHotelProvider>(context, listen: false);

    _pageController = PageController(initialPage: 0);

    _loadTotal().then((_) {
      _loadPageIfNeeded(0);
    });
  }

  @override
  void dispose() {
    _pageController?.dispose();
    super.dispose();
  }

  /// ===============================
  /// UKUPAN BROJ STRANICA
  /// ===============================
  Future<void> _loadTotal() async {
    final filter = <String, dynamic>{
      "RetrieveAll": true,
      if (widget.subCategoryId != null)
        "subCategoryId": widget.subCategoryId,
      if (widget.searchQuery != null && widget.searchQuery!.isNotEmpty)
        "FTS": widget.searchQuery,
    };

    final result = await offerProvider.get(filter: filter);
    totalPages = (result.items.length / pageSize).ceil();
  }

  /// ===============================
  /// LOAD STRANICE (CACHE)
  /// ===============================
  Future<void> _loadPageIfNeeded(int page) async {
    if (_pageOffers.containsKey(page)) {
      setState(() => currentPage = page);
      return;
    }

    setState(() => isLoading = true);

    final filter = <String, dynamic>{
      "page": page,
      "pageSize": pageSize,
      if (widget.subCategoryId != null)
        "subCategoryId": widget.subCategoryId,
      if (widget.searchQuery != null && widget.searchQuery!.isNotEmpty)
        "FTS": widget.searchQuery,
    };

    final result = await offerProvider.get(filter: filter);

    _pageOffers[page] = result.items;
    currentPage = page;

    setState(() => isLoading = false);
  }

  /// ===============================
  /// DATUMI
  /// ===============================
  Future<List<String>> _ucitajDatume(int offerId) async {
    final result = await offerHotelProvider.get(
      filter: {"offerDetailsId": offerId},
    );

    final datumi = result.items
        .map((h) => h.departureDate)
        .map(
          (d) =>
              "${d.day.toString().padLeft(2, '0')}.${d.month.toString().padLeft(2, '0')}.${d.year}",
        )
        .toSet()
        .toList()
      ..sort();

    return datumi;
  }

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    final String pageTitle =
        widget.searchQuery != null && widget.searchQuery!.isNotEmpty
            ? 'Rezultati pretrazivanja: "${widget.searchQuery}"'
            : widget.title ?? "Ponude";

    final bool nemaRezultata = totalPages == 0;

    return Scaffold(
      backgroundColor: Colors.white,
      body: CustomScrollView(
        slivers: [
          SljedecaDestinacijaIMenuBar(daLijeKliknuo: false),

          /// NASLOV
          SliverToBoxAdapter(
            child: Container(
              height: screenHeight * 0.1,
              alignment: Alignment.center,
              child: Text(
                pageTitle,
                textAlign: TextAlign.center,
                style: TextStyle(
                  fontFamily: 'AROneSans',
                  fontWeight: FontWeight.bold,
                  fontSize: screenWidth * 0.05,
                  
                ),
              ),
            ),
          ),

          /// PAGE VIEW ILI PORUKA
          SliverToBoxAdapter(
  child: nemaRezultata
      ? SizedBox(
          height: screenHeight * 0.47, // ista visina kao PageView
          child: Center(
            child: Text(
              widget.searchQuery != null &&
                      widget.searchQuery!.isNotEmpty
                  ? 'Nema ponuda za pojam "${widget.searchQuery}".'
                  : 'Nema dostupnih ponuda.',
              textAlign: TextAlign.center,
              style: TextStyle(
                fontFamily: 'AROneSans',
                fontSize: screenWidth * 0.045,
                fontWeight: FontWeight.w600,
                color: Colors.black54,
              ),
            ),
          ),
        )
      : SizedBox(
          height: screenHeight * 0.47,
          child: PageView.builder(
            controller: _pageController,
            itemCount: totalPages,
            onPageChanged: _loadPageIfNeeded,
            itemBuilder: (context, pageIndex) {
              if (isLoading && currentPage == pageIndex) {
                return const Center(
                  child: CircularProgressIndicator(
                    color: Color(0xFF67B1E5),
                  ),
                );
              }

              final offers = _pageOffers[pageIndex] ?? [];

              return Column(
                children: offers.map((offer) {
                  final glavnaSlika =
                      offer.offerImages?.firstWhere(
                            (i) => i.isMain,
                            orElse: () => offer.offerImages!.first,
                          ).imageUrl ??
                          "default.jpg";

                  final sporedneSlike =
                      offer.offerImages
                              ?.where((i) => !i.isMain)
                              .map((i) => i.imageUrl ?? "default.jpg")
                              .toList() ??
                          [];

                  return FutureBuilder<List<String>>(
                    future: _ucitajDatume(offer.offerId),
                    builder: (context, snapshot) {
                      return destinationContainers(
                        naziv: offer.title,
                        opis: offer.description,
                        glavnaslikaPath: glavnaSlika,
                        slike: sporedneSlike,
                        cijena:
                            "${offer.minimalPrice.toInt()}\$",
                        detalji: {
                          "brojDana":
                              "${offer.daysInTotal} dana",
                          "nacinPrevoza":
                              offer.wayOfTravel ?? "N/A",
                        },
                        offerId: offer.offerId,
                        datumi: snapshot.data ?? [],
                      );
                    },
                  );
                }).toList(),
              );
            },
          ),
        ),
),


          /// PAGE INFO (SAMO AKO IMA REZULTATA)
          if (!nemaRezultata)
            SliverToBoxAdapter(
              child: Padding(
                padding: const EdgeInsets.only(top: 12),
                child: Center(
                  child: Text(
                    "Stranica ${currentPage + 1} / $totalPages",
                    style: const TextStyle(fontWeight: FontWeight.bold),
                  ),
                ),
              ),
            ),

          SliverToBoxAdapter(child: eTravelFooter()),
        ],
      ),
    );
  }
}
