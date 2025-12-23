import 'package:etravel_app/screens/ContactPage.dart';
import 'package:etravel_app/screens/JobSearchPage.dart';
import 'package:etravel_app/screens/ProfilePage.dart';
import 'package:etravel_app/screens/PromoCodePage.dart';
import 'package:etravel_app/screens/StartingPage.dart';
import 'package:etravel_app/screens/UniversalOfferPage.dart';
import 'package:etravel_app/screens/loginPage.dart';
import 'package:etravel_app/screens/registerPage.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../providers/category_provider.dart';
import '../providers/sub_category_provider.dart';
import '../utils/session.dart';
import '../widgets/SljedecaDestinacijaIMenuBar.dart';

class NavigationPage extends StatefulWidget {
  const NavigationPage({super.key});

  @override
  State<NavigationPage> createState() => _NavigationPageState();
}

class _NavigationPageState extends State<NavigationPage> {
  late CategoryProvider _categoryProvider;
  late SubCategoryProvider _subCategoryProvider;

  List<Map<String, dynamic>> menuItems = [];
  bool isLoadingMenu = true;

  @override
  void initState() {
    super.initState();
    _categoryProvider = Provider.of<CategoryProvider>(context, listen: false);
    _subCategoryProvider = Provider.of<SubCategoryProvider>(
      context,
      listen: false,
    );
    _loadMenuItems();
  }

  /// ============================
  /// LOAD MENU ITEMS
  /// ============================
  Future<void> _loadMenuItems() async {
    List<Map<String, dynamic>> items = [];

    // LOGIN / REGISTER
    if (Session.token == null) {
      items.add({"sekcija": "LOGIN", "widget": LoginPage()});
      items.add({"sekcija": "REGISTRACIJA", "widget": RegisterPage()});
    } else {
      items.add({"sekcija": "PROFIL", "widget": profilePage()});
    }

    // Load categories
    var categories = await _categoryProvider.get();

    for (var cat in categories.items) {
      // Load subcategories for this category
      var subCats = await _subCategoryProvider.get(
        filter: {"categoryId": cat.id},
      );

      bool hasNoSubcategories =
          subCats.items.length == 1 && subCats.items.first.id == -1;

      // Category without subcategories → direct open
      if (hasNoSubcategories) {
        items.add({
          "sekcija": cat.name.toUpperCase(),
          "subCategoryId": cat.id,
          "hasSub": false,
        });
      }
      // Category with subcategories
      else {
        items.add({
          "sekcija": cat.name.toUpperCase(),
          "hasSub": true,
          "podSekcije":
              subCats.items.map((e) => {"name": e.name, "id": e.id}).toList(),
        });
      }
    }

    Future<void> _handleLogout(BuildContext context) async {
      final confirm = await _showLogoutConfirmPopup(context);

      if (confirm != true) return;

      final username = Session.username ?? "";

      // očisti session
      Session.odjava();

      // toast (mora prije navigacije)
      _showLogoutToast(context, username);

      // kratki delay da se toast vidi
      await Future.delayed(const Duration(milliseconds: 300));

      // navigacija
      Navigator.of(context).pushAndRemoveUntil(
        MaterialPageRoute(builder: (_) => const StartingPage()),
        (route) => false,
      );
    }

    // STATIC ITEMS
    items.add({"sekcija": "KONTAKT", "widget": ContactPage()});
    if (Session.token != null) {
      items.add({"sekcija": "TRAŽI SE POSAO", "widget": JobSearchPage()});

      items.add({"sekcija": "VAUČERI", "widget": VoucherPage()});
    }

    if (Session.token != null) {
      items.add({"sekcija": "LOGOUT", "onTap": () => _handleLogout(context)});
    }

    setState(() {
      menuItems = items;
      isLoadingMenu = false;
    });
  }

  /// ============================
  /// BUILD
  /// ============================
  @override
  Widget build(BuildContext context) {
    if (isLoadingMenu) {
      return const Scaffold(body: Center(child: CircularProgressIndicator()));
    }

    return Scaffold(
      backgroundColor: Colors.white,
      body: CustomScrollView(
        slivers: [
          SljedecaDestinacijaIMenuBar(daLijeKliknuo: true),

          SliverList(
            delegate: SliverChildListDelegate(
              menuItems.map((item) {
                final String title = item["sekcija"];
                final bool hasSub = item["hasSub"] == true;

                // 🔴 1️⃣ ACTION ITEM (LOGOUT)
                // 🔴 LOGOUT / ACTION ITEM (NE OTVARA SCREEN)
                if (item.containsKey("onTap")) {
                  return Container(
                    decoration: const BoxDecoration(
                      border: Border(
                        bottom: BorderSide(color: Color(0xFFDDDDDD), width: 1),
                      ),
                    ),
                    child: Material(
                      color: Colors.transparent,
                      child: ListTile(
                        title: Text(
                          title,
                          style: const TextStyle(fontWeight: FontWeight.bold),
                        ),
                        onTap: item["onTap"],
                      ),
                    ),
                  );
                }

                // 1️⃣ CATEGORY WITH SUBCATEGORIES
                if (hasSub) {
                  final List<Map<String, dynamic>> subs =
                      (item["podSekcije"] as List).cast<Map<String, dynamic>>();

                  return Column(
                    children: [
                      ExpansionTile(
                        title: Text(
                          title,
                          style: const TextStyle(fontWeight: FontWeight.bold),
                        ),
                        children:
                            subs.map((sub) {
                              return Column(
                                children: [
                                  ListTile(
                                    title: Text(sub["name"]),
                                    onTap: () {
                                      Navigator.push(
                                        context,
                                        MaterialPageRoute(
                                          builder:
                                              (_) => Universalofferpage(
                                                title: sub["name"],
                                                subCategoryId: sub["id"],
                                              ),
                                        ),
                                      );
                                    },
                                  ),
                                  const Divider(
                                    height: 1,
                                    thickness: 1,
                                    color: Color(0xFFDDDDDD),
                                  ),
                                ],
                              );
                            }).toList(),
                      ),
                      const Divider(
                        height: 1,
                        thickness: 1,
                        color: Color(0xFFDDDDDD),
                      ),
                    ],
                  );
                }

                // 2️⃣ CATEGORY WITHOUT SUBCATEGORIES
                if (!item.containsKey("widget")) {
                  return Container(
                    decoration: const BoxDecoration(
                      border: Border(
                        bottom: BorderSide(color: Color(0xFFDDDDDD), width: 1),
                      ),
                    ),
                    child: ListTile(
                      title: Text(
                        title,
                        style: const TextStyle(fontWeight: FontWeight.bold),
                      ),
                      onTap: () {
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder:
                                (_) => Universalofferpage(
                                  title: title,
                                  subCategoryId: item["subCategoryId"],
                                ),
                          ),
                        );
                      },
                    ),
                  );
                }

                // 3️⃣ CLASSIC MENU ITEM
                return Container(
                  decoration: const BoxDecoration(
                    border: Border(
                      bottom: BorderSide(color: Color(0xFFDDDDDD), width: 1),
                    ),
                  ),
                  child: ListTile(
                    title: Text(
                      title,
                      style: const TextStyle(fontWeight: FontWeight.bold),
                    ),
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(builder: (_) => item["widget"]),
                      );
                    },
                  ),
                );
              }).toList(),
            ),
          ),
        ],
      ),
    );
  }

  Future<bool?> _showLogoutConfirmPopup(BuildContext context) {
    return showDialog<bool>(
      context: context,
      barrierDismissible: false,
      builder: (_) {
        return AlertDialog(
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(12),
          ),
          title: const Text(
            "Odjava",
            textAlign: TextAlign.center,
            style: TextStyle(fontWeight: FontWeight.bold),
          ),
          content: Text(
            "Da li ste sigurni da želite da se odjavite?\n\n"
            "👤 ${Session.username ?? ''}",
            textAlign: TextAlign.center,
          ),
          actionsAlignment: MainAxisAlignment.center,
          actions: [
            OutlinedButton(
              onPressed: () => Navigator.pop(context, false),
              style: OutlinedButton.styleFrom(
                foregroundColor: Colors.blue,
                side: const BorderSide(color: Colors.blue, width: 2),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: const Text("Otkaži"),
            ),
            ElevatedButton(
              onPressed: () => Navigator.pop(context, true),
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.redAccent,
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: const Text("Da, odjavi"),
            ),
          ],
        );
      },
    );
  }

  void _showLogoutToast(BuildContext context, String username) {
    final overlay = Overlay.of(context);
    if (overlay == null) return;

    late OverlayEntry entry;

    entry = OverlayEntry(
      builder:
          (_) => Positioned(
            bottom: 20,
            right: 20,
            child: Material(
              color: Colors.transparent,
              child: AnimatedOpacity(
                opacity: 1,
                duration: const Duration(milliseconds: 300),
                child: Container(
                  padding: const EdgeInsets.symmetric(
                    horizontal: 16,
                    vertical: 12,
                  ),
                  decoration: BoxDecoration(
                    color: Colors.green.shade600,
                    borderRadius: BorderRadius.circular(10),
                  ),
                  child: Text(
                    "✓ $username uspješno odjavljen",
                    style: const TextStyle(color: Colors.white, fontSize: 16),
                  ),
                ),
              ),
            ),
          ),
    );

    overlay.insert(entry);

    Future.delayed(const Duration(seconds: 3), () {
      entry.remove();
    });
  }
}
