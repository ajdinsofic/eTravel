import 'package:etravel_app/screens/UniversalOfferPage.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart'; // 👈 prilagodi putanju

class TravelTogetherHeadlineISearchBar extends StatefulWidget {
  const TravelTogetherHeadlineISearchBar({super.key});

  @override
  State<TravelTogetherHeadlineISearchBar> createState() =>
      _TravelTogetherHeadlineISearchBarState();
}

class _TravelTogetherHeadlineISearchBarState
    extends State<TravelTogetherHeadlineISearchBar> {
  final TextEditingController _searchController = TextEditingController();

  void _onSubmit(String value) {
    final query = value.trim();

    if (query.isEmpty) return;

    Navigator.of(context).push(
      MaterialPageRoute(
        builder: (_) => Universalofferpage(
          searchQuery: query, // 🔥 OVDJE IDE SEARCH
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    return SliverAppBar(
      automaticallyImplyLeading: false,
      expandedHeight: screenHeight * 0.45,
      pinned: false,
      backgroundColor: const Color(0xFF67B1E5),
      flexibleSpace: FlexibleSpaceBar(
        background: Center(
          child: SizedBox(
            height: screenHeight * 0.45,
            width: screenWidth * 0.95,
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                SizedBox(
                  height: screenHeight * 0.29,
                  width: screenWidth * 0.9,
                  child: Session.token == null
    ? Text(
        'Putujemo zajedno i sa osmjehom',
        style: TextStyle(
          color: Colors.white,
          fontSize: screenWidth * 0.13,
          fontWeight: FontWeight.bold,
          fontFamily: 'AROneSans',
        ),
      )
    : Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            'Dobrodošli, ${Session.username}',
            textAlign: TextAlign.center,
            style: TextStyle(
              color: Colors.white,
              fontSize: screenWidth * 0.10, // ⬅ ISTA VELIČINA
              fontWeight: FontWeight.bold,
              fontFamily: 'AROneSans',

            ),
          ),
          const SizedBox(height: 8),
          Text(
            'Gdje putujemo danas?',
            textAlign: TextAlign.center,
            style: TextStyle(
              color: Colors.white,
              fontSize: screenWidth * 0.08, // ⬅ ISTI FONT & SIZE
              fontWeight: FontWeight.bold,
              fontFamily: 'AROneSans',
            ),
          ),
        ],
      ),

                ),

                // SEARCH BAR
                Container(
                  margin: EdgeInsets.only(top: screenHeight * 0.1 - 50),
                  child: TextField(
                    controller: _searchController,
                    textInputAction: TextInputAction.search,
                    onSubmitted: _onSubmit, // 🔥 KLJUČNO
                    decoration: InputDecoration(
                      filled: true,
                      fillColor: const Color(0xFFECE6F0),
                      hintText: 'Istražite destinacije',
                      hintStyle: TextStyle(
                        color: Colors.black,
                        fontWeight: FontWeight.bold,
                        fontSize: screenWidth * 0.045,
                      ),
                      prefixIcon:
                          const Icon(Icons.search, color: Colors.black),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(40),
                        borderSide: BorderSide.none,
                      ),
                    ),
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
