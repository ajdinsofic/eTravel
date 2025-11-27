import 'package:json_annotation/json_annotation.dart';
import 'room.dart';

part 'hotel_room.g.dart';

@JsonSerializable(explicitToJson: true)
class HotelRoom {
  final int hotelId;
  final int roomId;
  final int roomsLeft;

  final Room? room; // može biti null

  HotelRoom({
    required this.hotelId,
    required this.roomId,
    required this.roomsLeft,
    this.room,
  });

  factory HotelRoom.fromJson(Map<String, dynamic> json) =>
      _$HotelRoomFromJson(json);
  Map<String, dynamic> toJson() => _$HotelRoomToJson(this);
}
