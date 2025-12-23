import 'dart:convert';
import 'package:etravel_app/providers/base_provider.dart';
import 'package:http/http.dart' as http;

import '../config/api_config.dart';
import '../utils/session.dart';
import '../models/user_role.dart';

class UserRoleProvider extends BaseProvider<UserRole> {
  UserRoleProvider() : super("UserRole");

  static const String baseUrl = "${ApiConfig.apiBase}/api/UserRole";

  @override
  UserRole fromJson(dynamic data) {
    return UserRole.fromJson(data);
  }

  Future<bool> deleteComposite(int userId, int roleId) async {
  final url = Uri.parse(
      "$baseUrl/deleteComposite/$userId/$roleId");

  final response = await http.delete(
    url,
    headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
  );

  if (response.statusCode == 200) {
    return true;
  } else {
    throw Exception("Greška pri uklanjanju role: ${response.body}");
  }
}

}
