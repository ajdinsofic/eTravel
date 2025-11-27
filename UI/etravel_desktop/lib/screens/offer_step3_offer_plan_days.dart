import 'package:etravel_desktop/models/hotel.dart';
import 'package:etravel_desktop/models/hotel_form_data.dart';
import 'package:etravel_desktop/models/one_plan_day.dart';
import 'package:etravel_desktop/providers/offer_plan_day_provider.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class OfferStep3OfferPlanDays extends StatefulWidget {
  final int offerId;
  final int daysCount;

  /// Završetak wizard-a
  final void Function(bool saved) onFinish;


  /// Vraća hotele nazad u step2
  final Function(List<HotelFormData>) onBack;

  /// Hoteli koje je korisnik unio u step2
  final List<HotelFormData> hotels;

  const OfferStep3OfferPlanDays({
    super.key,
    required this.offerId,
    required this.daysCount,
    required this.onFinish,
    required this.onBack,
    required this.hotels,
  });

  @override
  State<OfferStep3OfferPlanDays> createState() =>
      _OfferStep3OfferPlanDaysState();
}

class _OfferStep3OfferPlanDaysState extends State<OfferStep3OfferPlanDays> {
  late List<PlanDay> planDays;
  late OfferPlanDayProvider _offerPlanDayProvider;

  @override
  void initState() {
    super.initState();

    _offerPlanDayProvider = Provider.of<OfferPlanDayProvider>(
      context,
      listen: false,
    );

    planDays = List.generate(
      widget.daysCount,
      (i) => PlanDay(dayNumber: i + 1),
    );
  }

  Future<bool> _confirmSaveOffer(BuildContext context) async {
    return await showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) {
        return AlertDialog(
          title: const Text(
            "Spasiti ponudu?",
            style: TextStyle(fontWeight: FontWeight.bold),
          ),
          content: const Text(
            "Da li ste sigurni da želite spremiti ovu ponudu?\n"
            "Nakon spremanja više nećete moći uređivati neke podatke.",
          ),
          actions: [
            // 🔴 NE — crvena pozadina, bijeli tekst
            ElevatedButton(
              onPressed: () => Navigator.pop(context, false),
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.red,
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
                padding: const EdgeInsets.symmetric(
                  horizontal: 22,
                  vertical: 12,
                ),
              ),
              child: const Text("Otkaži"),
            ),

            const SizedBox(width: 10),

            // 🔵 DA — plava pozadina, bijeli tekst
            ElevatedButton(
              onPressed: () => Navigator.pop(context, true),
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.blue,
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
                padding: const EdgeInsets.symmetric(
                  horizontal: 22,
                  vertical: 12,
                ),
              ),
              child: const Text("Spasi"),
            ),
          ],
        );
      },
    );
  }

  void _addDay() {
    setState(() {
      planDays.add(PlanDay(dayNumber: planDays.length + 1));
    });
  }

  void _deleteDay(PlanDay day) {
    setState(() {
      planDays.remove(day);

      // Re-indexiranje dana
      for (int i = 0; i < planDays.length; i++) {
        planDays[i].dayNumber = i + 1;
      }
    });
  }

  Future<void> _saveAllDays() async {
    for (var day in planDays) {
      await _offerPlanDayProvider.insert({
        "offerDetailsId": widget.offerId,
        "dayNumber": day.dayNumber,
        "title": day.titleController.text,
        "description": day.descriptionController.text,
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color(0xfff3f3f3),
      body: Padding(
        padding: const EdgeInsets.all(26),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // HEADER + ADD DAY BUTTON
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                const Text(
                  "PLAN I PROGRAM PUTOVANJA",
                  style: TextStyle(fontSize: 20, fontWeight: FontWeight.w700),
                ),

                ElevatedButton(
                  onPressed:
                      planDays.length < widget.daysCount ? _addDay : null,
                  style: ElevatedButton.styleFrom(
                    backgroundColor: const Color(0xff67B1E5),
                    padding: const EdgeInsets.symmetric(
                      horizontal: 22,
                      vertical: 12,
                    ),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(10),
                    ),
                  ),
                  child: Text(
                    planDays.length < widget.daysCount
                        ? "dodajte dan"
                        : "maksimalno dana: ${widget.daysCount}",
                    style: const TextStyle(color: Colors.white),
                  ),
                ),
              ],
            ),

            const SizedBox(height: 20),

            // LISTA DANA
            Expanded(
              child: ListView.builder(
                itemCount: planDays.length,
                itemBuilder: (context, index) {
                  return _buildDayCard(planDays[index]);
                },
              ),
            ),

            const SizedBox(height: 16),

            // FOOTER: BACK + FINISH
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                // NAZAD
                SizedBox(
                  width: 180,
                  height: 48,
                  child: OutlinedButton(
                    onPressed: () {
                      widget.onBack(widget.hotels);
                    },
                    style: OutlinedButton.styleFrom(
                      side: const BorderSide(
                        color: Colors.blueAccent,
                        width: 2,
                      ),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(12),
                      ),
                    ),
                    child: const Text(
                      "Nazad",
                      style: TextStyle(fontSize: 16, color: Colors.blueAccent),
                    ),
                  ),
                ),

                // ZAVRŠI
                SizedBox(
                  width: 180,
                  height: 48,
                  child: ElevatedButton(
                    onPressed: () async {
                      final confirm = await _confirmSaveOffer(context);
                      if (!confirm) return;

                      await _saveAllDays();
                      widget.onFinish(true);
                    },
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.blueAccent,
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(12),
                      ),
                    ),
                    child: const Text(
                      "Završi",
                      style: TextStyle(color: Colors.white, fontSize: 16),
                    ),
                  ),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }

  // CARD ZA JEDAN DAN
  Widget _buildDayCard(PlanDay day) {
    return AnimatedContainer(
      duration: const Duration(milliseconds: 300),
      margin: const EdgeInsets.only(bottom: 18),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(18),
        border: Border.all(
          color: day.isOpen ? const Color(0xff67B1E5) : Colors.grey.shade300,
          width: day.isOpen ? 2 : 1,
        ),
        color: Colors.white,
      ),
      child: Column(
        children: [
          InkWell(
            borderRadius: BorderRadius.circular(18),
            onTap: () {
              setState(() {
                day.isOpen = !day.isOpen;
              });
            },
            child: Container(
              padding: const EdgeInsets.all(20),
              height: 70,
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text(
                    "${day.dayNumber} DAN",
                    style: const TextStyle(
                      fontSize: 18,
                      fontWeight: FontWeight.w700,
                    ),
                  ),

                  Icon(
                    day.isOpen
                        ? Icons.keyboard_arrow_up
                        : Icons.keyboard_arrow_down,
                    size: 28,
                  ),
                ],
              ),
            ),
          ),

          if (day.isOpen) _buildDayExpanded(day),
        ],
      ),
    );
  }

  // EXPANDED – detalji dana
  Widget _buildDayExpanded(PlanDay day) {
    return Padding(
      padding: const EdgeInsets.all(20),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // Top bar: title + delete
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              const Text(
                "Detalji dana",
                style: TextStyle(fontSize: 16, fontWeight: FontWeight.w700),
              ),
            ],
          ),

          const SizedBox(height: 20),

          Row(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              // Naslov plana
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      "Naslov plana",
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w600,
                      ),
                    ),
                    const SizedBox(height: 6),
                    TextField(
                      controller: day.titleController,
                      decoration: _inputDecoration(),
                    ),
                  ],
                ),
              ),

              const SizedBox(width: 30),

              // Opis plana
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      "Opis plana",
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w600,
                      ),
                    ),
                    const SizedBox(height: 6),
                    TextField(
                      controller: day.descriptionController,
                      maxLines: 8,
                      decoration: _inputDecoration(),
                    ),
                  ],
                ),
              ),
            ],
          ),
        ],
      ),
    );
  }

  InputDecoration _inputDecoration() {
    return InputDecoration(
      filled: true,
      fillColor: const Color(0xffF4F4F4),
      border: OutlineInputBorder(
        borderRadius: BorderRadius.circular(10),
        borderSide: BorderSide.none,
      ),
      contentPadding: const EdgeInsets.symmetric(vertical: 12, horizontal: 14),
    );
  }
}
