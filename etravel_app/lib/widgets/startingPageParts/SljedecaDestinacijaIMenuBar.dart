import 'package:flutter/material.dart';

class SljedecaDestinacijaIMenuBar extends StatefulWidget implements PreferredSizeWidget {
  final double screenWidth;
  final double screenHeight;

  const SljedecaDestinacijaIMenuBar({
    super.key,
    required this.screenWidth,
    required this.screenHeight,
  });

  @override
  State<SljedecaDestinacijaIMenuBar> createState() =>
      _SljedecaDestinacijaIMenuBarState();

  @override
  Size get preferredSize => Size.fromHeight(screenHeight * 0.1);
}

class _SljedecaDestinacijaIMenuBarState
    extends State<SljedecaDestinacijaIMenuBar> {
  @override
  Widget build(BuildContext context) {
    return AppBar(
      toolbarHeight: widget.screenHeight * 0.1,
      backgroundColor: const Color(0xFF67B1E5),
      elevation: 0,
      flexibleSpace: Center(
        child: Container(
          height: widget.screenHeight * 0.09,
          margin: EdgeInsets.only(top: widget.screenHeight * 0.045),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Container(
                height: widget.screenHeight * 0.05,
                width: widget.screenWidth * 0.6,
                margin: EdgeInsets.only(left: widget.screenWidth * 0.025),
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
                        fontSize: widget.screenWidth * 0.03,
                        fontFamily: 'AROneSans',
                      ),
                    ),
                  ],
                ),
              ),
              Container(
                height: widget.screenHeight * 0.045,
                width: widget.screenWidth * 0.12,
                margin: EdgeInsets.only(right: widget.screenWidth * 0.025),
                child: Center(child: Image.asset('assets/images/lines.png')),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
