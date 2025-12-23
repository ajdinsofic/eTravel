import 'package:etravel_app/screens/NavigationPage.dart';
import 'package:etravel_app/screens/StartingPage.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class SljedecaDestinacijaIMenuBar extends StatefulWidget
    implements PreferredSizeWidget {
  final bool daLijeKliknuo;
  final Future<bool> Function()? onBeforeNavigate;
  const SljedecaDestinacijaIMenuBar({
    super.key,
    required this.daLijeKliknuo,
    this.onBeforeNavigate,
  });

  @override
  State<SljedecaDestinacijaIMenuBar> createState() =>
      _SljedecaDestinacijaIMenuBarState();

  @override
  Size get preferredSize => Size.fromHeight(screenHeight * 0.1);
}

class _SljedecaDestinacijaIMenuBarState
    extends State<SljedecaDestinacijaIMenuBar> {
  Future<bool> _canNavigate() async {
    if (widget.onBeforeNavigate == null) return true;
    return await widget.onBeforeNavigate!();
  }

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);
    bool daLijeKliknuo = widget.daLijeKliknuo;

    return SliverAppBar(
      pinned: true, // 🔥 FIKSNIRANO ZA VRH
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
              //-----------------------------------------
              // LIJEVA STRANA — "SLJEDECA DESTINACIJA"
              //-----------------------------------------
              GestureDetector(
                onTap: () async {
                  final canGo = await _canNavigate();
                  if (!canGo) return;

                  setState(() {
                    daLijeKliknuo = false;
                  });

                  Navigator.pushAndRemoveUntil(
                    context,
                    MaterialPageRoute(builder: (context) => StartingPage()),
                    (route) => false,
                  );
                },

                child: Container(
                  height: screenHeight * 0.05,
                  width: screenWidth * 0.6,
                  margin: EdgeInsets.only(left: screenWidth * 0.025),
                  decoration: BoxDecoration(
                    border: Border.all(color: Colors.white, width: 2),
                    borderRadius: BorderRadius.circular(20),
                  ),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Image.asset('assets/images/world.png'),
                      const SizedBox(width: 6),
                      Text(
                        'SLJEDECA DESTINACIJA?',
                        style: TextStyle(
                          color: Colors.white,
                          fontWeight: FontWeight.bold,
                          fontSize: screenWidth * 0.03,
                          fontFamily: 'AROneSans',
                        ),
                      ),
                    ],
                  ),
                ),
              ),

              //-----------------------------------------
              // DESNA STRANA — MENI IKONA (LINES / X)
              //-----------------------------------------
              Container(
                height: screenHeight * 0.045,
                width: screenWidth * 0.12,
                margin: EdgeInsets.only(right: screenWidth * 0.025),
                child: GestureDetector(
                  onTap: () async {
                    final canGo = await _canNavigate();
                    if (!canGo) return;

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
