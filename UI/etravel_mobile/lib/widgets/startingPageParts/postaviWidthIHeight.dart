import 'package:flutter/widgets.dart';

double screenWidth = 0;
double screenHeight = 0;

void postaviWidthIHeight(BuildContext context) {
  screenWidth = MediaQuery.of(context).size.width;
  screenHeight = MediaQuery.of(context).size.height;
}
