import 'dart:convert';
import 'package:etravel_app/providers/base_provider.dart';
import 'package:http/http.dart' as http;
import '../utils/session.dart';
import '../models/room.dart';
import '../config/api_config.dart';

class RoomProvider extends BaseProvider<Room> {
  RoomProvider() : super("Room");

  @override
  Room fromJson(dynamic data) {
    return Room.fromJson(data);
  }
}
