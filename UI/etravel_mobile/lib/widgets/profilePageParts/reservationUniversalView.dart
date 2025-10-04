import 'package:etravel_app/providers/reservationsProvider.dart';
import 'package:etravel_app/widgets/profilePageParts/reservationActiveContainer.dart';
import 'package:etravel_app/widgets/profilePageParts/reservationEndContainer.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class ReservationUniversalView extends StatelessWidget {
  final String imeRezervacija;
  final bool daliJeActive;

  final List<String> listaSlika = ["istambulPonuda.jpg", "coverSantorini.jpg", "santoriniPonuda.jpg"];

  ReservationUniversalView({
    super.key,
    required this.imeRezervacija,
    required this.daliJeActive,
  });

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);
    return Consumer<ReservationsProvider>(
      builder: (context, value, child) {
        return Container(
          width: screenWidth * 0.95,
          decoration: BoxDecoration(
            border: Border.all(color: Color(0xFF67B1E5)),
            borderRadius: BorderRadius.circular(20),
          ),
          child: Column(
            children: [
              Container(
                width: screenWidth,
                height: screenHeight * 0.1,
                alignment: Alignment.center,
                child: Text(
                  imeRezervacija,
                  style: TextStyle(
                    fontFamily: 'AROneSans',
                    fontWeight: FontWeight.bold,
                    fontSize: screenWidth * 0.06,
                  ),
                ),
              ),
              SizedBox(height: screenHeight * 0.03),
              Column(
                children: List.generate(
                  3,
                  (index) => Column(
                    children: [
                      daliJeActive
                          ? ReservationActiveContainer(index: index, slika: listaSlika[index])
                          : ReservationEndContainer(index: index, slika: listaSlika[index]),
                      SizedBox(height: screenHeight * 0.01),
                    ],
                  ),
                ),
              ),
            ],
          ),
        );
      },
    );
  }
}
