import 'dart:convert';
import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/age_group_stats_response.dart';
import 'package:etravel_desktop/models/destination_stats_response.dart';
import 'package:etravel_desktop/utils/session.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

class ReportProvider with ChangeNotifier {
  static const String endpoint = "Report";

  Future<List<DestinationStatsResponse>> getTopDestinations() async {
    final url = "${ApiConfig.apiBase}/api/$endpoint/top-destinations";

    final response = await http.get(
      Uri.parse(url),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
    );

    if (response.statusCode < 200 || response.statusCode >= 300) {
      throw Exception("Failed to load report");
    }

    final json = jsonDecode(response.body);

    return DestinationStatsResponse.listFromJson(json);
  }

  Future<List<AgeGroupStatsResponse>> getAgeReport(String range) async {
    final url = Uri.parse(
      "${ApiConfig.apiBase}/api/Report/age?range=$range",
    );

    final response = await http.get(url, headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },);

    if (response.statusCode < 200 || response.statusCode >= 300) {
      throw Exception("Greška u dobijanju age reporta: ${response.body}");
    }

    final data = jsonDecode(response.body);

    // Backend vraća List<AgeGroupStatsResponse>
    return (data as List)
        .map((item) => AgeGroupStatsResponse.fromJson(item))
        .toList();
  }
}
