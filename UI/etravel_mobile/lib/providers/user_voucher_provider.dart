import 'dart:convert';

import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/models/user_voucher.dart';
import 'package:etravel_app/providers/base_provider.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:http/http.dart' as http;

class UserVoucherProvider extends BaseProvider<UserVoucher> {
  UserVoucherProvider() : super("UserVoucher");

  @override
  UserVoucher fromJson(data) {
    return UserVoucher.fromJson(data);
  }

  /// ============================
  /// BUY VOUCHER (POST)
  /// ============================
  Future<void> buyVoucher({
    required Map<String, dynamic> dynamicRequest,
  }) async {
    final url = "${ApiConfig.apiBase}/api/UserVoucher/buy";

    final response = await http.post(
      Uri.parse(url),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
      body: jsonEncode(dynamicRequest),
    );

    if (response.statusCode != 200) {
      throw Exception(
        "Greška pri kupovini vaučera: ${response.body}",
      );
    }
  }

  Future<bool> markAsUsed(int voucherId) async {
    final url =
        "${ApiConfig.apiBase}/api/UserVoucher/mark-as-used";

    final response = await http.put(
      Uri.parse(url),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
      body: jsonEncode({
        "userId": Session.userId,
        "voucherId": voucherId,
      }),
    );

    return response.statusCode == 200;
  }
}
