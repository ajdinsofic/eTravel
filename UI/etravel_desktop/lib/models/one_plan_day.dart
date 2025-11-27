import 'package:flutter/material.dart';

class PlanDay {
  int dayNumber;
  bool isOpen;

  final TextEditingController titleController = TextEditingController();
  final TextEditingController descriptionController = TextEditingController();

  PlanDay({required this.dayNumber, this.isOpen = false});
}