import 'package:json_annotation/json_annotation.dart';
import 'paypal_link.dart';

part 'paypal_order_response.g.dart';

@JsonSerializable()
class PayPalOrderResponse {
  final String id;
  final String status;
  final List<PayPalLink> links;

  PayPalOrderResponse({
    required this.id,
    required this.status,
    required this.links,
  });

  factory PayPalOrderResponse.fromJson(Map<String, dynamic> json) =>
      _$PayPalOrderResponseFromJson(json);

  Map<String, dynamic> toJson() => _$PayPalOrderResponseToJson(this);
}
