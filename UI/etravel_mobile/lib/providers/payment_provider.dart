import 'dart:convert';
import 'package:etravel_app/models/payment.dart';
import 'package:etravel_app/models/payment_summary.dart';
import 'package:etravel_app/providers/base_provider.dart';
import 'package:http/http.dart' as http;
import '../config/api_config.dart';
import '../utils/session.dart';

class PaymentProvider extends BaseProvider<Payment> {
  PaymentProvider() : super("Payment");

  static const String baseUrl = "${ApiConfig.apiBase}/api/Payment";
  PaymentSummary? paymentSummary;
  bool isLoading = false;

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

Future<PaymentSummary> getPaymentSummary(int reservationId) async {
  final uri = Uri.parse(
    '${ApiConfig.apiBase}/api/Payment/summary/$reservationId',
  );

  final response = await http.get(
    uri,
    headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
  );

  if (response.statusCode == 200) {
    final data = json.decode(response.body);
    return PaymentSummary.fromJson(data);
  } else {
    throw Exception(
      'Greška pri učitavanju PaymentSummary: ${response.body}',
    );
  }
}


  void clear() {
    paymentSummary = null;
    notifyListeners();
  }

}
