import 'dart:convert';
import 'package:etravel_app/models/rate.dart';
import 'package:etravel_app/providers/base_provider.dart';
import 'package:http/http.dart' as http;

class RateProvider extends BaseProvider<Rate> {
  RateProvider() : super("Rate");

  @override
  Rate fromJson(dynamic data) {
    return Rate.fromJson(data);
  }
}
