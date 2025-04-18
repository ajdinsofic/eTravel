import 'package:etravel_app/screens/startingPage.dart';
import 'package:etravel_app/widgets/navigationPageParts/routingPart.dart';
import 'package:etravel_app/widgets/startingPageParts/SljedecaDestinacijaIMenuBar.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class NavigationPage extends StatefulWidget {
  const NavigationPage({super.key});

  @override
  State<NavigationPage> createState() => _NavigationpageState();
}

class _NavigationpageState extends State<NavigationPage> {
  bool isPraznicnaExpanded = false;
  bool isMjesecExpanded = false;

  final List<String> mainItems = [
    'PROFIL',
    'PUTOVANJA',
    'SPECIJALNE PONUDE',
    'PRAZNICNA PUTOVANJA',
    'OSJETITE MJESEC',
    'KONTAKT',
    'TRAŽI SE POSAO',
    'VAUČERI',
    'LOGOUT',
  ];

  final List<String> praznicnaPutovanja = [
    'Nova godina',
    'Božić',
    'Prvi maj',
  ];

  final List<String> mjeseci = [
    'JANUAR', 'FEBRUAR', 'MART', 'APRIL', 'MAJ', 'JUNI',
    'JULI', 'AUGUST', 'SEPTEMBAR', 'OKTOBAR', 'NOVEMBAR', 'DECEMBAR',
  ];

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: SljedecaDestinacijaIMenuBar(),
      body: ListView(
        children: [
          ...mainItems.map((item) {
            if (item == 'PRAZNICNA PUTOVANJA') {
              return routingPars(
                title: item,
                children: praznicnaPutovanja.map((e) => Text(e)).toList(),
              );
            } else if (item == 'OSJETITE MJESEC') {
              return routingPars(
                title: item,
                // isExpanded: isMjesecExpanded,
                // onTap: () {
                //   setState(() {
                //     isMjesecExpanded = !isMjesecExpanded;
                //   });
                // },
                children: mjeseci.map((e) => Text(e)).toList(),
              );
            } else {
              return Column(
                children: [
                  ListTile(
                    title: Text(
                      item,
                      style: const TextStyle(fontWeight: FontWeight.bold),
                    ),
                    onTap: () {
                      // Navigacija po potrebi
                    },
                  ),
                  const Divider(height: 1),
                ],
              );
            }
          }).toList(),
        ],
      ),
    );
  }
}
