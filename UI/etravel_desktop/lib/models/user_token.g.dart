// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_token.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserToken _$UserTokenFromJson(Map<String, dynamic> json) => UserToken(
  userId: (json['userId'] as num).toInt(),
  equity: (json['equity'] as num).toInt(),
);

Map<String, dynamic> _$UserTokenToJson(UserToken instance) => <String, dynamic>{
  'userId': instance.userId,
  'equity': instance.equity,
};
