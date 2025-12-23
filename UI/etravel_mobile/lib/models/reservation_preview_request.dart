import 'package:json_annotation/json_annotation.dart';

part 'reservation_preview_request.g.dart';

@JsonSerializable()
class ReservationPreviewRequest {
  final int userId;
  final int offerId;
  final int hotelId;
  final int roomId;
  final double basePrice;
  final bool includeInsurance;
  final String? voucherCode;

  ReservationPreviewRequest({
    required this.userId,
    required this.offerId,
    required this.hotelId,
    required this.roomId,
    required this.basePrice,
    required this.includeInsurance,
    this.voucherCode,
  });

  factory ReservationPreviewRequest.fromJson(Map<String, dynamic> json) =>
      _$ReservationPreviewRequestFromJson(json);

  Map<String, dynamic> toJson() =>
      _$ReservationPreviewRequestToJson(this);
}
