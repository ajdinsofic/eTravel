import 'package:flutter/material.dart';

Widget dotSlider(bool isActive) {
    return Container(
      width: 20,
      height: 20,
      decoration: BoxDecoration(
        color: isActive ? Color(0xFF67B1E5) : Color(0xFFECE6F0),
        border: Border.all(color: Colors.black, width: 2),
        borderRadius: BorderRadius.circular(20),
      ),
    );
  }