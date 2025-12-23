import 'package:etravel_app/screens/NavigationPage.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class ReservationBackIMenuBar extends StatefulWidget
    implements PreferredSizeWidget {

  final bool daLijeKliknuo;
  const ReservationBackIMenuBar({
    super.key,
    required this.daLijeKliknuo,
  });

  @override
  State<ReservationBackIMenuBar> createState() =>
      _ReservationBackIMenuBarState();

  @override
  Size get preferredSize => Size.fromHeight(screenHeight * 0.1);
}

class _ReservationBackIMenuBarState
    extends State<ReservationBackIMenuBar> {

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);
    bool daLijeKliknuo = widget.daLijeKliknuo;

    return SliverAppBar(
      pinned: true,
      floating: false,
      snap: false,
      expandedHeight: screenHeight * 0.12,
      collapsedHeight: screenHeight * 0.12,
      toolbarHeight: screenHeight * 0.1,
      backgroundColor: const Color(0xFF67B1E5),
      automaticallyImplyLeading: false,
      elevation: 0,
      flexibleSpace: Center(
        child: Container(
          height: screenHeight * 0.09,
          margin: EdgeInsets.only(top: screenHeight * 0.045),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [

              // -----------------------------------------
              // LIJEVA STRANA — BACK NA PRETHODNI SCREEN
              // -----------------------------------------
              GestureDetector(
                onTap: () {
                  Navigator.pop(context);
                },
                child: Container(
                  height: screenHeight * 0.05,
                  width: screenWidth * 0.45,
                  margin: EdgeInsets.only(left: screenWidth * 0.025),
                  decoration: BoxDecoration(
                    border: Border.all(color: Colors.white, width: 2),
                    borderRadius: BorderRadius.circular(20),
                  ),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      const Icon(
                        Icons.arrow_back_ios_new,
                        color: Colors.white,
                        size: 16,
                      ),
                      const SizedBox(width: 6),
                      Text(
                        'POVRATAK',
                        style: TextStyle(
                          color: Colors.white,
                          fontWeight: FontWeight.bold,
                          fontSize: screenWidth * 0.032,
                          fontFamily: 'AROneSans',
                        ),
                      ),
                    ],
                  ),
                ),
              ),

              // -----------------------------------------
              // DESNA STRANA — MENI (LINES / X)
              // -----------------------------------------
              Container(
                height: screenHeight * 0.045,
                width: screenWidth * 0.12,
                margin: EdgeInsets.only(right: screenWidth * 0.025),
                child: GestureDetector(
                  onTap: () {
                    if (!daLijeKliknuo) {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => NavigationPage(),
                        ),
                      );
                    } else {
                      Navigator.pop(context);
                    }

                    setState(() {
                      daLijeKliknuo = !daLijeKliknuo;
                    });
                  },
                  child: Center(
                    child: Image.asset(
                      daLijeKliknuo
                          ? 'assets/images/iks.png'
                          : 'assets/images/lines.png',
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
