import 'dart:convert';
import 'package:etravel_desktop/models/offer_plan_day.dart';
import 'package:etravel_desktop/models/search_provider.dart';
import 'package:etravel_desktop/providers/base_provider.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

import '../config/api_config.dart';
import '../utils/session.dart';

class OfferPlanDayProvider extends BaseProvider<OfferPlanDay> {
  OfferPlanDayProvider() : super("OfferPlanDay");

  // -----------------------------------------------------
  // OVERRIDE: Pretvaranje JSON → OfferPlanDay model
  // -----------------------------------------------------
  @override
  OfferPlanDay fromJson(item) {
    return OfferPlanDay.fromJson(item);
  }

}
