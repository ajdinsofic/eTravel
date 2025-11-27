import 'package:json_annotation/json_annotation.dart';

part 'room.g.dart';

@JsonSerializable()
class Room {
  final int id;
  final String roomType;

  Room({
    required this.id,
    required this.roomType,
  });

  factory Room.fromJson(Map<String, dynamic> json) => _$RoomFromJson(json);
  Map<String, dynamic> toJson() => _$RoomToJson(this);
}
