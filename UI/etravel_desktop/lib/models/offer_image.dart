class OfferImage {
  final int id;
  final int offerId;
  final String imageUrl;
  final bool isMain;

  OfferImage({
    required this.id,
    required this.offerId,
    required this.imageUrl,
    required this.isMain,
  });

  factory OfferImage.fromJson(Map<String, dynamic> json) {
    return OfferImage(
      id: json['id'],
      offerId: json['offerId'],
      imageUrl: json['imageUrl'],
      isMain: json['isMain'] ?? false,
    );
  }
}
