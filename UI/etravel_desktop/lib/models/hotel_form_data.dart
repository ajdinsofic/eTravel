import 'package:etravel_desktop/models/hotel_image_insert.dart';
import 'package:etravel_desktop/models/hotel_room_insert.dart';

class HotelFormData {
  int? hotelId;
  String name;
  String address;
  int stars;
  List<HotelRoomInsertRequest> selectedRooms;
  List<HotelImageInsertRequest> images;
  String departureDate;
  String returnDate;

  // Originalne vrijednosti iz baze
  String? originalName;
  String? originalAddress;
  int? originalStars;
  String? originalDepartureDate;
  String? originalReturnDate;

  //Flag da li je upisan u bazu
  bool isNew;

  // VALIDATION FLAGS
  String? nameError;
  String? addressError;
  String? starsError;
  String? dateError;
  String? roomsError;
  String? imagesError;

  HotelFormData({
    this.hotelId,
    this.isNew = true,
    this.name = "",
    this.address = "",
    this.stars = 0,
    List<HotelRoomInsertRequest>? selectedRooms,
    List<HotelImageInsertRequest>? images,
    this.departureDate = "",
    this.returnDate = "",
  })  : selectedRooms = selectedRooms ?? [],
        images = images ?? [];
}
