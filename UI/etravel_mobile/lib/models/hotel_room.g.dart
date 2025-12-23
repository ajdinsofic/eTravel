// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'hotel_room.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

HotelRoom _$HotelRoomFromJson(Map<String, dynamic> json) => HotelRoom(
  hotelId: (json['hotelId'] as num).toInt(),
  roomId: (json['roomId'] as num).toInt(),
  roomsLeft: (json['roomsLeft'] as num).toInt(),
  room:
      json['room'] == null
          ? null
          : Room.fromJson(json['room'] as Map<String, dynamic>),
);

Map<String, dynamic> _$HotelRoomToJson(HotelRoom instance) => <String, dynamic>{
  'hotelId': instance.hotelId,
  'roomId': instance.roomId,
  'roomsLeft': instance.roomsLeft,
  'room': instance.room?.toJson(),
};
