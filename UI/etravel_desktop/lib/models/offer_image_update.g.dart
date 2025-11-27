// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'offer_image_update.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

OfferImageUpdateRequest _$OfferImageUpdateRequestFromJson(
  Map<String, dynamic> json,
) => OfferImageUpdateRequest(
  id: (json['id'] as num?)?.toInt(),
  offerId: (json['offerId'] as num).toInt(),
  isMain: json['isMain'] as bool,
);

Map<String, dynamic> _$OfferImageUpdateRequestToJson(
  OfferImageUpdateRequest instance,
) => <String, dynamic>{
  'id': instance.id,
  'offerId': instance.offerId,
  'isMain': instance.isMain,
};
