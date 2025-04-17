import 'package:etravel_app/widgets/startingPageParts/popularneDestinacijeIDodaci/parts/sliderTackeZaPopularneDestinacije.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class popularneDestinacije extends StatelessWidget {

  const popularneDestinacije({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);
    return Column(
      children: [
        Text(
          'Popularne destinacije',
          style: TextStyle(
            color: Colors.black,
            fontWeight: FontWeight.bold,
            fontFamily: 'AROneSans',
            fontSize: screenWidth * 0.06,
          ),
        ),
        SizedBox(height: screenHeight * 0.02),
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: [
            Transform.translate(
              offset: Offset(-screenWidth * 0.08, 0),
              child: ClipRRect(
                borderRadius: BorderRadius.circular(20),
                child: SizedBox(
                  height: screenWidth * 0.5,
                  width: screenWidth * 0.3,
                  child: AspectRatio(
                    aspectRatio: 13 / 18,
                    child: Image.asset(
                      'assets/images/istambulPonuda.jpg',
                      fit: BoxFit.cover,
                    ),
                  ),
                ),
              ),
            ),
            ClipRRect(
              borderRadius: BorderRadius.circular(20),
              child: SizedBox(
                height: screenWidth * 0.5,
                width: screenWidth * 0.35,
                child: AspectRatio(
                  aspectRatio: 13 / 18,
                  child: Image.asset(
                    'assets/images/coverSantorini.jpg',
                    fit: BoxFit.cover,
                  ),
                ),
              ),
            ),
            Transform.translate(
              offset: Offset(screenWidth * 0.08, 0),
              child: ClipRRect(
                borderRadius: BorderRadius.circular(20),
                child: SizedBox(
                  height: screenWidth * 0.5,
                  width: screenWidth * 0.3,
                  child: AspectRatio(
                    aspectRatio: 13 / 18,
                    child: Image.asset(
                      'assets/images/santoriniPonuda.jpg',
                      fit: BoxFit.cover,
                    ),
                  ),
                ),
              ),
            ),
          ],
        ),
        SizedBox(height: screenHeight * 0.02),
        SizedBox(
          width: screenWidth * 0.35,
          height: screenHeight * 0.05,
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            children: [
              sliderTackeZaPopularneDestinacije(false),
              sliderTackeZaPopularneDestinacije(true),
              sliderTackeZaPopularneDestinacije(false),
            ],
          ),
        ),
      ],
    );
  }
}
