// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'offer_sub_category.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

OfferSubCategory _$OfferSubCategoryFromJson(Map<String, dynamic> json) =>
    OfferSubCategory(
      id: (json['id'] as num).toInt(),
      name: json['name'] as String,
      categoryId: (json['categoryId'] as num).toInt(),
    );

Map<String, dynamic> _$OfferSubCategoryToJson(OfferSubCategory instance) =>
    <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'categoryId': instance.categoryId,
    };
