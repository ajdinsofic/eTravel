import 'dart:convert';
import 'dart:io';
import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/hotel_image.dart';
import 'package:etravel_desktop/models/hotel_image_insert.dart';
import 'package:etravel_desktop/utils/session.dart';
import 'package:http/http.dart' as http;

import 'base_provider.dart';

class HotelImageProvider extends BaseProvider<HotelImage> {
  HotelImageProvider() : super("HotelImage");

  @override
  HotelImage fromJson(dynamic data) {
    return HotelImage.fromJson(data);
  }

  Future<void> insertHotelImage(HotelImageInsertRequest req) async {
    final uri = Uri.parse("${ApiConfig.apiBase}/api/HotelImage");

    final request = http.MultipartRequest("POST", uri);

    request.fields["hotelId"] = req.hotelId.toString();
    request.fields["isMain"] = req.isMain.toString();

    // attach image file
    request.files.add(
      await http.MultipartFile.fromPath(
        "image",
        req.image!.path,
      ),
    );

    request.headers["Authorization"] = "Bearer ${Session.token}";

    final streamed = await request.send();
    final response = await http.Response.fromStream(streamed);

    if (response.statusCode != 200) {
      throw Exception("Greška pri uploadu slike: ${response.body}");
    }
  }
}
