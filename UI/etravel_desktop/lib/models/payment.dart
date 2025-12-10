import 'package:json_annotation/json_annotation.dart';

part 'payment.g.dart';

@JsonSerializable()
class Payment {
  final int reservationId;
  final int rateId;
  final double amount;
  final DateTime paymentDate;
  final String paymentMethod;
  final DateTime paymentDeadline;
  final bool isConfirmed;
  final bool deadlineExtended;

  Payment({
    required this.reservationId,
    required this.rateId,
    required this.amount,
    required this.paymentDate,
    required this.paymentMethod,
    required this.paymentDeadline,
    required this.isConfirmed,
    required this.deadlineExtended,
  });

  factory Payment.fromJson(Map<String, dynamic> json) =>
      _$PaymentFromJson(json);

  Map<String, dynamic> toJson() => _$PaymentToJson(this);
}
