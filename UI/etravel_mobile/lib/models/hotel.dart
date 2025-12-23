import 'package:json_annotation/json_annotation.dart';
import 'hotel_room.dart';
import 'hotel_image.dart';
import 'offer_hotel.dart';

part 'hotel.g.dart';

@JsonSerializable(explicitToJson: true)
class Hotel {
  final int id;
  final String name;
  final String address;
  final int stars;
  final double calculatedPrice;

  final List<HotelRoom> hotelRooms;
  final List<HotelImage> hotelImages;
  final List<OfferHotel> offerHotels;

  Hotel({
    required this.id,
    required this.name,
    required this.address,
    required this.stars,
    required this.calculatedPrice,
    required this.hotelRooms,
    required this.hotelImages,
    required this.offerHotels,
  });

  factory Hotel.fromJson(Map<String, dynamic> json) => _$HotelFromJson(json);
  Map<String, dynamic> toJson() => _$HotelToJson(this);
}
