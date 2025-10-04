import 'package:flutter/material.dart';

class ReservationsProvider with ChangeNotifier {
  final Map<int, bool> _prikaziAktivnePoIndexu = {};
  final Map<int, bool> _prikaziZavrsenePoIndexu = {};

  bool _daLiJePlacenaRataI = false;
  bool _daLiJePlacenaRataII = false;
  bool _daLiJePlacenaRataIII = false;
  bool _daLiJePlacenaPreostala = false;

  bool prikaziAktivneDetalje(int index) =>
      _prikaziAktivnePoIndexu[index] ?? false;
  bool prikaziZavrseneDetalje(int index) =>
      _prikaziZavrsenePoIndexu[index] ?? false;
  bool get daLiJePlacenaRataI => _daLiJePlacenaRataI;
  bool get daLiJePlacenaRataII => _daLiJePlacenaRataII;
  bool get daLiJePlacenaRataIII => _daLiJePlacenaRataIII;
  bool get daLiJePlacenaPreostala => _daLiJePlacenaPreostala;

  void togglePrikaziAktivneDetalje(int index) {
    _prikaziAktivnePoIndexu[index] = !prikaziAktivneDetalje(index);
    notifyListeners();
  }

  void togglePlacenaRataI() {
    _daLiJePlacenaRataI = !_daLiJePlacenaRataI;
    notifyListeners();
  }

  void togglePlacenaRataII() {
    _daLiJePlacenaRataII = !_daLiJePlacenaRataII;
    notifyListeners();
  }

  void togglePlacenaRataIII() {
    _daLiJePlacenaRataIII = !_daLiJePlacenaRataIII;
    notifyListeners();
  }

  void togglePlacenaPreostala() {
    _daLiJePlacenaPreostala = !_daLiJePlacenaPreostala;
    notifyListeners();
  }

  void togglePrikaziZavrseneDetalje(int index) {
    _prikaziZavrsenePoIndexu[index] = !prikaziZavrseneDetalje(index);
    notifyListeners();
  }
}
