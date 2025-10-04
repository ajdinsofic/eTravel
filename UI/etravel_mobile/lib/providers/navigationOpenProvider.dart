import 'package:flutter/material.dart';

class NavigationProvider with ChangeNotifier {
  bool _daLijeKliknuo = false;
  bool _isPraznicnaExpanded = false;
  bool _isMjesecExpanded = false;

  bool get daLijeKliknuo => _daLijeKliknuo;
  bool get isPraznicnaExpanded => _isPraznicnaExpanded;
  bool get isMjesecExpanded => _isMjesecExpanded;

  // Metode za promenu stanja
  void toggleDaLijeKliknuo() {
    _daLijeKliknuo = !_daLijeKliknuo;
    notifyListeners();
  }

  void setDaLijeKliknuo(bool value) {
    _daLijeKliknuo = value;
    notifyListeners();
  }

  void togglePraznicna() {
    _isPraznicnaExpanded = !_isPraznicnaExpanded;
    notifyListeners();
  }

  // void setPraznicnaExpanded(bool value) {
  //   _isPraznicnaExpanded = value;
  //   notifyListeners();
  // }

  void toggleMjesec() {
    _isMjesecExpanded = !_isMjesecExpanded;
    notifyListeners();
  }

  // void setMjesecExpanded(bool value) {
  //   _isMjesecExpanded = value;
  //   notifyListeners();
  // }
}
