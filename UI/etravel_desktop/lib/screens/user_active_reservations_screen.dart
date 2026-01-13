import 'package:etravel_desktop/helper/image_helper.dart';
import 'package:etravel_desktop/models/reservations.dart';
import 'package:etravel_desktop/models/user_active_reservations.dart';
import 'package:etravel_desktop/providers/hotel_room_provider.dart';
import 'package:etravel_desktop/providers/user_token_provider.dart';
import 'package:etravel_desktop/screens/reservation_popup.dart';
import 'package:etravel_desktop/utils/session.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

import '../models/user.dart';
import '../models/offer.dart';
import '../providers/reservation_provider.dart';
import '../providers/offer_provider.dart';
import '../config/api_config.dart';

class UserActiveReservationsScreen extends StatefulWidget {
  final User user;

  const UserActiveReservationsScreen({super.key, required this.user});

  @override
  State<UserActiveReservationsScreen> createState() =>
      _UserActiveReservationsScreenState();
}

class _UserActiveReservationsScreenState
    extends State<UserActiveReservationsScreen> {
  late ReservationProvider _reservationProvider;
  late OfferProvider _offerProvider;
  late HotelRoomProvider _hotelRoomProvider;
  late UserTokenProvider _userTokenProvider;

  bool isLoading = true;

  List<ActiveReservationDisplay> activeReservationsDisplay = [];

  @override
  void initState() {
    super.initState();

    _offerProvider = Provider.of<OfferProvider>(context, listen: false);
    _reservationProvider = Provider.of<ReservationProvider>(context, listen: false);
    _hotelRoomProvider = Provider.of<HotelRoomProvider>(context, listen: false);
    _userTokenProvider = Provider.of<UserTokenProvider>(context, listen: false);

    _loadData();
  }

  Future<void> _loadData() async {
    try {
      // 1) Učitaj rezervacije
      final reservationsResult = await _reservationProvider.get(
        filter: {
          "userId": widget.user.id,
          "isActive": true,
        },
      );

      List<ActiveReservationDisplay> tempList = [];

      // 2) Za svaku rezervaciju povuci naslov i sliku iz OfferProvider
      for (Reservation res in reservationsResult.items) {
        final offerResult = await _offerProvider.get(
          filter: {
            "offerId": res.offerId,
            "isMainImage": true,
          },
        );

        String title = "Nepoznato";
        String imageUrl = "";

        if (offerResult.items.isNotEmpty) {
          Offer offer = offerResult.items.first;
          title = offer.title ?? "Nepoznato";

          if (offer.offerImages.isNotEmpty) {
            imageUrl = offer.offerImages.first.imageUrl ?? "";
          }
        }

        tempList.add(
          ActiveReservationDisplay(
            reservation: res,
            title: title,
            imageUrl: imageUrl,
          ),
        );
      }

      setState(() {
        activeReservationsDisplay = tempList;
        isLoading = false;
      });
    } catch (e) {
      debugPrint("Greška pri dohvatu rezervacija: $e");
      setState(() => isLoading = false);
    }
  }

  Future<void> _confirmAndCancelReservation(Reservation reservation) async {
  final bool? confirm = await showDialog<bool>(
    context: context,
    builder: (context) {
      return AlertDialog(
        title: const Text(
          'Otkazivanje rezervacije',
          style: TextStyle(fontWeight: FontWeight.bold),
        ),
        content: const Text(
          'Da li ste sigurni da želite otkazati ovu rezervaciju?',
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context, false),
            style: TextButton.styleFrom(
              backgroundColor: const Color(0xFF67B1E5),
              foregroundColor: Colors.white,
            ),
            child: const Text('Ne'),
          ),
          ElevatedButton(
            onPressed: () => Navigator.pop(context, true),
            style: ElevatedButton.styleFrom(
              backgroundColor: const Color(0xFFD62929),
              foregroundColor: Colors.white,
            ),
            child: const Text('Da'),
          ),
        ],
      );
    },
  );

  if (confirm != true) return;

  try {
    await _reservationProvider.cancelReservationSendEmail(
      reservationId: reservation.id!,
      email: widget.user.email!, 
   );
   
    await _reservationProvider.delete(reservation.id!);


    await _offerProvider.decreaseTotalReservation(reservation.offerId!);


    _hotelRoomProvider.increaseRoomsLeft(
      hotelId: reservation.hotelId!,
      roomId: reservation.roomId!,
    );

    await _userTokenProvider.decreaseTokens(widget.user.id!);


    // 5) Obavijesti korisnika
    final bool? shouldRefresh = await showDialog<bool>(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: const Text(
            'Rezervacija otkazana',
            style: TextStyle(fontWeight: FontWeight.bold),
            textAlign: TextAlign.center,
          ),
          content: const Text(
            'Korisnik je ce dobiti email sa detaljima otkazivanja rezervacije.',
            textAlign: TextAlign.center,
          ),
          actions: [
            ElevatedButton(
              onPressed: () => Navigator.pop(context, true),
              style: ElevatedButton.styleFrom(
                backgroundColor: const Color(0xFF67B1E5),
                foregroundColor: Colors.white,
              ),
              child: const Text('U redu'),
            ),
          ],
        );
      },
    );

    if (shouldRefresh == true) {
      _loadData(); 
    }
  } catch (e) {
    if (!mounted) return;
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text('Greška pri otkazivanju rezervacije: $e'),
        backgroundColor: Colors.red,
      ),
    );
  }
}

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color(0xfff3f3f3),
      body: Column(
        children: [
          _header(context),

          const SizedBox(height: 20),

          Text(
            "Aktivne rezervacije korisnika ${widget.user.firstName} ${widget.user.lastName}",
            style: GoogleFonts.openSans(
              fontSize: 28,
              fontWeight: FontWeight.bold,
            ),
          ),

          const SizedBox(height: 30),

          Expanded(
            child: isLoading
                ? const Center(child: CircularProgressIndicator())
                : activeReservationsDisplay.isEmpty
                    ? const Center(
                        child: Text("Nema aktivnih rezervacija."),
                      )
                    : ListView.builder(
                        padding: const EdgeInsets.symmetric(horizontal: 100),
                        itemCount: activeReservationsDisplay.length,
                        itemBuilder: (context, index) {
                          final item = activeReservationsDisplay[index];
                          return _reservationCard(item);
                        },
                      ),
          ),
        ],
      ),
    );
  }

  // =====================================================================
  //                                 HEADER
  // =====================================================================
  Widget _header(BuildContext context) {
    return Container(
      height: 80,
      width: double.infinity,
      color: const Color(0xff67B1E5),
      padding: const EdgeInsets.symmetric(horizontal: 25),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          // Nazad
          InkWell(
            onTap: () => Navigator.pop(context),
            child: Row(
              children: [
                const Icon(Icons.arrow_back, color: Colors.white, size: 26),
                const SizedBox(width: 8),
                Text(
                  "nazad",
                  style: GoogleFonts.openSans(
                    color: Colors.white,
                    fontSize: 22,
                    fontWeight: FontWeight.bold,
                  ),
                )
              ],
            ),
          ),

          // Logo
          Text(
            "eTravel",
            style: GoogleFonts.leckerliOne(color: Colors.white, fontSize: 32),
          ),
        ],
      ),
    );
  }

  // =====================================================================
  //                   POJEDINAČNA KARTICA ZA REZERVACIJU
  // =====================================================================
  Widget _reservationCard(ActiveReservationDisplay item) {
    return Container(
      margin: const EdgeInsets.only(bottom: 25),
      padding: const EdgeInsets.symmetric(horizontal: 25, vertical: 16),
      decoration: BoxDecoration(
        color: const Color(0xffE9E9E9),
        borderRadius: BorderRadius.circular(45),
      ),
      child: Row(
        children: [
          // Slika destinacije
          ClipRRect(
            borderRadius: BorderRadius.circular(50),
            child: Image.network(
              resolveOfferImageUrl(item.imageUrl),
              width: 70,
              height: 70,
              fit: BoxFit.cover,
              errorBuilder: (_, __, ___) =>
                  const Icon(Icons.image, size: 50),
            ),
          ),

          const SizedBox(width: 25),

          // Naziv destinacije
          Expanded(
            child: Text(
              item.title,
              style: GoogleFonts.openSans(
                fontSize: 26,
                fontWeight: FontWeight.bold,
              ),
            ),
          ),

          // DETALJI
          ElevatedButton(
            onPressed: () {
              
              showDialog(context: context, builder: (_) => ReservationDetailsDialog(reservation: item.reservation, user: widget.user,));
            },
            style: ElevatedButton.styleFrom(
              backgroundColor: const Color(0xff67B1E5),
              padding:
                  const EdgeInsets.symmetric(horizontal: 28, vertical: 12),
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(30),
              ),
            ),
            child: Text(
              "detalji rezervacije",
              style: GoogleFonts.openSans(
                color: Colors.white,
                fontSize: 18,
              ),
            ),
          ),

          const SizedBox(width: 20),

          // OTKAŽI REZ.
          ElevatedButton(
            onPressed: () async {
              await _confirmAndCancelReservation(item.reservation);
            },
            style: ElevatedButton.styleFrom(
              backgroundColor: const Color(0xffE57373),
              padding:
                  const EdgeInsets.symmetric(horizontal: 28, vertical: 12),
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(30),
              ),
            ),
            child: Text(
              "otkaži rezervaciju",
              style: GoogleFonts.openSans(
                color: Colors.white,
                fontSize: 18,
              ),
            ),
          ),
        ],
      ),
    );
  }
}

