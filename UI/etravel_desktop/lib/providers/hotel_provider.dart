import 'dart:convert';
import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/hotel.dart';
import '../utils/session.dart';
import 'package:http/http.dart' as http;

import 'base_provider.dart';

class HotelProvider extends BaseProvider<Hotel> {
  HotelProvider() : super("Hotel");

  @override
  Hotel fromJson(dynamic data) {
    return Hotel.fromJson(data);
  }
}
