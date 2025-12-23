import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/models/voucher.dart';
import 'package:etravel_app/providers/base_provider.dart';

class VoucherProvider extends BaseProvider<Voucher> {
  VoucherProvider() : super("Voucher");

  @override
  Voucher fromJson(data) {
    return Voucher.fromJson(data);
  }
}
