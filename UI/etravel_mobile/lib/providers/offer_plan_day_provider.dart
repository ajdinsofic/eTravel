import 'dart:convert';
import 'package:etravel_app/models/offer_plan_day.dart';
import 'package:etravel_app/providers/base_provider.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

import '../config/api_config.dart';
import '../utils/session.dart';

class OfferPlanDayProvider extends BaseProvider<OfferPlanDay> {
  OfferPlanDayProvider() : super("OfferPlanDay");

  @override
  OfferPlanDay fromJson(item) {
    return OfferPlanDay.fromJson(item);
  }

  Future<void> deleteDay(int offerId, int dayNumber) async {
    final url = Uri.parse(
      "${ApiConfig.apiBase}/api/OfferPlanDay/$offerId/$dayNumber",
    );

    final response = await http.delete(
      url,
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
    );

    if (response.statusCode != 200) {
      throw Exception("Greška pri brisanju dana: ${response.body}");
    }
  }

  Future<void> updateDay(
    int offerDetailsId,
    int dayNumber,
    dynamic request,
  ) async {
    final url =
        "${ApiConfig.apiBase}/api/OfferPlanDay/$offerDetailsId/$dayNumber";

    final response = await http.put(
      Uri.parse(url),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
      body: jsonEncode(request),
    );

    if (response.statusCode != 200) {
      throw Exception("Greška pri ažuriranju dana: ${response.body}");
    }
  }
}
