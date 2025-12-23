import 'dart:io';
import 'dart:typed_data';

import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/models/work_application.dart';
import 'package:etravel_app/models/work_application_insert.dart';
import 'package:etravel_app/providers/base_provider.dart';
import 'package:etravel_app/utils/session.dart';
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

  Future<void> insertApplication(WorkApplicationInsert request) async {
    final uri = Uri.parse(
      "${ApiConfig.apiBase}/api/WorkApplication/insertCV",
    );

    final multipartRequest = http.MultipartRequest("POST", uri);

    multipartRequest.headers.addAll({
      "Authorization": "Bearer ${Session.token}",
    });

    multipartRequest.fields.addAll(request.toFields());

    multipartRequest.files.add(
      await http.MultipartFile.fromPath(
        "CvFile",
        request.cvFile.path,
      ),
    );

    final response = await multipartRequest.send();

    if (response.statusCode != 200) {
      throw Exception("Greška pri slanju prijave za posao");
    }
  }

}
