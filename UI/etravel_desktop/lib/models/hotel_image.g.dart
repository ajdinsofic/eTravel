// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'hotel_image.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

HotelImage _$HotelImageFromJson(Map<String, dynamic> json) => HotelImage(
  imageId: (json['imageId'] as num).toInt(),
  hotelId: (json['hotelId'] as num).toInt(),
  imageUrl: json['imageUrl'] as String,
  isMain: json['isMain'] as bool,
);

Map<String, dynamic> _$HotelImageToJson(HotelImage instance) =>
    <String, dynamic>{
      'imageId': instance.imageId,
      'hotelId': instance.hotelId,
      'imageUrl': instance.imageUrl,
      'isMain': instance.isMain,
    };
