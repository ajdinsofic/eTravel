import 'dart:io';

class WorkApplicationInsert {
  final int userId;
  final File cvFile;
  final String letter;

  WorkApplicationInsert({
    required this.userId,
    required this.cvFile,
    required this.letter,
  });

  Map<String, String> toFields() {
    return {
      "UserId": userId.toString(),
      "Letter": letter,
    };
  }
}
