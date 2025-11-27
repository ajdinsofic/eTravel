import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/offer_category.dart';
import 'package:etravel_desktop/models/offer_sub_category.dart';
import 'package:etravel_desktop/models/search_provider.dart';
import 'package:etravel_desktop/providers/category_provider.dart';
import 'package:etravel_desktop/providers/sub_category_provider.dart';
import 'package:etravel_desktop/screens/offer_wizard_popup.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:dotted_border/dotted_border.dart';

// MODELI & PROVIDER
import '../models/offer.dart';
import '../providers/offer_provider.dart';

class OfferScreen extends StatefulWidget {
  const OfferScreen({super.key});

  @override
  State<OfferScreen> createState() => _OfferScreenState();
}

class _OfferScreenState extends State<OfferScreen> {
  late OfferProvider _offerProvider;
  late CategoryProvider _categoryProvider;
  late SubCategoryProvider _subCategoryProvider;

  dynamic filter;

  SearchResult<Offer>? offers;
  SearchResult<OfferCategory>? categories;
  SearchResult<OfferSubCategory>? subcategories;

  OfferCategory? selectedCategory;
  OfferSubCategory? selectedSubCategory;

  bool showSubcategoryDropdown = false;

  String title = "Dodane ponude";

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
    _loadOffers();
  }

  Future<void> _loadOffers([dynamic filter]) async {
    final result = await _offerProvider.get(filter: filter);

    setState(() {
      offers = result;
    });
  }

  Future<void> _searchOffers(String query, [int? subCategory]) async {
    final result = await _offerProvider.get(
      filter: {"fts": query, "isMainImage": true, "subCategoryId": subCategory},
    );

    setState(() {
      offers = result;
    });
  }

  Future<void> _loadCategories() async {
    final result = await _categoryProvider.get();

    setState(() {
      categories = result;
    });
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
    _loadSubCategories({"categoryId": category?.id, "RetrieveAll": true});
  }

  Future<void> _showSuccessOfferPopup(BuildContext context) async {
    return showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) {
        return Dialog(
          shape: RoundedRectangleBorder(
            side: const BorderSide(color: Colors.blue, width: 3),
            borderRadius: BorderRadius.circular(16),
          ),
          child: Container(
            padding: const EdgeInsets.all(25),
            decoration: BoxDecoration(
              color: Colors.white,
              borderRadius: BorderRadius.circular(16),
            ),
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                const Text(
                  "Uspješno ste kreirali ponudu!",
                  style: TextStyle(
                    fontSize: 22,
                    fontWeight: FontWeight.bold,
                    color: Colors.black,
                  ),
                  textAlign: TextAlign.center,
                ),

                const SizedBox(height: 15),

                const Text(
                  "Vaša nova ponuda je sada dostupna\nu odabranoj kategoriji ili podkategoriji.",
                  style: TextStyle(fontSize: 16, color: Colors.black),
                  textAlign: TextAlign.center,
                ),

                const SizedBox(height: 25),

                SizedBox(
                  width: 120,
                  child: ElevatedButton(
                    onPressed: () => Navigator.pop(context),
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.blue,
                      foregroundColor: Colors.white,
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(10),
                      ),
                      padding: const EdgeInsets.symmetric(
                        vertical: 12,
                        horizontal: 20,
                      ),
                    ),
                    child: const Text("OK"),
                  ),
                ),
              ],
            ),
          ),
        );
      },
    );
  }

  // ============================================================
  // MAIN BUILD
  // ============================================================

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Stack(
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

                      Text(
                        title,
                        style: TextStyle(
                          fontSize: 28,
                          fontWeight: FontWeight.bold,
                        ),
                      ),

                      const SizedBox(height: 20),

                      // 🔍 SEARCH BAR
                      Container(
                        margin: EdgeInsets.only(right: 630),
                        child: Padding(
                          padding: const EdgeInsets.only(
                            left: 48,
                            right: 48,
                            bottom: 20,
                          ),
                          child: SizedBox(
                            width: 320,
                            child: TextField(
                              onChanged: (value) {
                                if (selectedCategory == null &&
                                    selectedSubCategory == null) {
                                  _searchOffers(value);
                                } else if (selectedCategory != null &&
                                    selectedSubCategory == null) {
                                  _searchOffers(value, -1);
                                } else {
                                  _searchOffers(value, selectedSubCategory?.id);
                                }
                              },
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
                      ),

                      _offerGrid(),

                      const SizedBox(height: 40),
                    ],
                  ),
                ),
              ),
            ],
          ),
        ],
      ),
    );
  }

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
        alignment: Alignment.center,
        color: Colors.black.withOpacity(0.35),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const Text(
              "Ponude",
              style: TextStyle(
                fontSize: 48,
                color: Colors.white,
                fontWeight: FontWeight.bold,
              ),
            ),
            const SizedBox(height: 10),
            Text(
              "Sekcija za naše popularne destinacije\nradujemo se jos novim dodanim putovanjima",
              textAlign: TextAlign.center,
              style: TextStyle(
                fontSize: 15,
                color: Colors.white.withOpacity(0.95),
              ),
            ),
          ],
        ),
      ),
    );
  }

  // ============================================================
  // HELPER ZA FORM INPUT
  // ============================================================
  InputDecoration _inputDecoration(String label) {
    return InputDecoration(
      labelText: label,
      labelStyle: TextStyle(fontSize: 15, color: Colors.grey.shade700),
      filled: true,
      fillColor: Colors.white,
      border: OutlineInputBorder(borderRadius: BorderRadius.circular(12)),
    );
  }

  // ============================================================
  // FILTER
  // ============================================================
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
              // CATEGORY DROPDOWN
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

              // SUBCATEGORY DROPDOWN
              if (showSubcategoryDropdown)
                SizedBox(
                  width: 300,
                  child: DropdownButtonFormField<OfferSubCategory>(
                    value:
                        selectedSubCategory, // ← ovo će biti OfferSubCategory?
                    decoration: _inputDecoration("odaberite podkategoriju"),
                    items:
                        (subcategories?.items ?? [])
                            .map(
                              (s) => DropdownMenuItem(
                                value: s, // ← vraća cijeli model, ne samo ID
                                child: Text(s.name),
                              ),
                            )
                            .toList(),
                    onChanged: (value) {
                      setState(() {
                        selectedSubCategory = value;
                      });
                    },
                  ),
                ),
            ],
          ),

          const SizedBox(height: 22),

          SizedBox(
            width: 220,
            height: 45,
            child: ElevatedButton(
              onPressed: () {
                if (selectedCategory != null && selectedSubCategory == null) {
                  title = "Ponude iz kategorije ${selectedCategory?.name}";
                } else if (selectedCategory != null &&
                    selectedSubCategory != null) {
                  title =
                      "Ponude iz podkategorije ${selectedSubCategory?.name}";
                }
                var filter = {"subCategoryId": selectedSubCategory?.id ?? -1};
                _loadOffers(filter);
              },
              style: ElevatedButton.styleFrom(
                backgroundColor: const Color(0xFF64B5F6),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(30),
                ),
              ),
              child: const Text(
                "pretražite",
                style: TextStyle(
                  color: Colors.white,
                  fontSize: 16,
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }

  // ============================================================
  // GRID PONUDA (DINAMIČKO)
  // ============================================================
  Widget _offerGrid() {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 48),
      child: Wrap(
        spacing: 20,
        runSpacing: 20,
        children: [
          _addOfferCard(),

          ...(offers?.items ?? []).map((o) {
            final mainImage =
                (o.offerImages.isNotEmpty &&
                        o.offerImages.first.imageUrl.isNotEmpty)
                    ? "${ApiConfig.imagesOffers}/${o.offerImages.first.imageUrl}"
                    : "assets/images/placeholder.jpg";

            return _offerCard(o.title.toUpperCase(), mainImage);
          }).toList(),
        ],
      ),
    );
  }

  // ============================================================
  // KARTICA -> DODAJ PONUDU
  // ============================================================
  Widget _addOfferCard() {
    return DottedBorder(
      color: Colors.black38,
      strokeWidth: 2,
      dashPattern: const [6, 4],
      borderType: BorderType.RRect,
      radius: const Radius.circular(16),
      child: Container(
        width: 220,
        height: 260,
        padding: const EdgeInsets.all(16),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const Text(
              "dodaj ponudu",
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.w600,
                color: Colors.black87,
              ),
            ),

            const SizedBox(height: 18),

            ElevatedButton(
              onPressed: () async {
                final result = await showDialog(
                  context: context,
                  barrierDismissible: false,
                  builder:
                      (_) => OfferWizardPopup(
                        selectedSubCategoryId: selectedCategory?.id,
                      ),
                );

                // ⭐ Ako je wizard završio uspješno
                if (result == true) {
                  await _showSuccessOfferPopup(context);
                  await _loadOffers(); // refresh liste
                }
              },

              style: ElevatedButton.styleFrom(
                shape: const CircleBorder(),
                backgroundColor: const Color.fromARGB(255, 173, 172, 172),
                foregroundColor: Colors.white,
                padding: const EdgeInsets.all(18),
              ),
              child: const Icon(Icons.add, size: 32),
            ),
            const SizedBox(height: 16),

            const Text(
              "ljudi se raduju novim\nputovanjima",
              textAlign: TextAlign.center,
              style: TextStyle(fontSize: 12, color: Colors.black45),
            ),
          ],
        ),
      ),
    );
  }

  // ============================================================
  // KARTICA -> PONUDA
  // ============================================================
  Widget _offerCard(String title, String imageUrl) {
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
                        letterSpacing: 1.2,
                      ),
                    ),
                  ),
                ),
              ),

              Container(
                width: double.infinity,
                padding: const EdgeInsets.symmetric(vertical: 2, horizontal: 8),
                decoration: const BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.only(
                    bottomLeft: Radius.circular(16),
                    bottomRight: Radius.circular(16),
                  ),
                ),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  children: [
                    IconButton(
                      icon: const Icon(Icons.visibility_outlined, size: 20),
                      onPressed: () {},
                    ),
                    IconButton(
                      icon: const Icon(Icons.edit_outlined, size: 20),
                      onPressed: () {},
                    ),
                    IconButton(
                      icon: const Icon(Icons.delete_outline, size: 20),
                      onPressed: () {},
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
