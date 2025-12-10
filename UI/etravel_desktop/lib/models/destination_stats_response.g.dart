// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'destination_stats_response.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

DestinationStatsResponse _$DestinationStatsResponseFromJson(
  Map<String, dynamic> json,
) => DestinationStatsResponse(
  destinationName: json['destinationName'] as String,
  count: (json['count'] as num).toInt(),
  percentage: (json['percentage'] as num).toDouble(),
);

Map<String, dynamic> _$DestinationStatsResponseToJson(
  DestinationStatsResponse instance,
) => <String, dynamic>{
  'destinationName': instance.destinationName,
  'count': instance.count,
  'percentage': instance.percentage,
};
