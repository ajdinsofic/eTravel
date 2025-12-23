// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'offer_category.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

OfferCategory _$OfferCategoryFromJson(Map<String, dynamic> json) =>
    OfferCategory(
      id: (json['id'] as num).toInt(),
      name: json['name'] as String,
      subCategories:
          (json['subCategories'] as List<dynamic>)
              .map((e) => OfferSubCategory.fromJson(e as Map<String, dynamic>))
              .toList(),
    );

Map<String, dynamic> _$OfferCategoryToJson(OfferCategory instance) =>
    <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'subCategories': instance.subCategories.map((e) => e.toJson()).toList(),
    };
