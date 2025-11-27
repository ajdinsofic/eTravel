import 'dart:io';

class OfferImageInsertRequest {
  int offerId;
  bool isMain;
  File image;

  OfferImageInsertRequest({
    required this.offerId,
    required this.isMain,
    required this.image,
  });
}
