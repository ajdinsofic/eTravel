class DateConverter {
  /// Pretvara "d.M.yyyy" u ISO-UTC format bez milisekundi
  ///  "2.3.2026" -> "2026-03-02T00:00:00Z"
  static String? toUtcIso(String? dateString) {
    if (dateString == null || dateString.isEmpty) return null;

    try {
      final parts = dateString.split('.');
      if (parts.length < 3) return null;

      final day = int.parse(parts[0]);
      final month = int.parse(parts[1]);
      final year = int.parse(parts[2]);

      final utcDate = DateTime.utc(year, month, day);
      return "${utcDate.toIso8601String().split('.').first}Z";
    } catch (e) {
      print("DateConverter error: $e");
      return null;
    }
  }

  /// Pretvara DateTime u ISO-UTC format bez milisekundi
  ///  DateTime(2026,3,2) -> "2026-03-02T00:00:00Z"
  static String toUtcIsoFromDate(DateTime date) {
    final utcDate = DateTime.utc(date.year, date.month, date.day);
    return "${utcDate.toIso8601String().split('.').first}Z";
  }

  // ======================================================
  // 🆕 NOVO: UTC → dd.MM.yyyy
  // ======================================================

  /// Prima ISO-UTC string (npr. "2026-03-02T00:00:00Z")
  /// i vraća "02.03.2026"
  static String? fromUtcIsoToDate(String? utcIso) {
    if (utcIso == null || utcIso.isEmpty) return null;

    try {
      final date = DateTime.parse(utcIso).toUtc();
      return _formatDate(date);
    } catch (e) {
      print("DateConverter format error: $e");
      return null;
    }
  }

  /// Prima DateTime (UTC) i vraća "dd.MM.yyyy"
  static String formatDate(DateTime date) {
    final utc = date.toUtc();
    return _formatDate(utc);
  }

  /// Interni formatter (bez pomjeranja dana)
  static String _formatDate(DateTime date) {
    final day = date.day.toString().padLeft(2, '0');
    final month = date.month.toString().padLeft(2, '0');
    final year = date.year.toString();
    return "$day.$month.$year";
  }
}
