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

class Roomoffergenerator extends StatefulWidget {
  final int OfferId;
  final String? selectedDate;
  final int? roomId;

  const Roomoffergenerator({
    super.key,
    required this.OfferId,
    required this.selectedDate,
    required this.roomId,
  });

  @override
  State<Roomoffergenerator> createState() => _RoomoffergeneratorState();
}

class _RoomoffergeneratorState extends State<Roomoffergenerator> {
  late HotelProvider _hotelProvider;
  late HotelRoomProvider _hotelRoomProvider;

  /// CACHE PO STRANICI
  final Map<int, Hotel> _pageHotels = {};
  final Map<int, int> _roomsLeftByHotel = {};

  bool isLoading = true;

  /// PAGING
  late PageController _pageController;
  int _currentPage = 0;
  final int _pageSize = 1;
  int _totalPages = 1;

  @override
  void initState() {
    super.initState();
    _hotelProvider = Provider.of<HotelProvider>(context, listen: false);
    _hotelRoomProvider = Provider.of<HotelRoomProvider>(context, listen: false);

    _pageController = PageController(initialPage: 0);
    _init();
  }

  @override
  void dispose() {
    _pageController.dispose();
    super.dispose();
  }

  @override
  void didUpdateWidget(covariant Roomoffergenerator oldWidget) {
    super.didUpdateWidget(oldWidget);

    if (oldWidget.selectedDate != widget.selectedDate ||
        oldWidget.roomId != widget.roomId) {
      _currentPage = 0;
      _pageHotels.clear();
      _roomsLeftByHotel.clear();
      _pageController.jumpToPage(0);
      _init();
    }
  }

  /// ===============================
  /// INIT
  /// ===============================
  Future<void> _init() async {
    setState(() => isLoading = true);
    await _loadTotalHotels();
    await _loadHotelForPage(0);
  }

  /// ===============================
  /// TOTAL COUNT
  /// ===============================
  Future<void> _loadTotalHotels() async {
    final result = await _hotelProvider.get(
      filter: {
        "offerId": widget.OfferId,
        "roomId": widget.roomId,
        "departureDate": DateConverter.toUtcIso(widget.selectedDate),
        "RetrieveAll": true,
      },
    );

    _totalPages = (result.items.length / _pageSize).ceil();
  }

  /// ===============================
  /// LOAD JEDNE STRANICE (CACHE)
  /// ===============================
  Future<void> _loadHotelForPage(int page) async {
    if (_pageHotels.containsKey(page)) {
      setState(() {
        _currentPage = page;
        isLoading = false;
      });
      return;
    }

    setState(() => isLoading = true);

    try {
      final result = await _hotelProvider.get(
        filter: {
          "offerId": widget.OfferId,
          "roomId": widget.roomId,
          "departureDate": DateConverter.toUtcIso(widget.selectedDate),
          "page": page,
          "pageSize": _pageSize,
        },
      );

      if (result.items.isEmpty) {
        setState(() => isLoading = false);
        return;
      }

      final hotel = result.items.first;
      _pageHotels[page] = hotel;

      /// 🔥 UZMI roomsLeft SAMO ZA TAJ HOTEL + TU SOBU
      final hotelRoom = await _hotelRoomProvider.getComposite(
        hotel.id,
        widget.roomId!,
      );

      final int roomsLeft = hotelRoom?.roomsLeft ?? 0;

      _roomsLeftByHotel[hotel.id] = roomsLeft;

      setState(() {
        _currentPage = page;
        isLoading = false;
      });
    } catch (e) {
      debugPrint("Greška učitavanja hotela: $e");
      setState(() => isLoading = false);
    }
  }

  void _showLoginRequiredDialog(BuildContext context) {
    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(16),
          ),
          title: const Text(
            "Prijava je potrebna",
            style: TextStyle(fontWeight: FontWeight.bold),
          ),
          content: const Text(
            "Da biste mogli rezervisati hotel, potrebno je da budete prijavljeni na svoj nalog.",
          ),
          actions: [
            TextButton(
              onPressed: () => Navigator.of(context).pop(),
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
                Navigator.of(context).pop();

                Navigator.of(
                  context,
                ).push(MaterialPageRoute(builder: (_) => const LoginPage()));
              },
              child: const Text(
                "Prijavi se",
                style: TextStyle(color: Colors.white),
              ),
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    if (_totalPages == 0) {
      return const Center(child: Text("Nema dostupnih hotela za ovaj termin."));
    }

    return Column(
      children: [
        SizedBox(
          height: screenHeight * 0.65,
          child: PageView.builder(
            controller: _pageController,
            itemCount: _totalPages,
            onPageChanged: _loadHotelForPage,
            itemBuilder: (context, pageIndex) {
              if (isLoading && pageIndex == _currentPage) {
                return const Center(
                  child: CircularProgressIndicator(color: Color(0xFF67B1E5)),
                );
              }

              final hotel = _pageHotels[pageIndex];
              if (hotel == null) return const SizedBox.shrink();

              final roomsLeft = _roomsLeftByHotel[hotel.id] ?? 0;

              return _buildHotelCard(hotel, roomsLeft);
            },
          ),
        ),
        const SizedBox(height: 12),
        Text(
          "Stranica ${_currentPage + 1} / $_totalPages",
          style: const TextStyle(
            fontWeight: FontWeight.bold,
            color: Colors.black54,
          ),
        ),
      ],
    );
  }

  /// ===============================
  /// HOTEL CARD
  /// ===============================
  Widget _buildHotelCard(Hotel hotel, int roomsLeft) {
    final mainImage = hotel.hotelImages.firstWhere(
      (img) => img.isMain,
      orElse: () => hotel.hotelImages.first,
    );

    final roomType =
        hotel.hotelRooms.isNotEmpty
            ? hotel.hotelRooms.first.room?.roomType ?? "Nepoznato"
            : "Nepoznato";

    final offerHotel = hotel.offerHotels.first;
    final dep = offerHotel.departureDate;
    final ret = offerHotel.returnDate;
    final duration = ret.difference(dep).inDays;

    final isSoldOut = roomsLeft == 0;

    return Container(
      width: screenWidth * 0.9,
      margin: EdgeInsets.all(screenWidth * 0.025),
      decoration: BoxDecoration(
        color: const Color(0xFFF5F5F5),
        borderRadius: BorderRadius.circular(15),
        boxShadow: [BoxShadow(color: Colors.grey.shade300, blurRadius: 6)],
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          /// ================= IMAGE =================
          ClipRRect(
            borderRadius: const BorderRadius.vertical(top: Radius.circular(15)),
            child: Image.network(
              "${ApiConfig.imagesHotels}/${mainImage.imageUrl}",
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
                /// ================= HOTEL + STANJE SOBA =================
                Row(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            hotel.name,
                            style: const TextStyle(
                              fontWeight: FontWeight.bold,
                              fontSize: 16,
                            ),
                          ),
                          const SizedBox(height: 4),
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
                        ],
                      ),
                    ),

                    /// 🔴🟢 ROOMS LEFT
                    Text(
                      isSoldOut ? "RASPRODANO" : "Još $roomsLeft soba",
                      style: TextStyle(
                        color: isSoldOut ? Colors.red : Colors.green,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ],
                ),

                const SizedBox(height: 10),

                /// ================= TIP SOBE + DETALJI =================
                Column(
                  crossAxisAlignment:
                      CrossAxisAlignment.start, // 🔥 OVO JE KLJUČ
                  children: [
                    Row(
                      children: [
                        const Icon(
                          Icons.bed_outlined,
                          color: Color(0xFF67B1E5),
                          size: 20,
                        ),
                        const SizedBox(width: 6),
                        Text(
                          "Tip sobe: $roomType",
                          style: const TextStyle(fontWeight: FontWeight.w600),
                        ),
                      ],
                    ),

                    const SizedBox(height: 6),

                    OutlinedButton(
                      onPressed: () {
                        // TODO: detalji
                      },
                      style: OutlinedButton.styleFrom(
                        side: const BorderSide(color: Color(0xFF67B1E5)),
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(20),
                        ),
                      ),
                      child: const Text(
                        "Detalji",
                        style: TextStyle(color: Color(0xFF67B1E5)),
                      ),
                    ),
                  ],
                ),

                const SizedBox(height: 12),

                /// ================= TERMIN =================
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

                /// ================= CTA =================
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    ElevatedButton(
                      onPressed:
                          isSoldOut
                              ? null
                              : () {
                                if (Session.userId == null) {
                                  _showLoginRequiredDialog(context);
                                  return;
                                }

                                Navigator.of(context).push(
                                  MaterialPageRoute(
                                    builder:
                                        (_) => ReservationPreviewScreen(
                                          departureDate: dep,
                                          returnDate: ret,
                                          offerId: widget.OfferId,
                                          hotelId: hotel.id,
                                          roomId: widget.roomId!,
                                          basePrice:
                                              hotel.calculatedPrice.toDouble(),
                                          imageUrl: mainImage.imageUrl,
                                        ),
                                  ),
                                );
                              },

                      style: ElevatedButton.styleFrom(
                        backgroundColor:
                            isSoldOut ? Colors.grey : const Color(0xFF67B1E5),
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
