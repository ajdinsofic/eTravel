import 'package:flutter/material.dart';

class AppState extends ChangeNotifier {
  bool _daLijeKliknuo = false;

  bool get daLijeKliknuo => _daLijeKliknuo;

  void toggleDaLijeKliknuo() {
    _daLijeKliknuo = !_daLijeKliknuo;
    notifyListeners(); // Obavještava sve widgete koji slušaju
  }

  void setDaLijeKliknuo(bool value) {
    _daLijeKliknuo = value;
    notifyListeners();
  }
}
