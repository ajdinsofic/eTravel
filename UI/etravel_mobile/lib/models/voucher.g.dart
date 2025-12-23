// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'voucher.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Voucher _$VoucherFromJson(Map<String, dynamic> json) => Voucher(
  id: (json['id'] as num).toInt(),
  voucherCode: json['voucherCode'] as String,
  discount: (json['discount'] as num).toDouble(),
  priceInTokens: (json['priceInTokens'] as num).toInt(),
);

Map<String, dynamic> _$VoucherToJson(Voucher instance) => <String, dynamic>{
  'id': instance.id,
  'voucherCode': instance.voucherCode,
  'discount': instance.discount,
  'priceInTokens': instance.priceInTokens,
};
