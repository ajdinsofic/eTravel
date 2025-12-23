import 'dart:convert';

import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/models/reservation_preview.dart';
import 'package:etravel_app/providers/base_provider.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:http/http.dart' as http;

class ReservationPreviewProvider
    extends BaseProvider<ReservationPreview> {

  ReservationPreviewProvider()
      : super("ReservationPreview");

  @override
  ReservationPreview fromJson(data) {
    return ReservationPreview.fromJson(data);
  }

  Future<ReservationPreview> generatePreview({
  required int userId,
  required int offerId,
  required int hotelId,
  required int roomId,
  double? basePrice,
  bool? includeInsurance,
  String? voucherCode,
}) async {
  final Map<String, String> queryParameters = {
    "UserId": userId.toString(),
    "OfferId": offerId.toString(),
    "HotelId": hotelId.toString(),
    "RoomId": roomId.toString(),
  };

  // ✅ dodaj samo ako nije null
  if (basePrice != null) {
    queryParameters["BasePrice"] = basePrice.toInt().toString();
  }

  if (includeInsurance != null) {
    queryParameters["IncludeInsurance"] = includeInsurance.toString();
  }

  if (voucherCode != null && voucherCode.isNotEmpty) {
    queryParameters["VoucherCode"] = voucherCode;
  }

  final uri = Uri.parse(
    "${ApiConfig.apiBase}/generate",
  ).replace(queryParameters: queryParameters);

  final response = await http.get(
    uri,
    headers: {
      "Content-Type": "application/json",
    },
  );

  if (response.statusCode == 200) {
    return ReservationPreview.fromJson(json.decode(response.body));
  } else {
    throw Exception("Greška pri generisanju preview-a: ${response.body}");
  }
}


}
