import 'package:json_annotation/json_annotation.dart';

part 'user_voucher.g.dart';

@JsonSerializable()
class UserVoucher {
  final int userId;
  final int voucherId;
  final bool isUsed;

  UserVoucher({
    required this.userId,
    required this.voucherId,
    required this.isUsed
  });

  factory UserVoucher.fromJson(Map<String, dynamic> json) =>
      _$UserVoucherFromJson(json);

  Map<String, dynamic> toJson() => _$UserVoucherToJson(this);
}
