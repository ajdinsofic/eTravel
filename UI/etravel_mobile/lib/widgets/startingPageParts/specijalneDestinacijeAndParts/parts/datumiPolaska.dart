import 'package:flutter/material.dart';

class DatumiPolaska extends StatelessWidget {
  final double screenWidth;
  final double screenHeight;
  final List<String> datumi;

  const DatumiPolaska({
    super.key,
    required this.screenWidth,
    required this.screenHeight,
    required this.datumi,
  });

  @override
  Widget build(BuildContext context) {
    if (datumi.isEmpty) {
      return Padding(
        padding: const EdgeInsets.all(8.0),
        child: Text(
          "Nema dostupnih datuma.",
          style: TextStyle(
            fontFamily: 'AROneSans',
            fontSize: screenWidth * 0.035,
          ),
        ),
      );
    }

    return Container(
      margin: EdgeInsets.only(
        left: screenWidth * 0.035,
        top: screenHeight * 0.01,
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // NASLOV
          Row(
            children: [
              Image.asset("assets/images/date.png", width: 25, height: 25),
              const SizedBox(width: 6),
              const Text(
                "Datumi polaska",
                style: TextStyle(
                  fontWeight: FontWeight.bold,
                  fontFamily: 'AROneSans',
                  fontSize: 14,
                ),
              ),
            ],
          ),
          const SizedBox(height: 8),

          // DATUM TAGOVI
          Wrap(
            spacing: 8,
            runSpacing: 8,
            children: datumi.map(_datumTag).toList(),
          ),
        ],
      ),
    );
  }

  Widget _datumTag(String datum) {
    return Container(
      alignment: Alignment.center,
      width: 80,
      height: 26,
      decoration: BoxDecoration(
        color: const Color(0xFF67B1E5),
        borderRadius: BorderRadius.circular(20),
      ),
      child: Text(
        datum,
        style: TextStyle(
          color: Colors.white,
          fontSize: screenWidth * 0.03,
          fontWeight: FontWeight.bold,
        ),
      ),
    );
  }
}
