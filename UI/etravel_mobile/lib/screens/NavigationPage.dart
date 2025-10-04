import 'package:etravel_app/providers/navigationOpenProvider.dart';
import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:flutter/material.dart';
import 'package:etravel_app/screens/ProfilePage.dart'; // Primer za dodavanje stranica
import 'package:etravel_app/screens/universalOfferPage.dart';
import 'package:etravel_app/screens/ContactPage.dart';
import 'package:etravel_app/screens/JobSearchPage.dart';
import 'package:etravel_app/screens/PromoCodePage.dart';
import 'package:etravel_app/screens/LogOutPage.dart';
import 'package:provider/provider.dart';

class NavigationPage extends StatefulWidget {
  const NavigationPage({super.key});

  @override
  State<NavigationPage> createState() => _NavigationpageState();
}

class _NavigationpageState extends State<NavigationPage> {
  final List<Map<String, Object>> mainItems2 = [
    {"sekcija": "PROFIL", "widget": profilePage()},
    {"sekcija": "SPECIJALNE PONUDE", "widget": Universalofferpage(mjesec: 'SPECIJALNE PONUDE')},
    {
      "sekcija": "PRAZNICNA PUTOVANJA",
      "podSekcije": ["Nova godina", "Božić", "Prvi maj"],
    },
    {
      "sekcija": "OSJETITE MJESEC",
      "podSekcije": [
        'JANUAR',
        'FEBRUAR',
        'MART',
        'APRIL',
        'MAJ',
        'JUNI',
        'JULI',
        'AUGUST',
        'SEPTEMBAR',
        'OKTOBAR',
        'NOVEMBAR',
        'DECEMBAR',
      ],
    },
    {"sekcija": "KONTAKT", "widget": contactPage()},
    {"sekcija": "TRAŽI SE POSAO", "widget": jobSearchPage()},
    {"sekcija": "VAUČERI", "widget": promoCodePage()},
    {"sekcija": "LOGOUT", "widget": logoutPage()},
  ];

  @override
  Widget build(BuildContext context) {
    return Consumer<NavigationProvider>(
      builder: (context, provider, child) {
        return Scaffold(
          backgroundColor: Colors.white,
          appBar: SljedecaDestinacijaIMenuBar(),
          body: ListView(
            children:
                mainItems2.map((item) {
                  String title = item["sekcija"] as String;
                  if (title == "PRAZNICNA PUTOVANJA") {
                    return ExpansionTile(
                      title: Text(
                        title,
                        style: const TextStyle(fontWeight: FontWeight.bold),
                      ),
                      initiallyExpanded: provider.isPraznicnaExpanded,
                      onExpansionChanged: (expanded) {
                        provider.togglePraznicna();
                      },
                      children:
                          (item["podSekcije"] as List<String>)
                              .map(
                                (e) => ListTile(
                                  title: Text(e),
                                  onTap: () {
                                    provider.setDaLijeKliknuo(false);
                                    Navigator.pushAndRemoveUntil(
                                      context,
                                      MaterialPageRoute(
                                        builder:
                                            (context) => Universalofferpage(
                                              mjesec: e,
                                            ), // Passing the required mjesec
                                      ),
                                      (Route<dynamic> route) =>
                                          false, // Clears previous routes
                                    );
                                  },
                                ),
                              )
                              .toList(),
                    );
                  } else if (title == "OSJETITE MJESEC") {
                    return ExpansionTile(
                      title: Text(
                        title,
                        style: const TextStyle(fontWeight: FontWeight.bold),
                      ),
                      initiallyExpanded: provider.isMjesecExpanded,
                      onExpansionChanged: (expanded) {
                        provider.toggleMjesec();
                      },
                      children:
                          (item["podSekcije"] as List<String>)
                              .map(
                                (e) => ListTile(
                                  title: Text(e),
                                  onTap: () {
                                    provider.setDaLijeKliknuo(false);
                                    Navigator.pushAndRemoveUntil(
                                      context,
                                      MaterialPageRoute(
                                        builder:
                                            (context) => Universalofferpage(mjesec: e),
                                      ),
                                      (Route<dynamic> route) =>
                                          false, // Briše sve prethodne rute
                                    );
                                  },
                                ),
                              )
                              .toList(),
                    );
                  } else {
                    return Column(
                      children: [
                        ListTile(
                          title: Text(
                            title,
                            style: const TextStyle(fontWeight: FontWeight.bold),
                          ),
                          onTap: () {
                            provider.setDaLijeKliknuo(false);
                            Navigator.pushAndRemoveUntil(
                              context,
                              MaterialPageRoute(
                                builder: (context) => item["widget"] as Widget,
                              ),
                              (Route<dynamic> route) =>
                                  false, // Briše sve prethodne rute
                            );
                          },
                        ),
                        const Divider(height: 1),
                      ],
                    );
                  }
                }).toList(),
          ),
        );
      },
    );
  }
}
