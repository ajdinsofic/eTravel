import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:flutter/material.dart';

class logoutPage extends StatefulWidget {
  const logoutPage({super.key});

  @override
  State<logoutPage> createState() => _logoutPageState();
}

class _logoutPageState extends State<logoutPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: SljedecaDestinacijaIMenuBar(),
      body: Text("Ovo je log out page"),
    );
  }
}