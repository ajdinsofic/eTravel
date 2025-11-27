import 'package:json_annotation/json_annotation.dart';

part 'hotel_room_insert.g.dart';

@JsonSerializable()
class HotelRoomInsertRequest {
  int? hotelId;
  final int roomId;
  final int roomsLeft;

  @JsonKey(ignore: true)
    bool isNew;
    int? originalRoomsLeft;
    int? originalRoomId;


  HotelRoomInsertRequest({
    this.hotelId,
    required this.roomId,
    required this.roomsLeft,
    this.isNew = false
  });

  factory HotelRoomInsertRequest.fromJson(Map<String, dynamic> json) =>
      _$HotelRoomInsertRequestFromJson(json);

  Map<String, dynamic> toJson() => _$HotelRoomInsertRequestToJson(this);
}
