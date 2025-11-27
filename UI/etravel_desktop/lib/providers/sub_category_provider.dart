import 'dart:convert';
import 'package:etravel_desktop/providers/base_provider.dart';
import 'package:http/http.dart' as http;
import '../utils/session.dart';
import '../providers/paged_result.dart';
import '../models/offer_sub_category.dart';

class SubCategoryProvider extends BaseProvider<OfferSubCategory> {
  SubCategoryProvider() : super("OfferSubCategory");

  @override
  OfferSubCategory fromJson(data) => OfferSubCategory.fromJson(data);
}
