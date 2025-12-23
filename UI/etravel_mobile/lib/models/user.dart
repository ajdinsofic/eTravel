import 'package:json_annotation/json_annotation.dart';

part 'user.g.dart';

@JsonSerializable()
class User {
  final int id;
  final String firstName;
  final String lastName;
  final String email;
  String username;
  final String? mainImage;
  final DateTime createdAt;
  final DateTime? lastLoginAt;
  final String phoneNumber;
  final String? token;
  final bool isBlocked;
  final DateTime? dateBirth;
  String? imageUrl;

  User({
    required this.id,
    required this.firstName,
    required this.lastName,
    required this.email,
    required this.username,
    this.mainImage,
    required this.createdAt,
    this.lastLoginAt,
    required this.phoneNumber,
    this.token,
    required this.isBlocked,
    required this.dateBirth,
    this.imageUrl,
  });

  factory User.fromJson(Map<String, dynamic> json) =>
      _$UserFromJson(json);

  Map<String, dynamic> toJson() => _$UserToJson(this);
}
