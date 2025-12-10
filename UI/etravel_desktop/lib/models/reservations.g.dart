// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'reservations.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Reservation _$ReservationFromJson(Map<String, dynamic> json) => Reservation(
  id: (json['id'] as num).toInt(),
  userId: (json['userId'] as num).toInt(),
  offerId: (json['offerId'] as num).toInt(),
  hotelId: (json['hotelId'] as num).toInt(),
  roomId: (json['roomId'] as num).toInt(),
  isActive: json['isActive'] as bool,
  includeInsurance: json['includeInsurance'] as bool,
  isFirstRatePaid: json['isFirstRatePaid'] as bool,
  isFullPaid: json['isFullPaid'] as bool,
  totalPrice: (json['totalPrice'] as num).toDouble(),
  priceLeftToPay: (json['priceLeftToPay'] as num).toDouble(),
  userNeeds: json['userNeeds'] as String,
  createdAt:
      json['createdAt'] == null
          ? null
          : DateTime.parse(json['createdAt'] as String),
);

Map<String, dynamic> _$ReservationToJson(Reservation instance) =>
    <String, dynamic>{
      'id': instance.id,
      'userId': instance.userId,
      'offerId': instance.offerId,
      'hotelId': instance.hotelId,
      'roomId': instance.roomId,
      'isActive': instance.isActive,
      'includeInsurance': instance.includeInsurance,
      'isFirstRatePaid': instance.isFirstRatePaid,
      'isFullPaid': instance.isFullPaid,
      'createdAt': instance.createdAt?.toIso8601String(),
      'totalPrice': instance.totalPrice,
      'priceLeftToPay': instance.priceLeftToPay,
      'userNeeds': instance.userNeeds,
    };
