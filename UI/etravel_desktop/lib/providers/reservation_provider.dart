
import 'dart:convert';
import 'dart:typed_data';

import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/reservations.dart';
import 'package:etravel_desktop/providers/base_provider.dart';
import 'package:etravel_desktop/utils/session.dart';
import 'package:http/http.dart' as http;

class ReservationProvider extends BaseProvider<Reservation> {
  ReservationProvider() : super("Reservation");

  @override
  Reservation fromJson(dynamic data) {
    return Reservation.fromJson(data);
  }

  Future<Uint8List> generateInvoice(dynamic request) async {
  final response = await http.post(
    Uri.parse("${ApiConfig.apiBase}/api/Reservation/generate"),
    headers: {
      "Authorization": "Bearer ${Session.token}",
      "Content-Type": "application/json",
      "Accept": "application/pdf",
    },
    body: jsonEncode(request),
  );

  if (response.statusCode != 200) {
    throw Exception("Neuspješno generisanje računa");
  }

  return response.bodyBytes;
}



}
