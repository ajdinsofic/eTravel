
import 'package:etravel_app/config/api_config.dart';

String resolveOfferImageUrl(String? imagePath) {
  if (imagePath == null || imagePath.isEmpty) {
    return "${ApiConfig.imagesOffers}/default.jpg";
  }

  // Ako je već full URL (https / http)
  if (imagePath.startsWith("http://") || imagePath.startsWith("https://")) {
    return imagePath;
  }

  // Inače je filename (npr. dubrovnik.jpg)
  return "${ApiConfig.imagesOffers}/$imagePath";
}

String resolveHotelsImageUrl(String? imagePath) {
  if (imagePath == null || imagePath.isEmpty) {
    return "${ApiConfig.imagesHotels}/default.jpg";
  }

  // Ako je već full URL (https / http)
  if (imagePath.startsWith("http://") || imagePath.startsWith("https://")) {
    return imagePath;
  }

  // Inače je filename (npr. dubrovnik.jpg)
  return "${ApiConfig.imagesHotels}/$imagePath";
}

String resolveUsersImageUrl(String? imagePath) {
  if (imagePath == null || imagePath.isEmpty) {
    return "${ApiConfig.imagesUsers}/default.jpg";
  }

  // Ako je već full URL (https / http)
  if (imagePath.startsWith("http://") || imagePath.startsWith("https://")) {
    return imagePath;
  }

  // Inače je filename (npr. dubrovnik.jpg)
  return "${ApiConfig.imagesUsers}/$imagePath";
}
