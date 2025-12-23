// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'reservation_preview.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ReservationPreview _$ReservationPreviewFromJson(Map<String, dynamic> json) =>
    ReservationPreview(
      offerTitle: json['offerTitle'] as String,
      hotelTitle: json['hotelTitle'] as String,
      hotelStars: json['hotelStars'] as String,
      roomType: json['roomType'] as String,
      basePrice: (json['basePrice'] as num).toDouble(),
      insurance: (json['insurance'] as num).toDouble(),
      residenceTaxTotal: (json['residenceTaxTotal'] as num).toDouble(),
      totalPrice: (json['totalPrice'] as num).toDouble(),
      includeInsurance: json['includeInsurance'] as bool,
      voucherCode: json['voucherCode'] as String?,
    );

Map<String, dynamic> _$ReservationPreviewToJson(ReservationPreview instance) =>
    <String, dynamic>{
      'offerTitle': instance.offerTitle,
      'hotelTitle': instance.hotelTitle,
      'hotelStars': instance.hotelStars,
      'roomType': instance.roomType,
      'basePrice': instance.basePrice,
      'insurance': instance.insurance,
      'residenceTaxTotal': instance.residenceTaxTotal,
      'totalPrice': instance.totalPrice,
      'includeInsurance': instance.includeInsurance,
      'voucherCode': instance.voucherCode,
    };
