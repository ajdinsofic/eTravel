// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'reservation_preview_request.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ReservationPreviewRequest _$ReservationPreviewRequestFromJson(
  Map<String, dynamic> json,
) => ReservationPreviewRequest(
  userId: (json['userId'] as num).toInt(),
  offerId: (json['offerId'] as num).toInt(),
  hotelId: (json['hotelId'] as num).toInt(),
  roomId: (json['roomId'] as num).toInt(),
  basePrice: (json['basePrice'] as num).toDouble(),
  includeInsurance: json['includeInsurance'] as bool,
  voucherCode: json['voucherCode'] as String?,
);

Map<String, dynamic> _$ReservationPreviewRequestToJson(
  ReservationPreviewRequest instance,
) => <String, dynamic>{
  'userId': instance.userId,
  'offerId': instance.offerId,
  'hotelId': instance.hotelId,
  'roomId': instance.roomId,
  'basePrice': instance.basePrice,
  'includeInsurance': instance.includeInsurance,
  'voucherCode': instance.voucherCode,
};
