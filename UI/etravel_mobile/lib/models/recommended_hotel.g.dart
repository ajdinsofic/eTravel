// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'recommended_hotel.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

RecommendedHotel _$RecommendedHotelFromJson(Map<String, dynamic> json) =>
    RecommendedHotel(
      hotelId: (json['hotelId'] as num).toInt(),
      roomId: (json['roomId'] as num).toInt(),
    );

Map<String, dynamic> _$RecommendedHotelToJson(RecommendedHotel instance) =>
    <String, dynamic>{'hotelId': instance.hotelId, 'roomId': instance.roomId};
