import 'package:json_annotation/json_annotation.dart';

part 'comment.g.dart';

@JsonSerializable()
class Comment {
  int id;
  int userId;
  int offerId;
  String comment;
  int starRate;

  // frontend-only flag → nije u bazi, ne ide u JSON
  @JsonKey(ignore: true)
  bool isEdited = true;

  Comment({
    required this.id,
    required this.userId,
    required this.offerId,
    required this.comment,
    required this.starRate,
  });

  factory Comment.fromJson(Map<String, dynamic> json) =>
      _$CommentFromJson(json);

  Map<String, dynamic> toJson() => _$CommentToJson(this);
}
