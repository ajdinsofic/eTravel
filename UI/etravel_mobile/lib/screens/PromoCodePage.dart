import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:flutter/material.dart';

class promoCodePage extends StatefulWidget {
  const promoCodePage({super.key});

  @override
  State<promoCodePage> createState() => _promoCodePageState();
}

class _promoCodePageState extends State<promoCodePage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: SljedecaDestinacijaIMenuBar(),
      body: Text("Ovo je promo code page"),
    );
  }
}