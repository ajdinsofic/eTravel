import 'dart:io';

class UserImageRequest {
  final int userId;
  final File image;

  UserImageRequest({
    required this.userId,
    required this.image,
  });

  // Pretvorba u "filter" mapu ako želiš
  Map<String, dynamic> toMap() {
    return {
      "userId": userId,
      "image": image,
    };
  }
}
