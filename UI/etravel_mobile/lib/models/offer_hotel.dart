import 'package:json_annotation/json_annotation.dart';

part 'offer_hotel.g.dart';

@JsonSerializable()
class OfferHotel {
  final int offerId;
  final int hotelId;
  final DateTime departureDate;
  final DateTime returnDate;

  OfferHotel({
    required this.offerId,
    required this.hotelId,
    required this.departureDate,
    required this.returnDate,
  });

  factory OfferHotel.fromJson(Map<String, dynamic> json) => _$OfferHotelFromJson(json);
  Map<String, dynamic> toJson() => _$OfferHotelToJson(this);
}
