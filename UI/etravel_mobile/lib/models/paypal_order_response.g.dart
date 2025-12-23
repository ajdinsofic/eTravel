// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'paypal_order_response.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

PayPalOrderResponse _$PayPalOrderResponseFromJson(Map<String, dynamic> json) =>
    PayPalOrderResponse(
      id: json['id'] as String,
      status: json['status'] as String,
      links:
          (json['links'] as List<dynamic>)
              .map((e) => PayPalLink.fromJson(e as Map<String, dynamic>))
              .toList(),
    );

Map<String, dynamic> _$PayPalOrderResponseToJson(
  PayPalOrderResponse instance,
) => <String, dynamic>{
  'id': instance.id,
  'status': instance.status,
  'links': instance.links,
};
