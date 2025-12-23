// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'paypal_link.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

PayPalLink _$PayPalLinkFromJson(Map<String, dynamic> json) => PayPalLink(
  href: json['href'] as String,
  rel: json['rel'] as String,
  method: json['method'] as String,
);

Map<String, dynamic> _$PayPalLinkToJson(PayPalLink instance) =>
    <String, dynamic>{
      'href': instance.href,
      'rel': instance.rel,
      'method': instance.method,
    };
