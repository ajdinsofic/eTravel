import 'package:json_annotation/json_annotation.dart';

part 'hotel_room_update.g.dart';

@JsonSerializable()
class HotelRoomUpdateRequest {
  final int roomsLeft;

  HotelRoomUpdateRequest({required this.roomsLeft});

  factory HotelRoomUpdateRequest.fromJson(Map<String, dynamic> json) =>
      _$HotelRoomUpdateRequestFromJson(json);

  Map<String, dynamic> toJson() => _$HotelRoomUpdateRequestToJson(this);
}
