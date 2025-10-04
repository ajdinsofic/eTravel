import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class PutujemoZajednoNaslovISearchBar extends StatefulWidget {

  const PutujemoZajednoNaslovISearchBar({
    super.key,
  });

  @override
  State<PutujemoZajednoNaslovISearchBar> createState() =>
      _PutujemoZajednoNaslovISearchBarState();
}

class _PutujemoZajednoNaslovISearchBarState
    extends State<PutujemoZajednoNaslovISearchBar> {
  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);
    return SliverAppBar(
      automaticallyImplyLeading: false,
      expandedHeight: screenHeight * 0.45,
      pinned: false,
      backgroundColor: Color(0xFF67B1E5),
      flexibleSpace: FlexibleSpaceBar(
        background: Center(
          child: Align(
            alignment: Alignment(0, 0),
            child: SizedBox(
              height: screenHeight * 0.45,
              width: screenWidth * 0.95,
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  SizedBox(
                    height: screenHeight * 0.29,
                    width: screenWidth * 0.9,
                    child: Text(
                      'Putujemo zajedno i sa osmjehom',
                      style: TextStyle(
                        color: Colors.white,
                        fontSize: screenWidth * 0.13,
                        fontWeight: FontWeight.bold,
                        fontFamily: 'AROneSans',
                      ),
                    ),
                  ),
                  Container(
                    margin: EdgeInsets.only(top: screenHeight * 0.1 - 50),
                    child: TextField(
                      decoration: InputDecoration(
                        filled: true,
                        fillColor: Color(0xFFECE6F0),
                        hintText: 'Istražite destinacije',
                        hintStyle: TextStyle(
                          color: Colors.black,
                          fontWeight: FontWeight.bold,
                          fontSize: screenWidth * 0.045,
                        ),
                        prefixIcon: Padding(
                          padding: EdgeInsets.only(
                            left: screenWidth * 0.05,
                            right: screenWidth * 0.02,
                          ),
                          child: Icon(
                            Icons.search,
                            color: Colors.black,
                          ), // ili stavi Image.asset ako želiš tvoju sliku
                        ),
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(40),
                          borderSide: BorderSide.none,
                        ),
                        contentPadding: EdgeInsets.symmetric(
                          vertical: screenHeight * 0.02,
                          horizontal: screenWidth * 0.03,
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}
