import 'package:etravel_app/providers/offer_plan_day_provider.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:etravel_app/models/offer_plan_day.dart';

class PlanPutovanja extends StatefulWidget {
  final int offerId;

  const PlanPutovanja({
    super.key,
    required this.offerId,
  });

  @override
  State<PlanPutovanja> createState() => _PlanPutovanjaState();
}

class _PlanPutovanjaState extends State<PlanPutovanja> {
  late OfferPlanDayProvider _planProvider;

  List<OfferPlanDay> dani = [];
  bool isLoading = true;

  @override
  void initState() {
    super.initState();
    _planProvider =
        Provider.of<OfferPlanDayProvider>(context, listen: false);
    _loadPlan();
  }

  Future<void> _loadPlan() async {
    try {
      final result = await _planProvider.get(
        filter: {
          "offerId": widget.offerId,
        },
      );

      // 🔥 sortiranje po danu
      result.items.sort(
        (a, b) => a.dayNumber.compareTo(b.dayNumber),
      );

      setState(() {
        dani = result.items;
        isLoading = false;
      });
    } catch (e) {
      debugPrint("Greška učitavanja plana putovanja: $e");
      setState(() => isLoading = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    if (isLoading) {
      return const Center(
        child: CircularProgressIndicator(
          color: Color(0xFF67B1E5),
        ),
      );
    }

    if (dani.isEmpty) {
      return const Center(
        child: Text(
          "Plan putovanja nije dostupan.",
          textAlign: TextAlign.center,
        ),
      );
    }

    return CustomScrollView(
      slivers: [
        SliverToBoxAdapter(
          child: Column(
            children: List.generate(dani.length, (index) {
              final dan = dani[index];

              return Container(
                margin:
                    const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
                decoration: BoxDecoration(
                  border: Border.all(color: Colors.blue.shade200),
                  borderRadius: BorderRadius.circular(10),
                ),
                child: ExpansionTile(
                  tilePadding:
                      const EdgeInsets.symmetric(horizontal: 12),
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
                          "${dan.dayNumber}. DAN",
                          style: const TextStyle(
                            color: Colors.white,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ),
                      const SizedBox(width: 12),
                      Expanded(
                        child: Text(
                          dan.dayTitle,
                          style: const TextStyle(
                            fontWeight: FontWeight.w600,
                          ),
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
                        dan.dayDescription,
                        style: const TextStyle(
                          color: Color(0xFF67B1E5),
                        ),
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
