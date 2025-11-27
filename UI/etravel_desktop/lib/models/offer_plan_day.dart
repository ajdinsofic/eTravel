import 'package:json_annotation/json_annotation.dart';

part 'offer_plan_day.g.dart';

@JsonSerializable()
class OfferPlanDay {
  final int offerDetailsId;
  final String dayTitle;
  final String dayDescription;
  final int dayNumber;

  OfferPlanDay({
    required this.offerDetailsId,
    required this.dayTitle,
    required this.dayDescription,
    required this.dayNumber,
  });

  factory OfferPlanDay.fromJson(Map<String, dynamic> json) => _$OfferPlanDayFromJson(json);
  Map<String, dynamic> toJson() => _$OfferPlanDayToJson(this);
}
