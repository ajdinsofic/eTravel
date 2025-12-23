import 'dart:convert';
import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/models/offer_hotel.dart';

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
    required int? offerId,
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
      throw Exception("Greška pri Update OfferHotel datuma: ${response.body}");
    }
  }

  /// GET /api/offerHotel/{offerId}/{hotelId}
  Future<OfferHotel> getByOfferAndHotel(int offerId, int hotelId) async {
    final uri = Uri.parse(
      '${ApiConfig.apiBase}/api/offerHotel/$offerId/$hotelId',
    );

    final response = await http.get(
      uri,
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
    );

    if (response.statusCode < 200 || response.statusCode >= 300) {
      throw Exception('Greška: ${response.body}');
    }

    final data = jsonDecode(response.body);
    return OfferHotel.fromJson(data);
  }
}
