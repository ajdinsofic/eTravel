import 'dart:convert';
import 'package:etravel_app/providers/base_provider.dart';
import 'package:http/http.dart' as http;
import '../utils/session.dart';
import '../models/offer.dart';
import '../config/api_config.dart';

class OfferProvider extends BaseProvider<Offer> {
  OfferProvider() : super("Offer");

  @override
  Offer fromJson(dynamic data) {
    return Offer.fromJson(data);
  }

  Future<void> increaseTotalReservation(int offerId) async {
  final url =
      "${ApiConfig.apiBase}/api/Offer/$offerId/increaseTotalReservation";

  final response = await http.put(
    Uri.parse(url),
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer ${Session.token}",
    },
  );

  if (response.statusCode != 200) {
    throw Exception(
      "Greška pri povećanju broja rezervacija: ${response.body}",
    );
  }
}

Future<List<Offer>> getRecommendedForUser(int userId) async {
  final url =
      "${ApiConfig.apiBase}/api/Offer/recommended/OffersForUser/$userId";

  final response = await http.get(
    Uri.parse(url),
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer ${Session.token}",
    },
  );

  if (response.statusCode != 200) {
    throw Exception("Greška pri učitavanju preporučenih destinacija");
  }

  final data = jsonDecode(response.body) as List;
  return data.map((e) => Offer.fromJson(e)).toList();
}


Future<void> decreaseTotalReservation(int offerId) async {
  final url =
      "${ApiConfig.apiBase}/api/Offer/$offerId/decreaseTotalReservation";

  final response = await http.put(
    Uri.parse(url),
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer ${Session.token}",
    },
  );

  if (response.statusCode != 200) {
    throw Exception(
      "Greška pri smanjenju broja rezervacija: ${response.body}",
    );
  }
}
}
