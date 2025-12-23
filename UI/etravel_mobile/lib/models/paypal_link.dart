import 'package:json_annotation/json_annotation.dart';

part 'paypal_link.g.dart';

@JsonSerializable()
class PayPalLink {
  final String href;
  final String rel;
  final String method;

  PayPalLink({
    required this.href,
    required this.rel,
    required this.method,
  });

  factory PayPalLink.fromJson(Map<String, dynamic> json) =>
      _$PayPalLinkFromJson(json);

  Map<String, dynamic> toJson() => _$PayPalLinkToJson(this);
}
