import 'package:json_annotation/json_annotation.dart';

part 'hotel_image.g.dart';

@JsonSerializable()
class HotelImage {
  final int imageId;
  final int hotelId;
  final String imageUrl;
  final bool isMain;

  HotelImage({
    required this.imageId,
    required this.hotelId,
    required this.imageUrl,
    required this.isMain,
  });

  factory HotelImage.fromJson(Map<String, dynamic> json) =>
      _$HotelImageFromJson(json);
  Map<String, dynamic> toJson() => _$HotelImageToJson(this);
}
