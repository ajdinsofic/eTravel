import 'package:etravel_desktop/models/user.dart';
import 'package:etravel_desktop/providers/user_provider.dart';
import 'package:etravel_desktop/screens/login_screen.dart';
import 'package:etravel_desktop/screens/offer_screen.dart';
import 'package:etravel_desktop/screens/report_screen.dart';
import 'package:etravel_desktop/screens/reservations_screen.dart';
import 'package:etravel_desktop/screens/review_screen.dart';
import 'package:etravel_desktop/screens/user_screen.dart';
import 'package:etravel_desktop/screens/view_my_profile_screen.dart';
import 'package:etravel_desktop/screens/work_applicatons_screen.dart';
import 'package:etravel_desktop/screens/worker_screen.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';
import '../config/api_config.dart';
import '../utils/session.dart';

class MasterScreen extends StatefulWidget {
  final Widget child;
  final int selectedIndex;

  const MasterScreen({
    super.key,
    required this.child,
    required this.selectedIndex,
  });

  @override
  State<MasterScreen> createState() => _MasterScreenState();
}

class _MasterScreenState extends State<MasterScreen> {
  bool _showUserMenu = false;
  User? loggedUser;

  void _navigate(int index) {
    Widget screen;

    switch (index) {
      case 0:
        screen = OfferScreen();
        break;
      case 1:
        screen = ReviewScreen();
        break;
      case 2:
        screen = ReservationScreen();
        break;
      case 3:
        screen = UserScreen();
        break;
      case 4:
        screen = EmployeeScreen();
        break;
      case 5:
        screen = WorkApplicationsScreen();
        break;
      case 6:
        screen = ReportScreen();
        break;
      default:
        screen = OfferScreen();
    }

    Navigator.pushReplacement(
      context,
      MaterialPageRoute(
        builder: (_) => MasterScreen(selectedIndex: index, child: screen),
      ),
    );
  }

  @override
  void initState() {
    super.initState();
    _loadMyUser();
  }

  Future<void> _loadMyUser() async {
    final userProvider = Provider.of<UserProvider>(context, listen: false);

    // učitaj ponovo trenutnog usera
    final myUser = await userProvider.getById(Session.userId!);

    setState(() {
      loggedUser =
          myUser; // ovo je varijabla koja drži podatke u MasterScreen-u
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Stack(
        children: [
          Column(children: [_buildHeader(), Expanded(child: widget.child)]),

          // USER MENU DROPDOWN
          if (_showUserMenu) Positioned(top: 70, right: 20, child: _userMenu()),
        ],
      ),
    );
  }

  // ============================================================
  // HEADER (TOP NAVIGATION BAR)
  // ============================================================
  Widget _buildHeader() {
    return Container(
      height: 70,
      padding: const EdgeInsets.symmetric(horizontal: 24),
      color: const Color(0xFF64B5F6),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          // LOGO
          Text(
            "eTravel",
            style: GoogleFonts.leckerliOne(color: Colors.white, fontSize: 32),
          ),

          // NAVIGATION LINKS
          Row(
            children: [
              _topLink("Ponude", 0),
              _topLink("Recenzije", 1),
              _topLink("Rezervacije", 2),
              _topLink("Korisnici", 3),
              if (Session.roles.contains("Direktor")) _topLink("Radnici", 4),
              if (Session.roles.contains("Direktor")) _topLink("Aplikanti", 5),
              _topLink("Izvještaji", 6),
            ],
          ),

          // USER MENU BUTTON
          GestureDetector(
            onTap:
                () => setState(() {
                  _showUserMenu = !_showUserMenu;
                }),
            child: Row(
              children: [
                const CircleAvatar(
                  radius: 18,
                  backgroundColor: Colors.white,
                  child: Icon(Icons.person, color: Colors.black87),
                ),
                const SizedBox(width: 8),
                Text(
                  loggedUser!.username ?? "Korisnik",
                  style: const TextStyle(color: Colors.white),
                ),
                const Icon(Icons.keyboard_arrow_down, color: Colors.white),
              ],
            ),
          ),
        ],
      ),
    );
  }

  // ============================================================
  // TOP LINKS (NAVIGATION)
  // ============================================================
  Widget _topLink(String text, int index) {
    final bool selected = widget.selectedIndex == index;

    return InkWell(
      onTap: () => _navigate(index),
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 14),
        child: Text(
          text,
          style: TextStyle(
            fontSize: 16,
            color: selected ? Colors.white : Colors.white70,
            fontWeight: selected ? FontWeight.bold : FontWeight.normal,
          ),
        ),
      ),
    );
  }

  // ============================================================
  // USER DROPDOWN MENU
  // ============================================================
  Widget _userMenu() {
    return Container(
      width: 220,
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(14),
        boxShadow: [
          BoxShadow(
            color: Colors.black.withOpacity(0.15),
            blurRadius: 12,
            offset: const Offset(0, 4),
          ),
        ],
      ),
      child: Column(
        children: [
          // USER INFO
          Container(
            padding: const EdgeInsets.all(16),
            alignment: Alignment.centerLeft,
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  Session.username ?? "",
                  style: const TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 4),
                Text(
                  Session.roles.join(", "),
                  style: const TextStyle(fontSize: 12, color: Colors.grey),
                ),
              ],
            ),
          ),

          const Divider(),

          ListTile(
            leading: const Icon(Icons.person),
            title: const Text("Moj profil"),
            onTap: () async {
              final userProvider = Provider.of<UserProvider>(
                context,
                listen: false,
              );

              final myUser = await userProvider.getById(Session.userId!);

              final result = await showDialog(
                context: context,
                barrierDismissible: true,
                builder: (_) => ViewMyProfileEditPopup(user: myUser),
              );

              if (result == true) {
                await _loadMyUser(); // ponovo učitaj usera
                setState(() {}); // osvježi UI
              }
            },
          ),

          const Divider(),

          ListTile(
            leading: const Icon(Icons.logout),
            title: const Text("Odjava"),
            onTap: () {
              Session.odjava();

              Navigator.pushReplacement(
                context,
                MaterialPageRoute(builder: (context) => LoginScreen()),
              );
            },
          ),
        ],
      ),
    );
  }
}
