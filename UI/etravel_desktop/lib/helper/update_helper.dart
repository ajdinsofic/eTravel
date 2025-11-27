import 'package:etravel_desktop/models/hotel_form_data.dart';

extension HotelUpdateCheck on HotelFormData {
  bool needsUpdate() {
    return name != originalName ||
        address != originalAddress ||
        stars != originalStars ||
        departureDate != originalDepartureDate ||
        returnDate != originalReturnDate;
  }

  void setOriginals() {
  originalName = name;
  originalAddress = address;
  originalStars = stars;
  originalDepartureDate = departureDate;
  originalReturnDate = returnDate;
  }
}
