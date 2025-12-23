// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'offer_hotel.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

OfferHotel _$OfferHotelFromJson(Map<String, dynamic> json) => OfferHotel(
  offerId: (json['offerId'] as num).toInt(),
  hotelId: (json['hotelId'] as num).toInt(),
  departureDate: DateTime.parse(json['departureDate'] as String),
  returnDate: DateTime.parse(json['returnDate'] as String),
);

Map<String, dynamic> _$OfferHotelToJson(OfferHotel instance) =>
    <String, dynamic>{
      'offerId': instance.offerId,
      'hotelId': instance.hotelId,
      'departureDate': instance.departureDate.toIso8601String(),
      'returnDate': instance.returnDate.toIso8601String(),
    };
