import 'package:json_annotation/json_annotation.dart';

part 'user_token.g.dart';

@JsonSerializable()
class UserToken {
  final int userId;
  final int equity;

  UserToken({
    required this.userId,
    required this.equity,
  });

  factory UserToken.fromJson(Map<String, dynamic> json) =>
      _$UserTokenFromJson(json);

  Map<String, dynamic> toJson() => _$UserTokenToJson(this);
}
