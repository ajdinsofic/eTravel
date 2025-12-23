class LoginResponse {
  int userId;
  String username;
  String token;
  List<String> roles;

  LoginResponse({
    required this.userId,
    required this.username,
    required this.token,
    required this.roles,
  });

  factory LoginResponse.fromJson(Map<String, dynamic> json) {
    return LoginResponse(
      userId: json["userid"],
      username: json["username"],
      token: json["token"],
      roles: List<String>.from(json["roles"]),
    );
  }
}
