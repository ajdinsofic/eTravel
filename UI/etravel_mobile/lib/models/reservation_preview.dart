import 'package:json_annotation/json_annotation.dart';

part 'reservation_preview.g.dart';

@JsonSerializable()
class ReservationPreview {

  final String offerTitle;
  final String hotelTitle;
  final String hotelStars;
  final String roomType;
  final double basePrice;
  final double insurance;
  final double residenceTaxTotal;
  final double totalPrice;

  final bool includeInsurance;
  final String? voucherCode;

  ReservationPreview({
    required this.offerTitle,
    required this.hotelTitle,
    required this.hotelStars,
    required this.roomType,
    required this.basePrice,
    required this.insurance,
    required this.residenceTaxTotal,
    required this.totalPrice,
    required this.includeInsurance,
    this.voucherCode,
  });

  factory ReservationPreview.fromJson(Map<String, dynamic> json) =>
      _$ReservationPreviewFromJson(json);

  Map<String, dynamic> toJson() => _$ReservationPreviewToJson(this);
}
