import 'dart:typed_data';

import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/search_provider.dart';
import 'package:etravel_desktop/models/work_application.dart';
import 'package:etravel_desktop/providers/base_provider.dart';
import 'package:etravel_desktop/utils/session.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

class WorkApplicationProvider extends BaseProvider<WorkApplication> {
  WorkApplicationProvider() : super("WorkApplication");
  


  @override
  WorkApplication fromJson(data) {
    return WorkApplication.fromJson(data);
  }

  Future<Uint8List?> downloadCv(int id) async {
    final url = "${ApiConfig.apiBase}/api/WorkApplication/$id/download-cv";

    final response = await http.get(
      Uri.parse(url),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
    );

    if (response.statusCode == 200) {
      return response.bodyBytes;
    }

    debugPrint("Download greska: ${response.statusCode}");
    return null;
  }

  Future<void> inviteToInterview(int applicationId) async {
  final url = Uri.parse(
    "${ApiConfig.apiBase}/api/WorkApplication/$applicationId/invite",
  );

  final response = await http.post(
    url,
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer ${Session.token}",
    },
  );

  if (response.statusCode != 200) {
    throw Exception("Greška pri slanju poziva");
  }
}


}
