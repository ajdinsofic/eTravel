import 'dart:convert';
import 'package:etravel_desktop/providers/paged_result.dart';
import 'package:http/http.dart' as http;
import '../utils/session.dart';
import '../models/offer.dart';

class OfferProvider {
  static const String baseUrl = "http://localhost:5265/api/Offer";

  Future<PagedResult<Offer>> getOffers({
    int page = 0,
    int pageSize = 5,
    bool isMainImage = true,
  }) async {
    final uri = Uri.parse(
      "$baseUrl?page=$page&pageSize=$pageSize&isMainImage=$isMainImage",
    );

    final response = await http.get(
      uri,
      headers: {
        "Authorization": "Bearer ${Session.token}",
        "Content-Type": "application/json"
      },
    );

    if (response.statusCode == 200) {
      final json = jsonDecode(response.body);

      return PagedResult.fromJson(
        json,
        (map) => Offer.fromJson(map),
      );
    }

    throw Exception(
        "Greška pri dohvaćanju ponuda: ${response.statusCode}  ${response.body}");
  }
}
