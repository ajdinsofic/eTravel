import 'dart:convert';
import 'dart:io';
import 'dart:typed_data';
import 'package:etravel_app/models/user_image_request.dart';
import 'package:etravel_app/providers/base_provider.dart';
import 'package:http/http.dart' as http;

import '../config/api_config.dart';
import '../utils/session.dart';
import '../models/user.dart';

class UserProvider extends BaseProvider<User> {
  UserProvider() : super("User");

  @override
  User fromJson(dynamic data) {
    return User.fromJson(data);
  }

  Future<void> unblockUser(int userId) async {
  final url = "${ApiConfig.apiBase}/api/User/unblock/$userId";

  final response = await http.put(
    Uri.parse(url),
    headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
  );

 if (response.statusCode != 200) {
      throw Exception("Greška pri ažuriranju dana: ${response.body}");
    }
}

Future<void> blockUser(int userId) async {
  final url = "${ApiConfig.apiBase}/api/User/block/$userId";

  final response = await http.put(
    Uri.parse(url),
    headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
  );

 if (response.statusCode != 200) {
      throw Exception("Greška pri ažuriranju dana: ${response.body}");
    }
}

Future<bool> addUserImage(UserImageRequest req) async {
  final url = Uri.parse("${ApiConfig.apiBase}/api/User/userImage");

  var request = http.MultipartRequest("POST", url);
  request.headers["Authorization"] = "Bearer ${Session.token}";

  // POLJA IZ FORME
  request.fields["userId"] = req.userId.toString();

  // FAJL
  request.files.add(
    await http.MultipartFile.fromPath(
      "image",
      req.image.path,
      filename: "user_${DateTime.now().millisecondsSinceEpoch}.jpg",
    ),
  );

  final response = await request.send();
  final body = await response.stream.bytesToString();

  if (response.statusCode != 200) {
    throw Exception("Greška upload slike: $body");
  }

  return true;
}

Future<bool> deleteUserImage(int userId) async {
  final url = Uri.parse("${ApiConfig.apiBase}/api/User/$userId/delete-image");

  final response = await http.delete(
    url,
    headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
  );

  if (response.statusCode != 200) {
    throw Exception("Greška pri brisanju slike: ${response.body}");
  }

  return true;
}

Future<bool> checkCurrentPassword(dynamic filter) async {
  final url = Uri.parse("${ApiConfig.apiBase}/api/User/check-password");

  final response = await http.post(
    url,
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer ${Session.token}",
    },
    body: jsonEncode({
      "userId": filter["userId"],
      "currentPassword": filter["currentPassword"],
    }),
  );

  if (response.statusCode != 200) {
    throw Exception("Greška pri provjeri lozinke: ${response.body}");
  }

  final json = jsonDecode(response.body);
  return json["isValid"] == true;
}


Future<bool> updateNewPassword(dynamic request) async {
  final url = Uri.parse("${ApiConfig.apiBase}/api/User/update-password");

  final response = await http.put(
    url,
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer ${Session.token}",
    },
    body: jsonEncode({
      "userId": request["userId"],
      "newPassword": request["newPassword"],
    }),
  );

  if (response.statusCode != 200) {
    throw Exception("Greška pri promjeni lozinke: ${response.body}");
  }

  return true;
}

Future<bool> forgotPassword(String email) async {
  final url = Uri.parse(
    "${ApiConfig.apiBase}/api/User/forgot-password",
  );

  final response = await http.post(
    url,
    headers: {
      "Content-Type": "application/json",
    },
    body: jsonEncode({
      "email": email,
    }),
  );

  // Backend uvijek vraća 200 (security razlog)
  if (response.statusCode != 200) {
    throw Exception(
      "Greška pri slanju reset emaila: ${response.body}",
    );
  }

  return true;
}

Future<bool> resetPassword({
  required String token,
  required String newPassword,
}) async {
  final url = Uri.parse(
    "${ApiConfig.apiBase}/api/User/reset-password",
  );

  final response = await http.post(
    url,
    headers: {
      "Content-Type": "application/json",
    },
    body: jsonEncode({
      "token": token,
      "newPassword": newPassword,
    }),
  );

  if (response.statusCode != 200) {
    throw Exception(
      "Greška pri resetovanju lozinke: ${response.body}",
    );
  }

  return true;
}




}
