import 'dart:convert';
import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/models/hotel.dart';
import 'package:etravel_app/models/recommended_hotel.dart';
import '../utils/session.dart';
import 'package:http/http.dart' as http;

import 'base_provider.dart';

class HotelProvider extends BaseProvider<Hotel> {
  HotelProvider() : super("Hotel");

  @override
  Hotel fromJson(dynamic data) {
    return Hotel.fromJson(data);
  }

  Future<RecommendedHotel?> getRecommendedHotelRoomForOffer({
    required int offerId,
    required int userId,
  }) async {
    final url =
        "${ApiConfig.apiBase}/api/Hotel/offers/$offerId/recommendedHotel?userId=$userId";

    final response = await http.get(
      Uri.parse(url),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);
      return RecommendedHotel.fromJson(data);
    }

    if (response.statusCode == 404) {
      return null; 
    }

    throw Exception(
        "Greška pri dohvaćanju preporučenog hotela: ${response.body}");
  }
}
