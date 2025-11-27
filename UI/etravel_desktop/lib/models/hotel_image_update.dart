import 'package:json_annotation/json_annotation.dart';

part 'hotel_image_update.g.dart';

@JsonSerializable()
class HotelImageUpdateRequest {
  final int hotelId;
  final bool isMain;

  HotelImageUpdateRequest({
    required this.hotelId,
    this.isMain = false,
  });

  factory HotelImageUpdateRequest.fromJson(Map<String, dynamic> json) =>
      _$HotelImageUpdateRequestFromJson(json);

  Map<String, dynamic> toJson() => _$HotelImageUpdateRequestToJson(this);
}
