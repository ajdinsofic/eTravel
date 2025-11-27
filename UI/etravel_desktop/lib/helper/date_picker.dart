import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

class DatePickerHelper {
  final TextEditingController? departureController;
  final TextEditingController? returnController;

  DatePickerHelper({
    this.departureController,
    this.returnController,
  });

  // Format za prikaz u UI-u
  final DateFormat _formatUi = DateFormat("dd.MM.yyyy");

  DateTime _parseDate(String value) {
    try {
      final dt = DateTime.parse(value);
      return dt.isUtc ? dt : dt.toUtc();
    } catch (_) {
      return DateTime.now().toUtc();
    }
  }

  String toBackendIso(String dateString) {
    final parts = dateString.split('.');

    if (parts.length == 3) {
      final day = int.parse(parts[0]);
      final month = int.parse(parts[1]);
      final year = int.parse(parts[2]);

      final dt = DateTime.utc(year, month, day);
      return dt.toIso8601String();
    }

    return DateTime.now().toUtc().toIso8601String();
  }

  Future<void> pickDate({
    required BuildContext context,
    required bool isDeparture,
    required int daysCount,
  }) async {
    final now = DateTime.now();
    final tomorrow = now.add(const Duration(days: 1));

    final picked = await showDatePicker(
      context: context,
      initialDate: tomorrow,        // ✔ mora biti >= firstDate
      firstDate: tomorrow,          // ✔ danas + juče onemogućeni
      lastDate: now.add(const Duration(days: 365 * 3)),
      helpText: isDeparture ? "Odaberite datum polaska" : "Datum povratka",
      cancelText: "Otkaži",
      confirmText: "Potvrdi",
    );

    if (picked == null) return;

    // Ako je departure date
    if (isDeparture) {
      departureController?.text = _formatUi.format(picked);

      // Automatski izračunaj return date
      final returnDate = picked.add(Duration(days: daysCount));
      returnController?.text = _formatUi.format(returnDate);
    }
    // Ako je return date biran ručno (ali obično ga ne biramo)
    else {
      returnController?.text = _formatUi.format(picked);
    }
  }
}
