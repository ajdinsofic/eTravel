// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'hotel_image_update.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

HotelImageUpdateRequest _$HotelImageUpdateRequestFromJson(
  Map<String, dynamic> json,
) => HotelImageUpdateRequest(
  hotelId: (json['hotelId'] as num).toInt(),
  isMain: json['isMain'] as bool? ?? false,
);

Map<String, dynamic> _$HotelImageUpdateRequestToJson(
  HotelImageUpdateRequest instance,
) => <String, dynamic>{'hotelId': instance.hotelId, 'isMain': instance.isMain};
