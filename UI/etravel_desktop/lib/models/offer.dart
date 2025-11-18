import 'package:etravel_desktop/models/offer_hotel.dart';
import 'package:etravel_desktop/models/offer_image.dart';
import 'package:etravel_desktop/models/offer_plan_day.dart';
import 'package:etravel_desktop/models/sub_category.dart';

class Offer {
  final int offerId;
  final String title;
  final int daysInTotal;
  final String wayOfTravel;
  final int? subCategoryId;
  final SubCategory? subCategory;
  final String description;
  final String country;
  final String city;
  final double minimalPrice;
  final double residenceTaxPerDay;
  final double travelInsuranceTotal;
  final double residenceTotal;

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
  });

  factory Offer.fromJson(Map<String, dynamic> json) {
    return Offer(
      offerId: json['offerId'],
      title: json['title'],
      daysInTotal: json['daysInTotal'],
      wayOfTravel: json['wayOfTravel'],
      subCategoryId: json['subCategoryId'],
      subCategory: json["subCategory"] == null
        ? null
        : SubCategory.fromJson(json["subCategory"]),
      description: json['description'],
      country: json['country'],
      city: json['city'],
      minimalPrice: (json['minimalPrice'] as num).toDouble(),
      residenceTaxPerDay: (json['residenceTaxPerDay'] as num).toDouble(),
      travelInsuranceTotal: (json['travelInsuranceTotal'] as num).toDouble(),
      residenceTotal: (json['residenceTotal'] as num).toDouble(),

      offerImages: (json['offerImages'] as List<dynamic>)
          .map((x) => OfferImage.fromJson(x))
          .toList(),

      offerPlanDays: (json['offerPlanDays'] as List<dynamic>)
          .map((x) => OfferPlanDay.fromJson(x))
          .toList(),

      offerHotels: (json['offerHotels'] as List<dynamic>)
          .map((x) => OfferHotel.fromJson(x))
          .toList(),
    );
  }
}
