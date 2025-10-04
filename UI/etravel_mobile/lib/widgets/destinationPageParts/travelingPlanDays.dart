import 'package:flutter/material.dart';

class PlanPutovanja extends StatelessWidget {
  final List<Map<String, String>> dani = [
    {
      "naslov": "BIH - Italija",
      "opis": "Lorem ipsum dolor sit amet, consectetur adipiscing elit...",
    },
    {
      "naslov": "Istraživanje romantične Verone",
      "opis": "Započinjemo dan šetnjom kroz čuvenu Arenu, Julijinu kuću...",
    },
    {
      "naslov": "Čarolije Venecije i vožnja gondolom",
      "opis": "Cijelodnevni izlet u Veneciju – Trg svetog Marka, kanali itd.",
    },
    {
      "naslov": "Toskana i srednjovjekovni gradići",
      "opis": "Obilazak vinograda, degustacija vina i posjeta San Gimignanu.",
    },
    {
      "naslov": "Italija - BIH",
      "opis": "Povratak prema BIH uz kraće pauze za odmor.",
    },
  ];

  PlanPutovanja({super.key});

  @override
  Widget build(BuildContext context) {
    return CustomScrollView(
      slivers: [
        SliverToBoxAdapter(
          child: Column(
            children: List.generate(dani.length, (index) {
              final dan = dani[index];
              return Container(
                margin: const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
                decoration: BoxDecoration(
                  border: Border.all(color: Colors.blue.shade200),
                  borderRadius: BorderRadius.circular(10),
                ),
                child: ExpansionTile(
                  tilePadding: const EdgeInsets.symmetric(horizontal: 12),
                  title: Row(
                    children: [
                      Container(
                        padding: const EdgeInsets.symmetric(
                          horizontal: 12,
                          vertical: 8,
                        ),
                        decoration: BoxDecoration(
                          color: const Color(0xFF67B1E5),
                          borderRadius: BorderRadius.circular(8),
                        ),
                        child: Text(
                          "${index + 1} DAN",
                          style: const TextStyle(
                            color: Colors.white,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ),
                      const SizedBox(width: 12),
                      Expanded(
                        child: Text(
                          dan["naslov"]!,
                          style: const TextStyle(fontWeight: FontWeight.w600),
                        ),
                      ),
                    ],
                  ),
                  children: [
                    Padding(
                      padding: const EdgeInsets.symmetric(
                        horizontal: 16,
                        vertical: 10,
                      ),
                      child: Text(
                        dan["opis"]!,
                        style: const TextStyle(color: Color(0xFF67B1E5)),
                      ),
                    ),
                  ],
                ),
              );
            }),
          ),
        ),
      ],
    );
  }
}
