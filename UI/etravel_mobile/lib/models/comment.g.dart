// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'comment.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Comment _$CommentFromJson(Map<String, dynamic> json) => Comment(
  id: (json['id'] as num).toInt(),
  userId: (json['userId'] as num).toInt(),
  offerId: (json['offerId'] as num).toInt(),
  comment: json['comment'] as String,
  starRate: (json['starRate'] as num).toInt(),
);

Map<String, dynamic> _$CommentToJson(Comment instance) => <String, dynamic>{
  'id': instance.id,
  'userId': instance.userId,
  'offerId': instance.offerId,
  'comment': instance.comment,
  'starRate': instance.starRate,
};
