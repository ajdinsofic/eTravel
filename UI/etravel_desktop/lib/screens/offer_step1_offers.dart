import 'dart:io';
import 'package:dotted_border/dotted_border.dart';
import 'package:etravel_desktop/config/api_config.dart';
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
// WIDGET
// ==========================================================
class OfferStep1Offers extends StatefulWidget {
  final int? selectedSubCategoryId;
  final Function(int? offerId, int daysCount) onStepComplete;
  final Function(bool changed) onChanged;
  final int? existingOfferId;
  final int? offerId;
  final bool isReadOnly;
  final bool isViewOrEditButton;

  const OfferStep1Offers({
    super.key,
    required this.onStepComplete,
    required this.onChanged,
    required this.existingOfferId,
    required this.selectedSubCategoryId,
    this.offerId,
    required this.isReadOnly,
    required this.isViewOrEditButton,
  });

  @override
  State<OfferStep1Offers> createState() => _OfferStep1BasicInfoState();
}

class _OfferStep1BasicInfoState extends State<OfferStep1Offers> {
  late OfferProvider _offerProvider;
  late OfferImageProvider _offerImageProvider;

  SearchResult<OfferImage>? offerImage;
  final picker = ImagePicker();

  final List<OfferImageDisplay> images = [];
  final List<OfferImageInsertRequest> imagesForInsert = [];
  final List<OfferImageUpdateRequest> imagesForUpdate = [];

  //Dugme Azuriraj na sekciji "Uredite"
  bool hasChanges = false;

  // Radi lakseg baratanja sa slikama
  int selectedPreviewIndex = 0;

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
  final wayoftravelController = TextEditingController();

  String selectedWayOfTravel = "AVION";
  Map<String, String> errors = {};
  bool _showOverlay = false;

  late int currentOfferId;

  // ==========================================================
  // INIT
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

  @override
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
    wayoftravelController.dispose();
    super.dispose();
  }

  String resolveImageUrl(String? imageUrl) {
  if (imageUrl == null || imageUrl.isEmpty) {
    return "default.jpg";
  }

  // Ako je već full URL (https)
  if (imageUrl.startsWith("http")) {
    return imageUrl;
  }

  // Lokalna slika sa servera
  return "${ApiConfig.imagesOffers}/$imageUrl";
}


  void markAsChanged() {
    if (widget.isViewOrEditButton) {
      if (!hasChanges) {
        setState(() => hasChanges = true);
        widget.onChanged(true); // 🔥 javimo wizardu
      }
    }
  }

  void _showSuccessToast() {
    final overlay = Overlay.of(context);
    if (overlay == null) return;

    late OverlayEntry entry;

    entry = OverlayEntry(
      builder:
          (context) => Positioned(
            bottom: 20,
            right: 20,
            child: Material(
              color: Colors.transparent,
              child: AnimatedOpacity(
                opacity: 1,
                duration: const Duration(milliseconds: 300),
                child: Container(
                  padding: const EdgeInsets.symmetric(
                    horizontal: 16,
                    vertical: 12,
                  ),
                  decoration: BoxDecoration(
                    color: Colors.green.shade600,
                    borderRadius: BorderRadius.circular(10),
                  ),
                  child: const Text(
                    "✓ Uspješno ažurirano",
                    style: TextStyle(color: Colors.white, fontSize: 16),
                  ),
                ),
              ),
            ),
          ),
    );

    overlay.insert(entry);

    // Auto-remove after 3 seconds
    Future.delayed(const Duration(seconds: 3), () {
      entry.remove();
    });
  }

  // ==========================================================
  // LOAD EXISTING OFFER
  // ==========================================================
  Future<void> _loadExistingOffer(int offerId) async {
    try {
      final existingOffer = await _offerProvider.getById(offerId);
      final existingImages = await _offerImageProvider.get(
        filter: {"offerId": offerId},
      );

      setState(() {
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
        wayoftravelController.text = existingOffer.wayOfTravel;

        // Za sta je vec selektovano
        selectedWayOfTravel = existingOffer.wayOfTravel;

        images.clear();
        imagesForInsert.clear();
        imagesForUpdate.clear();

        for (var img in existingImages.items) {
          imagesForUpdate.add(
            OfferImageUpdateRequest(
              id: img.id,
              offerId: img.offerId,
              isMain: img.isMain,
              isUpdated: false,
            ),
          );

          images.add(
            OfferImageDisplay(
              id: img.id,
              isMain: img.isMain,
              path: resolveImageUrl(img.imageUrl),
              isNetwork: true,
            ),
          );
        }

        // osiguraj main image
        if (images.isNotEmpty && !images.any((x) => x.isMain)) {
          images.first.isMain = true;
        }

        // 🔥 automatski selektuj main sliku u thumbnail listi
        final int mainIndex = images.indexWhere((x) => x.isMain);
        selectedPreviewIndex = mainIndex >= 0 ? mainIndex : 0;

        Future.microtask(() {
          if (mounted) setState(() {});
        });
      });
    } catch (e) {
      print("❌ Greška pri učitavanju ponude: $e");
    }
  }

  Future<List<OfferImage>> uploadAllImages(int offerId) async {
    List<OfferImage> uploaded = [];

    // --------------------------
    // 1) INSERT nove slike
    // --------------------------
    for (var img in imagesForInsert) {
      img.offerId = offerId;
      var result = await _offerImageProvider.insertImage(img);
      uploaded.add(result);
    }

    // --------------------------
    // 2) UPDATE postojeće slike (samo ako isUpdated == true)
    // --------------------------
    for (var img in imagesForUpdate) {
      if (img.isUpdated == true) {
        var result = await _offerImageProvider.updateImage(
          img.id!,
          OfferImageUpdateRequest(
            id: img.id,
            offerId: img.offerId,
            isMain: img.isMain,
          ),
        );

        img.isUpdated = false; // reset, jer je update završen
      }
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
          path: resolveImageUrl(img.imageUrl),
          isMain: img.isMain,
          isNetwork: true,
        ),
      );

      imagesForUpdate.add(
        OfferImageUpdateRequest(
          id: img.id,
          offerId: img.offerId,
          isMain: img.isMain,
          isUpdated: false,
        ),
      );
    }

    if (!images.any((e) => e.isMain) && images.isNotEmpty) {
      images.first.isMain = true;
    }

    setState(() {});
  }

  // ==========================================================
  // REMOVE IMAGE (DISABLED IN READONLY)
  // ==========================================================
  void _removeImage(int index) async {
    if (widget.isReadOnly) return;

    final img = images[index];

    if (img.id != null && currentOfferId > 0) {
      try {
        await _offerImageProvider.delete(img.id!);
      } catch (_) {}
      imagesForUpdate.removeWhere((e) => e.id == img.id);
    }

    imagesForInsert.removeWhere((e) => e.image.path == img.path);
    images.removeAt(index);

    if (images.isNotEmpty && !images.any((e) => e.isMain)) {
      images.first.isMain = true;
    }

    markAsChanged();
    setState(() {});
  }

  // ==========================================================
  // VALIDACIJA
  // ==========================================================
  bool _validate() {
    errors.clear();

    if (widget.isReadOnly) return true;

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
  // PICK IMAGES (DISABLED IN READONLY)
  // ==========================================================
  Future<void> _pickImages() async {
    if (widget.isReadOnly) return;

    final picked = await picker.pickMultiImage();
    if (picked.isEmpty) return;

    for (var x in picked) {
      final file = File(x.path);

      images.add(
        OfferImageDisplay(path: file.path, isMain: false, isNetwork: false),
      );

      imagesForInsert.add(
        OfferImageInsertRequest(
          offerId: currentOfferId,
          image: file,
          isMain: false,
        ),
      );
    }

    if (!images.any((e) => e.isMain)) {
      images.first.isMain = true;
      imagesForInsert.first.isMain = true;
    }

    setState(() {});
  }

  // ==========================================================
  // SET MAIN IMAGE (DISABLED IN READONLY)
  // ==========================================================
  Future<void> _setMainImage(int index) async {
    if (widget.isReadOnly) return;

    final selected = images[index];

    // ----------------------------------------------------------
    // 1) PRONAĐI I UKLONI STARU GLAVNU SLIKU (ako postoji)
    // ----------------------------------------------------------

    // 🔵 Stare slike (imagesForUpdate)
    for (var u in imagesForUpdate) {
      if (u.isMain) {
        u.isMain = false;
        u.isUpdated = true; // 🔥 backend mora znati da nije više main
      }
    }

    // 🟢 Nove slike (imagesForInsert)
    for (var ins in imagesForInsert) {
      if (ins.isMain) {
        ins.isMain = false; // 🔵 nema isUpdated jer insert ide jednom
      }
    }

    // ----------------------------------------------------------
    // 2) SAD OZNAČI NOVU GLAVNU SLIKU
    // ----------------------------------------------------------

    if (selected.id != null) {
      // 🔵 radi se o staroj slici
      for (var u in imagesForUpdate) {
        if (u.id == selected.id) {
          u.isMain = true;
          u.isUpdated =
              true; // 🔥 važno: backend mora znati da je nova main slika
        }
      }
    } else {
      // 🟢 nova slika
      for (var ins in imagesForInsert) {
        if (ins.image != null && ins.image!.path == selected.path) {
          ins.isMain = true;
        }
      }
    }

    // ----------------------------------------------------------
    // 3) UPDATE DISPLAY LISTE
    // ----------------------------------------------------------
    for (var img in images) {
      img.isMain = false;
    }
    selected.isMain = true;

    // ----------------------------------------------------------
    // 4) PROMIJENI PRIKAZ (thumbnail highlight)
    // ----------------------------------------------------------
    selectedPreviewIndex = index;

    // ----------------------------------------------------------
    // 5) JAVIMO DA IMA PROMJENA
    // ----------------------------------------------------------
    markAsChanged();

    setState(() {});
  }

  // ==========================================================
  // SUBMIT — CREATE OFFER + SAVE IMAGES
  // ==========================================================
  Future<void> _submit(bool? isNext) async {
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

        // univerzalna funkcija koja ce raditi za svaki update bilo prvi put ili da se vracamo
        var uploaded = await uploadAllImages(currentId);

        for (var img in uploaded) {
          imagesForUpdate.add(
            OfferImageUpdateRequest(
              id: img.id,
              offerId: img.offerId,
              isMain: img.isMain,
              isUpdated: false,
            ),
          );

          images.add(
            OfferImageDisplay(
              id: img.id,
              path: resolveImageUrl(img.imageUrl),
              isMain: img.isMain,
              isNetwork: true,
            ),
          );
        }

        imagesForInsert.clear();

        // 1️⃣ Resetujemo state izmjena
        final bool shouldShowToast = hasChanges; // <— ZAPAMTIMO PRVO
        setState(() => hasChanges = false);
        widget.onChanged(false);

        // 2️⃣ Ako je korisnik stvarno mijenjao nešto → prikaži toast
        if (shouldShowToast) {
          _showSuccessToast();
        }

        if(isNext == true){
          widget.onStepComplete(currentId, int.parse(daysController.text));
        }
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
            isUpdated: false,
          ),
        );
      }

      widget.onStepComplete(offer.offerId, int.parse(daysController.text));
      setState(() => hasChanges = false);
      widget.onChanged(false); // 🔥 javimo wizardu
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

          // ==========================================================
          // SUBMIT BUTTONS SECTION (Ažuriraj + Nastavi)
          // ==========================================================
          Padding(
  padding: const EdgeInsets.all(20),
  child: Row(
    mainAxisAlignment: MainAxisAlignment.center,
    children: [

      // ======================================================
      // 1) EDIT MODE — samo ako si došao iz "uredi" dugmeta
      //    isViewOrEditButton = true AND isReadOnly = false
      // ======================================================
      if (widget.isViewOrEditButton && !widget.isReadOnly)
        SizedBox(
          width: 220,
          height: 48,
          child: ElevatedButton(
            onPressed: hasChanges
                ? () async {
                    await _submit(false); // samo azurira
                  }
                : null,
            style: ElevatedButton.styleFrom(
              backgroundColor:
                  hasChanges ? Colors.green : Colors.grey.shade400,
              foregroundColor: Colors.white,
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(12),
              ),
            ),
            child: const Text("Ažuriraj"),
          ),
        ),

      if (widget.isViewOrEditButton && !widget.isReadOnly)
        const SizedBox(width: 12),


      // ======================================================
      // 2) VIEW MODE — samo detalji ekran
      //    isViewOrEditButton = true AND isReadOnly = true
      // ======================================================
      if (widget.isViewOrEditButton && widget.isReadOnly)
        SizedBox(
          width: 220,
          height: 48,
          child: ElevatedButton(
            onPressed: () {
              widget.onStepComplete(
                widget.existingOfferId,
                int.parse(daysController.text),
              );
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


      // ======================================================
      // 3) CREATE MODE — samo kada kreiraš offer
      //    isViewOrEditButton = false
      // ======================================================
      if (!widget.isViewOrEditButton)
        SizedBox(
          width: 220,
          height: 48,
          child: ElevatedButton(
            onPressed: () => _submit(true), // normalni submit za create
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

    ],
  ),
)

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
                    onTap:
                        widget.isReadOnly
                            ? null
                            : () async {
                              await _pickImages();
                              markAsChanged();
                            },
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

    final mainImage = images[selectedPreviewIndex];

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
            // ======================================================
            // MAIN IMAGE LEFT
            // ======================================================
            Expanded(
              flex: 2,
              child: MouseRegion(
                onEnter:
                    widget.isReadOnly
                        ? null
                        : (_) => setState(() => _showOverlay = true),
                onExit:
                    widget.isReadOnly
                        ? null
                        : (_) => setState(() => _showOverlay = false),
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

                    // ======================================================
                    // MAIN IMAGE OVERLAY (ADD IMAGE BUTTON)
                    // ======================================================
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
                          child: Column(
                            mainAxisSize: MainAxisSize.min,
                            children: [
                              // 🔵 OZNAČI KAO GLAVNU
                              ElevatedButton.icon(
                                onPressed:
                                    widget.isReadOnly
                                        ? null
                                        : () async {
                                          await _setMainImage(
                                            images.indexOf(mainImage),
                                          );
                                          markAsChanged();
                                        },
                                icon: const Icon(Icons.star),
                                label: const Text(
                                  "Označi kao glavnu sliku",
                                  style: TextStyle(color: Colors.black),
                                ),
                                style: ElevatedButton.styleFrom(
                                  backgroundColor: Colors.white,
                                  padding: const EdgeInsets.symmetric(
                                    horizontal: 22,
                                    vertical: 16,
                                  ),
                                  shape: RoundedRectangleBorder(
                                    borderRadius: BorderRadius.circular(30),
                                  ),
                                ),
                              ),

                              const SizedBox(height: 15),

                              // 🟦 DODAJ SLIKU
                              ElevatedButton.icon(
                                onPressed:
                                    widget.isReadOnly
                                        ? null
                                        : () async {
                                          await _pickImages();
                                          markAsChanged();
                                        },
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
                            ],
                          ),
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ),

            const SizedBox(width: 30),

            // RIGHT LIST OF IMAGES
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

                  // sortiranje unutar anonimne funkcije
                  ...(() {
                    final sortedImages = [...images];
                    sortedImages.sort((a, b) {
                      if (a.isMain && !b.isMain) return -1;
                      if (!a.isMain && b.isMain) return 1;
                      return 0;
                    });
                    return sortedImages.map((img) => _imageItem(img)).toList();
                  })(),
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

    return GestureDetector(
      onTap: () {
        // Klik na thumbnail → samo promijeni preview
        setState(() {
          selectedPreviewIndex = index;
        });
      },
      child: Container(
        margin: const EdgeInsets.only(bottom: 10),
        padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 7),
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(12),

          // ⭐ Thumbnail koji je trenutno izabran za prikaz (preview)
          border: Border.all(
            color:
                selectedPreviewIndex == index
                    ? const Color(0xFF64B5F6)
                    : Colors.grey.shade300,
            width: selectedPreviewIndex == index ? 2 : 1,
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

            if (!widget.isReadOnly)
              GestureDetector(
                onTap: () {
                  _removeImage(index);
                  markAsChanged();
                },
                child: Container(
                  padding: const EdgeInsets.symmetric(
                    horizontal: 10,
                    vertical: 5,
                  ),
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
      ),
    );
  }

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
              // ======================================================
              // LEFT SIDE
              // ======================================================
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    _labelField("Naslov", "title"),
                    _inputField(
                      titleController,
                      onChanged: (_) => markAsChanged(),
                    ),
                    const SizedBox(height: 5),

                    _labelField("Minimalna cijena", "minimal"),
                    _inputField(
                      minimalPriceController,
                      onChanged: (_) => markAsChanged(),
                    ),
                    const SizedBox(height: 5),

                    _labelField("Država", "country"),
                    _inputField(
                      countryController,
                      onChanged: (_) => markAsChanged(),
                    ),
                    const SizedBox(height: 5),

                    _labelField("Grad", "city"),
                    _inputField(
                      cityController,
                      onChanged: (_) => markAsChanged(),
                    ),
                    const SizedBox(height: 5),

                    _labelField("Iznos ukupnog osiguranja", "insurance"),
                    _inputField(
                      insuranceController,
                      onChanged: (_) => markAsChanged(),
                    ),
                  ],
                ),
              ),

              const SizedBox(width: 50),

              // ======================================================
              // RIGHT SIDE
              // ======================================================
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
                        enabled: !widget.isReadOnly,
                        onChanged: (_) => markAsChanged(),
                        decoration: _decoration("unesite opis"),
                      ),
                    ),
                    const SizedBox(height: 10),

                    _label("Način putovanja"),
                    widget.isReadOnly
                        ? TextField(
                          controller: wayoftravelController,
                          readOnly: true,
                          decoration: InputDecoration(
                            labelText: "Tip ponude",
                            filled: true,
                            fillColor: Colors.grey.shade200,
                            border: OutlineInputBorder(
                              borderRadius: BorderRadius.circular(10),
                            ),
                          ),
                        )
                        : DropdownButtonFormField<String>(
                          value:
                              ["AVION", "AUTOBUS"].contains(selectedWayOfTravel)
                                  ? selectedWayOfTravel
                                  : null,
                          items: const [
                            DropdownMenuItem(
                              value: "AVION",
                              child: Text("Avion"),
                            ),
                            DropdownMenuItem(
                              value: "AUTOBUS",
                              child: Text("Autobus"),
                            ),
                          ],
                          onChanged: (v) {
                            if (v == null) return;
                            setState(() {
                              selectedWayOfTravel = v;
                              markAsChanged();
                            });
                          },
                          decoration: _decoration("odaberite način"),
                        ),

                    const SizedBox(height: 10),

                    _labelField("Ukupan broj dana", "days"),
                    _inputField(
                      daysController,
                      onChanged: (_) {
                        _recalculateResidenceTotal();
                        markAsChanged();
                      },
                    ),

                    const SizedBox(height: 10),

                    _labelField("Boravišna taksa po danu", "residenceTax"),
                    _inputField(
                      residenceTaxController,
                      onChanged: (_) {
                        _recalculateResidenceTotal();
                        markAsChanged();
                      },
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
        enabled: !widget.isReadOnly,
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
