// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_role.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserRole _$UserRoleFromJson(Map<String, dynamic> json) => UserRole(
  userId: (json['userId'] as num).toInt(),
  roleId: (json['roleId'] as num).toInt(),
);

Map<String, dynamic> _$UserRoleToJson(UserRole instance) => <String, dynamic>{
  'userId': instance.userId,
  'roleId': instance.roleId,
};
