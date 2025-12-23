


import 'package:etravel_app/models/reservations.dart';
import 'package:etravel_app/providers/base_provider.dart';

class ReservationProvider extends BaseProvider<Reservation> {
  ReservationProvider() : super("Reservation");

  @override
  Reservation fromJson(dynamic data) {
    return Reservation.fromJson(data);
  }
}
