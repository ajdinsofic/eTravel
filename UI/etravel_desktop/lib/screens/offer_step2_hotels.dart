import 'dart:io';

import 'package:dotted_border/dotted_border.dart';
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
import 'package:provider/provider.dart';

class OfferStep2Hotels extends StatefulWidget {
  final int daysCount;
  final int offerId;
  final List<HotelFormData> initialHotels;
  final Function(List<HotelFormData>) onNext;
  final Function(List<HotelFormData>) onBack;

  const OfferStep2Hotels({
    super.key,
    required this.daysCount,
    required this.offerId,
    required this.initialHotels,
    required this.onNext,
    required this.onBack,
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

  @override
  void initState() {
    super.initState();
    datePicker = DatePickerHelper();
    _hotel = Provider.of<HotelProvider>(context, listen: false);
    _hotelRoomProvider = Provider.of<HotelRoomProvider>(context, listen: false);
    _hotelImageProvider = Provider.of<HotelImageProvider>(
      context,
      listen: false,
    );
    _offerHotelProvider = Provider.of<OfferHotelProvider>(
      context,
      listen: false,
    );

    if (widget.initialHotels != null) {
      hotels = List.from(widget.initialHotels!); // ⭐ Vrati postojeće kartice
    }
  }

  Future<bool> _confirmHotelDelete(BuildContext context) async {
    return await showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) {
        return AlertDialog(
          title: const Text(
            "Obrisati hotel?",
            style: TextStyle(fontWeight: FontWeight.bold),
          ),
          content: const Text(
            "Da li ste sigurni da želite obrisati ovaj hotel? "
            "Ova akcija se ne može poništiti.",
          ),
          actions: [
            // 🔴 NE — crvena pozadina, bijeli tekst
            ElevatedButton(
              onPressed: () => Navigator.pop(context, false),
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.red,
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
                padding: const EdgeInsets.symmetric(
                  horizontal: 22,
                  vertical: 12,
                ),
              ),
              child: const Text("Ne"),
            ),

            const SizedBox(width: 10),

            // 🔵 DA — plava pozadina, bijeli tekst
            ElevatedButton(
              onPressed: () => Navigator.pop(context, true),
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.blue,
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
                padding: const EdgeInsets.symmetric(
                  horizontal: 22,
                  vertical: 12,
                ),
              ),
              child: const Text("Da"),
            ),
          ],
        );
      },
    );
  }

  bool validate() {
    bool isValid = true;

    for (final hotel in hotels) {
      hotel.nameError = null;
      hotel.addressError = null;
      hotel.starsError = null;
      hotel.dateError = null;
      hotel.roomsError = null;
      hotel.imagesError = null;

      if (hotel.name.isEmpty) {
        hotel.nameError = "Unesite naziv hotela";
        isValid = false;
      }

      if (hotel.address.isEmpty) {
        hotel.addressError = "Unesite adresu hotela";
        isValid = false;
      }

      if (hotel.stars < 1 || hotel.stars > 5) {
        hotel.starsError = "Ocjena mora biti 1–5";
        isValid = false;
      }

      if (hotel.departureDate.isEmpty || hotel.returnDate.isEmpty) {
        hotel.dateError = "Odaberite datume";
        isValid = false;
      }

      if (hotel.selectedRooms.isEmpty) {
        hotel.roomsError = "Dodajte barem jedan tip sobe";
        isValid = false;
      }

      if (hotel.images.isEmpty) {
        hotel.imagesError = "Dodajte barem jednu sliku";
        isValid = false;
      }

      final hasMain = hotel.images.any((img) => img.isMain);
      if (!hasMain) {
        hotel.imagesError = "Označite glavnu sliku";
        isValid = false;
      }
    }

    setState(() {}); // da bi UI prikazao crvene poruke
    return isValid;
  }

  Future<void> saveAllHotels(int offerId) async {
    for (final hotelData in hotels) {
      // ==========================================================
      // 1) NOVI HOTEL → INSERT
      // ==========================================================
      if (hotelData.isNew) {
        final createdHotel = await _hotel.insert({
          "name": hotelData.name,
          "stars": hotelData.stars,
          "address": hotelData.address,
        });

        final int hotelId = createdHotel.id;

        hotelData.hotelId = hotelId;

        // Update lokalne sobe i slike
        for (final room in hotelData.selectedRooms) {
          room.hotelId = hotelId;
          room.isNew = false;
        }
        for (final img in hotelData.images) {
          img.hotelId = hotelId;
        }

        // Insert rooms
        for (final room in hotelData.selectedRooms) {
          await _hotelRoomProvider.insert({
            "hotelId": hotelId,
            "roomId": room.roomId,
            "roomsLeft": room.roomsLeft,
          });
        }

        // Insert images
        for (final img in hotelData.images) {
          await _hotelImageProvider.insertHotelImage(
            HotelImageInsertRequest(
              hotelId: hotelId,
              isMain: img.isMain,
              image: img.image,
            ),
          );
        }

        // Insert offer-hotel link
        await _offerHotelProvider.insert({
          "offerId": offerId,
          "hotelId": hotelId,
          "departureDate": datePicker.toBackendIso(hotelData.departureDate),
          "returnDate": datePicker.toBackendIso(hotelData.returnDate),
        });

        // Postavi original values
        hotelData.isNew = false;
        hotelData.originalName = hotelData.name;
        hotelData.originalAddress = hotelData.address;
        hotelData.originalStars = hotelData.stars;
        hotelData.originalDepartureDate = hotelData.departureDate;
        hotelData.originalReturnDate = hotelData.returnDate;

        continue;
      }

      // ==========================================================
      // 2) POSTOJEĆI HOTEL → UPDATE (samo ako treba)
      // ==========================================================
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

        // Refresh original values
        hotelData.originalName = hotelData.name;
        hotelData.originalAddress = hotelData.address;
        hotelData.originalStars = hotelData.stars;
        hotelData.originalDepartureDate = hotelData.departureDate;
        hotelData.originalReturnDate = hotelData.returnDate;
      }

      // ==========================================================
      // 3) IMAGES UPDATE → INSERT new
      // ==========================================================

      // INSERT novih slika (one bez ID-ja)
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

      // ==============================================
      // ROOMS: samo INSERT i UPDATE (DELETE se radi u popupu)
      // ==============================================
      for (final room in hotelData.selectedRooms) {
        if (room.isNew) {
          await _hotelRoomProvider.insert({
            "hotelId": hotelData.hotelId!,
            "roomId": room.roomId,
            "roomsLeft": room.roomsLeft,
          });

          room.isNew = false;
          room.originalRoomId = room.roomId;
          room.originalRoomsLeft = room.roomsLeft;
          continue;
        }

        // UPDATE ako se promijenio roomsLeft
        if (room.roomsLeft != room.originalRoomsLeft) {
          await _hotelRoomProvider.updateRoom(
            hotelId: hotelData.hotelId!,
            roomId: room.roomId,
            roomsLeft: room.roomsLeft,
          );

          room.originalRoomsLeft = room.roomsLeft;
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
            // TITLE
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
              onTap: () {
                setState(() {
                  hotels.add(HotelFormData());
                });
              },
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
                      padding: const EdgeInsets.symmetric(
                        vertical: 12,
                        horizontal: 40,
                      ),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          Text(
                            "dodajte hotel",
                            style: GoogleFonts.openSans(
                              fontSize: 22,
                              fontWeight: FontWeight.w700,
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
                            child: const Center(
                              child: Icon(
                                Icons.add,
                                size: 16,
                                color: Colors.black,
                              ),
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                ),
              ),
            ),

            const SizedBox(height: 20),

            // FULL LIST OF ADDED HOTELS
            Column(
              children:
                  hotels.map((hotel) {
                    return HotelCard(
                      data: hotel,
                      daysCount: widget.daysCount,

                      onDelete: () async {
                        final confirm = await _confirmHotelDelete(context);
                        if (!confirm) return;

                        // Brisanje iz baze ako postoji
                        if (hotel.hotelId != null) {
                          await _hotel.delete(hotel.hotelId!);
                        }

                        // Ukloni iz UI liste
                        setState(() {
                          hotels.remove(hotel);
                        });
                      },
                    );
                  }).toList(),
            ),

            const SizedBox(height: 20),

            // NAVIGATION BUTTONS
            Padding(
              padding: const EdgeInsets.all(20),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  // BACK BUTTON
                  SizedBox(
                    width: 180,
                    height: 48,
                    child: OutlinedButton(
                      onPressed: () {
                        widget.onBack(hotels);
                      },
                      style: OutlinedButton.styleFrom(
                        side: const BorderSide(
                          color: Colors.blueAccent,
                          width: 2,
                        ),
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(12),
                        ),
                      ),
                      child: const Text(
                        "Nazad",
                        style: TextStyle(
                          fontSize: 16,
                          color: Colors.blueAccent,
                        ),
                      ),
                    ),
                  ),

                  // NEXT BUTTON
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
                      child: const Text(
                        "Nastavi",
                        style: TextStyle(fontSize: 16),
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
