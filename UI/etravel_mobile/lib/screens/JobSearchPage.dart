import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:flutter/material.dart';


class jobSearchPage extends StatefulWidget {
  const jobSearchPage({super.key});

  @override
  State<jobSearchPage> createState() => _jobSearchPageState();
}

class _jobSearchPageState extends State<jobSearchPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: SljedecaDestinacijaIMenuBar(),
      body: Text("Ovo je job page"),
    );
  }
}