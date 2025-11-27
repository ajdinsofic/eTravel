// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'hotel.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Hotel _$HotelFromJson(Map<String, dynamic> json) => Hotel(
  id: (json['id'] as num).toInt(),
  name: json['name'] as String,
  address: json['address'] as String,
  stars: (json['stars'] as num).toInt(),
  calculatedPrice: (json['calculatedPrice'] as num).toDouble(),
  hotelRooms:
      (json['hotelRooms'] as List<dynamic>)
          .map((e) => HotelRoom.fromJson(e as Map<String, dynamic>))
          .toList(),
  hotelImages:
      (json['hotelImages'] as List<dynamic>)
          .map((e) => HotelImage.fromJson(e as Map<String, dynamic>))
          .toList(),
  offerHotels:
      (json['offerHotels'] as List<dynamic>)
          .map((e) => OfferHotel.fromJson(e as Map<String, dynamic>))
          .toList(),
);

Map<String, dynamic> _$HotelToJson(Hotel instance) => <String, dynamic>{
  'id': instance.id,
  'name': instance.name,
  'address': instance.address,
  'stars': instance.stars,
  'calculatedPrice': instance.calculatedPrice,
  'hotelRooms': instance.hotelRooms.map((e) => e.toJson()).toList(),
  'hotelImages': instance.hotelImages.map((e) => e.toJson()).toList(),
  'offerHotels': instance.offerHotels.map((e) => e.toJson()).toList(),
};
