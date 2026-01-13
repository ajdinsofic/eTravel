import 'dart:convert';
import 'package:etravel_desktop/providers/base_provider.dart';
import 'package:etravel_desktop/providers/paged_result.dart';
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
