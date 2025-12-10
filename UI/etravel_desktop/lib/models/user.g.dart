// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

User _$UserFromJson(Map<String, dynamic> json) => User(
  id: (json['id'] as num).toInt(),
  firstName: json['firstName'] as String,
  lastName: json['lastName'] as String,
  email: json['email'] as String,
  username: json['username'] as String,
  mainImage: json['mainImage'] as String?,
  createdAt: DateTime.parse(json['createdAt'] as String),
  lastLoginAt:
      json['lastLoginAt'] == null
          ? null
          : DateTime.parse(json['lastLoginAt'] as String),
  phoneNumber: json['phoneNumber'] as String,
  token: json['token'] as String?,
  isBlocked: json['isBlocked'] as bool,
  dateBirth:
      json['dateBirth'] == null
          ? null
          : DateTime.parse(json['dateBirth'] as String),
  imageUrl: json['imageUrl'] as String?,
);

Map<String, dynamic> _$UserToJson(User instance) => <String, dynamic>{
  'id': instance.id,
  'firstName': instance.firstName,
  'lastName': instance.lastName,
  'email': instance.email,
  'username': instance.username,
  'mainImage': instance.mainImage,
  'createdAt': instance.createdAt.toIso8601String(),
  'lastLoginAt': instance.lastLoginAt?.toIso8601String(),
  'phoneNumber': instance.phoneNumber,
  'token': instance.token,
  'isBlocked': instance.isBlocked,
  'dateBirth': instance.dateBirth?.toIso8601String(),
  'imageUrl': instance.imageUrl,
};
