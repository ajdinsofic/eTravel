import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:flutter/material.dart';

class contactPage extends StatefulWidget {
  const contactPage({super.key});

  @override
  State<contactPage> createState() => _contactPageState();
}

class _contactPageState extends State<contactPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: SljedecaDestinacijaIMenuBar(),
      body: Text("Ovo je contact page"),
    );
  }
}