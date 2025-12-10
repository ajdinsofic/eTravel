import 'package:etravel_desktop/models/reservations.dart';

class ActiveReservationDisplay {
  final Reservation reservation;
  final String title;
  final String imageUrl;

  ActiveReservationDisplay({
    required this.reservation,
    required this.title,
    required this.imageUrl,
  });
}
