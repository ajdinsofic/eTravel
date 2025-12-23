import 'package:flutter/material.dart';

class PlanDay {
  int? id;            //  🔥 ID iz baze — null znači novi dan
  int dayNumber;
  bool isOpen;
  bool isNew = false;

  final TextEditingController titleController = TextEditingController();
  final TextEditingController descriptionController = TextEditingController();

  PlanDay({
    this.id,
    required this.dayNumber,
    this.isOpen = false,
    required this.isNew
  });
}
