import 'dart:io';
import 'package:dotted_border/dotted_border.dart';
import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/offer.dart';
import 'package:etravel_desktop/models/offer_image.dart';
import 'package:etravel_desktop/models/offer_image_display.dart';
import 'package:etravel_desktop/models/offer_image_insert.dart';
import 'package:etravel_desktop/models/offer_image_update.dart';
import 'package:etravel_desktop/models/search_provider.dart';
import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

import '../providers/offer_provider.dart';
import '../providers/offer_image_provider.dart';

// ==========================================================
// MODEL
// ==========================================================

// ==========================================================
// WIDGET
// ==========================================================
class OfferStep1Offers extends StatefulWidget {
  final int? selectedSubCategoryId;
  final Function(int offerId, int daysCount) onStepComplete;
  final int? existingOfferId;
  final int? offerId; // <-- DODANO

  const OfferStep1Offers({
    super.key,
    required this.onStepComplete,
    required this.existingOfferId,
    required this.selectedSubCategoryId,
    this.offerId,
  });
  @override
  State<OfferStep1Offers> createState() => _OfferStep1BasicInfoState();
}

class _OfferStep1BasicInfoState extends State<OfferStep1Offers> {
  late OfferProvider _offerProvider;
  late OfferImageProvider _offerImageProvider;

  Offer? offer;
  SearchResult<OfferImage>? offerImage;

  final picker = ImagePicker();

  final List<OfferImageDisplay> images = []; // UI prikaz
  final List<OfferImageInsertRequest> imagesForInsert = []; // nove slike
  final List<OfferImageUpdateRequest> imagesForUpdate = []; // server slike

  // CONTROLLERS
  final titleController = TextEditingController();
  final priceController = TextEditingController();
  final countryController = TextEditingController();
  final cityController = TextEditingController();
  final descriptionController = TextEditingController();
  final minimalPriceController = TextEditingController();
  final residenceTaxController = TextEditingController();
  final insuranceController = TextEditingController();
  final residenceTotalController = TextEditingController();
  final daysController = TextEditingController();

  String selectedWayOfTravel = "AVION";
  Map<String, String> errors = {};
  bool _showOverlay = false;
  late int currentOfferId;

  // ==========================================================
  // INIT + DISPOSE (za automatski izračun residenceTotal)
  // ==========================================================
  @override
  void initState() {
    super.initState();
    daysController.addListener(_recalculateResidenceTotal);
    residenceTaxController.addListener(_recalculateResidenceTotal);
    _offerProvider = Provider.of<OfferProvider>(context, listen: false);
    _offerImageProvider = Provider.of<OfferImageProvider>(
      context,
      listen: false,
    );
    currentOfferId = widget.offerId ?? widget.existingOfferId ?? 0;

    if (widget.existingOfferId != null) {
      _loadExistingOffer(widget.existingOfferId!);
    }
  }

  void dispose() {
    daysController.removeListener(_recalculateResidenceTotal);
    residenceTaxController.removeListener(_recalculateResidenceTotal);
    titleController.dispose();
    priceController.dispose();
    countryController.dispose();
    cityController.dispose();
    descriptionController.dispose();
    minimalPriceController.dispose();
    residenceTaxController.dispose();
    insuranceController.dispose();
    residenceTotalController.dispose();
    daysController.dispose();

    super.dispose();
  }

  Future<void> _loadExistingOffer(int offerId) async {
    try {
      // 1) UČITAJ OFFER
      final existingOffer = await _offerProvider.getById(offerId);

      // 2) UČITAJ SLIKE
      final existingImages = await _offerImageProvider.get(
        filter: {"offerId": offerId},
      );

      setState(() {
        // ================================
        // POPUNI TEXTUALNA POLJA
        // ================================
        titleController.text = existingOffer.title;
        daysController.text = existingOffer.daysInTotal.toString();
        selectedWayOfTravel = existingOffer.wayOfTravel;
        minimalPriceController.text = existingOffer.minimalPrice.toString();
        insuranceController.text =
        existingOffer.travelInsuranceTotal.toString();
        residenceTaxController.text =
        existingOffer.residenceTaxPerDay.toString();
        residenceTotalController.text = existingOffer.residenceTotal.toString();
        descriptionController.text = existingOffer.description;
        countryController.text = existingOffer.country;
        cityController.text = existingOffer.city;

        // ================================
        // PRIPREMI 3 LISTE ZA SLIKE
        // ================================
        images.clear();
        imagesForInsert.clear();
        imagesForUpdate.clear();

        for (var img in existingImages.items) {
          final updateModel = OfferImageUpdateRequest(
            id: img.id,
            offerId: img.offerId,
            isMain: img.isMain,
          );

          imagesForUpdate.add(updateModel);

          images.add(
            OfferImageDisplay(
              id: img.id,
              isMain: img.isMain,
              path: "${ApiConfig.imagesOffers}/${img.imageUrl}",
              isNetwork: true,
            ),
          );
        }

        if (images.isNotEmpty && !images.any((x) => x.isMain)) {
          images.first.isMain = true;
        }
      });
    } catch (e) {
      print("❌ Greška pri loadanju offera: $e");
    }
  }

  Future<List<OfferImage>> uploadAllImages(int offerId) async {
    List<OfferImage> uploaded = [];

    for (var img in imagesForInsert) {
      img.offerId = offerId;
      var result = await _offerImageProvider.insertImage(img);
      uploaded.add(result);
    }

    return uploaded;
  }

  Future<void> loadExistingImages(List<OfferImage> serverImages) async {
    images.clear();
    imagesForUpdate.clear();

    for (var img in serverImages) {
      images.add(
        OfferImageDisplay(
          id: img.id,
          path: "${ApiConfig.imagesOffers}/${img.imageUrl}",
          isMain: img.isMain,
          isNetwork: true,
        ),
      );

      imagesForUpdate.add(
        OfferImageUpdateRequest(
          id: img.id,
          offerId: img.offerId,
          isMain: img.isMain,
        ),
      );
    }

    if (!images.any((e) => e.isMain) && images.isNotEmpty) {
      images.first.isMain = true;
    }

    setState(() {});
  }

  void _removeImage(int index) async {
    final img = images[index];

    // DELETE MODE
    if (img.id != null && currentOfferId > 0) {
      try {
        await _offerImageProvider.delete(img.id!);
      } catch (_) {}
      imagesForUpdate.removeWhere((e) => e.id == img.id);
    }

    // Ako je INSERT slika
    imagesForInsert.removeWhere((e) => e.image.path == img.path);

    // UI uklanjanje
    images.removeAt(index);

    // Postavi novu main ako je uklonjena
    if (images.isNotEmpty && !images.any((e) => e.isMain)) {
      images.first.isMain = true;
    }

    setState(() {});
  }

  // ==========================================================
  // VALIDACIJA
  // ==========================================================
  bool _validate() {
    errors.clear();

    double? minimal = double.tryParse(minimalPriceController.text);
    double? insurance = double.tryParse(insuranceController.text);
    double? residenceTax = double.tryParse(residenceTaxController.text);
    double? days = double.tryParse(daysController.text);

    if (countryController.text.isEmpty)
      errors["country"] = "Država je obavezna.";
    if (cityController.text.isEmpty) errors["city"] = "Grad je obavezan.";
    if (descriptionController.text.isEmpty)
      errors["description"] = "Opis je obavezan.";

    if (daysController.text.isEmpty) {
      errors["days"] = "Broj dana je obavezan.";
    } else if (days! <= 0) {
      errors["days"] = "Broj dana mora biti veći od 0.";
    }

    if (minimalPriceController.text.isEmpty) {
      errors["minimal"] = "Minimalna cijena je obavezna.";
    } else if (minimal! <= 0) {
      errors["minimal"] = "Minimalna cijena mora biti veća od 0.";
    }

    if (insuranceController.text.isEmpty) {
      errors["insurance"] = "Iznos osiguranja je obavezan.";
    } else if (insurance == null) {
      errors["insurance"] = "Osiguranje mora biti broj.";
    } else if (insurance <= 0) {
      errors["insurance"] = "Osiguranje mora biti veće od 0.";
    }

    if (residenceTaxController.text.isNotEmpty && residenceTax! <= 0) {
      errors["residenceTax"] = "Boravišna taksa mora biti veća od 0.";
    }

    if (imagesForInsert.isEmpty && images.isEmpty) {
      errors["images"] = "Morate dodati barem jednu sliku.";
    }

    setState(() {});
    return errors.isEmpty;
  }

  // ==========================================================
  // PICK IMAGES
  // ==========================================================
  Future<void> _pickImages() async {
    final picked = await picker.pickMultiImage();
    if (picked.isEmpty) return;

    for (var x in picked) {
      final file = File(x.path);

      // UI slika
      images.add(
        OfferImageDisplay(path: file.path, isMain: false, isNetwork: false),
      );

      // Insert slika
      imagesForInsert.add(
        OfferImageInsertRequest(
          offerId: currentOfferId,
          image: file,
          isMain: false,
        ),
      );
    }

    // Ako nije postavljena ni jedna glavna slika
    if (!images.any((e) => e.isMain)) {
      images.first.isMain = true;
      imagesForInsert.first.isMain = true;
    }

    setState(() {});
  }

  Future<void> _setMainImage(int index) async {
    final selected = images[index];

    // 1) Lokalno resetuj sve
    for (var img in images) {
      img.isMain = false;
    }
    selected.isMain = true;

    // 2) UPDATE MODE → update na serveru
    if (currentOfferId > 0) {
      for (var img in imagesForUpdate) {
        await _offerImageProvider.updateImage(
          img.id!,
          OfferImageUpdateRequest(
            offerId: currentOfferId,
            isMain: img.id == selected.id,
          ),
        );
        img.isMain = (img.id == selected.id);
      }
    }

    // 3) CREATE MODE → postavi samo local state
    for (var insert in imagesForInsert) {
      insert.isMain = (insert.image.path == selected.path);
    }

    setState(() {});
  }

  // ==========================================================
  // SUBMIT — CREATE OFFER + SAVE IMAGES
  // ==========================================================
  Future<void> _submit() async {
    if (!_validate()) return;

    try {
      int currentId = widget.existingOfferId ?? 0;

      var request = {
        "title": titleController.text.trim(),
        "daysInTotal": int.tryParse(daysController.text) ?? 0,
        "wayOfTravel": selectedWayOfTravel,
        "subCategoryId": widget.selectedSubCategoryId ?? -1,
        "minimalPrice": double.tryParse(minimalPriceController.text) ?? 0,
        "travelInsuranceTotal": double.tryParse(insuranceController.text) ?? 0,
        "residenceTotal": double.tryParse(residenceTotalController.text) ?? 0,
        "residenceTaxPerDay": double.tryParse(residenceTaxController.text) ?? 0,
        "description": descriptionController.text.trim(),
        "country": countryController.text.trim(),
        "city": cityController.text.trim(),
      }; // tvoja ponuda

      // UPDATE MODE
      if (currentId > 0) {
        await _offerProvider.update(currentId, request);

        var uploaded = await uploadAllImages(currentId);

        // dodaj uploadane slike u update listu
        for (var img in uploaded) {
          imagesForUpdate.add(
            OfferImageUpdateRequest(
              id: img.id,
              offerId: img.offerId,
              isMain: img.isMain,
            ),
          );

          images.add(
            OfferImageDisplay(
              id: img.id,
              path: "${ApiConfig.imagesOffers}/${img.imageUrl}",
              isMain: img.isMain,
              isNetwork: true,
            ),
          );
        }

        imagesForInsert.clear();

        widget.onStepComplete(currentId, int.parse(daysController.text));
        return;
      }

      // CREATE MODE
      var offer = await _offerProvider.insert(request);

      var uploaded = await uploadAllImages(offer.offerId);

      // refresh update list
      imagesForUpdate.clear();
      for (var img in uploaded) {
        imagesForUpdate.add(
          OfferImageUpdateRequest(
            id: img.id,
            offerId: offer.offerId,
            isMain: img.isMain,
          ),
        );
      }

      widget.onStepComplete(offer.offerId, int.parse(daysController.text));
    } catch (e) {
      print("Greška: $e");
    }
  }

  // ==========================================================
  // AUTORACUN BORAVIŠNE TAKSE
  // ==========================================================
  void _recalculateResidenceTotal() {
    final days = int.tryParse(daysController.text) ?? 0;
    final tax = double.tryParse(residenceTaxController.text) ?? 0;

    if (days > 0 && tax > 0) {
      residenceTotalController.text = (days * tax).toStringAsFixed(2);
    } else {
      residenceTotalController.text = "";
    }

    setState(() {});
  }

  // ==========================================================
  // BUILD UI
  // ==========================================================
  @override
  Widget build(BuildContext context) {
    return Material(
      color: Colors.transparent,
      child: Column(
        children: [
          Expanded(
            child: SingleChildScrollView(
              child: Column(
                children: [
                  _imageSection(),
                  const SizedBox(height: 40),
                  _basicInfoSection(),
                ],
              ),
            ),
          ),

          // SUBMIT BUTTON
          Padding(
            padding: const EdgeInsets.all(20),
            child: SizedBox(
              width: 220,
              height: 48,
              child: ElevatedButton(
                onPressed: () {
                  _submit();
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.blueAccent,
                  foregroundColor: Colors.white,
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(12),
                  ),
                ),
                child: const Text("Nastavi"),
              ),
            ),
          ),
        ],
      ),
    );
  }

  // ==========================================================
  // IMAGE SECTION (MAIN + THUMBNAILS)
  // ==========================================================
  Widget _imageSection() {
    if (images.isEmpty) {
      return Column(
        children: [
          DottedBorder(
            color: Colors.grey,
            dashPattern: const [8, 6],
            borderType: BorderType.RRect,
            radius: const Radius.circular(16),
            strokeWidth: 2,
            child: Container(
              width: double.infinity,
              padding: const EdgeInsets.symmetric(vertical: 60),
              color: const Color(0xffF5F5F5),
              child: Column(
                children: [
                  Text(
                    "dodaj sliku ponude",
                    style: GoogleFonts.openSans(
                      fontSize: 32,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  const SizedBox(height: 25),

                  GestureDetector(
                    onTap: _pickImages,
                    child: Container(
                      width: 70,
                      height: 70,
                      decoration: BoxDecoration(
                        shape: BoxShape.circle,
                        color: Colors.white,
                        border: Border.all(color: Colors.grey, width: 2),
                      ),
                      child: const Icon(
                        Icons.add,
                        size: 38,
                        color: Colors.grey,
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ),
          if (errors.containsKey("images"))
            Padding(
              padding: const EdgeInsets.only(top: 8),
              child: Text(
                errors["images"]!,
                style: const TextStyle(color: Colors.red),
              ),
            ),
        ],
      );
    }

    final mainImage = images.firstWhere((x) => x.isMain);

    return DottedBorder(
      dashPattern: const [8, 6],
      color: Colors.grey,
      strokeWidth: 2,
      borderType: BorderType.RRect,
      radius: const Radius.circular(16),
      child: Container(
        padding: const EdgeInsets.all(20),
        color: const Color(0xffF5F5F5),
        child: Row(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // MAIN IMAGE LEFT
            Expanded(
              flex: 2,
              child: MouseRegion(
                onEnter: (_) => setState(() => _showOverlay = true),
                onExit: (_) => setState(() => _showOverlay = false),
                child: Stack(
                  children: [
                    Container(
                      height: 320,
                      decoration: BoxDecoration(
                        borderRadius: BorderRadius.circular(16),
                        image: DecorationImage(
                          fit: BoxFit.cover,
                          image:
                              mainImage.isNetwork
                                  ? NetworkImage(mainImage.path)
                                  : FileImage(File(mainImage.path))
                                      as ImageProvider,
                        ),
                      ),
                    ),

                    AnimatedOpacity(
                      opacity: _showOverlay ? 1 : 0,
                      duration: const Duration(milliseconds: 200),
                      child: Container(
                        height: 320,
                        decoration: BoxDecoration(
                          color: Colors.black.withOpacity(0.45),
                          borderRadius: BorderRadius.circular(16),
                        ),
                        child: Center(
                          child: ElevatedButton.icon(
                            onPressed: _pickImages,
                            icon: const Icon(Icons.add_photo_alternate),
                            label: const Text("Dodaj sliku"),
                            style: ElevatedButton.styleFrom(
                              backgroundColor: const Color(0xFF64B5F6),
                              foregroundColor: Colors.white,
                              padding: const EdgeInsets.symmetric(
                                horizontal: 22,
                                vertical: 16,
                              ),
                              shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(30),
                              ),
                            ),
                          ),
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ),

            const SizedBox(width: 30),

            // RIGHT LIST
            Expanded(
              flex: 1,
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    "Dodane slike",
                    style: GoogleFonts.openSans(
                      fontSize: 19,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  const SizedBox(height: 14),

                  ...images.map(_imageItem).toList(),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget _imageItem(OfferImageDisplay img) {
    final index = images.indexOf(img);
    final fileName = img.path.split(RegExp(r'[\/\\]')).last;

    return Container(
      margin: const EdgeInsets.only(bottom: 10),
      padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 7),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(12),
        border: Border.all(
          color: img.isMain ? const Color(0xFF64B5F6) : Colors.grey.shade300,
          width: img.isMain ? 2 : 1,
        ),
      ),
      child: Row(
        children: [
          const Icon(Icons.image, size: 18),
          const SizedBox(width: 8),

          Expanded(
            child: Text(
              fileName,
              overflow: TextOverflow.ellipsis,
              style: GoogleFonts.openSans(fontSize: 13),
            ),
          ),

          const SizedBox(width: 10),

          GestureDetector(
            onTap: () => _setMainImage(index),
            child: Container(
              padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 3),
              decoration: BoxDecoration(
                color:
                    img.isMain ? const Color(0xFF64B5F6) : Colors.grey.shade300,
                borderRadius: BorderRadius.circular(6),
              ),
              child: Text(
                img.isMain ? "MAIN" : "postavi",
                style: GoogleFonts.openSans(
                  fontSize: 11,
                  color: img.isMain ? Colors.white : Colors.black87,
                ),
              ),
            ),
          ),

          const SizedBox(width: 10),

          GestureDetector(
            onTap: () => _removeImage(index),
            child: Container(
              padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 5),
              decoration: BoxDecoration(
                color: Colors.redAccent,
                borderRadius: BorderRadius.circular(6),
              ),
              child: const Text(
                "Obriši",
                style: TextStyle(
                  color: Colors.white,
                  fontSize: 11,
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }

  // ==========================================================
  // BASIC INFO SECTION
  // ==========================================================
  Widget _basicInfoSection() {
    return Container(
      margin: const EdgeInsets.symmetric(horizontal: 50),
      padding: const EdgeInsets.symmetric(horizontal: 32, vertical: 28),
      decoration: BoxDecoration(
        color: const Color(0xFFF5F5F5),
        borderRadius: BorderRadius.circular(18),
        border: Border.all(color: Colors.grey.shade400),
      ),
      child: Column(
        children: [
          Text(
            "OSNOVNE INFORMACIJE",
            style: GoogleFonts.openSans(
              fontSize: 19,
              fontWeight: FontWeight.bold,
            ),
          ),
          const SizedBox(height: 30),

          Row(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              // LEFT SIDE
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    _labelField("Naslov", "title"),
                    _inputField(titleController),
                    const SizedBox(height: 5),

                    _labelField("Minimalna cijena", "minimal"),
                    _inputField(minimalPriceController),
                    const SizedBox(height: 5),

                    _labelField("Država", "country"),
                    _inputField(countryController),
                    const SizedBox(height: 5),

                    _labelField("Grad", "city"),
                    _inputField(cityController),
                    const SizedBox(height: 5),

                    _labelField("Iznos ukupnog osiguranja", "insurance"),
                    _inputField(insuranceController),
                  ],
                ),
              ),

              const SizedBox(width: 50),

              // RIGHT SIDE
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    _labelField("O destinaciji", "description"),
                    SizedBox(
                      height: 120,
                      child: TextField(
                        controller: descriptionController,
                        maxLines: null,
                        expands: true,
                        decoration: _decoration("unesite opis"),
                      ),
                    ),
                    const SizedBox(height: 10),

                    _label("Način putovanja"),
                    DropdownButtonFormField<String>(
                      value: selectedWayOfTravel,
                      items: const [
                        DropdownMenuItem(value: "AVION", child: Text("Avion")),
                        DropdownMenuItem(
                          value: "AUTOBUS",
                          child: Text("Autobus"),
                        ),
                      ],
                      onChanged: (v) {
                        setState(() => selectedWayOfTravel = v!);
                      },
                      decoration: _decoration("odaberite način"),
                    ),
                    const SizedBox(height: 10),

                    _labelField("Ukupan broj dana", "days"),
                    _inputField(
                      daysController,
                      onChanged: (_) => _recalculateResidenceTotal(),
                    ),

                    const SizedBox(height: 10),

                    _labelField("Boravišna taksa po danu", "residenceTax"),
                    _inputField(
                      residenceTaxController,
                      onChanged: (_) => _recalculateResidenceTotal(),
                    ),

                    const SizedBox(height: 10),

                    _label("Ukupna boravišna taksa"),
                    SizedBox(
                      height: 40,
                      child: TextField(
                        controller: residenceTotalController,
                        readOnly: true,
                        decoration: _decoration("automatski izračun"),
                      ),
                    ),
                  ],
                ),
              ),
            ],
          ),
        ],
      ),
    );
  }

  Widget _labelField(String text, String key) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        _label(text),
        if (errors.containsKey(key))
          Text(errors[key]!, style: const TextStyle(color: Colors.red)),
        const SizedBox(height: 5),
      ],
    );
  }

  Widget _label(String text) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 6),
      child: Text(
        text,
        style: GoogleFonts.openSans(fontSize: 15, fontWeight: FontWeight.bold),
      ),
    );
  }

  Widget _inputField(
    TextEditingController controller, {
    void Function(String)? onChanged,
  }) {
    return SizedBox(
      height: 40,
      child: TextField(
        controller: controller,
        onChanged: onChanged,
        decoration: _decoration(""),
      ),
    );
  }

  InputDecoration _decoration(String hint) {
    return InputDecoration(
      filled: true,
      fillColor: Colors.white,
      hintText: hint,
      contentPadding: const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
      border: OutlineInputBorder(
        borderRadius: BorderRadius.circular(10),
        borderSide: const BorderSide(color: Colors.grey),
      ),
    );
  }
}
