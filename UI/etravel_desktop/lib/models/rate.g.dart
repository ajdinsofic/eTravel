// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'rate.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Rate _$RateFromJson(Map<String, dynamic> json) => Rate(
  id: (json['id'] as num).toInt(),
  name: json['name'] as String,
  orderNumber: (json['orderNumber'] as num?)?.toInt(),
);

Map<String, dynamic> _$RateToJson(Rate instance) => <String, dynamic>{
  'id': instance.id,
  'name': instance.name,
  'orderNumber': instance.orderNumber,
};
