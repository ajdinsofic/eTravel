import 'dart:convert';
import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/rate.dart';
import 'package:etravel_desktop/models/search_provider.dart';
import 'package:etravel_desktop/providers/base_provider.dart';
import 'package:http/http.dart' as http;

class RateProvider extends BaseProvider<Rate> {
  RateProvider() : super("Rate");

  @override
  Rate fromJson(dynamic data) {
    return Rate.fromJson(data);
  }
}
