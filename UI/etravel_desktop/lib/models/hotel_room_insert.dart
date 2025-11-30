import 'package:json_annotation/json_annotation.dart';

part 'hotel_room_insert.g.dart';

@JsonSerializable()
class HotelRoomInsertRequest {
  int? hotelId;
  int roomId;
  int roomsLeft;
  String? roomTypeError;
  String? roomCountError;



  @JsonKey(ignore: true)
    bool isNew;
    int? originalRoomsLeft;
    int? originalRoomId;


  HotelRoomInsertRequest({
    this.hotelId,
    required this.roomId,
    required this.roomsLeft,
    this.isNew = false,
    this.roomTypeError,
    this.roomCountError
  });

  factory HotelRoomInsertRequest.fromJson(Map<String, dynamic> json) =>
      _$HotelRoomInsertRequestFromJson(json);

  Map<String, dynamic> toJson() => _$HotelRoomInsertRequestToJson(this);
}
