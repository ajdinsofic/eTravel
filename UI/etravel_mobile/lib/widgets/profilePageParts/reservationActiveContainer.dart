import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/models/reservations.dart';
import 'package:etravel_app/providers/hotel_room_provider.dart';
import 'package:etravel_app/providers/offer_hotel_provider.dart';
import 'package:etravel_app/providers/offer_provider.dart';
import 'package:etravel_app/providers/reservation_provider.dart';
import 'package:etravel_app/screens/PhasePaymentPage.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class ReservationActiveContainer extends StatefulWidget {
  final Reservation reservation;
  final double price;
  final dynamic preview;
  final String imageUrl;
  final VoidCallback onCancelled;

  const ReservationActiveContainer({
    super.key,
    required this.reservation,
    required this.preview,
    required this.imageUrl,
    required this.price,
    required this.onCancelled
  });

  @override
  State<ReservationActiveContainer> createState() =>
      _ReservationActiveContainerState();
}

class _ReservationActiveContainerState
    extends State<ReservationActiveContainer> {
  late ReservationProvider _reservationProvider;
  late HotelRoomProvider _hotelRoomProvider;
  late OfferProvider _offerProvider;

  bool prikaziDetalje = false;

  DateTime? departureDate;
  DateTime? returnDate;

  @override
  void initState() {
    super.initState();
    _reservationProvider = Provider.of<ReservationProvider>(
      context,
      listen: false,
    );
    _hotelRoomProvider = Provider.of<HotelRoomProvider>(
    context,
    listen: false,
    );
    _offerProvider = Provider.of<OfferProvider>(
  context,
  listen: false,
);

    _loadDates();
  }

  Future<void> _loadDates() async {
    final offerHotelProvider = Provider.of<OfferHotelProvider>(
      context,
      listen: false,
    );

    final result = await offerHotelProvider.getByOfferAndHotel(
      widget.reservation.offerId,
      widget.reservation.hotelId,
    );

    setState(() {
      departureDate = result.departureDate;
      returnDate = result.returnDate;
    });
  }

  Future<void> _confirmAndCancelReservation() async {
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
            ),
            child: const Text('Ne', style: TextStyle(color: Colors.white)),
          ),
          ElevatedButton(
            onPressed: () => Navigator.pop(context, true),
            style: ElevatedButton.styleFrom(
              backgroundColor: const Color(0xFFD62929),
            ),
            child: const Text('Da', style: TextStyle(color: Colors.white)),
          ),
        ],
      );
    },
  );

  if (confirm != true) return;

  try {
  await _reservationProvider.delete(widget.reservation.id!);

  await _offerProvider.decreaseTotalReservation(
  widget.reservation.offerId!,
  );

  _hotelRoomProvider.increaseRoomsLeft(
    hotelId: widget.reservation.hotelId!,
    roomId: widget.reservation.roomId!,
  );

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
          'Iznos koji ste do sada uplatili biće vraćen.\n\n'
          'Naš tim će vas kontaktirati u najkraćem roku '
          'kako biste povrat sredstava preuzeli u najbližoj poslovnici.',
          textAlign: TextAlign.center,
        ),
        actions: [
          ElevatedButton(
            onPressed: () => Navigator.pop(context, true),
            style: ElevatedButton.styleFrom(
              backgroundColor: const Color(0xFF67B1E5),
            ),
            child: const Text(
              'U redu',
              style: TextStyle(color: Colors.white),
            ),
          ),
        ],
      );
    },
  );

  if (shouldRefresh == true) {
    widget.onCancelled();
  }
} catch (e) {
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
    postaviWidthIHeight(context);

    return Container(
      width: screenWidth * 0.9,
      padding: EdgeInsets.symmetric(vertical: screenHeight * 0.015),
      decoration: BoxDecoration(
        color: const Color(0xFFF5F5F5),
        border: Border.all(color: const Color(0xFF67B1E5)),
        borderRadius: BorderRadius.circular(20),
      ),
      child: Column(
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: [
              CircleAvatar(
                radius: screenWidth * 0.08,
                backgroundImage: NetworkImage(widget.imageUrl),
              ),
              SizedBox(
                width: screenWidth * 0.38,
                height: screenHeight * 0.1,
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      widget.preview.offerTitle ?? '',
                      style: TextStyle(
                        fontWeight: FontWeight.bold,
                        fontSize: screenWidth * 0.05,
                      ),
                    ),
                  ],
                ),
              ),

              TextButton(
                onPressed: () {
                  setState(() => prikaziDetalje = !prikaziDetalje);
                },
                child: Container(
                  width: screenWidth * 0.2,
                  height: screenHeight * 0.05,
                  alignment: Alignment.center,
                  decoration: BoxDecoration(
                    color: const Color(0xFF67B1E5),
                    borderRadius: BorderRadius.circular(10),
                  ),
                  child: Text(
                    prikaziDetalje ? 'Manje detalja' : 'Više detalja',
                    style: TextStyle(
                      fontSize: screenWidth * 0.025,
                      color: Colors.white,
                    ),
                  ),
                ),
              ),
            ],
          ),

          if (prikaziDetalje)
            Padding(
              padding: EdgeInsets.symmetric(
                horizontal: screenWidth * 0.04,
                vertical: screenHeight * 0.015,
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  const Divider(color: Color(0xFF67B1E5)),

                  Column(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text(
                        "${widget.preview.roomType} soba" ?? '',
                        style: const TextStyle(
                          fontFamily: 'AROneSans',
                          fontSize: 17.5,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                      Text(
                        widget.preview.hotelTitle ?? '',
                        style: const TextStyle(
                          fontFamily: 'AROneSans',
                          fontSize: 17,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ],
                  ),

                  SizedBox(height: 10),

                  Row(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Expanded(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            const Text(
                              'Vrijeme polaska:',
                              style: TextStyle(
                                fontFamily: 'AROneSans',
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                            Text(
                              departureDate != null && returnDate != null
                                  ? '${departureDate!.day}.${departureDate!.month}.${departureDate!.year} - '
                                      '${returnDate!.day}.${returnDate!.month}.${returnDate!.year}'
                                  : 'Učitavanje...',
                              style: const TextStyle(
                                fontFamily: 'AROneSans',
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                          ],
                        ),
                      ),

                      Container(
                        margin: const EdgeInsets.only(top: 4),
                        height: screenHeight * 0.045,
                        decoration: BoxDecoration(
                          color: const Color(0xFF67B1E5),
                          borderRadius: BorderRadius.circular(20),
                        ),
                        child: TextButton(
                          onPressed: () {
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (context) => PhasePaymentPage(
                                  reservation: widget.reservation,
                                ),
                              ),
                            );
                          },
                          style: TextButton.styleFrom(
                            padding: const EdgeInsets.symmetric(horizontal: 14),
                            foregroundColor: Colors.white,
                          ),
                          child: const Text(
                            'detalji uplate rata',
                            style: TextStyle(
                              fontFamily: 'AROneSans',
                              fontSize: 12,
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                        ),
                      ),
                    ],
                  ),

                  SizedBox(height: screenHeight * 0.05),

                  const Text(
                    'Usluga:',
                    style: TextStyle(
                      fontFamily: 'AROneSans',
                      fontSize: 20,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  _uslugaRow('Putovanje', '${widget.price} KM'),

                  _uslugaRow(
                    'Boravišna taksa',
                    '${widget.preview.residenceTaxTotal} KM',
                  ),

                  if (widget.reservation.includeInsurance == true)
                    _uslugaRow(
                      'Putničko zdravstveno osiguranje',
                      '${widget.preview.insurance} KM',
                    ),

                  SizedBox(height: screenHeight * 0.03),

                  Align(
                    alignment: Alignment.centerRight,
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.end,
                      children: [
                        const Text(
                          'UKUPNO:',
                          style: TextStyle(
                            fontFamily: 'AROneSans',
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                        Text(
                          '${widget.reservation.totalPrice} KM',
                          style: const TextStyle(
                            color: Color(0xFF67B1E5),
                            fontFamily: 'AROneSans',
                            fontWeight: FontWeight.bold,
                            fontSize: 30,
                          ),
                        ),
                      ],
                    ),
                  ),

                  SizedBox(height: screenHeight * 0.03),

                  Align(
                    alignment: Alignment.centerRight,
                    child: ElevatedButton(
                      onPressed: _confirmAndCancelReservation,
                      style: ElevatedButton.styleFrom(
                        backgroundColor: const Color(0xFFD62929),
                        foregroundColor: Colors.white,
                      ),
                      child: const Text(
                        'Otkazi rezervaciju',
                        style: TextStyle(fontWeight: FontWeight.bold),
                      ),
                    ),
                  ),
                ],
              ),
            ),
        ],
      ),
    );
  }

  Widget _uslugaRow(String naziv, String cijena) {
    return Container(
      margin: const EdgeInsets.only(top: 20),
      padding: const EdgeInsets.only(bottom: 3),
      decoration: const BoxDecoration(
        border: Border(bottom: BorderSide(color: Color(0xFF67B1E5), width: 2)),
      ),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Text(
            naziv,
            style: const TextStyle(color: Color(0xFF67B1E5), fontSize: 15),
          ),
          Text(
            cijena,
            style: const TextStyle(color: Color(0xFF67B1E5), fontSize: 15),
          ),
        ],
      ),
    );
  }
}
