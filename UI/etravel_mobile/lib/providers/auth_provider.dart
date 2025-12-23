import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/login_request.dart';
import '../models/login_response.dart';
import '../utils/session.dart';
import '../config/api_config.dart';

class AuthProvider {
  static final String apiUrl =
      "${ApiConfig.apiBase}/api/User/authenticate";

  Future<String> prijava(LoginRequest request) async {
    final url = Uri.parse(apiUrl);

    final response = await http.post(
      url,
      headers: {"Content-Type": "application/json"},
      body: jsonEncode(request.toJson()),
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);
      final loginResp = LoginResponse.fromJson(data);

      // Dozvoljene uloge
      final dozvoljeneUloge = ["Korisnik"];
      final imaPristup = loginResp.roles.any(
        (rola) => dozvoljeneUloge.contains(rola),
      );

      if (!imaPristup) {
        return "ZABRANJENO";
      }

      // Spremi session
      Session.token = loginResp.token;
      Session.userId = loginResp.userId;
      Session.username = loginResp.username;
      Session.roles = loginResp.roles;

      return "OK";
    }

    if (response.statusCode == 401) {
      return "NEISPRAVNO";
    }

    if (response.statusCode == 403) {
      return "ZABRANJENO";
    }

    return "GRESKA";
  }
}
