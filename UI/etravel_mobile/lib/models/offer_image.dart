import 'package:json_annotation/json_annotation.dart';

part 'offer_image.g.dart';

@JsonSerializable()
class OfferImage {
  final int id;
  final int offerId;
  final String imageUrl;
  final bool isMain;

  OfferImage({
    required this.id,
    required this.offerId,
    required this.imageUrl,
    required this.isMain,
  });

  factory OfferImage.fromJson(Map<String, dynamic> json) => _$OfferImageFromJson(json);
  Map<String, dynamic> toJson() => _$OfferImageToJson(this);
}
