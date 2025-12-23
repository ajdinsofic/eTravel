import 'package:json_annotation/json_annotation.dart';

part 'destination_stats_response.g.dart';

@JsonSerializable()
class DestinationStatsResponse {
  @JsonKey(name: "destinationName")
  final String destinationName;

  @JsonKey(name: "count")
  final int count;

  @JsonKey(name: "percentage")
  final double percentage;

  DestinationStatsResponse({
    required this.destinationName,
    required this.count,
    required this.percentage,
  });

  factory DestinationStatsResponse.fromJson(Map<String, dynamic> json) =>
      _$DestinationStatsResponseFromJson(json);

  Map<String, dynamic> toJson() => _$DestinationStatsResponseToJson(this);

  static List<DestinationStatsResponse> listFromJson(List<dynamic> jsonList) {
    return jsonList
        .map((item) => DestinationStatsResponse.fromJson(item))
        .toList();
  }
}
