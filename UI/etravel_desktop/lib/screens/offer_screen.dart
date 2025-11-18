import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/offer_image.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import '../utils/session.dart';
import 'package:dotted_border/dotted_border.dart';

// MODELI & PROVIDER
import '../models/offer.dart';
import '../providers/offer_provider.dart';

class OfferScreen extends StatefulWidget {
  const OfferScreen({super.key});

  @override
  State<OfferScreen> createState() => _OfferScreenState();
}

class _OfferScreenState extends State<OfferScreen> {
  bool _showUserMenu = false;

  // STATE ZA API
  List<Offer> offers = [];
  bool loading = true;
  bool loadError = false;

  @override
  void initState() {
    super.initState();
    _fetchOffers();
  }

  Future<void> _fetchOffers() async {
    try {
      final provider = OfferProvider();
      final result = await provider.getOffers(
        page: 0,
        pageSize: 50,
        isMainImage: true,
      );

      setState(() {
        offers = result.items;
        loading = false;
      });
    } catch (e) {
      print("Greška API: $e");
      setState(() {
        loading = false;
        loadError = true;
      });
    }
  }

  // ============================================================
  // MAIN BUILD
  // ============================================================
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Stack(
        children: [
          Column(
            children: [
              _buildHeader(),

              Expanded(
                child: SingleChildScrollView(
                  child: Column(
                    children: [
                      _headerImage(),
                      const SizedBox(height: 32),
                      _filterSection(),
                      const SizedBox(height: 32),

                      const Text(
                        "Dodane ponude",
                        style: TextStyle(
                          fontSize: 24,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                      const SizedBox(height: 16),

                      _offerGrid(),

                      const SizedBox(height: 40),
                    ],
                  ),
                ),
              ),
            ],
          ),

          if (_showUserMenu) Positioned(top: 70, right: 24, child: _userMenu()),
        ],
      ),
    );
  }

  // ============================================================
  // HEADER
  // ============================================================
  Widget _buildHeader() {
    return Container(
      height: 70,
      padding: const EdgeInsets.symmetric(horizontal: 24),
      color: const Color(0xFF64B5F6),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Text(
            "eTravel",
            style: GoogleFonts.leckerliOne(color: Colors.white, fontSize: 32),
          ),

          Row(
            children: [
              _topLink("Ponude", selected: true),
              _topLink("Recenzije"),
              _topLink("Rezervacije"),
              _topLink("Korisnici"),
              _topLink("Radnici"),
              _topLink("Aplikanti"),
            ],
          ),

          GestureDetector(
            onTap: () => setState(() => _showUserMenu = !_showUserMenu),
            child: Row(
              children: [
                const CircleAvatar(
                  radius: 18,
                  backgroundColor: Colors.white,
                  child: Icon(Icons.person, color: Colors.black87),
                ),
                const SizedBox(width: 8),
                Text(
                  Session.username ?? "Korisnik",
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
  // USER MENU OVERLAY
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
            leading: const Icon(Icons.settings),
            title: const Text("Postavke"),
            onTap: () {},
          ),
          ListTile(
            leading: const Icon(Icons.person),
            title: const Text("Moj profil"),
            onTap: () {},
          ),

          const Divider(),

          ListTile(
            leading: const Icon(Icons.logout),
            title: const Text("Odjava"),
            onTap: () {
              Session.odjava();
              Navigator.pushReplacementNamed(context, "/login");
            },
          ),
        ],
      ),
    );
  }

  // ============================================================
  // HEADER SLIKA
  // ============================================================
  Widget _headerImage() {
    return Container(
      height: 260,
      width: double.infinity,
      decoration: BoxDecoration(
        image: DecorationImage(
          fit: BoxFit.cover,
          image: NetworkImage("${ApiConfig.imagesOffers}/firenca_main.jpg"),
        ),
      ),

      child: Container(
        alignment: Alignment.center,
        color: Colors.black.withOpacity(0.35),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const Text(
              "Ponude",
              style: TextStyle(
                fontSize: 48,
                color: Colors.white,
                fontWeight: FontWeight.bold,
              ),
            ),
            const SizedBox(height: 10),
            Text(
              "Sekcija za naše popularne destinacije\nradujemo se jos novim dodanim putovanjima",
              textAlign: TextAlign.center,
              style: TextStyle(
                fontSize: 15,
                color: Colors.white.withOpacity(0.95),
              ),
            ),
          ],
        ),
      ),
    );
  }

  // ============================================================
  // NAV LINK
  // ============================================================
  Widget _topLink(String text, {bool selected = false}) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 14),
      child: Text(
        text,
        style: TextStyle(
          fontSize: 16,
          color: selected ? Colors.white : Colors.white70,
          fontWeight: selected ? FontWeight.bold : FontWeight.normal,
        ),
      ),
    );
  }

  // ============================================================
  // FILTER
  // ============================================================
  Widget _filterSection() {
    return Container(
      width: 800,
      padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 28),
      decoration: BoxDecoration(
        color: const Color(0xFFD9D9D9),
        borderRadius: BorderRadius.circular(22),
      ),
      child: Column(
        children: [
          const Text(
            "Filter ponuda",
            style: TextStyle(
              fontSize: 24,
              fontWeight: FontWeight.bold,
              color: Colors.white,
            ),
          ),

          const SizedBox(height: 24),

          Row(
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            children: const [
              _FilterDropdown(label: "odaberi kategoriju"),
              _FilterDropdown(label: "odaberite podkategoriju"),
            ],
          ),

          const SizedBox(height: 22),

          SizedBox(
            width: 220,
            height: 45,
            child: ElevatedButton(
              onPressed: () {},
              style: ElevatedButton.styleFrom(
                backgroundColor: const Color(0xFF64B5F6),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(30),
                ),
              ),
              child: const Text(
                "pretražite",
                style: TextStyle(
                  color: Colors.white,
                  fontSize: 16,
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }

  // ============================================================
  // GRID PONUDA (DINAMIČKO)
  // ============================================================
  Widget _offerGrid() {
    if (loading) {
      return const Padding(
        padding: EdgeInsets.all(40),
        child: CircularProgressIndicator(),
      );
    }

    if (loadError) {
      return const Padding(
        padding: EdgeInsets.all(40),
        child: Text("Došlo je do greške pri učitavanju ponuda."),
      );
    }

    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 48),
      child: Wrap(
        spacing: 20,
        runSpacing: 20,
        children: [
          _addOfferCard(),

          ...offers.map((o) {
            final img =
                o.offerImages.isNotEmpty
                    ? o.offerImages.firstWhere(
                      (x) => x.isMain,
                      orElse:
                          () => OfferImage(
                            id: 0,
                            offerId: o.offerId,
                            imageUrl: "",
                            isMain: true,
                          ),
                    )
                    : OfferImage(
                      id: 0,
                      offerId: o.offerId,
                      imageUrl: "",
                      isMain: true,
                    );

            final safeImageUrl =
                img.imageUrl.isEmpty
                    ? "assets/images/placeholder.jpg"
                    : "${ApiConfig.imagesOffers}/${img.imageUrl}";

            return _offerCard(o.title.toUpperCase(), safeImageUrl);
          }).toList(),
        ],
      ),
    );
  }

  // ============================================================
  // KARTICA -> DODAJ PONUDU
  // ============================================================
  Widget _addOfferCard() {
    return DottedBorder(
      color: Colors.black38,
      strokeWidth: 2,
      dashPattern: const [6, 4],
      borderType: BorderType.RRect,
      radius: const Radius.circular(16),
      child: Container(
        width: 220,
        height: 260,
        padding: const EdgeInsets.all(16),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const Text(
              "dodaj ponudu",
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.w600,
                color: Colors.black87,
              ),
            ),

            const SizedBox(height: 18),

            ElevatedButton(
              onPressed: () => print("Klik na dodaj ponudu"),
              style: ElevatedButton.styleFrom(
                shape: const CircleBorder(),
                backgroundColor: const Color.fromARGB(255, 173, 172, 172),
                foregroundColor: Colors.white,
                padding: const EdgeInsets.all(18),
              ),
              child: const Icon(Icons.add, size: 32),
            ),

            const SizedBox(height: 16),

            const Text(
              "ljudi se raduju novim\nputovanjima",
              textAlign: TextAlign.center,
              style: TextStyle(fontSize: 12, color: Colors.black45),
            ),
          ],
        ),
      ),
    );
  }

  // ============================================================
  // KARTICA -> PONUDA
  // ============================================================
  Widget _offerCard(String title, String imageUrl) {
    final isLocalImage = imageUrl.startsWith("assets/");

    return Container(
      width: 220,
      height: 260,
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(16),
        image: DecorationImage(
          fit: BoxFit.cover,
          image:
              isLocalImage
                  ? AssetImage(imageUrl)
                  : NetworkImage(imageUrl) as ImageProvider,
        ),
      ),
      child: ClipRRect(
        borderRadius: BorderRadius.circular(16),
        child: Container(
          decoration: BoxDecoration(
            gradient: LinearGradient(
              begin: Alignment.topCenter,
              end: Alignment.bottomCenter,
              colors: [
                Colors.white.withOpacity(0.05),
                Colors.black.withOpacity(0.55),
              ],
            ),
          ),
          child: Column(
            children: [
              Expanded(
                child: Align(
                  alignment: Alignment.bottomCenter,
                  child: Padding(
                    padding: const EdgeInsets.only(bottom: 18),
                    child: Text(
                      title,
                      style: const TextStyle(
                        fontSize: 20,
                        color: Colors.white,
                        fontWeight: FontWeight.bold,
                        letterSpacing: 1.2,
                      ),
                    ),
                  ),
                ),
              ),

              Container(
                width: double.infinity,
                padding: const EdgeInsets.symmetric(vertical: 2, horizontal: 8),
                decoration: const BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.only(
                    bottomLeft: Radius.circular(16),
                    bottomRight: Radius.circular(16),
                  ),
                ),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  children: [
                    IconButton(
                      icon: const Icon(Icons.visibility_outlined, size: 20),
                      onPressed: () {},
                    ),
                    IconButton(
                      icon: const Icon(Icons.edit_outlined, size: 20),
                      onPressed: () {},
                    ),
                    IconButton(
                      icon: const Icon(Icons.delete_outline, size: 20),
                      onPressed: () {},
                    ),
                  ],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

// ============================================================
// DROPDOWN KOMPONENTA
// ============================================================
class _FilterDropdown extends StatelessWidget {
  final String label;
  const _FilterDropdown({super.key, required this.label});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: 300,
      child: DropdownButtonFormField<String>(
        decoration: InputDecoration(
          labelText: label,
          labelStyle: TextStyle(fontSize: 15, color: Colors.grey.shade700),
          filled: true,
          fillColor: Colors.white,
          border: OutlineInputBorder(borderRadius: BorderRadius.circular(12)),
        ),
        items: const [
          DropdownMenuItem(value: "1", child: Text("Januar")),
          DropdownMenuItem(value: "2", child: Text("Februar")),
          DropdownMenuItem(value: "3", child: Text("Mart")),
        ],
        onChanged: (value) {},
      ),
    );
  }
}
