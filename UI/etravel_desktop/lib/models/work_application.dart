import 'package:json_annotation/json_annotation.dart';

part 'work_application.g.dart';

@JsonSerializable()
class WorkApplication {
  int id;
  int userId;
  String cvFileName;
  DateTime appliedAt;
  String ? letter;

  WorkApplication({
    required this.id,
    required this.userId,
    required this.cvFileName,
    required this.appliedAt,
    required this.letter,
  });

  factory WorkApplication.fromJson(Map<String, dynamic> json) =>
      _$WorkApplicationFromJson(json);

  Map<String, dynamic> toJson() => _$WorkApplicationToJson(this);
}
