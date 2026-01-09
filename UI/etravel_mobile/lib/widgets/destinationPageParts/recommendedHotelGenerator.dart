import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/helper/date_converter.dart';
import 'package:etravel_app/models/hotel.dart';
import 'package:etravel_app/providers/hotel_provider.dart';
import 'package:etravel_app/providers/hotel_room_provider.dart';
import 'package:etravel_app/screens/ReservationPreviewPage.dart';
import 'package:etravel_app/screens/loginPage.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class RecommendedHotelGenerator extends StatefulWidget {
  final int offerId;
  final String selectedDate;
  final int hotelId;
  final int roomId;

  const RecommendedHotelGenerator({
    super.key,
    required this.offerId,
    required this.selectedDate,
    required this.hotelId,
    required this.roomId,
  });

  @override
  State<RecommendedHotelGenerator> createState() =>
      _RecommendedHotelGeneratorState();
}

class _RecommendedHotelGeneratorState
    extends State<RecommendedHotelGenerator> {
  late HotelProvider _hotelProvider;
  late HotelRoomProvider _hotelRoomProvider;

  Hotel? _hotel;
  int _roomsLeft = 0;
  bool isLoading = true;

  @override
  void initState() {
    super.initState();

    _hotelProvider = Provider.of<HotelProvider>(context, listen: false);
    _hotelRoomProvider =
        Provider.of<HotelRoomProvider>(context, listen: false);

    _loadHotel();
  }

  Future<void> _loadHotel() async {
    if (Session.userId == null) return;

    setState(() => isLoading = true);

    try {
      final result = await _hotelProvider.get(
        filter: {
          "offerId": widget.offerId,
          "roomId": widget.roomId,
          "departureDate": DateConverter.toUtcIso(widget.selectedDate),
          "RetrieveAll": true,
        },
      );

      final hotel =
          result.items.firstWhere((h) => h.id == widget.hotelId);

      final hotelRoom = await _hotelRoomProvider.getComposite(
        hotel.id,
        widget.roomId,
      );

      setState(() {
        _hotel = hotel;
        _roomsLeft = hotelRoom?.roomsLeft ?? 0;
      });
    } catch (e) {
      debugPrint("Greška ML hotela: $e");
    } finally {
      setState(() => isLoading = false);
    }
  }

  void _showLoginRequiredDialog(BuildContext context) {
    showDialog(
      context: context,
      builder: (_) => AlertDialog(
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(16),
        ),
        title: const Text(
          "Prijava je potrebna",
          style: TextStyle(fontWeight: FontWeight.bold),
        ),
        content: const Text(
          "Da biste mogli rezervisati hotel, potrebno je da budete prijavljeni.",
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text("Odustani"),
          ),
          ElevatedButton(
            style: ElevatedButton.styleFrom(
              backgroundColor: const Color(0xFF67B1E5),
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(20),
              ),
            ),
            onPressed: () {
              Navigator.pop(context);
              Navigator.push(
                context,
                MaterialPageRoute(builder: (_) => const LoginPage()),
              );
            },
            child: const Text("Prijavi se",
                style: TextStyle(color: Colors.white)),
          ),
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    if (Session.userId == null) return const SizedBox.shrink();

    if (isLoading) {
      return const Center(
        child: CircularProgressIndicator(color: Color(0xFFDAB400)),
      );
    }

    if (_hotel == null) return const SizedBox.shrink();

    final hotel = _hotel!;
    final mainImage = hotel.hotelImages.firstWhere(
      (img) => img.isMain,
      orElse: () => hotel.hotelImages.first,
    );

    final roomType = hotel.hotelRooms.first.room?.roomType ?? "Nepoznato";
    final offerHotel = hotel.offerHotels.first;
    final dep = offerHotel.departureDate;
    final ret = offerHotel.returnDate;
    final duration = ret.difference(dep).inDays;

    final isSoldOut = _roomsLeft == 0;

    return Container(
      width: screenWidth * 0.9,
      margin: EdgeInsets.all(screenWidth * 0.025),
      decoration: BoxDecoration(
        color: const Color(0xFFFFF7E0), // 🟡 zlatna pozadina
        borderRadius: BorderRadius.circular(16),
        border: Border.all(color: const Color(0xFFDAB400), width: 2),
        boxShadow: [
          BoxShadow(color: Colors.amber.shade100, blurRadius: 8),
        ],
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          ClipRRect(
            borderRadius:
                const BorderRadius.vertical(top: Radius.circular(16)),
            child: Image.network(
              //"${ApiConfig.imagesHotels}/${mainImage.imageUrl}",
              mainImage.imageUrl,
              height: screenHeight * 0.25,
              width: double.infinity,
              fit: BoxFit.cover,
            ),
          ),

          Padding(
            padding: EdgeInsets.all(screenWidth * 0.04),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  "⭐ Preporučeno za vas",
                  style: TextStyle(
                    color: Colors.orange.shade700,
                    fontWeight: FontWeight.bold,
                  ),
                ),

                const SizedBox(height: 6),

                Text(
                  hotel.name,
                  style: const TextStyle(
                    fontWeight: FontWeight.bold,
                    fontSize: 18,
                  ),
                ),

                Row(
                  children: List.generate(
                    hotel.stars,
                    (_) => const Icon(
                      Icons.star,
                      size: 14,
                      color: Color(0xFFDAB400),
                    ),
                  ),
                ),

                const SizedBox(height: 10),

                Row(
                  children: [
                    const Icon(Icons.bed_outlined,
                        color: Color(0xFF67B1E5)),
                    const SizedBox(width: 6),
                    Text("Tip sobe: $roomType"),
                  ],
                ),

                const SizedBox(height: 10),

                Text(
                  "Termin putovanja",
                  style: const TextStyle(fontWeight: FontWeight.bold),
                ),
                Text(
                  "${dep.day}.${dep.month}.${dep.year} - "
                  "${ret.day}.${ret.month}.${ret.year} | $duration dana",
                  style: const TextStyle(color: Color(0xFF67B1E5)),
                ),

                const SizedBox(height: 14),

                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    ElevatedButton(
                      onPressed: isSoldOut
                          ? null
                          : () {
                              if (Session.userId == null) {
                                _showLoginRequiredDialog(context);
                                return;
                              }

                              Navigator.push(
                                context,
                                MaterialPageRoute(
                                  builder: (_) => ReservationPreviewScreen(
                                    departureDate: dep,
                                    returnDate: ret,
                                    offerId: widget.offerId,
                                    hotelId: hotel.id,
                                    roomId: widget.roomId,
                                    basePrice:
                                        hotel.calculatedPrice.toDouble(),
                                    imageUrl: mainImage.imageUrl,
                                  ),
                                ),
                              );
                            },
                      style: ElevatedButton.styleFrom(
                        backgroundColor:
                            isSoldOut ? Colors.grey : Colors.amber,
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(20),
                        ),
                      ),
                      child: Text(
                        isSoldOut ? "Rasprodano" : "Rezerviši",
                        style: const TextStyle(color: Colors.white),
                      ),
                    ),

                    Text(
                      "Cijena: ${hotel.calculatedPrice}\$",
                      style: const TextStyle(fontWeight: FontWeight.bold),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
