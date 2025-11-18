class OfferPlanDay {
  final int offerDetailsId;
  final String dayTitle;
  final String dayDescription;
  final int dayNumber;

  OfferPlanDay({
    required this.offerDetailsId,
    required this.dayTitle,
    required this.dayDescription,
    required this.dayNumber,
  });

  factory OfferPlanDay.fromJson(Map<String, dynamic> json) {
    return OfferPlanDay(
      offerDetailsId: json['offerDetailsId'],
      dayTitle: json['dayTitle'],
      dayDescription: json['dayDescription'],
      dayNumber: json['dayNumber'],
    );
  }
}
