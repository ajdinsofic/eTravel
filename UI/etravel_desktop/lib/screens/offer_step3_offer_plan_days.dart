import 'package:etravel_desktop/models/hotel_form_data.dart';
import 'package:etravel_desktop/models/one_plan_day.dart';
import 'package:etravel_desktop/providers/offer_plan_day_provider.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class OfferStep3OfferPlanDays extends StatefulWidget {
  final int? offerId;
  final int daysCount;

  final void Function(bool saved) onFinish;
  final Function(List<HotelFormData>) onBack;

  final List<HotelFormData> hotels;

  final bool isReadOnly;
  final bool isViewOrEditButton;

  // ★ NEW: Wizard callback
  final void Function(bool isEditing)? onChanged;

  const OfferStep3OfferPlanDays({
    super.key,
    required this.offerId,
    required this.daysCount,
    required this.onFinish,
    required this.onBack,
    required this.hotels,
    required this.isReadOnly,
    required this.isViewOrEditButton,
    required this.onChanged,
  });

  @override
  State<OfferStep3OfferPlanDays> createState() =>
      _OfferStep3OfferPlanDaysState();
}

class _OfferStep3OfferPlanDaysState extends State<OfferStep3OfferPlanDays> {
  late List<PlanDay> planDays;
  late OfferPlanDayProvider _offerPlanDayProvider;

  bool hasChanges = false;
  String? validationMessage;

  @override
  void initState() {
    super.initState();

    _offerPlanDayProvider = Provider.of<OfferPlanDayProvider>(
      context,
      listen: false,
    );

    if (widget.isViewOrEditButton) {
      planDays = [];
      _loadOfferDays();
    } else {
      planDays = List.generate(
        widget.daysCount,
        (i) => PlanDay(isNew: true, dayNumber: i + 1),
      );
    }
  }

  // ★ Zabilježi da se nešto mijenja i obavijesti wizard
  void onChange() {
    markAsChanged();
    clearValidationError();

    if (widget.onChanged != null) {
      widget.onChanged!(true); // signal wizardu da je editing aktivan
    }
  }

  void markAsChanged() {
    if (widget.isViewOrEditButton && !widget.isReadOnly) {
      if (!hasChanges) {
        setState(() => hasChanges = true);
      }
    }
  }

  void clearValidationError() {
    if (validationMessage != null) {
      setState(() => validationMessage = null);
    }
  }

  bool validateDays() {
    validationMessage = null;

    if (planDays.length != widget.daysCount) {
      setState(() {
        validationMessage =
            "Broj dana ne odgovara broju dana koji je definisan u ponudi.";
      });
      return false;
    }

    for (var day in planDays) {
      if (day.titleController.text.trim().isEmpty ||
          day.descriptionController.text.trim().isEmpty) {
        setState(() {
          validationMessage = "Sva polja u sekcijama moraju biti popunjena.";
        });
        return false;
      }
    }

    return true;
  }

  Future<void> _loadOfferDays() async {
    if (widget.offerId == null) return;

    try {
      final result = await _offerPlanDayProvider.get(
        filter: {"offerId": widget.offerId},
      );

      planDays =
          result.items.map((one) {
            final pd = PlanDay(
              id: one.offerDetailsId,
              dayNumber: one.dayNumber,
              isNew: false,
            );

            pd.titleController.text = one.dayTitle;
            pd.descriptionController.text = one.dayDescription;

            return pd;
          }).toList();

      planDays.sort((a, b) => a.dayNumber.compareTo(b.dayNumber));
      setState(() {});
    } catch (_) {}
  }

  Future<void> _saveAllDays() async {
    for (var day in planDays) {
      if (!day.isNew) {
        await _offerPlanDayProvider.updateDay(widget.offerId!, day.dayNumber, {
          "title": day.titleController.text,
          "description": day.descriptionController.text,
        });
      } else {
        await _offerPlanDayProvider.insert({
          "offerDetailsId": widget.offerId,
          "dayNumber": day.dayNumber,
          "title": day.titleController.text,
          "description": day.descriptionController.text,
        });
        day.isNew = false;
      }
    }
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
            "Da li ste sigurni da želite spremiti ovu ponudu?",
          ),
          actions: [
            ElevatedButton(
              onPressed: () => Navigator.pop(context, false),
              style: ElevatedButton.styleFrom(backgroundColor: Colors.red),
              child: const Text("Otkaži"),
            ),
            ElevatedButton(
              onPressed: () => Navigator.pop(context, true),
              style: ElevatedButton.styleFrom(backgroundColor: Colors.blue),
              child: const Text("Spasi"),
            ),
          ],
        );
      },
    );
  }

  void _addDay() {
    setState(() {
      planDays.add(PlanDay(isNew: true, dayNumber: planDays.length + 1));
      onChange();
    });
  }

  Future<void> _deleteDay(PlanDay day) async {
    if (widget.isViewOrEditButton && !widget.isReadOnly) {
      if (!day.isNew) {
        await _offerPlanDayProvider.deleteDay(widget.offerId!, day.dayNumber);
      }
    }

    setState(() {
      planDays.remove(day);
      for (int i = 0; i < planDays.length; i++) {
        planDays[i].dayNumber = i + 1;
      }
      onChange();
    });
  }

  // SUCCESS TOAST
  void _showSuccessToast() {
    final overlay = Overlay.of(context);
    if (overlay == null) return;

    late OverlayEntry entry;
    entry = OverlayEntry(
      builder:
          (context) => Positioned(
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
                  child: const Text(
                    "✓ Uspješno ažurirano",
                    style: TextStyle(color: Colors.white, fontSize: 16),
                  ),
                ),
              ),
            ),
          ),
    );

    overlay.insert(entry);
    Future.delayed(const Duration(seconds: 3), () => entry.remove());
  }

  Widget _errorBox() {
    if (validationMessage == null) return const SizedBox.shrink();

    return Container(
      width: double.infinity,
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
      margin: const EdgeInsets.only(bottom: 12),
      alignment: Alignment.center,
      decoration: BoxDecoration(
        color: Colors.red.shade100,
        borderRadius: BorderRadius.circular(10),
        border: Border.all(color: Colors.red.shade400, width: 1),
      ),
      child: Text(
        validationMessage!,
        textAlign: TextAlign.center,
        style: const TextStyle(
          color: Colors.red,
          fontSize: 15,
          fontWeight: FontWeight.w600,
        ),
      ),
    );
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
            _header(),
            const SizedBox(height: 20),

            Expanded(
              child: ListView.builder(
                itemCount: planDays.length,
                itemBuilder: (context, index) {
                  return _buildDayCard(planDays[index]);
                },
              ),
            ),

            const SizedBox(height: 16),
            footerButtons(),
          ],
        ),
      ),
    );
  }

  Widget _header() {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        const Text(
          "PLAN I PROGRAM PUTOVANJA",
          style: TextStyle(fontSize: 20, fontWeight: FontWeight.w700),
        ),
        ElevatedButton(
          onPressed:
              widget.isReadOnly
                  ? null
                  : (planDays.length < widget.daysCount ? _addDay : null),
          style: ElevatedButton.styleFrom(
            backgroundColor: const Color(0xff67B1E5),
          ),
          child: Text(
            planDays.length < widget.daysCount
                ? "dodajte dan"
                : "maksimalno dana: ${widget.daysCount}",
            style: const TextStyle(color: Colors.white),
          ),
        ),
      ],
    );
  }

  Widget footerButtons() {
    if (!widget.isViewOrEditButton) {
      return Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [_backButton(), _finishButton()],
      );
    }

    if (widget.isViewOrEditButton && widget.isReadOnly) {
      return Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [_backButton(), _finishButton()],
      );
    }

    return Column(
      mainAxisSize: MainAxisSize.min,
      children: [
        _errorBox(),

        Center(
          child: SizedBox(
            width: 220,
            height: 48,
            child: ElevatedButton(
              onPressed:
                  hasChanges
                      ? () async {
                        if (!validateDays()) return;

                        await _saveAllDays();
                        setState(() {
                          hasChanges = false;
                          validationMessage = null;
                        });

                        if (widget.onChanged != null) {
                          widget.onChanged!(false); // wizard može otključati UI
                        }

                        _showSuccessToast();
                      }
                      : null,
              style: ElevatedButton.styleFrom(
                backgroundColor: const Color(0xFF66BB6A), // zelena
                padding: const EdgeInsets.symmetric(
                  vertical: 14,
                  horizontal: 40,
                ),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
                elevation: 0,
              ),
              child: const Text(
                "Ažuriraj",
                style: TextStyle(
                  color: Colors.white,
                  fontSize: 16,
                  fontWeight: FontWeight.w600,
                ),
              ),
            ),
          ),
        ),
      ],
    );
  }

  Widget _backButton() {
    return SizedBox(
      width: 180,
      height: 48,
      child: OutlinedButton(
        onPressed: () => widget.onBack(widget.hotels),
        style: OutlinedButton.styleFrom(
          side: const BorderSide(color: Colors.blueAccent, width: 2),
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(12),
          ),
        ),
        child: const Text(
          "Nazad",
          style: TextStyle(fontSize: 16, color: Colors.blueAccent),
        ),
      ),
    );
  }

  Widget _finishButton() {
    return SizedBox(
      width: 180,
      height: 48,
      child: ElevatedButton(
        onPressed: () async {
          if (!validateDays()) return;

          if(!widget.isViewOrEditButton){
            final confirm = await _confirmSaveOffer(context);
            if (!confirm) return;

            await _saveAllDays();
            widget.onFinish(true);
          }

          await _saveAllDays();
          widget.onFinish(false);
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
    );
  }

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
              setState(() => day.isOpen = !day.isOpen);
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
                  Row(
                    children: [
                      Icon(
                        day.isOpen
                            ? Icons.keyboard_arrow_up
                            : Icons.keyboard_arrow_down,
                        size: 28,
                      ),
                      const SizedBox(width: 10),
                      if (widget.isViewOrEditButton && !widget.isReadOnly)
                        GestureDetector(
                          onTap: () => _deleteDay(day),
                          child: const Icon(
                            Icons.delete,
                            color: Colors.redAccent,
                            size: 26,
                          ),
                        ),
                    ],
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

  Widget _buildDayExpanded(PlanDay day) {
    return Padding(
      padding: const EdgeInsets.all(20),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text(
            "Detalji dana",
            style: TextStyle(fontSize: 16, fontWeight: FontWeight.w700),
          ),
          const SizedBox(height: 20),
          Row(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
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
                      enabled: !widget.isReadOnly,
                      onChanged: (_) => onChange(),
                      decoration: _inputDecoration(),
                    ),
                  ],
                ),
              ),
              const SizedBox(width: 30),
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
                      enabled: !widget.isReadOnly,
                      onChanged: (_) => onChange(),
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
