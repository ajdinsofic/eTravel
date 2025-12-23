import 'dart:convert';
import 'dart:io';
import 'package:etravel_app/models/offer_image.dart';
import 'package:etravel_app/models/offer_image_insert.dart';
import 'package:etravel_app/models/offer_image_update.dart';
import 'package:http/http.dart' as http;
import '../utils/session.dart';
import '../config/api_config.dart';

import 'base_provider.dart';

class OfferImageProvider extends BaseProvider<OfferImage> {
  OfferImageProvider() : super("OfferImage");

  @override
  OfferImage fromJson(dynamic data) {
    return OfferImage.fromJson(data);
  }

  Future<OfferImage> insertImage(OfferImageInsertRequest req) async {
    final uri = Uri.parse("${ApiConfig.apiBase}/api/OfferImage");

    var request = http.MultipartRequest("POST", uri);

    request.headers["Authorization"] = "Bearer ${Session.token}";
    request.fields["offerId"] = req.offerId.toString();
    request.fields["isMain"] = req.isMain.toString();

    request.files.add(
      await http.MultipartFile.fromPath("image", req.image.path),
    );

    final response = await request.send();
    final body = await response.stream.bytesToString();

    if (response.statusCode != 200) {
      throw Exception("Greška pri uploadu slike: $body");
    }

    final json = jsonDecode(body);
  
    return OfferImage.fromJson(json);

  }

  Future<OfferImage> updateImage(int imageId, OfferImageUpdateRequest request) async {
    final url = "${ApiConfig.apiBase}/api/$endpoint/$imageId";

    final response = await http.put(
      Uri.parse(url),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
      body: jsonEncode(request.toJson()),
    );

    if (response.statusCode != 200) {
      throw Exception("Greška pri update slike: ${response.body}");
    }

    return fromJson(jsonDecode(response.body));
  }


}
