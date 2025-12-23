// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'age_group_stats_response.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

AgeGroupStatsResponse _$AgeGroupStatsResponseFromJson(
  Map<String, dynamic> json,
) => AgeGroupStatsResponse(
  ageGroup: json['ageGroup'] as String,
  count: (json['count'] as num).toInt(),
  percentage: (json['percentage'] as num).toDouble(),
);

Map<String, dynamic> _$AgeGroupStatsResponseToJson(
  AgeGroupStatsResponse instance,
) => <String, dynamic>{
  'ageGroup': instance.ageGroup,
  'count': instance.count,
  'percentage': instance.percentage,
};
