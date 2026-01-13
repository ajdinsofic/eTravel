import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/widgets/headerIFooterAplikacije/eTravelFooter.dart';
import 'package:flutter/material.dart';
import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:google_fonts/google_fonts.dart';

class ContactPage extends StatefulWidget {
  const ContactPage({super.key});

  @override
  State<ContactPage> createState() => _ContactPageState();
}

class _ContactPageState extends State<ContactPage> {
  @override
  Widget build(BuildContext context) {
    final offices = <Map<String, dynamic>>[
      {
        "title": "POSLOVNICA U SARAJEVU",
        "address": "Ulica Malih 15, 71000 Sarajevo",
        "hours":
            "Ponedjeljak - Petak\n09:00h- 17:00h\nSubota\n08:00h- 15:00h\nNedjelja - NERADNA",
        "contact": "+387 61 369 899",
        "image": "${ApiConfig.imagesOffers}/sarajevo.png",
      },
      {
        "title": "POSLOVNICA U BANJALUCI",
        "address": "Bulevar Kralja Petra 45, 78000 Banja Luka",
        "hours":
            "Ponedjeljak - Petak\n08:00h- 17:00h\nSubota\n08:00h- 15:00h\nNedjelja - NERADNA",
        "contact": "+387 57 321 567",
        "image": "${ApiConfig.imagesOffers}/banjaluka.png",
      },
      {
        "title": "POSLOVNICA U TRAVNIKU",
        "address": "Ulica Sunca 22, 72270 Travnik",
        "hours":
            "Ponedjeljak - Petak\n09:00h- 17:00h\nSubota\n08:00h- 15:00h\nNedjelja - NERADNA",
        "contact": "+387 63 987 654",
        "image": "${ApiConfig.imagesOffers}/travnik.png",
      },
    ];

    return Scaffold(
      backgroundColor: Colors.white,
      body: CustomScrollView(
        slivers: [
          SljedecaDestinacijaIMenuBar(daLijeKliknuo: false),

          SliverToBoxAdapter(
            child: Padding(
              padding: const EdgeInsets.all(40),
              child: Center(
                child: Text(
                  "Kontaktirajte nas",
                  style: GoogleFonts.openSans(
                    fontSize: 22,
                    fontWeight: FontWeight.w700,
                  ),
                ),
              ),
            ),
          ),

          SliverPadding(
            padding: const EdgeInsets.symmetric(horizontal: 14),
            sliver: SliverList.separated(
              itemCount: offices.length,
              separatorBuilder: (_, __) => const SizedBox(height: 16),
              itemBuilder: (context, index) {
                final o = offices[index];
                return _OfficeCard(
                  title: o["title"],
                  address: o["address"],
                  hours: o["hours"],
                  contact: o["contact"],
                  image: o["image"],
                );
              },
            ),
          ),

          SliverToBoxAdapter(
            child: Padding(
              padding: const EdgeInsets.only(top: 18),
              child: eTravelFooter(),
            ),
          ),

          const SliverToBoxAdapter(child: SizedBox(height: 16)),
        ],
      ),
    );
  }
}

class _OfficeCard extends StatelessWidget {
  final String title;
  final String address;
  final String hours;
  final String contact;
  final String image;

  const _OfficeCard({
    required this.title,
    required this.address,
    required this.hours,
    required this.contact,
    required this.image,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(14),
        boxShadow: const [
          BoxShadow(
            color: Color(0x1F000000), // malo jače da bude kao na slici
            blurRadius: 18,
            spreadRadius: 1,
            offset: Offset(0, 8),
          ),
        ],
      ),
      child: ClipRRect(
        borderRadius: BorderRadius.circular(14),
        child: Container(
          color: Colors.white,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              // MAP IMAGE
              SizedBox(
                height: 190,
                child: Image.network(
                  image,
                  fit: BoxFit.cover,
                  loadingBuilder: (context, child, loadingProgress) {
                    if (loadingProgress == null) return child;
                    return const Center(
                      child: CircularProgressIndicator(strokeWidth: 2),
                    );
                  },
                  errorBuilder: (context, error, stackTrace) {
                    return const Center(
                      child: Icon(Icons.image_not_supported),
                    );
                  },
                ),
              ),

              // PLAVA LINIJA ISPOD MAPE (kao na slici)
              Container(
                height: 8,
                color: const Color(0xFF6FB6E6),
              ),

              // CONTENT (kao na screenshotu)
              Padding(
                padding: const EdgeInsets.fromLTRB(18, 16, 18, 18),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      title,
                      style: GoogleFonts.openSans(
                        fontSize: 18,
                        fontWeight: FontWeight.w900,
                        letterSpacing: 0.2,
                      ),
                    ),

                    const SizedBox(height: 18),

                    _Section(
                      label: "Adresa:",
                      value: address,
                    ),

                    const SizedBox(height: 18),

                    _Section(
                      label: "Radno vrijeme:",
                      value: hours,
                    ),

                    const SizedBox(height: 22),

                    _Section(
                      label: "Kontakt:",
                      value: contact,
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

class _Section extends StatelessWidget {
  final String label;
  final String value;

  const _Section({
    required this.label,
    required this.value,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          label,
          style: GoogleFonts.openSans(
            fontSize: 16,
            fontWeight: FontWeight.w800,
          ),
        ),
        const SizedBox(height: 6),
        Text(
          value,
          style: GoogleFonts.openSans(
            fontSize: 14,
            height: 1.35,
            fontWeight: FontWeight.w500,
            color: const Color(0xFF111111),
          ),
        ),
      ],
    );
  }
}
