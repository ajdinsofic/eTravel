import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/login_request.dart';
import '../models/login_response.dart';
import '../utils/session.dart';

class AuthProvider {
  static const String apiUrl = "http://localhost:5265/api/User/authenticate";

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

      // Provjera privilegija
      final dozvoljeneUloge = ["Radnik", "Direktor"];
      final imaPristup = loginResp.roles.any(
        (rola) => dozvoljeneUloge.contains(rola),
      );

      if (!imaPristup) {
        return "ZABRANJENO"; // korisnik nema privilegiju
      }

      // Ako je sve OK — spremi session
      Session.token = loginResp.token;
      Session.userId = loginResp.userId;
      Session.username = loginResp.username;
      Session.roles = loginResp.roles;

      return "OK";
    }

    if (response.statusCode == 403) {
      return "ZABRANJENO"; // Nema privilegije
    }

    if (response.statusCode == 401) {
      return "NEISPRAVNO"; // Pogrešno korisničko ime ili lozinka
    }

    return "GRESKA";
  }
}
