import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/offer_category.dart';
import 'package:etravel_desktop/models/offer_sub_category.dart';
import 'package:etravel_desktop/models/search_provider.dart';
import 'package:etravel_desktop/providers/category_provider.dart';
import 'package:etravel_desktop/providers/hotel_provider.dart';
import 'package:etravel_desktop/providers/offer_hotel_provider.dart';
import 'package:etravel_desktop/providers/sub_category_provider.dart';
import 'package:etravel_desktop/screens/comments_popup.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models/offer.dart';
import '../providers/offer_provider.dart';

class ReviewScreen extends StatefulWidget {

  const ReviewScreen({super.key});

  @override
  State<ReviewScreen> createState() => _ReviewScreenState();
}

class _ReviewScreenState extends State<ReviewScreen> {
  late OfferProvider _offerProvider;
  late CategoryProvider _categoryProvider;
  late SubCategoryProvider _subCategoryProvider;

  SearchResult<Offer>? offers;
  SearchResult<OfferCategory>? categories;
  SearchResult<OfferSubCategory>? subcategories;

  OfferCategory? selectedCategory;
  OfferSubCategory? selectedSubCategory;

  bool showSubcategoryDropdown = false;
  bool showResults = false;
  bool isEditMode = false;
  String title = "";

  

  @override
  void initState() {
    super.initState();
    _offerProvider = Provider.of<OfferProvider>(context, listen: false);
    _categoryProvider = Provider.of<CategoryProvider>(context, listen: false);
    _subCategoryProvider = Provider.of<SubCategoryProvider>(
      context,
      listen: false,
    );

    _loadCategories();
  }

  // ---------------------------------------------------------------------------
  // LOADERS
  // ---------------------------------------------------------------------------

  Future<void> _loadOffers(dynamic filter) async {
    final result = await _offerProvider.get(filter: filter);
    setState(() => offers = result);
  }

  Future<void> _searchOffers(String query) async {
    if (!showResults) return;
    final result = await _offerProvider.get(
      filter: {
        "fts": query,
        "isMainImage": true,
        "subCategoryId": selectedSubCategory?.id ?? -1,
      },
    );
    setState(() => offers = result);
  }

  Future<void> _loadCategories() async {
    final result = await _categoryProvider.get();
    setState(() => categories = result);
  }

  Future<void> _loadSubCategories(dynamic filter) async {
    final result = await _subCategoryProvider.get(filter: filter);

    setState(() {
      subcategories = result;
      final containsMinusOne = result.items.any((s) => s.id == -1);
      showSubcategoryDropdown = !containsMinusOne && result.items.isNotEmpty;
    });
  }

  void _onCategorySelected(OfferCategory category) {
    setState(() {
      selectedCategory = category;
      selectedSubCategory = null;
    });
    _loadSubCategories({"categoryId": category.id, "RetrieveAll": true});
  }

  // ---------------------------------------------------------------------------
  // BUILD
  // ---------------------------------------------------------------------------

  @override
  Widget build(BuildContext context) {
    return Stack(
      children: [
        Column(
          children: [
            Expanded(
              child: SingleChildScrollView(
                child: Column(
                  children: [
                    _headerImage(),
                    const SizedBox(height: 32),
                    _filterSection(),
                    const SizedBox(height: 32),

                    if (showResults) ...[
                      Text(
                        title,
                        style: const TextStyle(
                          fontSize: 28,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                      const SizedBox(height: 20),
                      _searchBar(),
                      _offerGrid(),
                      const SizedBox(height: 40),
                    ],
                  ],
                ),
              ),
            ),
          ],
        ),
      ],
    );
  }

  // ---------------------------------------------------------------------------
  // HEADER IMAGE
  // ---------------------------------------------------------------------------

  Widget _headerImage() {
    return Container(
      height: 260,
      width: double.infinity,
      decoration: BoxDecoration(
        image: DecorationImage(
          fit: BoxFit.cover,
          image: NetworkImage("${ApiConfig.imagesOffers}/firenca_main.jpg"),
        ),
      ),
      child: Container(
        color: Colors.black.withOpacity(0.35),
        alignment: Alignment.center,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const Text(
              "Recenzije",
              style: TextStyle(
                fontSize: 48,
                color: Colors.white,
                fontWeight: FontWeight.bold,
              ),
            ),
            const SizedBox(height: 10),
            Text(
              "Upravljanje korisničkim komentarima i ocjenama",
              textAlign: TextAlign.center,
              style: TextStyle(color: Colors.white.withOpacity(0.95)),
            ),
          ],
        ),
      ),
    );
  }

  // ---------------------------------------------------------------------------
  // FILTER SECTION
  // ---------------------------------------------------------------------------

  InputDecoration _inputDecoration(String label) {
    return InputDecoration(
      labelText: label,
      filled: true,
      fillColor: Colors.white,
      border: OutlineInputBorder(borderRadius: BorderRadius.circular(12)),
    );
  }

  Widget _filterSection() {
    return Container(
      width: 800,
      padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 28),
      decoration: BoxDecoration(
        color: const Color(0xFFD9D9D9),
        borderRadius: BorderRadius.circular(22),
      ),
      child: Column(
        children: [
          const Text(
            "Filter ponuda",
            style: TextStyle(
              fontSize: 24,
              fontWeight: FontWeight.bold,
              color: Colors.white,
            ),
          ),

          const SizedBox(height: 24),

          Row(
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            children: [
              // CATEGORY
              SizedBox(
                width: 300,
                child: DropdownButtonFormField<OfferCategory>(
                  value: selectedCategory,
                  decoration: _inputDecoration("odaberi kategoriju"),
                  items:
                      (categories?.items ?? [])
                          .map(
                            (c) =>
                                DropdownMenuItem(value: c, child: Text(c.name)),
                          )
                          .toList(),
                  onChanged: (value) {
                    if (value != null) _onCategorySelected(value);
                  },
                ),
              ),

              if (showSubcategoryDropdown)
                SizedBox(
                  width: 300,
                  child: DropdownButtonFormField<OfferSubCategory>(
                    value: selectedSubCategory,
                    decoration: _inputDecoration("odaberite podkategoriju"),
                    items:
                        (subcategories?.items ?? [])
                            .map(
                              (s) => DropdownMenuItem(
                                value: s,
                                child: Text(s.name),
                              ),
                            )
                            .toList(),
                    onChanged:
                        (value) => setState(() => selectedSubCategory = value),
                  ),
                ),
            ],
          ),

          const SizedBox(height: 22),

          SizedBox(
            width: 220,
            height: 45,
            child: ElevatedButton(
              onPressed: () async {
                if (selectedCategory == null) return;

                if (selectedCategory != null && selectedSubCategory == null) {
                  title = "Ponude iz kategorije ${selectedCategory!.name}";
                } else if (selectedSubCategory != null) {
                  title =
                      "Ponude iz podkategorije ${selectedSubCategory!.name}";
                }

                showResults = true;

                await _loadOffers({
                  "isMainImage": true,
                  "subCategoryId": selectedSubCategory?.id ?? -1,
                });
              },
              style: ElevatedButton.styleFrom(
                backgroundColor: const Color(0xFF64B5F6),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(30),
                ),
              ),
              child: const Text(
                "pretražite",
                style: TextStyle(color: Colors.white, fontSize: 16),
              ),
            ),
          ),
        ],
      ),
    );
  }

  // ---------------------------------------------------------------------------
  // SEARCH BAR
  // ---------------------------------------------------------------------------

  Widget _searchBar() {
    return Container(
      margin: const EdgeInsets.only(right: 630),
      child: Padding(
        padding: const EdgeInsets.only(left: 48, right: 48, bottom: 20),
        child: SizedBox(
          width: 320,
          child: TextField(
            onChanged: (value) => _searchOffers(value),
            decoration: InputDecoration(
              hintText: "pretražite destinacije",
              prefixIcon: const Icon(
                Icons.search,
                size: 20,
                color: Colors.black45,
              ),
              filled: true,
              fillColor: const Color(0xfff4eef6),
              contentPadding: const EdgeInsets.symmetric(
                vertical: 14,
                horizontal: 18,
              ),
              border: OutlineInputBorder(
                borderRadius: BorderRadius.circular(28),
                borderSide: BorderSide.none,
              ),
            ),
          ),
        ),
      ),
    );
  }

  // ---------------------------------------------------------------------------
  // GRID
  // ---------------------------------------------------------------------------

  Widget _offerGrid() {
    if (!showResults) return const SizedBox();

    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 48),
      child: Wrap(
        spacing: 20,
        runSpacing: 20,
        children:
            (offers?.items ?? []).map((o) {
              final mainImage =
                  (o.offerImages.isNotEmpty &&
                          o.offerImages.first.imageUrl.isNotEmpty)
                      ? "${ApiConfig.imagesOffers}/${o.offerImages.first.imageUrl}"
                      : "assets/images/placeholder.jpg";

              return _reviewCard(
                o.title.toUpperCase(),
                mainImage,
                o.offerId,
                o.subCategoryId,
              );
            }).toList(),
      ),
    );
  }

  // ---------------------------------------------------------------------------
  // REVIEW CARD (bez dodavanja + bez delete)
  // ---------------------------------------------------------------------------

  Widget _reviewCard(
    String title,
    String imageUrl,
    int offerId,
    int? subCategory,
  ) {
    final isLocalImage = imageUrl.startsWith("assets/");

    return Container(
      width: 220,
      height: 260,
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(16),
        image: DecorationImage(
          fit: BoxFit.cover,
          image:
              isLocalImage
                  ? AssetImage(imageUrl)
                  : NetworkImage(imageUrl) as ImageProvider,
        ),
      ),
      child: ClipRRect(
        borderRadius: BorderRadius.circular(16),
        child: Container(
          decoration: BoxDecoration(
            gradient: LinearGradient(
              begin: Alignment.topCenter,
              end: Alignment.bottomCenter,
              colors: [
                Colors.white.withOpacity(0.05),
                Colors.black.withOpacity(0.55),
              ],
            ),
          ),
          child: Column(
            children: [
              Expanded(
                child: Align(
                  alignment: Alignment.bottomCenter,
                  child: Padding(
                    padding: const EdgeInsets.only(bottom: 18),
                    child: Text(
                      title,
                      style: const TextStyle(
                        fontSize: 20,
                        color: Colors.white,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),
                ),
              ),

              // -----------------------------------------------------------------
              // ONLY VIEW + EDIT
              // -----------------------------------------------------------------
              Container(
                color: Colors.white,
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  children: [
                    IconButton(
                      icon: const Icon(Icons.visibility_outlined),
                      onPressed: () async {
                        await showDialog(
                          context: context,
                          barrierDismissible: false,
                          builder:
                              (_) => Dialog(
                                insetPadding: EdgeInsets.zero,
                                backgroundColor: Colors.transparent,
                                child: ReviewPopup(offerId: offerId),
                              ),
                        );
                      },
                    ),

                    IconButton(
  icon: const Icon(Icons.edit_outlined),
  onPressed: () async {
    await showDialog(
      context: context,
      barrierDismissible: false,
      builder: (_) => Dialog(
        insetPadding: EdgeInsets.zero,
        backgroundColor: Colors.transparent,
        child: ReviewPopup(
          offerId: offerId,
          openInEditMode: true,  // 🔥 OVDJE SE OTVARA ODMAH U EDIT MODU
        ),
      ),
    );
  },
),


                  ],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
