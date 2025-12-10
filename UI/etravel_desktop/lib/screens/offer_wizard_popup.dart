import 'dart:io';

import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/helper/force_exit_wizard.dart';
import 'package:etravel_desktop/models/hotel_form_data.dart';
import 'package:etravel_desktop/models/hotel_image_update.dart';
import 'package:etravel_desktop/providers/hotel_image_provider.dart';
import 'package:etravel_desktop/providers/hotel_provider.dart';
import 'package:etravel_desktop/providers/hotel_room_provider.dart';
import 'package:etravel_desktop/providers/offer_hotel_provider.dart';
import 'package:etravel_desktop/providers/offer_provider.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'offer_step1_offers.dart';
import 'offer_step2_hotels.dart';
import 'offer_step3_offer_plan_days.dart';
import 'package:etravel_desktop/models/hotel.dart';

class OfferWizardPopup extends StatefulWidget {
  final int? existingOfferId;
  final int? selectedSubCategoryId;

  final bool isReadOnly;
  final bool isViewOrEditButton;

  final String? selectedTab;

  const OfferWizardPopup({
    super.key,
    this.existingOfferId,
    required this.isReadOnly,
    required this.selectedSubCategoryId,
    required this.isViewOrEditButton,
    this.selectedTab,
  });

  @override
  State<OfferWizardPopup> createState() => _OfferWizardPopupState();
}

class _OfferWizardPopupState extends State<OfferWizardPopup> {
  int step = 1;
  int? offerId = 0;
  int daysCount = 0;
  late OfferProvider _offerProvider;
  late OfferHotelProvider _offerHotelProvider;
  late HotelProvider _hotelProvider;

  String? selectedTabControl;

  //Radi final isReadOnly ovom cemo kontrolisati ponasanje dugmadi detalji, uredi InputDecoration
  late bool isReadOnlyMode;

  // Radi Ažuriranja ako postoji da se ne moze kliknuti na detalji ili uredi
  bool hasUnsavedChanges = false;

  /// 🔥 Čuvamo hotele kroz sve korake
  List<HotelFormData> savedHotels = [];

  @override
  void initState() {
    super.initState();

    isReadOnlyMode = widget.isReadOnly;
    selectedTabControl = widget.selectedTab;

    _offerProvider = Provider.of<OfferProvider>(context, listen: false);
    _offerHotelProvider = Provider.of<OfferHotelProvider>(
      context,
      listen: false,
    );
    _hotelProvider = Provider.of<HotelProvider>(context, listen: false);

    if (widget.existingOfferId != null) {
      offerId = widget.existingOfferId!;
    }
  }

  Future<bool> _confirmExit(BuildContext context) async {
    return await showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) {
        return AlertDialog(
          title: const Text(
            "Napustiti kreiranje ponude?",
            style: TextStyle(fontWeight: FontWeight.bold),
          ),
          content: const Text(
            "Ako napustite, sav do sada unesen sadržaj će biti izgubljen.\n"
            "Da li ste sigurni da želite prekinuti kreiranje nove ponude?",
          ),
          actions: [
            // ❌ OTKAZI → bijela pozadina, plavi tekst, plavi border
            OutlinedButton(
              onPressed: () => Navigator.pop(context, false),
              style: OutlinedButton.styleFrom(
                backgroundColor: Colors.white,
                foregroundColor: Colors.blue,
                side: const BorderSide(color: Colors.blue, width: 2),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: const Text("Otkaži"),
            ),

            // 🔵 DA, NAPUSTI → plava pozadina, bijeli tekst
            ElevatedButton(
              onPressed: () => Navigator.pop(context, true),
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.blue,
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: const Text("Da, napusti"),
            ),
          ],
        );
      },
    );
  }

  void _showUnsavedOfferWarning() {
  showDialog(
    context: context,
    builder: (context) {
      return AlertDialog(
        title: const Text(
          "Sačuvajte izmjene",
          style: TextStyle(fontWeight: FontWeight.bold),
          textAlign: TextAlign.center,
        ),
        content: const Text(
          "Ne možete mijenjati tab dok imate nesačuvane izmjene.\n"
          "Prvo sačuvajte podatke",
          textAlign: TextAlign.center,
        ),
        actions: [
          ElevatedButton(
            onPressed: () => Navigator.pop(context),
            style: ElevatedButton.styleFrom(
              backgroundColor: Colors.blueAccent,
              foregroundColor: Colors.white,
            ),
            child: const Text("U redu"),
          ),
        ],
      );
    },
  );
}


  Future<bool> _confirmDeleteOffer(BuildContext context) async {
    return await showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) {
        return AlertDialog(
          title: const Text(
            "Izbrisati ponudu?",
            style: TextStyle(fontWeight: FontWeight.bold),
          ),
          content: const Text(
            "Da li ste sigurni da želite obrisati ovu ponudu?\n\n"
            "⚠️ Nakon brisanja, ponudu neće biti moguće vratiti.",
          ),
          actions: [
            // ❌ OTKAZI
            OutlinedButton(
              onPressed: () => Navigator.pop(context, false),
              style: OutlinedButton.styleFrom(
                backgroundColor: Colors.white,
                foregroundColor: Colors.blue,
                side: const BorderSide(color: Colors.blue, width: 2),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: const Text("Otkaži"),
            ),

            // 🗑️ DA, IZBRIŠI
            ElevatedButton(
              onPressed: () => Navigator.pop(context, true),
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.redAccent,
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: const Text("Da, izbriši"),
            ),
          ],
        );
      },
    );
  }

  Future<void> _cleanupOfferData() async {
    if (offerId == 0) {
      debugPrint("Nema kreiranog offera — nema šta brisati.");
      return;
    }

    try {
      await _offerProvider.delete(offerId);
      debugPrint("Offer $offerId obrisan.");
    } catch (e) {
      debugPrint("Greška pri brisanju offera: $e");
    }

    final result = await _offerHotelProvider.get(
      filter: {"offerDetailsId": offerId},
    );

    for (var oh in result.items) {
      final hotelId = oh.hotelId;

      try {
        await _hotelProvider.delete(hotelId);
        debugPrint("Hotel $hotelId obrisan jer je bio vezan za offer $offerId");
      } catch (e) {
        debugPrint("Greška pri brisanju hotela $hotelId: $e");
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Dialog(
      backgroundColor: Colors.transparent,
      child: Center(
        child: Container(
          width: 1000,
          height: 700,
          decoration: BoxDecoration(
            color: Colors.white,
            borderRadius: BorderRadius.circular(20),
          ),

          child: Column(
            children: [
              _buildHeader(),
              Expanded(
                child: Container(
                  color: const Color(0xffD9D9D9),
                  child: Padding(
                    padding: const EdgeInsets.all(25),
                    child: Container(
                      decoration: BoxDecoration(
                        color: const Color(0xffF5F5F5),
                        borderRadius: BorderRadius.circular(20),
                      ),
                      child: _buildStep(),
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildHeader() {
    return Container(
      height: 50,
      padding: const EdgeInsets.symmetric(horizontal: 16),
      decoration: const BoxDecoration(
        color: Color(0xFF64B5F6),
        borderRadius: BorderRadius.only(
          topLeft: Radius.circular(20),
          topRight: Radius.circular(20),
        ),
      ),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          widget.isViewOrEditButton
              ? Row(
                children: [
                  _tabButton("detalji", "Detalji"),
                  _tabDivider(),
                  _tabButton("uredi", "Uredi"),
                  _tabDivider(),
                  _tabButton("izbrisi", "Izbriši"),
                ],
              )
              : Text(
                "Dodaj ponudu — Korak $step od 3",
                style: const TextStyle(
                  color: Colors.white,
                  fontSize: 18,
                  fontWeight: FontWeight.bold,
                ),
              ),

          // DESNO - CLOSE ICON
          GestureDetector(
            onTap: () async {
              if (widget.isViewOrEditButton == true) {
                Navigator.pop(context);
                return;
              } else {}

              final exit = await _confirmExit(context);
              if (!exit) return;

              await _cleanupOfferData();
              if (context.mounted) Navigator.pop(context);
            },
            child: const Icon(Icons.close, color: Colors.white),
          ),
        ],
      ),
    );
  }

  Widget _tabButton(String key, String label) {
  final bool selected = selectedTabControl == key;

  return MouseRegion(
    cursor: SystemMouseCursors.click,
    child: GestureDetector(
      onTap: () async {
        // 1. Ako postoje nesačuvane izmjene → blokiraj i pokaži upozorenje
        if (hasUnsavedChanges) {
          _showUnsavedOfferWarning();
          return;
        }

        // 2. Ako je tab = IZBRISI → delete popup
        if (key == "izbrisi") {
          final confirm = await _confirmDeleteOffer(context);
          if (confirm) {
            await _cleanupOfferData();
            if (context.mounted) Navigator.pop(context);
          }
          return;
        }

        // 3. Normalni tab switch
        setState(() {
          selectedTabControl = key;
          isReadOnlyMode = (key == "detalji");
        });
      },

      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 6.0),
        child: Text(
          label,
          style: TextStyle(
            color: selected ? Colors.black : Colors.white,
            fontSize: 17,
            fontWeight: selected ? FontWeight.bold : FontWeight.w500,
          ),
        ),
      ),
    ),
  );
}


  Widget _tabDivider() {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 6.0),
      child: Text(
        "|",
        style: TextStyle(
          color: Colors.white.withOpacity(0.75),
          fontSize: 17,
          fontWeight: FontWeight.w600,
        ),
      ),
    );
  }

  // ------------------------------------------------------------
  // 🔥 STEP SWITCH
  // ------------------------------------------------------------
  Widget _buildStep() {
    switch (step) {
      case 1:
        return OfferStep1Offers(
          offerId: offerId,
          existingOfferId: offerId,
          selectedSubCategoryId: widget.selectedSubCategoryId ?? -1,
          isReadOnly: isReadOnlyMode,
          isViewOrEditButton: widget.isViewOrEditButton,
          onChanged: (changed) {
            setState(() => hasUnsavedChanges = changed);
          },
          onStepComplete: (int? id, int days) {
            offerId = id;
            daysCount = days;
            setState(() => step = 2);
          },
        );

      case 2:
        return OfferStep2Hotels(
          daysCount: daysCount,
          offerId: offerId,
          isReadOnly: isReadOnlyMode,
          isViewOrEditButton: widget.isViewOrEditButton,
          onChanged: (changed) {
            setState(() => hasUnsavedChanges = changed);
          },

          /// 🟦 VAŽNO: Step2 dobija prethodne hotele (ako ih ima)
          initialHotels: savedHotels,

          /// 🟦 Step2 → Step3
          onNext: (newHotels) {
            savedHotels = newHotels;
            setState(() => step = 3);
          },

          onBack: (updatedHotels) {
            savedHotels = updatedHotels; // spremi izmjene prije povratka
            setState(() => step = 1);
          },
        );

      case 3:
        return OfferStep3OfferPlanDays(
          offerId: offerId,
          daysCount: daysCount,
          hotels: savedHotels,
          isReadOnly: isReadOnlyMode,
          isViewOrEditButton: widget.isViewOrEditButton,
          onChanged: (changed) {
            setState(() => hasUnsavedChanges = changed);
          },
          onBack: (hotelsFromStep3) {
            savedHotels = hotelsFromStep3;

            // 🔥 Popuni originalne vrijednosti za svaki hotel
            for (var h in savedHotels) {
              h.originalName = h.name;
              h.originalAddress = h.address;
              h.originalStars = h.stars;
              h.originalDepartureDate = h.departureDate;
              h.originalReturnDate = h.returnDate;

              // 🔥 Popuni originalne vrijednosti za rooms
              for (var r in h.selectedRooms) {
                r.originalRoomId = r.roomId;
                r.originalRoomsLeft = r.roomsLeft;
                r.isNew = false; // nije nova soba
              }
            }

            // Vrati na step 2
            setState(() => step = 2);
          },

          onFinish: (bool saved) {
            Navigator.pop(context, saved);
          },
        );

      default:
        return const Center(child: Text("Nepoznat korak"));
    }
  }
}
