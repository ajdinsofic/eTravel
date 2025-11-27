// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'hotel_room_insert.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

HotelRoomInsertRequest _$HotelRoomInsertRequestFromJson(
  Map<String, dynamic> json,
) => HotelRoomInsertRequest(
  hotelId: (json['hotelId'] as num).toInt(),
  roomId: (json['roomId'] as num).toInt(),
  roomsLeft: (json['roomsLeft'] as num).toInt(),
);

Map<String, dynamic> _$HotelRoomInsertRequestToJson(
  HotelRoomInsertRequest instance,
) => <String, dynamic>{
  'hotelId': instance.hotelId,
  'roomId': instance.roomId,
  'roomsLeft': instance.roomsLeft,
};
