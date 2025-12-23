import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/models/reservations.dart';
import 'package:etravel_app/providers/offer_image_provider.dart';
import 'package:etravel_app/providers/reservation_preview_provider.dart';
import 'package:etravel_app/providers/reservation_provider.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:etravel_app/widgets/profilePageParts/reservationActiveContainer.dart';
import 'package:etravel_app/widgets/profilePageParts/reservationEndContainer.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class ReservationUniversalView extends StatefulWidget {
  final String imeRezervacija;
  final bool activationStatus; // true = aktivne, false = zavrsene

  const ReservationUniversalView({
    super.key,
    required this.imeRezervacija,
    required this.activationStatus,
  });

  @override
  State<ReservationUniversalView> createState() =>
      _ReservationUniversalViewState();
}

class _ReservationUniversalViewState extends State<ReservationUniversalView> {
  late ReservationProvider _reservationProvider;
  late ReservationPreviewProvider _previewProvider;
  late OfferImageProvider _offerImageProvider;

  bool isLoading = true;

  List<Reservation> reservations = [];

  // key = reservationId, value = calculated price
  final Map<int, double> calculatedPrices = {};

  // key = offerId, value = imageUrl
  final Map<int, String> offerImages = {};

  // key = reservationId, value = preview response
  final Map<int, dynamic> reservationPreviews = {};

  @override
  void initState() {
    super.initState();

    _reservationProvider = Provider.of<ReservationProvider>(
      context,
      listen: false,
    );
    _previewProvider = Provider.of<ReservationPreviewProvider>(
      context,
      listen: false,
    );
    _offerImageProvider = Provider.of<OfferImageProvider>(
      context,
      listen: false,
    );

    _loadReservations();
  }

  Future<void> _loadReservations() async {
    try {
      final result = await _reservationProvider.get(
        filter: {"userId": Session.userId, "isActive": widget.activationStatus},
      );

      reservations = result.items;

      for (final reservation in reservations) {
        // -----------------------------
        // 1️⃣ PREVIEW
        // -----------------------------
        final preview = await _previewProvider.generatePreview(
          userId: Session.userId!,
          offerId: reservation.offerId,
          hotelId: reservation.hotelId,
          roomId: reservation.roomId,
        );

        reservationPreviews[reservation.id!] = preview;

        // -----------------------------
        // 2️⃣ CIJENA (bez tax & insurance)
        // -----------------------------
        double price = reservation.totalPrice;
        price -= preview.residenceTaxTotal;

        if (reservation.includeInsurance == true) {
          price -= preview.insurance;
        }

        calculatedPrices[reservation.id!] = price;

        // -----------------------------
        // 3️⃣ SLIKA PONUDE
        // -----------------------------
        final image = await _offerImageProvider.getById(reservation.offerId);

        offerImages[reservation.offerId] = image.imageUrl;
      }
    } catch (e) {
      debugPrint("Greška pri učitavanju rezervacija: $e");
    }

    setState(() => isLoading = false);
  }

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    return Container(
      width: screenWidth * 0.95,
      decoration: BoxDecoration(
        border: Border.all(color: const Color(0xFF67B1E5)),
        borderRadius: BorderRadius.circular(20),
      ),
      child: Column(
        children: [
          //-----------------------------------------
          // NASLOV SEKCIJE
          //-----------------------------------------
          Container(
            width: screenWidth,
            height: screenHeight * 0.1,
            alignment: Alignment.center,
            child: Text(
              widget.imeRezervacija,
              style: TextStyle(
                fontFamily: 'AROneSans',
                fontWeight: FontWeight.bold,
                fontSize: screenWidth * 0.06,
              ),
            ),
          ),

          SizedBox(height: screenHeight * 0.03),

          //-----------------------------------------
          // LISTA REZERVACIJA
          //-----------------------------------------
          if (isLoading)
            const Padding(
              padding: EdgeInsets.all(16),
              child: CircularProgressIndicator(),
            )
          else
            Column(
              children:
                  reservations.map((reservation) {
                    final imageUrl = offerImages[reservation.offerId] ?? '';

                    final price = calculatedPrices[reservation.id] ?? 0;

                    final preview = reservationPreviews[reservation.id];

                    return Column(
                      children: [
                        widget.activationStatus
                            ? ReservationActiveContainer(
                              reservation: reservation,
                              preview: preview,
                              imageUrl: "${ApiConfig.imagesOffers}/$imageUrl",
                              price: price,
                              onCancelled: () {
                                _loadReservations(); // refresh nakon otkazivanja
                              },
                            )
                            : ReservationEndContainer(
                              reservation: reservation,
                              preview: preview,
                              imageUrl: "${ApiConfig.imagesOffers}/$imageUrl",
                              onCommentSent: () {
                                _loadReservations(); // refresh završenih
                              },
                            ),
                        SizedBox(height: screenHeight * 0.01),
                      ],
                    );
                  }).toList(),
            ),
        ],
      ),
    );
  }
}
