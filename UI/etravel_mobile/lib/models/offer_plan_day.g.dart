// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'offer_plan_day.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

OfferPlanDay _$OfferPlanDayFromJson(Map<String, dynamic> json) => OfferPlanDay(
  offerDetailsId: (json['offerDetailsId'] as num).toInt(),
  dayTitle: json['dayTitle'] as String,
  dayDescription: json['dayDescription'] as String,
  dayNumber: (json['dayNumber'] as num).toInt(),
);

Map<String, dynamic> _$OfferPlanDayToJson(OfferPlanDay instance) =>
    <String, dynamic>{
      'offerDetailsId': instance.offerDetailsId,
      'dayTitle': instance.dayTitle,
      'dayDescription': instance.dayDescription,
      'dayNumber': instance.dayNumber,
    };
