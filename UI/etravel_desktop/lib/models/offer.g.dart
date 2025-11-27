// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'offer.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Offer _$OfferFromJson(Map<String, dynamic> json) => Offer(
  offerId: (json['offerId'] as num).toInt(),
  title: json['title'] as String,
  daysInTotal: (json['daysInTotal'] as num).toInt(),
  wayOfTravel: json['wayOfTravel'] as String,
  subCategoryId: (json['subCategoryId'] as num?)?.toInt(),
  subCategory:
      json['subCategory'] == null
          ? null
          : OfferSubCategory.fromJson(
            json['subCategory'] as Map<String, dynamic>,
          ),
  description: json['description'] as String,
  country: json['country'] as String,
  city: json['city'] as String,
  minimalPrice: (json['minimalPrice'] as num).toDouble(),
  residenceTaxPerDay: (json['residenceTaxPerDay'] as num).toDouble(),
  travelInsuranceTotal: (json['travelInsuranceTotal'] as num).toDouble(),
  residenceTotal: (json['residenceTotal'] as num).toDouble(),
  offerImages:
      (json['offerImages'] as List<dynamic>)
          .map((e) => OfferImage.fromJson(e as Map<String, dynamic>))
          .toList(),
  offerPlanDays:
      (json['offerPlanDays'] as List<dynamic>)
          .map((e) => OfferPlanDay.fromJson(e as Map<String, dynamic>))
          .toList(),
  offerHotels:
      (json['offerHotels'] as List<dynamic>)
          .map((e) => OfferHotel.fromJson(e as Map<String, dynamic>))
          .toList(),
);

Map<String, dynamic> _$OfferToJson(Offer instance) => <String, dynamic>{
  'offerId': instance.offerId,
  'title': instance.title,
  'daysInTotal': instance.daysInTotal,
  'wayOfTravel': instance.wayOfTravel,
  'subCategoryId': instance.subCategoryId,
  'subCategory': instance.subCategory?.toJson(),
  'description': instance.description,
  'country': instance.country,
  'city': instance.city,
  'minimalPrice': instance.minimalPrice,
  'residenceTaxPerDay': instance.residenceTaxPerDay,
  'travelInsuranceTotal': instance.travelInsuranceTotal,
  'residenceTotal': instance.residenceTotal,
  'offerImages': instance.offerImages.map((e) => e.toJson()).toList(),
  'offerPlanDays': instance.offerPlanDays.map((e) => e.toJson()).toList(),
  'offerHotels': instance.offerHotels.map((e) => e.toJson()).toList(),
};
