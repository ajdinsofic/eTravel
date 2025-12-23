// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_voucher.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserVoucher _$UserVoucherFromJson(Map<String, dynamic> json) => UserVoucher(
  userId: (json['userId'] as num).toInt(),
  voucherId: (json['voucherId'] as num).toInt(),
  isUsed: json['isUsed'] as bool,
);

Map<String, dynamic> _$UserVoucherToJson(UserVoucher instance) =>
    <String, dynamic>{
      'userId': instance.userId,
      'voucherId': instance.voucherId,
      'isUsed': instance.isUsed,
    };
