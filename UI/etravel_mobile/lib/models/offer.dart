import 'package:etravel_app/models/offer_hotel.dart';
import 'package:etravel_app/models/offer_image.dart';
import 'package:etravel_app/models/offer_plan_day.dart';
import 'package:etravel_app/models/offer_sub_category.dart';
import 'package:json_annotation/json_annotation.dart';


part 'offer.g.dart';

@JsonSerializable(explicitToJson: true)
class Offer {
  final int offerId;
  final String title;
  final int daysInTotal;
  final String wayOfTravel;
  final int? subCategoryId;
  final OfferSubCategory? subCategory;
  final String description;
  final String country;
  final String city;
  final double minimalPrice;
  final double residenceTaxPerDay;
  final double travelInsuranceTotal;
  final double residenceTotal;
  final int totalCountOfReservations;
  final List<OfferImage> offerImages;
  final List<OfferPlanDay> offerPlanDays;
  final List<OfferHotel> offerHotels;

  Offer({
    required this.offerId,
    required this.title,
    required this.daysInTotal,
    required this.wayOfTravel,
    required this.subCategoryId,
    required this.subCategory,
    required this.description,
    required this.country,
    required this.city,
    required this.minimalPrice,
    required this.residenceTaxPerDay,
    required this.travelInsuranceTotal,
    required this.residenceTotal,
    required this.offerImages,
    required this.offerPlanDays,
    required this.offerHotels,
    required this.totalCountOfReservations,
  });

  factory Offer.fromJson(Map<String, dynamic> json) => _$OfferFromJson(json);
  Map<String, dynamic> toJson() => _$OfferToJson(this);
}
