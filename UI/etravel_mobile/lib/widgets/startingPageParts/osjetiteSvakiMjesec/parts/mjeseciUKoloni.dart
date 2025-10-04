import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class mjeseciUKoloni extends StatefulWidget {

  final double smallerWidth; 

  const mjeseciUKoloni({
    super.key,
    required this.smallerWidth,
  });

  @override
  State<mjeseciUKoloni> createState() => _MjeseciUKoloniState();
}


class _MjeseciUKoloniState extends State<mjeseciUKoloni> {
  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    final widthFactor = 0.99;

    return Container(
      width: screenWidth * (widthFactor - widget.smallerWidth),
      height: screenHeight * 0.06,
      margin: const EdgeInsets.only(top: 10),
      decoration: BoxDecoration(
        border: Border.all(color: Colors.white),
      ),
    );
  }
}
