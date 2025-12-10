import 'package:json_annotation/json_annotation.dart';

part 'reservations.g.dart';

@JsonSerializable()
class Reservation {
  final int id;
  final int userId;
  final int offerId;
  final int hotelId;
  final int roomId;

  final bool isActive;
  final bool includeInsurance;
  final bool isFirstRatePaid;
  final bool isFullPaid;
  final DateTime? createdAt;

  final double totalPrice;
  final double priceLeftToPay;
  final String userNeeds;

  Reservation({
    required this.id,
    required this.userId,
    required this.offerId,
    required this.hotelId,
    required this.roomId,
    required this.isActive,
    required this.includeInsurance,
    required this.isFirstRatePaid,
    required this.isFullPaid,
    required this.totalPrice,
    required this.priceLeftToPay,
    required this.userNeeds,
    required this.createdAt,
  });

  factory Reservation.fromJson(Map<String, dynamic> json) =>
      _$ReservationFromJson(json);

  Map<String, dynamic> toJson() => _$ReservationToJson(this);
}
