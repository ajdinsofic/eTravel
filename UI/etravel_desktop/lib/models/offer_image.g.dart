// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'offer_image.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

OfferImage _$OfferImageFromJson(Map<String, dynamic> json) => OfferImage(
  id: (json['id'] as num).toInt(),
  offerId: (json['offerId'] as num).toInt(),
  imageUrl: json['imageUrl'] as String,
  isMain: json['isMain'] as bool,
);

Map<String, dynamic> _$OfferImageToJson(OfferImage instance) =>
    <String, dynamic>{
      'id': instance.id,
      'offerId': instance.offerId,
      'imageUrl': instance.imageUrl,
      'isMain': instance.isMain,
    };
