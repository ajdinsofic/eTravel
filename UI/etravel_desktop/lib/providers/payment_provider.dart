import 'dart:convert';
import 'package:etravel_desktop/providers/base_provider.dart';
import 'package:etravel_desktop/models/payment.dart';
import 'package:etravel_desktop/models/search_provider.dart';
import 'package:http/http.dart' as http;
import '../config/api_config.dart';
import '../utils/session.dart';

class PaymentProvider extends BaseProvider<Payment> {
  PaymentProvider() : super("Payment");

  static const String baseUrl = "${ApiConfig.apiBase}/api/Payment";

  @override
  Payment fromJson(dynamic data) {
    return Payment.fromJson(data);
  }

  Future<Payment> updatePayment(int reservationId, int rateId, Map<String, dynamic> data) async {
  final url = "${ApiConfig.apiBase}/api/Payment/$reservationId/$rateId";

  final response = await http.put(
    Uri.parse(url),
    headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
    body: jsonEncode(data),
  );

  if (response.statusCode != 200) {
      throw Exception("Greška pri ažuriranju dana: ${response.body}");
    }

  return Payment.fromJson(jsonDecode(response.body));
}

}
