import 'dart:convert';
import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/hotel_room.dart';
import '../utils/session.dart';
import 'package:http/http.dart' as http;

import 'base_provider.dart';

class HotelRoomProvider extends BaseProvider<HotelRoom> {
  HotelRoomProvider() : super("HotelRoom");

  @override
  HotelRoom fromJson(dynamic data) {
    return HotelRoom.fromJson(data);
  }

  Future<void> deleteRoom(int hotelId, int roomId) async {
    final url = "${ApiConfig.apiBase}/api/HotelRoom/$hotelId/$roomId";

    final response = await http.delete(
      Uri.parse(url),
      headers: {
        "Content-Type": "text/plain",
        "Authorization": "Bearer ${Session.token}",
      },
    );

    if (response.statusCode != 200) {
      throw Exception("Failed to delete room: ${response.body}");
    }
  }

  Future<void> updateRoom({
    required int hotelId,
    required int roomId,
    required int roomsLeft,
  }) async {
    final uri = Uri.parse(
      "${ApiConfig.apiBase}/api/HotelRoom/$hotelId/$roomId",
    );

    final response = await http.put(
      uri,
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${Session.token}",
      },
      body: jsonEncode({"roomsLeft": roomsLeft}),
    );

    if (response.statusCode != 200) {
      throw Exception("Failed to update hotel room: ${response.body}");
    }
  }

  Future<bool> increaseRoomsLeft({
  required int hotelId,
  required int roomId,
}) async {
  final url =
      "${ApiConfig.apiBase}/api/HotelRoom/increase-rooms-left"
      "?hotelId=$hotelId&roomId=$roomId";

  final response = await http.put(
    Uri.parse(url),
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer ${Session.token}",
    },
  );

  if (response.statusCode != 200) {
    throw Exception(
      "Greška pri vraćanju sobe (hotelId=$hotelId, roomId=$roomId). "
      "Status: ${response.statusCode}",
    );
  }

  return true;
}
}
