import 'package:etravel_app/widgets/startingPageParts/popularneDestinacijeIDodaci/parts/sliderTackeZaPopularneDestinacije.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class popularneDestinacije extends StatefulWidget {
  const popularneDestinacije({super.key});

  @override
  State<popularneDestinacije> createState() => _popularneDestinacijeState();
}

class _popularneDestinacijeState extends State<popularneDestinacije> {
  final PageController _pageController = PageController(viewportFraction: 0.7);
  int _currentIndex = 0;

  final List<String> _slike = [
    'assets/images/istambulPonuda.jpg',
    'assets/images/coverSantorini.jpg',
    'assets/images/santoriniPonuda.jpg',
  ];

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
        SizedBox(
          height: screenWidth * 0.5,
          child: PageView.builder(
            controller: _pageController,
            itemCount: _slike.length,
            onPageChanged: (index) {
              setState(() {
                _currentIndex = index;
              });
            },
            itemBuilder: (context, index) {
              bool isActive = index == _currentIndex;
              final List<String> nazivi = ['Istambul', 'Santorini', 'Firenca'];

              return Transform.scale(
                scale: index == _currentIndex ? 1 : 0.9,
                child: Stack(
                  children: [
                    ClipRRect(
                      borderRadius: BorderRadius.circular(20),
                      child: Image.asset(
                        width: screenWidth * 0.8,
                        height: screenHeight * 0.5,
                        _slike[index],
                         fit: BoxFit.cover),
                    ),
                    if (isActive)
                      Container(
                        decoration: BoxDecoration(
                          color: Colors.black.withOpacity(0.3),
                          borderRadius: BorderRadius.circular(20)
                        ),
                      ),
                    if (isActive)
                      Center(
                        child: Text(
                          nazivi[index],
                          style: TextStyle(
                            color: Colors.white,
                            fontSize: screenWidth * 0.06,
                            fontWeight: FontWeight.bold,
                            shadows: [
                              Shadow(
                                color: Colors.black.withOpacity(0.7),
                                blurRadius: 10,
                                offset: Offset(0, 2),
                              ),
                            ],
                          ),
                        ),
                      ),
                  ],
                ),
              );
            },
          ),
        ),
        SizedBox(height: screenHeight * 0.02),
        SizedBox(
          width: screenWidth * 0.35,
          height: screenHeight * 0.05,
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            children: List.generate(_slike.length, (index) {
              return sliderTackeZaPopularneDestinacije(_currentIndex == index);
            }),
          ),
        ),
      ],
    );
  }
}
