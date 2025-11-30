import 'package:json_annotation/json_annotation.dart';

part 'offer_image_update.g.dart';

@JsonSerializable()
class OfferImageUpdateRequest {
  int? id;
  int offerId;
  bool isMain;

  @JsonKey(ignore: true)
  bool? isUpdated = false;

  OfferImageUpdateRequest({
    this.id,
    required this.offerId,
    required this.isMain,
    this.isUpdated
  });

  factory OfferImageUpdateRequest.fromJson(Map<String, dynamic> json) =>
      _$OfferImageUpdateRequestFromJson(json);

  Map<String, dynamic> toJson() => _$OfferImageUpdateRequestToJson(this);
}
