import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/models/hotel_room.dart';
import 'package:etravel_app/models/offer.dart';
import 'package:etravel_app/models/recommended_hotel.dart';
import 'package:etravel_app/models/room.dart';
import 'package:etravel_app/providers/hotel_provider.dart';
import 'package:etravel_app/providers/hotel_room_provider.dart';
import 'package:etravel_app/providers/offer_hotel_provider.dart';
import 'package:etravel_app/providers/offer_provider.dart';
import 'package:etravel_app/providers/room_provider.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:etravel_app/widgets/destinationPageParts/AllLists.dart';
import 'package:etravel_app/widgets/destinationPageParts/KomentariKontejner.dart';
import 'package:etravel_app/widgets/destinationPageParts/recommendedHotelGenerator.dart';
import 'package:etravel_app/widgets/destinationPageParts/roomofferGenerator.dart';
import 'package:etravel_app/widgets/destinationPageParts/travelingPlanDays.dart';
import 'package:etravel_app/widgets/headerIFooterAplikacije/eTravelFooter.dart';
import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:etravel_app/widgets/startingPageParts/popularneDestinacijeAndParts/parts/dotSlider.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeAndParts/parts/AllLists.dart';
import 'package:etravel_app/widgets/startingPageParts/specijalneDestinacijeAndParts/parts/specijalneDestinacijeKontejneri.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class destinationPage extends StatefulWidget {
  final String naziv;
  final String cijena;
  final String? brojDana;
  final String opis;
  final String glavnaSlikaPath;
  final int offerId;
  final List<String> slike;
  final List<String> datumi;

  const destinationPage({
    super.key,
    required this.naziv,
    required this.cijena,
    required this.opis,
    required this.brojDana,
    required this.glavnaSlikaPath,
    required this.slike,
    required this.datumi,
    required this.offerId,
  });

  @override
  State<destinationPage> createState() => _DestinationPageState();
}

class _DestinationPageState extends State<destinationPage> {
  final PageController _pageController = PageController(viewportFraction: 0.8);
  int _currentIndex = 0;
  late OfferHotelProvider _offerHotelProvider;
  late HotelRoomProvider _hotelRoomProvider;
  late RoomProvider _roomProvider;
  late HotelProvider _hotelProvider;

  RecommendedHotel? _recommendedHotel;
  bool _loadingRecommendedHotel = false;

  int hotelPage = 0;
  int hotelPageSize = 3;

  String? selectedDate;
  int? roomId;

  bool firstSelectionDone = false;

  List<String> get sveSlike {
    return [
      ...widget.slike, // ostale sporedne
    ];
  }

  @override
  void initState() {
    super.initState();

    // Inicijalizacija providera
    _offerHotelProvider = Provider.of<OfferHotelProvider>(
      context,
      listen: false,
    );
    _hotelRoomProvider = Provider.of<HotelRoomProvider>(context, listen: false);
    _roomProvider = Provider.of<RoomProvider>(context, listen: false);
    _hotelProvider = Provider.of<HotelProvider>(context, listen: false);
  }

  @override
  void dispose() {
    _pageController.dispose();
    super.dispose();
  }

  Future<void> _loadRecommendedHotel() async {
    if (Session.userId == null) return;

    try {
      setState(() => _loadingRecommendedHotel = true);

      final result = await _hotelProvider.getRecommendedHotelRoomForOffer(
        offerId: widget.offerId,
        userId: Session.userId!,
      );

      setState(() {
        _recommendedHotel = result;
      });
    } catch (e) {
      debugPrint("ML hotel nije dostupan: $e");
    } finally {
      setState(() => _loadingRecommendedHotel = false);
    }
  }

  Future<List<Room>> _ucitajTipoveSoba(int offerId) async {
    try {
      // 1) Sve hotele u offeru
      final offerHotelResult = await _offerHotelProvider.get(
        filter: {"offerDetailsId": offerId},
      );

      final hotelIds = offerHotelResult.items.map((e) => e.hotelId).toList();

      // Ako nema hotela → nema soba
      if (hotelIds.isEmpty) return [];

      // 2) Učitati sve hotel-room zapise za date hotele
      List<HotelRoom> hotelRooms = [];
      for (var id in hotelIds) {
        final hr = await _hotelRoomProvider.get(filter: {"hotelId": id});
        hotelRooms.addAll(hr.items);
      }

      // 3) Učitati sve sobe (master tabela)
      final roomResult = await _roomProvider.get();

      // 4) Povezati preko ID-a → dobiti nazive soba koje stvarno postoje u hotelima
      final Set<String> tipoviSoba = {};

      final Set<Room> roomSet = {};

      for (var hr in hotelRooms) {
        final room = roomResult.items.firstWhere(
          (r) => r.id == hr.roomId,
          orElse: () => Room(id: 0, roomType: ''),
        );

        if (room.id != 0) {
          roomSet.add(room);
        }
      }

      return roomSet.toList();
    } catch (e) {
      print("Greška pri učitavanju tipova soba: $e");
      return [];
    }
  }

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    return Scaffold(
      backgroundColor: Colors.white,
      body: CustomScrollView(
        slivers: [
          SljedecaDestinacijaIMenuBar(daLijeKliknuo: false), // jer je sliver
          SliverToBoxAdapter(
            child: Column(
              children: [
                SizedBox(
                  width: screenWidth,
                  height: screenHeight * 0.08,
                  child: Center(
                    child: Text(
                      widget.naziv,
                      style: TextStyle(
                        color: Colors.black,
                        fontWeight: FontWeight.bold,
                        fontFamily: 'AROneSans',
                        fontSize: screenWidth * 0.06,
                      ),
                    ),
                  ),
                ),
                SizedBox(height: screenHeight * 0.02),
                SizedBox(
                  height: screenWidth * 0.55,
                  child: PageView.builder(
                    controller: _pageController,
                    itemCount: sveSlike.length,
                    onPageChanged: (index) {
                      setState(() {
                        _currentIndex = index;
                      });
                    },
                    itemBuilder: (context, index) {
                      bool isActive = index == _currentIndex;

                      return Transform.scale(
                        scale: isActive ? 1 : 0.9,
                        child: Stack(
                          children: [
                            ClipRRect(
                              borderRadius: BorderRadius.circular(20),
                              child: Image.network(
                                sveSlike[index].contains("http")
                                    ? sveSlike[index]
                                    : "${ApiConfig.imagesOffers}/${sveSlike[index]}",
                                width: screenWidth * 0.8,
                                height: screenWidth * 0.55,
                                fit: BoxFit.cover,
                              ),
                            ),
                            if (isActive)
                              Container(
                                decoration: BoxDecoration(
                                  color: Colors.black.withOpacity(0.25),
                                  borderRadius: BorderRadius.circular(20),
                                ),
                              ),
                          ],
                        ),
                      );
                    },
                  ),
                ),
                SizedBox(height: screenHeight * 0.02),
                SizedBox(
                  width: screenWidth * 0.30,
                  height: screenHeight * 0.05,
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                    children: List.generate(
                      sveSlike.length,
                      (index) => GestureDetector(
                        onTap: () {
                          _pageController.animateToPage(
                            index,
                            duration: Duration(milliseconds: 300),
                            curve: Curves.easeInOut,
                          );
                        },
                        child: dotSlider(_currentIndex == index),
                      ),
                    ),
                  ),
                ),

                Container(
                  width: screenWidth * 0.9,
                  height: screenHeight * 0.05,
                  margin: EdgeInsets.only(top: screenHeight * 0.04, right: 10),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Image.asset('assets/images/clock.png'),
                      Text(
                        widget.brojDana!,
                        style: TextStyle(
                          fontWeight: FontWeight.bold,
                          fontSize: screenWidth * 0.05,
                        ),
                      ),
                    ],
                  ),
                ),
                Container(
                  width: screenWidth * 0.7,
                  height: screenHeight * 0.09,
                  margin: EdgeInsets.only(top: screenHeight * 0.025),
                  alignment: Alignment.center,
                  decoration: BoxDecoration(
                    border: Border.all(color: Color(0xFF67B1E5), width: 2),
                    borderRadius: BorderRadius.circular(screenWidth * 0.05),
                  ),
                  child: RichText(
                    text: TextSpan(
                      children: [
                        TextSpan(
                          text: 'već ',
                          style: TextStyle(
                            color: Colors.black,
                            fontSize: screenWidth * 0.045,
                            fontWeight: FontWeight.bold,
                            fontFamily: 'AROneSans',
                          ),
                        ),
                        TextSpan(
                          text: 'od ',
                          style: TextStyle(
                            color: Colors.black,
                            fontSize: screenWidth * 0.045,
                            fontWeight: FontWeight.bold,
                            fontFamily: 'AROneSans',
                          ),
                        ),
                        TextSpan(
                          text: widget.cijena,
                          style: TextStyle(
                            color: Color(0xFF67B1E5),
                            fontSize: screenWidth * 0.08,
                            fontWeight: FontWeight.bold,
                            fontFamily: 'AROneSans',
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
                Column(
                  children: [
                    Container(
                      width: screenWidth * 0.95,
                      height: screenHeight * 0.1,
                      alignment: Alignment.centerLeft,
                      margin: EdgeInsets.only(top: screenHeight * 0.01),
                      child: Text(
                        'O destinaciji',
                        style: TextStyle(
                          fontFamily: 'AROneSans',
                          fontSize: screenWidth * 0.06,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),
                    Container(
                      width: screenWidth * 0.95,
                      height: screenHeight * 0.4,
                      alignment: Alignment.center,
                      child: Text(
                        widget.opis,
                        style: TextStyle(
                          color: Color(0xFF67B1E5),
                          fontFamily: 'AROneSans',
                          fontSize: screenWidth * 0.04,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),
                  ],
                ),
                Container(
                  width: screenWidth * 0.9,
                  height: screenHeight * 0.15,
                  margin: EdgeInsets.only(top: screenHeight * 0.02),
                  alignment: Alignment.center,
                  child: Text(
                    'Izaberite datum putovanja',
                    style: TextStyle(
                      fontFamily: 'AROneSans',
                      fontSize: screenWidth * 0.06,
                      fontWeight: FontWeight.bold,
                    ),
                    textAlign: TextAlign.center,
                  ),
                ),
                Container(
                  width: screenWidth,
                  padding: EdgeInsets.symmetric(
                    vertical: screenHeight * 0.02,
                    horizontal: screenWidth * 0.04,
                  ),
                  decoration: BoxDecoration(
                    color: Color(0xFFF5F5F5),
                    border: Border.all(color: Color(0xFF67B1E5), width: 2),
                  ),
                  // 🔽 Prikaz upozorenja i dropdown logike
                  child: Column(
                    children: [
                      // 🔹 UPOZORENJE – PRVO BIRAJ DATUM
                      if (!firstSelectionDone && selectedDate == null)
                        Padding(
                          padding: const EdgeInsets.only(bottom: 12),
                          child: Text(
                            "Prvo izaberite datum putovanja",
                            style: TextStyle(
                              color: Colors.grey,
                              fontSize: screenWidth * 0.045,
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                        ),

                      // 🔹 AKO NEMA DATUMA UOPŠTE
                      if (widget.datumi.isEmpty)
                        Padding(
                          padding: const EdgeInsets.all(8),
                          child: Text(
                            "Nema dostupnih datuma",
                            style: TextStyle(
                              color: Colors.grey,
                              fontSize: screenWidth * 0.05,
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                        ),

                      // 🔹 LISTA DATUMA IZ BACKENDA
                      ...widget.datumi.map((datum) {
                        final bool isSelected = selectedDate == datum;

                        return Container(
                          margin: EdgeInsets.symmetric(
                            vertical: screenHeight * 0.015,
                          ),
                          width: double.infinity,
                          height: screenHeight * 0.12,
                          child: ElevatedButton(
                            onPressed: () {
                              setState(() {
                                selectedDate = datum;

                                // if (!firstSelectionDone) {
                                //   roomId = null; // reset samo prvi put
                                // }

                                hotelPage = 0;
                                //firstSelectionDone = false;
                              });
                            },

                            style: ElevatedButton.styleFrom(
                              backgroundColor:
                                  isSelected
                                      ? const Color(0xFF67B1E5)
                                      : Colors.white,
                              foregroundColor:
                                  isSelected
                                      ? Colors.white
                                      : const Color(0xFF67B1E5),
                              elevation: 3,
                              shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(
                                  screenWidth * 0.03,
                                ),
                                side: const BorderSide(
                                  color: Color(0xFF67B1E5),
                                ),
                              ),
                              padding: EdgeInsets.symmetric(
                                vertical: screenHeight * 0.015,
                              ),
                            ),
                            child: Text(
                              datum,
                              style: TextStyle(
                                fontWeight: FontWeight.bold,
                                fontSize: screenWidth * 0.06,
                                fontFamily: 'AROneSans',
                              ),
                            ),
                          ),
                        );
                      }).toList(),

                      SizedBox(height: screenHeight * 0.02),

                      // 🔹 UPOZORENJE – NAKON DATUMA BIRAJ TIP SOBE
                      if (!firstSelectionDone &&
                          (selectedDate != null && roomId == null))
                        Padding(
                          padding: const EdgeInsets.only(bottom: 12),
                          child: Text(
                            "Sada izaberite tip sobe",
                            style: TextStyle(
                              color: Colors.redAccent,
                              fontSize: screenWidth * 0.045,
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                        ),

                      // 🔹 DROPDOWN – TIP SOBE (VIDLJIV SAMO KAD JE IZABRAN DATUM)
                      if (selectedDate != null)
                        Container(
                          padding: EdgeInsets.symmetric(
                            horizontal: screenWidth * 0.03,
                          ),
                          decoration: BoxDecoration(
                            border: Border.all(
                              color: const Color(0xFF67B1E5),
                              width: 2,
                            ),
                            borderRadius: BorderRadius.circular(
                              screenWidth * 0.03,
                            ),
                          ),
                          child: FutureBuilder<List<Room>>(
                            future: _ucitajTipoveSoba(widget.offerId),
                            builder: (context, snapshot) {
                              if (snapshot.connectionState ==
                                  ConnectionState.waiting) {
                                return const Padding(
                                  padding: EdgeInsets.all(12),
                                  child: Center(
                                    child: CircularProgressIndicator(
                                      color: Color(0xFF67B1E5),
                                    ),
                                  ),
                                );
                              }

                              if (!snapshot.hasData || snapshot.data!.isEmpty) {
                                return const Padding(
                                  padding: EdgeInsets.all(12),
                                  child: Text(
                                    "Nema dostupnih soba",
                                    style: TextStyle(color: Colors.grey),
                                  ),
                                );
                              }

                              final tipovi = snapshot.data!;

                              return DropdownButtonFormField<int>(
                                decoration: const InputDecoration.collapsed(
                                  hintText: '',
                                ),
                                hint: const Text(
                                  'odaberite tip sobe',
                                  style: TextStyle(color: Color(0xFFC7C7C7)),
                                ),
                                value: roomId,
                                onChanged: (value) async {
                                  setState(() {
                                    roomId = value;

                                    if (!firstSelectionDone) {
                                      firstSelectionDone =
                                          true; // ✔ sada je logično
                                    }
                                    hotelPage = 0;
                                  });

                                  await _loadRecommendedHotel();
                                },

                                items:
                                    tipovi.map((room) {
                                      return DropdownMenuItem<int>(
                                        value: room.id, // vrijednost = ID sobe
                                        child: Text(
                                          room.roomType,
                                        ), // prikaz = naziv sobe
                                      );
                                    }).toList(),
                              );
                            },
                          ),
                        ),

                      SizedBox(height: screenHeight * 0.015),
                    ],
                  ),
                ),
                SizedBox(height: screenHeight * 0.1 - 20),

                if (selectedDate != null && roomId != null) ...[
                  Column(
                    children: [
                      Roomoffergenerator(
                        key: ValueKey("${selectedDate}_${roomId}"),
                        OfferId: widget.offerId,
                        selectedDate: selectedDate,
                        roomId: roomId,
                      ),

                      if (Session.userId != null &&
                          _recommendedHotel != null &&
                          !_loadingRecommendedHotel) ...[
                        SizedBox(height: screenHeight * 0.05),

                        /// 🧠 NASLOV
                        Text(
                          "Hotel po vašoj mjeri",
                          style: TextStyle(
                            fontFamily: 'AROneSans',
                            fontSize: screenWidth * 0.06,
                            fontWeight: FontWeight.bold,
                            color: const Color(0xFFDAB400),
                          ),
                        ),

                        SizedBox(height: screenHeight * 0.02),

                        /// ⭐ ML HOTEL (BEZ PAGINGA)
                        RecommendedHotelGenerator(
                          offerId: widget.offerId,
                          selectedDate: selectedDate!,
                          hotelId: _recommendedHotel!.hotelId,
                          roomId: _recommendedHotel!.roomId,
                        ),
                      ],
                    ],
                  ),
                  Container(
                    width: screenWidth,
                    height: screenHeight * 0.15,
                    alignment: Alignment.center,
                    child: Text(
                      'Plan i program putovanja',
                      style: TextStyle(
                        fontWeight: FontWeight.bold,
                        fontFamily: 'AROneSans',
                        fontSize: screenWidth * 0.06,
                      ),
                    ),
                  ),
                  SizedBox(
                    height: screenHeight * 0.4, // Adjust this as needed
                    child: PlanPutovanja(offerId: widget.offerId),
                  ),
                  Container(
                    width: screenWidth,
                    height: screenHeight * 0.05,
                    margin: EdgeInsets.only(top: screenHeight * 0.1 - 40),
                    color: Color(0xFF67B1E5),
                  ),
                  Container(
                    width: screenWidth,
                    height: screenHeight * 0.15,
                    alignment: Alignment.center,
                    child: Text(
                      'Šta je uračunato u cijenu',
                      style: TextStyle(
                        fontWeight: FontWeight.bold,
                        fontFamily: 'AROneSans',
                        fontSize: screenWidth * 0.06,
                      ),
                    ),
                  ),
                  Column(
                    children: List.generate(uracunatoUCijenu.length, (index) {
                      final stavka = uracunatoUCijenu[index];
                      return Container(
                        width: screenWidth * 0.8,
                        height: screenHeight * 0.12,
                        margin: EdgeInsets.only(bottom: 20),
                        decoration: BoxDecoration(
                          border: Border.all(color: Colors.black, width: 2),
                          borderRadius: BorderRadius.circular(10),
                        ),
                        child: Row(
                          mainAxisAlignment: MainAxisAlignment.spaceAround,
                          children: [
                            Image.asset(
                              width: screenWidth * 0.1 - 6,
                              height: screenHeight * 0.1 - 50,
                              'assets/images/check.png',
                            ),
                            SizedBox(
                              width: screenWidth - 200,
                              child: Text(
                                stavka,
                                textAlign: TextAlign.right,
                                style: TextStyle(
                                  fontWeight: FontWeight.bold,
                                  fontFamily: 'AROneSans',
                                ),
                              ),
                            ),
                          ],
                        ),
                      );
                    }),
                  ),
                  Container(
                    width: screenWidth,
                    height: screenHeight * 0.15,
                    alignment: Alignment.center,
                    margin: EdgeInsets.only(top: 20),
                    color: Color(0xFF67B1E5),
                    child: Text(
                      'Šta nije uračunato u cijenu',
                      style: TextStyle(
                        color: Colors.white,
                        fontWeight: FontWeight.bold,
                        fontFamily: 'AROneSans',
                        fontSize: screenWidth * 0.06,
                      ),
                    ),
                  ),
                  Container(
                    width: screenWidth,
                    height: screenHeight * 0.3,
                    color: Color(0xFF67B1E5),
                    child: Column(
                      children: List.generate(nijeUracunatoUCijenu.length, (
                        index,
                      ) {
                        final stavka = nijeUracunatoUCijenu[index];
                        return Container(
                          width: screenWidth * 0.8,
                          height: screenHeight * 0.12,
                          margin: EdgeInsets.only(bottom: 20),
                          decoration: BoxDecoration(
                            color: Color(0xFF67B1E5),
                            border: Border.all(color: Colors.white, width: 2),
                            borderRadius: BorderRadius.circular(10),
                          ),
                          child: Row(
                            mainAxisAlignment: MainAxisAlignment.spaceAround,
                            children: [
                              Image.asset(
                                stavka['slika']
                                    as String, // sada koristimo dinamičku putanju iz objekta
                                width: screenWidth * 0.1 - 6,
                                height: screenHeight * 0.1 - 50,
                              ),
                              SizedBox(
                                width: screenWidth - 200,
                                child: Text(
                                  stavka['opis']
                                      as String, // sada koristimo tekst iz objekta
                                  textAlign: TextAlign.center,
                                  style: TextStyle(
                                    color: Colors.white,
                                    fontWeight: FontWeight.bold,
                                    fontFamily: 'AROneSans',
                                  ),
                                ),
                              ),
                            ],
                          ),
                        );
                      }),
                    ),
                  ),
                  
                  SizedBox(
                    width: screenWidth,
                    height: screenHeight * 0.7, // Povećaj visinu da sve stane
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        Container(
                          width: screenWidth,
                          height: screenHeight * 0.1,
                          alignment: Alignment.center,
                          child: Text(
                            'Recenzije korisnika za ovo putovanje',
                            style: TextStyle(
                              fontSize: 21,
                              fontWeight: FontWeight.bold,
                            ),
                            textAlign: TextAlign.center,
                          ),
                        ),
                        KomentariKontejner(offerId: widget.offerId),
                      ],
                    ),
                  ),
                  eTravelFooter(),
                ],
              ],
            ),
          ),
        ],
      ),
    );
  }
}
