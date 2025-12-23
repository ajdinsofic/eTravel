import 'package:json_annotation/json_annotation.dart';

part 'age_group_stats_response.g.dart';

@JsonSerializable()
class AgeGroupStatsResponse {
  final String ageGroup;
  final int count;
  final double percentage;

  AgeGroupStatsResponse({
    required this.ageGroup,
    required this.count,
    required this.percentage,
  });

  factory AgeGroupStatsResponse.fromJson(Map<String, dynamic> json) =>
      _$AgeGroupStatsResponseFromJson(json);

  Map<String, dynamic> toJson() => _$AgeGroupStatsResponseToJson(this);
}
