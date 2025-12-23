import 'package:json_annotation/json_annotation.dart';

part 'voucher.g.dart';

@JsonSerializable()
class Voucher {
  final int id;
  final String voucherCode;

  /// Backend je decimal → u Flutteru ide double
  final double discount;

  /// zadržavamo isti naziv kao backend (priceInTokens)
  final int priceInTokens;

  Voucher({
    required this.id,
    required this.voucherCode,
    required this.discount,
    required this.priceInTokens,
  });

  factory Voucher.fromJson(Map<String, dynamic> json) =>
      _$VoucherFromJson(json);

  Map<String, dynamic> toJson() => _$VoucherToJson(this);
}
