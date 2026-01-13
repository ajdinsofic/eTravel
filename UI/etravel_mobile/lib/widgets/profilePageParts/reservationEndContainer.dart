import 'package:etravel_app/helper/image_helper.dart';
import 'package:etravel_app/models/reservation_preview.dart';
import 'package:etravel_app/models/reservations.dart';
import 'package:etravel_app/providers/comment_provider.dart';
import 'package:etravel_app/providers/reservation_provider.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class ReservationEndContainer extends StatefulWidget {
  final Reservation reservation;
  final ReservationPreview preview;
  final String imageUrl;
  final VoidCallback onCommentSent;

  const ReservationEndContainer({
    super.key,
    required this.imageUrl,
    required this.preview,
    required this.onCommentSent,
    required this.reservation,
  });

  @override
  State<ReservationEndContainer> createState() =>
      _ReservationEndContainerState();
}

class _ReservationEndContainerState extends State<ReservationEndContainer> {
  bool prikaziDetalje = false;
  int selectedStars = 0; // 0–5

  late CommentProvider _commentProvider;
  late ReservationProvider _reservationProvider;

  final TextEditingController _commentController = TextEditingController();

  String? commentError;
  String? starError;

  @override
  void initState() {
    super.initState();
    _commentProvider = Provider.of<CommentProvider>(context, listen: false);
    _reservationProvider = Provider.of<ReservationProvider>(
      context,
      listen: false,
    );
  }

  @override
  void dispose() {
    _commentController.dispose();
    super.dispose();
  }

  void _showReservationDeletedToast(BuildContext context) {
  final overlay = Overlay.of(context);
  if (overlay == null) return;

  late OverlayEntry entry;

  entry = OverlayEntry(
    builder: (_) => Positioned(
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
              color: const Color(0xFFD62929), // 🔴 crvena
              borderRadius: BorderRadius.circular(10),
            ),
            child: const Text(
              "Putovanje je uspješno uklonjeno",
              style: TextStyle(
                color: Colors.white,
                fontSize: 15,
                fontWeight: FontWeight.bold,
              ),
            ),
          ),
        ),
      ),
    ),
  );

  overlay.insert(entry);

  Future.delayed(const Duration(seconds: 3), () {
    entry.remove();
  });
}


  Future<void> _sendComment() async {
    setState(() {
      commentError = null;
      starError = null;

      if (selectedStars == 0) {
        starError = 'Molimo odaberite broj zvjezdica.';
      }

      if (_commentController.text.trim().isEmpty) {
        commentError = 'Molimo unesite komentar.';
      }
    });

    if (commentError != null || starError != null) return;

    try {
      await _commentProvider.insert({
        "userId": Session.userId,
        "offerId": widget.reservation.offerId,
        "comment": _commentController.text.trim(),
        "starRate": selectedStars,
      });

      // ✅ USPJEH POPUP
      final bool? shouldClose = await showDialog<bool>(
        context: context,
        builder: (context) {
          return AlertDialog(
            title: const Text(
              'Hvala na komentaru',
              style: TextStyle(fontWeight: FontWeight.bold),
              textAlign: TextAlign.center,
            ),
            content: const Text(
              'Hvala vam što ste podijelili svoje iskustvo.\n\n'
              'Završeno putovanje će biti uklonjeno iz liste.',
              textAlign: TextAlign.center,
            ),
            actions: [
              ElevatedButton(
                onPressed: () => Navigator.pop(context, true),
                style: ElevatedButton.styleFrom(
                  backgroundColor: const Color(0xFF67B1E5),
                ),
                child: const Text('U redu', style: TextStyle(color: Colors.white),),
              ),
            ],
          );
        },
      );

      if (shouldClose == true) {
        // 🗑️ obriši završenu rezervaciju
        await _reservationProvider.delete(widget.reservation.id!);

        _showReservationDeletedToast(context);

        // 🔄 refresh parent widgeta
        widget.onCommentSent();
      }
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('Greška pri slanju komentara: $e'),
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
          //-----------------------------------------
          // GORNJI RED
          //-----------------------------------------
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: [
              CircleAvatar(
                radius: screenWidth * 0.08,
                backgroundImage: NetworkImage(resolveOfferImageUrl(widget.imageUrl)),
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

              //-----------------------------------------
              // TOGGLE
              //-----------------------------------------
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

          //-----------------------------------------
          // DETALJI
          //-----------------------------------------
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

                  const Text(
                    'Vaše iskustvo nam je važno. Ostavite komentar:',
                    textAlign: TextAlign.center,
                    style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontFamily: 'AROneSans',
                    ),
                  ),
                  const SizedBox(height: 10),

                  //-----------------------------------------
                  // KOMENTAR
                  //-----------------------------------------
                  TextField(
                    controller: _commentController,
                    maxLines: 4,
                    decoration: InputDecoration(
                      hintText: 'Unesite komentar...',
                      border: const OutlineInputBorder(),
                      errorText: commentError,
                    ),
                  ),

                  const SizedBox(height: 16),

                  //-----------------------------------------
                  // ZVIJEZDICE
                  //-----------------------------------------
                  const Text(
                    'Unesite broj zvijezda:',
                    style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontFamily: 'AROneSans',
                    ),
                  ),

                  const SizedBox(height: 6),

                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Row(
                        children: List.generate(5, (index) {
                          final starIndex = index + 1;

                          return GestureDetector(
                            onTap: () {
                              setState(() {
                                selectedStars = starIndex;
                                starError = null;
                              });
                            },
                            child: Icon(
                              starIndex <= selectedStars
                                  ? Icons.star
                                  : Icons.star_border,
                              color: const Color(0xFFDAB400),
                              size: 28,
                            ),
                          );
                        }),
                      ),

                      //-----------------------------------------
                      // POŠALJI
                      //-----------------------------------------
                      ElevatedButton(
                        onPressed: _sendComment,
                        style: ElevatedButton.styleFrom(
                          backgroundColor: const Color(0xFF67B1E5),
                        ),
                        child: const Text(
                          'Pošalji',
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                    ],
                  ),

                  //-----------------------------------------
                  // ERROR ZA ZVIJEZDICE
                  //-----------------------------------------
                  if (starError != null)
                    Padding(
                      padding: const EdgeInsets.only(top: 6),
                      child: Text(
                        starError!,
                        style: const TextStyle(color: Colors.red, fontSize: 12),
                      ),
                    ),
                ],
              ),
            ),
        ],
      ),
    );
  }
}
