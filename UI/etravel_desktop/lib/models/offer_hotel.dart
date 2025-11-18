class OfferHotel {
  final int offerId;
  final int hotelId;
  final DateTime departureDate;
  final DateTime returnDate;

  OfferHotel({
    required this.offerId,
    required this.hotelId,
    required this.departureDate,
    required this.returnDate,
  });

  factory OfferHotel.fromJson(Map<String, dynamic> json) {
    return OfferHotel(
      offerId: json['offerId'],
      hotelId: json['hotelId'],
      departureDate: DateTime.parse(json['departureDate']),
      returnDate: DateTime.parse(json['returnDate']),
    );
  }
}
