import 'package:json_annotation/json_annotation.dart';

part 'offer_sub_category.g.dart';

@JsonSerializable()
class OfferSubCategory {
  final int id;
  final String name;
  final int categoryId;

  OfferSubCategory({
    required this.id,
    required this.name,
    required this.categoryId,
  });

  factory OfferSubCategory.fromJson(Map<String, dynamic> json) =>
      _$OfferSubCategoryFromJson(json);

  Map<String, dynamic> toJson() => _$OfferSubCategoryToJson(this);
}
