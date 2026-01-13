import 'dart:convert';


import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/user_token.dart';
import 'package:etravel_desktop/providers/base_provider.dart';
import 'package:etravel_desktop/utils/session.dart';
import 'package:http/http.dart' as http;

class UserTokenProvider extends BaseProvider<UserToken> {
  UserTokenProvider() : super("UserToken");

  @override
  UserToken fromJson(dynamic json) {
    return UserToken.fromJson(json);
  }


Future<UserToken> decreaseTokens(int userId) async {
  final url = Uri.parse(
    "${ApiConfig.apiBase}/api/UserToken/$userId/decrease",
  );

  final response = await http.post(
    url,
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer ${Session.token}",
    },
  );

  if (response.statusCode == 200) {
    return UserToken.fromJson(jsonDecode(response.body));
  } else {
    throw Exception("Nedovoljno tokena ili greška: ${response.body}");
  }
}

/// ============================
/// +10 TOKENA
/// POST /UserToken/{userId}/increase
/// ============================
Future<UserToken> increaseTokens(int userId) async {
  final url = Uri.parse(
    "${ApiConfig.apiBase}/api/UserToken/$userId/increase",
  );

  final response = await http.post(
    url,
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer ${Session.token}",
    },
  );

  if (response.statusCode == 200) {
    return UserToken.fromJson(jsonDecode(response.body));
  } else {
    throw Exception("Greška pri povećanju tokena: ${response.body}");
  }
}


}
