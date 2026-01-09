import 'dart:io';
import 'package:dotted_border/dotted_border.dart';
import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/helper/date_picker.dart';
import 'package:etravel_desktop/helper/update_helper.dart';
import 'package:etravel_desktop/models/hotel_form_data.dart';
import 'package:etravel_desktop/models/hotel_image_insert.dart';
import 'package:etravel_desktop/models/hotel_room_insert.dart';
import 'package:etravel_desktop/providers/hotel_image_provider.dart';
import 'package:etravel_desktop/providers/hotel_provider.dart';
import 'package:etravel_desktop/providers/hotel_room_provider.dart';
import 'package:etravel_desktop/providers/offer_hotel_provider.dart';
import 'package:etravel_desktop/widgets/hotel_card.dart';

import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';

class OfferStep2Hotels extends StatefulWidget {
  final int daysCount;
  final int? offerId;
  final List<HotelFormData> initialHotels;
  final Function(List<HotelFormData>) onNext;
  final Function(List<HotelFormData>) onBack;

  final bool isReadOnly;
  final bool isViewOrEditButton;

  // 🔥 NEW — wizard callback
  final void Function(bool isEditing)? onChanged;

  const OfferStep2Hotels({
    super.key,
    required this.daysCount,
    required this.offerId,
    required this.initialHotels,
    required this.onNext,
    required this.onBack,
    required this.isReadOnly,
    required this.isViewOrEditButton,
    required this.onChanged,
  });

  @override
  State<OfferStep2Hotels> createState() => _OfferStep2HotelsState();
}

class _OfferStep2HotelsState extends State<OfferStep2Hotels> {
  List<HotelFormData> hotels = [];
  late DatePickerHelper datePicker;

  late HotelProvider _hotel;
  late HotelRoomProvider _hotelRoomProvider;
  late HotelImageProvider _hotelImageProvider;
  late OfferHotelProvider _offerHotelProvider;

  bool hasChanges = false;

  @override
  void initState() {
    super.initState();

    datePicker = DatePickerHelper();

    _hotel = Provider.of<HotelProvider>(context, listen: false);
    _hotelRoomProvider = Provider.of<HotelRoomProvider>(context, listen: false);
    _hotelImageProvider = Provider.of<HotelImageProvider>(context, listen: false);
    _offerHotelProvider = Provider.of<OfferHotelProvider>(context, listen: false);

    hotels = List.from(widget.initialHotels);

    if (widget.isViewOrEditButton) {
      _loadExistingHotels();
    }
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

  void _showSuccessToast() {
  final overlay = Overlay.of(context);
  if (overlay == null) return;

  late OverlayEntry entry;

  entry = OverlayEntry(
    builder: (context) => Positioned(
      bottom: 20,
      right: 20,
      child: Material(
        color: Colors.transparent,
        child: AnimatedOpacity(
          opacity: 1,
          duration: const Duration(milliseconds: 300),
          child: Container(
            padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
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
  Future.delayed(const Duration(seconds: 3), () => entry.remove());
}


  // 🔥 Notify wizard
  void _triggerChanged() {
    if (!hasChanges && widget.isViewOrEditButton) {
      setState(() => hasChanges = true);
    }
    widget.onChanged?.call(true);
  }

  Future<void> _loadExistingHotels() async {
    if (widget.offerId == null) return;

    try {
      final offerHotelsResponse = await _offerHotelProvider.get(
        filter: {"offerDetailsId": widget.offerId},
      );

      Map<int, Map<String, String>> hotelDates = {};

      String formatDate(DateTime? dt) {
        if (dt == null) return "";
        return DateFormat("dd.MM.yyyy").format(dt.toLocal());
      }

      for (var oh in offerHotelsResponse.items) {
        hotelDates[oh.hotelId!] = {
          "departureDate": formatDate(oh.departureDate),
          "returnDate": formatDate(oh.returnDate),
        };
      }

      final hotelsResponse = await _hotel.get(
        filter: {"offerId": widget.offerId},
      );

      final loadedHotels = <HotelFormData>[];

      for (var hotel in hotelsResponse.items) {
        final int hid = hotel.id!;

        final roomsResponse = await _hotelRoomProvider.get(
          filter: {"hotelId": hid, "RetrieveAll": true},
        );

        final selectedRooms = roomsResponse.items.map((r) {
          return HotelRoomInsertRequest(
            hotelId: hid,
            roomId: r.roomId!,
            roomsLeft: r.roomsLeft!,
          );
        }).toList();

        final imagesResponse = await _hotelImageProvider.get(
          filter: {"hotelId": hid},
        );

        final images = imagesResponse.items.map((img) {
          return HotelImageInsertRequest(
            id: img.imageId,
            hotelId: hid,
            imageUrl: resolveImageUrl(img.imageUrl),
            isMain: img.isMain,
          );
        }).toList();

        final dates = hotelDates[hid] ?? {"departureDate": "", "returnDate": ""};

        loadedHotels.add(
          HotelFormData(
            isNew: false,
            hotelId: hid,
            name: hotel.name,
            address: hotel.address,
            stars: hotel.stars,
            departureDate: dates["departureDate"]!,
            returnDate: dates["returnDate"]!,
            selectedRooms: selectedRooms,
            images: images,
          )
            ..originalName = hotel.name
            ..originalAddress = hotel.address
            ..originalStars = hotel.stars
            ..originalDepartureDate = dates["departureDate"]
            ..originalReturnDate = dates["returnDate"],
        );
      }

      setState(() {
        hotels = loadedHotels;
      });
    } catch (e) {
      debugPrint("❌ Error loading hotels: $e");
    }
  }

  Future<bool> _confirmHotelDelete(BuildContext context) async {
    return await showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) {
        return AlertDialog(
          title: const Text("Obrisati hotel?", style: TextStyle(fontWeight: FontWeight.bold)),
          content: const Text(
            "Da li ste sigurni da želite obrisati ovaj hotel?\n"
            "Ova akcija se ne može poništiti.",
          ),
          actions: [
            ElevatedButton(
              onPressed: () => Navigator.pop(context, false),
              style: ElevatedButton.styleFrom(backgroundColor: Colors.red),
              child: const Text("Ne", style: TextStyle(color: Colors.white)),
            ),
            ElevatedButton(
              onPressed: () => Navigator.pop(context, true),
              style: ElevatedButton.styleFrom(backgroundColor: Colors.blue),
              child: const Text("Da", style: TextStyle(color: Colors.white)),
            ),
          ],
        );
      },
    );
  }

  bool validate() {
  bool valid = true;

  for (final hotel in hotels) {
    hotel.nameError = null;
    hotel.addressError = null;
    hotel.starsError = null;
    hotel.dateError = null;
    hotel.roomsError = null;
    hotel.imagesError = null;

    // -------------------
    // IME
    // -------------------
    if (hotel.name.isEmpty) {
      hotel.nameError = "Unesite naziv hotela";
      valid = false;
    }

    // -------------------
    // ADRESA
    // -------------------
    if (hotel.address.isEmpty) {
      hotel.addressError = "Unesite adresu hotela";
      valid = false;
    }

    // -------------------
    // ZVIJEZDE
    // -------------------
    if (hotel.stars < 1 || hotel.stars > 5) {
      hotel.starsError = "Ocjena mora biti 1–5";
      valid = false;
    }

    // -------------------
    // DATUMI — precizne poruke
    // -------------------
    final depEmpty = hotel.departureDate.isEmpty;
    final retEmpty = hotel.returnDate.isEmpty;

    if (depEmpty && retEmpty) {
      hotel.dateError = "Unesite datume polaska i vraćanja";
      valid = false;
    } else if (depEmpty) {
      hotel.dateError = "Unesite datum polaska";
      valid = false;
    } else if (retEmpty) {
      hotel.dateError = "Unesite datum vraćanja";
      valid = false;
    }

    // -------------------
    // SOBE
    // -------------------
    if (hotel.selectedRooms.isEmpty) {
      hotel.roomsError = "Dodajte barem jednu sobu";
      valid = false;
    }

    // -------------------
    // SLIKE — NAPRAVLJENO KAKO TREBA
    // -------------------
    if (hotel.images.isEmpty) {
      hotel.imagesError = "Dodajte barem jednu sliku";
      valid = false;
    } else {
      final hasMain = hotel.images.any((img) => img.isMain);
      if (!hasMain) {
        hotel.imagesError = "Označite glavnu sliku";
        valid = false;
      }
    }
  }

  setState(() {}); 
  return valid;
}



  Future<void> saveAllHotels(int? offerId) async {
    if (widget.isReadOnly) return;

    for (final hotelData in hotels) {
      // === NEW HOTEL
      if (hotelData.isNew) {
        final created = await _hotel.insert({
          "name": hotelData.name,
          "stars": hotelData.stars,
          "address": hotelData.address,
        });

        final hid = created.id;
        hotelData.hotelId = hid;

        for (final r in hotelData.selectedRooms) {
          await _hotelRoomProvider.insert({
            "hotelId": hid,
            "roomId": r.roomId,
            "roomsLeft": r.roomsLeft,
          });
        }

        for (final img in hotelData.images) {
          await _hotelImageProvider.insertHotelImage(
            HotelImageInsertRequest(
              hotelId: hid,
              isMain: img.isMain,
              image: img.image,
            ),
          );
        }

        await _offerHotelProvider.insert({
          "offerId": offerId,
          "hotelId": hid,
          "departureDate": datePicker.toBackendIso(hotelData.departureDate),
          "returnDate": datePicker.toBackendIso(hotelData.returnDate),
        });

        hotelData.isNew = false;
        hotelData.originalName = hotelData.name;
        hotelData.originalAddress = hotelData.address;
        hotelData.originalStars = hotelData.stars;
        hotelData.originalDepartureDate = hotelData.departureDate;
        hotelData.originalReturnDate = hotelData.returnDate;
        continue;
      }

      // === UPDATE HOTEL
      if (hotelData.needsUpdate()) {
        await _hotel.update(hotelData.hotelId!, {
          "name": hotelData.name,
          "address": hotelData.address,
          "stars": hotelData.stars,
        });

        await _offerHotelProvider.updateOfferHotelDates(
          offerId: offerId,
          hotelId: hotelData.hotelId!,
          departureDateIso: datePicker.toBackendIso(hotelData.departureDate),
          returnDateIso: datePicker.toBackendIso(hotelData.returnDate),
        );

        hotelData.originalName = hotelData.name;
        hotelData.originalAddress = hotelData.address;
        hotelData.originalStars = hotelData.stars;
        hotelData.originalDepartureDate = hotelData.departureDate;
        hotelData.originalReturnDate = hotelData.returnDate;
      }

      // === INSERT NEW IMAGES
      for (final img in hotelData.images) {
        if (img.id == null) {
          await _hotelImageProvider.insertHotelImage(
            HotelImageInsertRequest(
              hotelId: hotelData.hotelId!,
              isMain: img.isMain,
              image: img.image,
            ),
          );
        }
      }

      // === ROOM UPDATES
      for (final r in hotelData.selectedRooms) {
        if (r.isNew) {
          await _hotelRoomProvider.insert({
            "hotelId": hotelData.hotelId!,
            "roomId": r.roomId,
            "roomsLeft": r.roomsLeft,
          });
          r.isNew = false;
          continue;
        }

        if (r.roomsLeft != r.originalRoomsLeft) {
          await _hotelRoomProvider.updateRoom(
            hotelId: hotelData.hotelId!,
            roomId: r.roomId,
            roomsLeft: r.roomsLeft,
          );
          r.originalRoomsLeft = r.roomsLeft;
        }
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.all(20),
      child: SingleChildScrollView(
        child: Column(
          children: [
            Text(
              "HOTELI",
              style: GoogleFonts.openSans(
                fontSize: 20,
                fontWeight: FontWeight.bold,
              ),
            ),

            const SizedBox(height: 20),

            // ADD HOTEL BUTTON
            GestureDetector(
              onTap: widget.isReadOnly
                  ? null
                  : () {
                      setState(() {
                        hotels.add(HotelFormData());
                      });
                      _triggerChanged();
                    },
              child: Opacity(
                opacity: widget.isReadOnly ? 0.4 : 1,
                child: Container(
                  decoration: BoxDecoration(
                    color: const Color(0xFFD9D9D9),
                    borderRadius: BorderRadius.circular(12),
                  ),
                  child: Center(
                    child: DottedBorder(
                      dashPattern: const [6, 4],
                      borderType: BorderType.RRect,
                      radius: const Radius.circular(12),
                      color: Colors.black,
                      child: Container(
                        width: double.infinity,
                        padding:
                            const EdgeInsets.symmetric(vertical: 12, horizontal: 40),
                        child: Row(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                            Text(
                              "dodajte hotel",
                              style: GoogleFonts.openSans(
                                fontSize: 22,
                                fontWeight: FontWeight.w700,
                                color: Colors.black,
                              ),
                            ),
                            const SizedBox(width: 6),
                            Container(
                              width: 24,
                              height: 24,
                              decoration: BoxDecoration(
                                shape: BoxShape.circle,
                                color: Colors.white,
                                border: Border.all(color: Colors.black),
                              ),
                              child: const Icon(Icons.add, size: 16),
                            ),
                          ],
                        ),
                      ),
                    ),
                  ),
                ),
              ),
            ),

            const SizedBox(height: 20),

            // HOTEL LIST
            Column(
              children: hotels.map((hotel) {
                return HotelCard(
                  data: hotel,
                  daysCount: widget.daysCount,
                  isReadOnly: widget.isReadOnly,

                  // 🔥 svaki put kad se hotel promijeni
                  onChanged: () => _triggerChanged(),

                  onDelete: () async {
                    final confirm = await _confirmHotelDelete(context);
                    if (!confirm) return;

                    if (hotel.hotelId != null) {
                      await _hotel.delete(hotel.hotelId!);
                    }

                    setState(() => hotels.remove(hotel));
                    _triggerChanged();
                  },
                );
              }).toList(),
            ),

            const SizedBox(height: 20),

            // BUTTONS
            Padding(
              padding: const EdgeInsets.all(20),
              child: Row(
                mainAxisAlignment:
                    (widget.isViewOrEditButton && !widget.isReadOnly)
                        ? MainAxisAlignment.center
                        : MainAxisAlignment.spaceBetween,
                children: [
                  // === EDIT MODE: only AŽURIRAJ ===
                  if (widget.isViewOrEditButton && !widget.isReadOnly) ...[
                    SizedBox(
                      width: 220,
                      height: 48,
                      child: ElevatedButton(
                        onPressed: hasChanges
                            ? () async {
                                if (!validate()) return;

                                await saveAllHotels(widget.offerId);

                                setState(() => hasChanges = false);

                                widget.onChanged?.call(false);

                                _showSuccessToast();
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
                  ],

                  // === CREATE MODE ===
                  if (!widget.isViewOrEditButton) ...[
                    SizedBox(
                      width: 180,
                      height: 48,
                      child: OutlinedButton(
                        onPressed: () => widget.onBack(hotels),
                        style: OutlinedButton.styleFrom(
                          side: const BorderSide(color: Colors.blueAccent, width: 2),
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(12),
                          ),
                        ),
                        child: const Text(
                          "Nazad",
                          style: TextStyle(
                              fontSize: 16, color: Colors.blueAccent),
                        ),
                      ),
                    ),
                    SizedBox(
                      width: 180,
                      height: 48,
                      child: ElevatedButton(
                        onPressed: () async {
                          if (!validate()) return;
                          await saveAllHotels(widget.offerId);
                          widget.onNext(hotels);
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
                  ],

                  // === VIEW MODE ===
                  if (widget.isViewOrEditButton && widget.isReadOnly) ...[
                    SizedBox(
                      width: 180,
                      height: 48,
                      child: OutlinedButton(
                        onPressed: () => widget.onBack(hotels),
                        style: OutlinedButton.styleFrom(
                          side: const BorderSide(color: Colors.blueAccent, width: 2),
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(12),
                          ),
                        ),
                        child: const Text(
                          "Nazad",
                          style: TextStyle(
                              fontSize: 16, color: Colors.blueAccent),
                        ),
                      ),
                    ),
                    SizedBox(
                      width: 180,
                      height: 48,
                      child: ElevatedButton(
                        onPressed: () => widget.onNext(hotels),
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
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
