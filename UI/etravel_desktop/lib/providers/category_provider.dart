import 'package:etravel_desktop/providers/base_provider.dart';
import '../models/offer_category.dart';

class CategoryProvider extends BaseProvider<OfferCategory> {
  CategoryProvider() : super("OfferCategory");

  @override
  OfferCategory fromJson(data) => OfferCategory.fromJson(data);
}

