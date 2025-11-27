import 'dart:io';

class HotelImageInsertRequest {
  int? id;
  int hotelId;
  bool isMain;
  File image;
  String? imageUrl;  // slika iz baze

  HotelImageInsertRequest({
    this.id,
    this.imageUrl,
    required this.hotelId,
    this.isMain = false,
    required this.image,
  });
}
