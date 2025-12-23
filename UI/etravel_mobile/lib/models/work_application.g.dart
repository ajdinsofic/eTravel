// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'work_application.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

WorkApplication _$WorkApplicationFromJson(Map<String, dynamic> json) =>
    WorkApplication(
      id: (json['id'] as num).toInt(),
      userId: (json['userId'] as num).toInt(),
      cvFileName: json['cvFileName'] as String,
      appliedAt: DateTime.parse(json['appliedAt'] as String),
      letter: json['letter'] as String?,
    );

Map<String, dynamic> _$WorkApplicationToJson(WorkApplication instance) =>
    <String, dynamic>{
      'id': instance.id,
      'userId': instance.userId,
      'cvFileName': instance.cvFileName,
      'appliedAt': instance.appliedAt.toIso8601String(),
      'letter': instance.letter,
    };
