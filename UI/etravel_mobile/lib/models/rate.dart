import 'package:json_annotation/json_annotation.dart';

part 'rate.g.dart';

@JsonSerializable()
class Rate {
  int id;
  String name;
  int? orderNumber;

  Rate({
    required this.id,
    required this.name,
    this.orderNumber,
  });

  factory Rate.fromJson(Map<String, dynamic> json) => _$RateFromJson(json);
  Map<String, dynamic> toJson() => _$RateToJson(this);
}
