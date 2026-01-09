import 'dart:convert';

import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/models/reservations.dart';
import 'package:etravel_app/providers/base_provider.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:http/http.dart' as http;

class ReservationProvider extends BaseProvider<Reservation> {
  ReservationProvider() : super("Reservation");

  @override
  Reservation fromJson(dynamic data) {
    return Reservation.fromJson(data);
  }

  Future<void> confirmReservation(dynamic request) async {
  final url =
      Uri.parse("${ApiConfig.apiBase}/api/Reservation/confirm");

  final response = await http.post(
    url,
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer ${Session.token}",
    },
    body: jsonEncode(request),
  );

  if (response.statusCode != 200) {
    throw Exception("Greška pri potvrdi rezervacije");
  }
}

}
