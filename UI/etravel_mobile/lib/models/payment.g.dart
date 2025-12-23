// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'payment.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Payment _$PaymentFromJson(Map<String, dynamic> json) => Payment(
  reservationId: (json['reservationId'] as num).toInt(),
  rateId: (json['rateId'] as num).toInt(),
  amount: (json['amount'] as num).toDouble(),
  paymentDate: DateTime.parse(json['paymentDate'] as String),
  paymentMethod: json['paymentMethod'] as String,
  paymentDeadline: DateTime.parse(json['paymentDeadline'] as String),
  isConfirmed: json['isConfirmed'] as bool,
  deadlineExtended: json['deadlineExtended'] as bool,
);

Map<String, dynamic> _$PaymentToJson(Payment instance) => <String, dynamic>{
  'reservationId': instance.reservationId,
  'rateId': instance.rateId,
  'amount': instance.amount,
  'paymentDate': instance.paymentDate.toIso8601String(),
  'paymentMethod': instance.paymentMethod,
  'paymentDeadline': instance.paymentDeadline.toIso8601String(),
  'isConfirmed': instance.isConfirmed,
  'deadlineExtended': instance.deadlineExtended,
};
