import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';
import '../providers/user_provider.dart';
import '../utils/session.dart';

class ChangePasswordPopup extends StatefulWidget {
  const ChangePasswordPopup({super.key});

  @override
  State<ChangePasswordPopup> createState() => _ChangePasswordPopupState();
}

class _ChangePasswordPopupState extends State<ChangePasswordPopup> {
  final TextEditingController _current = TextEditingController();
  final TextEditingController _newPass = TextEditingController();
  final TextEditingController _repeatPass = TextEditingController();

  String? currentError, newError, repeatError;
  bool isSaving = false;

  // ------------------------------------------------------------
  // VALIDACIJE (kompletne, identične backend pravilima)
  // ------------------------------------------------------------
  bool _validate() {
    setState(() {
      // 1) CURRENT PASSWORD
      currentError = _current.text.isEmpty ? "Unesite trenutnu lozinku." : null;

      // 2) NEW PASSWORD RULES
      if (_newPass.text.isEmpty) {
        newError = "Unesite novu lozinku.";
      } else if (_newPass.text.length < 6 || _newPass.text.length > 10) {
        newError = "Lozinka mora imati 6–10 karaktera.";
      } else if (!RegExp(r'^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,10}$')
          .hasMatch(_newPass.text)) {
        newError =
            "Mora imati malo, veliko slovo, broj i specijalni znak.";
      } else {
        newError = null;
      }

      // 3) REPEATED PASSWORD
      if (_repeatPass.text.isEmpty) {
        repeatError = "Ponovite lozinku.";
      } else if (_repeatPass.text != _newPass.text) {
        repeatError = "Lozinke se ne poklapaju.";
      } else {
        repeatError = null;
      }
    });

    return currentError == null &&
        newError == null &&
        repeatError == null;
  }

  // ------------------------------------------------------------
  // SAVE
  // ------------------------------------------------------------
  Future<void> _save() async {
    if (!_validate()) return;

    setState(() => isSaving = true);

    final userProvider = Provider.of<UserProvider>(context, listen: false);

    try {
      // 1) PROVJERI CURRENT PASSWORD
      final isValid = await userProvider.checkCurrentPassword({
        "userId": Session.userId,
        "currentPassword": _current.text,
      });

      if (!isValid) {
        setState(() {
          currentError = "Trenutna lozinka nije tačna.";
        });
        setState(() => isSaving = false);
        return;
      }

      // 2) UPDATE NEW PASSWORD
      final updated = await userProvider.updateNewPassword({
        "userId": Session.userId,
        "newPassword": _newPass.text,
      });

      if (updated) {
        Navigator.pop(context, true); // OK
      } else {
        newError = "Server nije prihvatio novu lozinku.";
      }
    } catch (e) {
      currentError = "Greška na serveru.";
    }

    setState(() => isSaving = false);
  }

  // ------------------------------------------------------------
  // UI
  // ------------------------------------------------------------
  @override
Widget build(BuildContext context) {
  return Dialog(
    backgroundColor: Colors.white,
    insetPadding: const EdgeInsets.symmetric(horizontal: 260, vertical: 140),
    shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
    child: SizedBox(
      height: 420,     // povećano da ne overflowa
      child: Column(
        children: [
          // HEADER
          Container(
            height: 50,
            decoration: const BoxDecoration(
              color: Color(0xff67B1E5),
              borderRadius: BorderRadius.vertical(top: Radius.circular(16)),
            ),
            child: Center(
              child: Text(
                "Promjena lozinke",
                style: GoogleFonts.openSans(
                  fontSize: 18,
                  fontWeight: FontWeight.w600,
                  color: Colors.white,
                ),
              ),
            ),
          ),

          // SCROLLABLE FORM
          Expanded(
            child: Padding(
              padding: const EdgeInsets.symmetric(horizontal: 28, vertical: 16),
              child: SingleChildScrollView(
                child: Column(
                  children: [
                    _passwordField("Trenutna lozinka", _current, currentError),
                    _passwordField("Nova lozinka", _newPass, newError),
                    _passwordField("Ponovi lozinku", _repeatPass, repeatError),
                  ],
                ),
              ),
            ),
          ),

          // SAVE BUTTON
          Padding(
            padding: const EdgeInsets.only(bottom: 18),
            child: ElevatedButton(
              onPressed: isSaving ? null : _save,
              style: ElevatedButton.styleFrom(
                backgroundColor: const Color(0xff67B1E5),
                padding: const EdgeInsets.symmetric(horizontal: 40, vertical: 14),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(30),
                ),
              ),
              child: const Text(
                "Sačuvaj",
                style: TextStyle(color: Colors.white, fontSize: 16),
              ),
            ),
          ),
        ],
      ),
    ),
  );
}


  // ------------------------------------------------------------
  // FIELD WIDGET
  // ------------------------------------------------------------
  Widget _passwordField(
      String label, TextEditingController ctrl, String? error) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 14),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            label,
            style: GoogleFonts.openSans(
                fontSize: 14, fontWeight: FontWeight.w600),
          ),
          const SizedBox(height: 5),
          TextField(
            controller: ctrl,
            obscureText: true,
            decoration: InputDecoration(
              filled: true,
              fillColor: const Color(0xffEDEDED),
              errorText: error,
              border: OutlineInputBorder(
                borderRadius: BorderRadius.circular(8),
              ),
              errorBorder: OutlineInputBorder(
                borderRadius: BorderRadius.circular(8),
                borderSide: const BorderSide(color: Colors.red, width: 2),
              ),
              focusedErrorBorder: OutlineInputBorder(
                borderRadius: BorderRadius.circular(8),
                borderSide: const BorderSide(color: Colors.red, width: 2),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
