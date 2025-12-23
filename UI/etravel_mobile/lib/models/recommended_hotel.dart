import 'package:json_annotation/json_annotation.dart';

part 'recommended_hotel.g.dart';

@JsonSerializable()
class RecommendedHotel {
  final int hotelId;
  final int roomId;

  RecommendedHotel({
    required this.hotelId,
    required this.roomId,
  });

  factory RecommendedHotel.fromJson(Map<String, dynamic> json) =>
      _$RecommendedHotelFromJson(json);

  Map<String, dynamic> toJson() => _$RecommendedHotelToJson(this);
}
