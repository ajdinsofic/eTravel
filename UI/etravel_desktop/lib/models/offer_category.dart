import 'package:json_annotation/json_annotation.dart';
import 'offer_sub_category.dart';

part 'offer_category.g.dart';

@JsonSerializable(explicitToJson: true)
class OfferCategory {
  final int id;
  final String name;
  final List<OfferSubCategory> subCategories;

  OfferCategory({
    required this.id,
    required this.name,
    required this.subCategories,
  });

  factory OfferCategory.fromJson(Map<String, dynamic> json) =>
      _$OfferCategoryFromJson(json);

  Map<String, dynamic> toJson() => _$OfferCategoryToJson(this);
}
