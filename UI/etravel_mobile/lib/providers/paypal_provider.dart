import 'dart:convert';

import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/models/paypal_order_response.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

import '../models/paypal_order.dart';
import 'base_provider.dart';

class PayPalProvider extends BaseProvider<PayPalOrder> {
  PayPalProvider() : super("paypal");

  @override
  PayPalOrder fromJson(dynamic json) {
    return PayPalOrder.fromJson(json);
  }

  Future<PayPalOrderResponse?> createPayPalOrder(double amount) async {
    final url = "${ApiConfig.apiBase}/api/paypal/create-order";

    final response = await http.post(
      Uri.parse(url),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
      body: jsonEncode({"amount": amount}),
    );

    if (response.statusCode == 200) {
      final json = jsonDecode(response.body);
      return PayPalOrderResponse.fromJson(json);
    }

    debugPrint(
      "Create PayPal order greška: ${response.statusCode} | ${response.body}",
    );
    return null;
  }

  Future<bool> capturePayPalOrder(String orderId) async {
    final url = "${ApiConfig.apiBase}/api/paypal/capture";

    final response = await http.post(
      Uri.parse(url),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
      body: jsonEncode({"orderId": orderId}),
    );

    if (response.statusCode == 200) {
      return true;
    }

    debugPrint(
      "Capture PayPal order greška: ${response.statusCode} | ${response.body}",
    );
    return false;
  }
}
