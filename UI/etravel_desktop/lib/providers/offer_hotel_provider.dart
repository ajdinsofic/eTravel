import 'dart:convert';
import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/offer_hotel.dart';
import '../utils/session.dart';
import 'package:http/http.dart' as http;

import 'base_provider.dart';

class OfferHotelProvider extends BaseProvider<OfferHotel> {
  OfferHotelProvider() : super("OfferHotel");

  @override
  OfferHotel fromJson(dynamic data) {
    return OfferHotel.fromJson(data);
  }

  Future<void> updateOfferHotelDates({
  required int offerId,
  required int hotelId,
  required String departureDateIso,
  required String returnDateIso,
}) async {
  final url = "${ApiConfig.apiBase}/api/OfferHotel/$offerId/$hotelId";

  final payload = {
    "departureDate": departureDateIso,
    "returnDate": returnDateIso,
  };

  final response = await http.put(
    Uri.parse(url),
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer ${Session.token}",
    },
    body: jsonEncode(payload),
  );

  if (response.statusCode != 200) {
    throw Exception(
      "Greška pri Update OfferHotel datuma: ${response.body}",
    );
  }
}
}
