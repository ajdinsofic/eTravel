import 'dart:io';
import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/user.dart';
import 'package:etravel_desktop/models/user_image_request.dart';
import 'package:etravel_desktop/providers/user_provider.dart';
import 'package:etravel_desktop/screens/change_password_popup.dart';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

class ViewMyProfileEditPopup extends StatefulWidget {
  final User user;

  const ViewMyProfileEditPopup({super.key, required this.user});

  @override
  State<ViewMyProfileEditPopup> createState() => _ViewMyProfileEditPopupState();
}

class _ViewMyProfileEditPopupState extends State<ViewMyProfileEditPopup> {
  late TextEditingController _ime;
  late TextEditingController _prezime;
  late TextEditingController _email;
  late TextEditingController _username;
  late TextEditingController _phone;
  late TextEditingController _birthDate;

  File? newImage;
  bool hasChanges = false;

  // ERROR VARIABLES
  String? firstNameError,
      lastNameError,
      usernameError,
      emailError,
      phoneError,
      birthDateError;

  // ORIGINAL VALUES
  late String originalIme;
  late String originalPrezime;
  late String originalEmail;
  late String originalUsername;
  late String originalPhone;
  late String originalBirthDate;

  @override
  void initState() {
    super.initState();

    _ime = TextEditingController(text: widget.user.firstName);
    _prezime = TextEditingController(text: widget.user.lastName);
    _email = TextEditingController(text: widget.user.email);
    _username = TextEditingController(text: widget.user.username);
    _phone = TextEditingController(
      text: widget.user.phoneNumber.replaceAll("+387", ""),
    );
    _birthDate = TextEditingController(
      text: _formatDate(widget.user.dateBirth),
    );

    originalIme = widget.user.firstName;
    originalPrezime = widget.user.lastName;
    originalEmail = widget.user.email;
    originalUsername = widget.user.username;
    originalPhone = widget.user.phoneNumber.replaceAll("+387", "");
    originalBirthDate = _formatDate(widget.user.dateBirth);

    _ime.addListener(_onChanged);
    _prezime.addListener(_onChanged);
    _email.addListener(_onChanged);
    _username.addListener(_onChanged);
    _phone.addListener(_onChanged);
    _birthDate.addListener(_onChanged);
  }

  // FORMAT DATE
  String _formatDate(DateTime? dt) {
    if (dt == null) return "";
    return "${dt.day.toString().padLeft(2, '0')}."
        "${dt.month.toString().padLeft(2, '0')}."
        "${dt.year}";
  }

  DateTime? _parseBirthDate(String input) {
    try {
      final parts = input.split(".");
      if (parts.length != 3) return null;

      final day = int.parse(parts[0]);
      final month = int.parse(parts[1]);
      final year = int.parse(parts[2]);

      return DateTime.utc(year, month, day);
    } catch (_) {
      return null;
    }
  }

  // VALIDACIJA
  bool _validate() {
    setState(() {
      firstNameError = _ime.text.trim().isEmpty ? "Ime je obavezno." : null;

      lastNameError =
          _prezime.text.trim().isEmpty ? "Prezime je obavezno." : null;

      // USERNAME
      if (_username.text.trim().isEmpty) {
        usernameError = "Username je obavezan.";
      } else if (_username.text.length < 6) {
        usernameError = "Minimum 6 karaktera.";
      } else if (!RegExp(r'^(?=.*\d).{6,}$').hasMatch(_username.text)) {
        usernameError = "Mora sadržavati barem jedan broj.";
      } else {
        usernameError = null;
      }

      // EMAIL
      if (_email.text.trim().isEmpty) {
        emailError = "Email je obavezan.";
      } else if (!RegExp(
        r"^[\w\.-]+@[\w\.-]+\.\w+$",
      ).hasMatch(_email.text.trim())) {
        emailError = "Email nije validan.";
      } else {
        emailError = null;
      }

      // TELEFON (+387 only)
      if (_phone.text.trim().isEmpty) {
        phoneError = "Broj telefona je obavezan.";
      } else {
        final digits = _phone.text.trim();

        if (!RegExp(r'^\d+$').hasMatch(digits)) {
          phoneError = "Broj smije sadržavati samo cifre.";
        } else if (!digits.startsWith("6")) {
          phoneError = "BH broj mora početi sa 6 (61, 62 ili 60).";
        } else {
          final prefix = digits.substring(0, 2);

          if (prefix == "61" || prefix == "62") {
            phoneError =
                digits.length == 8
                    ? null
                    : "Mora imati 6 cifara nakon $prefix.";
          } else if (prefix == "60") {
            phoneError =
                digits.length == 9
                    ? null
                    : "60 mreža mora imati 7 cifara nakon 60.";
          } else {
            phoneError = "Dozvoljeno: 60, 61, 62.";
          }
        }
      }

      // DATE OF BIRTH
      birthDateError =
          _birthDate.text.trim().isEmpty ? "Datum rođenja je obavezan." : null;
    });

    return firstNameError == null &&
        lastNameError == null &&
        usernameError == null &&
        emailError == null &&
        phoneError == null &&
        birthDateError == null;
  }

  // DATE PICKER
  Future<void> _pickBirthDate() async {
    DateTime initial = widget.user.dateBirth ?? DateTime(1990, 1, 1);

    final picked = await showDatePicker(
      context: context,
      initialDate: initial,
      firstDate: DateTime(1900),
      lastDate: DateTime.now(),
      helpText: "Izaberite datum rođenja",
      cancelText: "Otkaži",
      confirmText: "Potvrdi",
    );

    if (picked != null) {
      _birthDate.text = _formatDate(picked);
      _onChanged();
    }
  }

  // WATCH FOR CHANGES
  void _onChanged() {
    final changed =
        _ime.text != originalIme ||
        _prezime.text != originalPrezime ||
        _email.text != originalEmail ||
        _username.text != originalUsername ||
        _phone.text != originalPhone ||
        _birthDate.text != originalBirthDate ||
        newImage != null;

    setState(() => hasChanges = changed);
  }

  // PICK IMAGE
  Future<void> _pickNewImage() async {
    final result = await FilePicker.platform.pickFiles(
      type: FileType.image,
      allowMultiple: false,
    );

    if (result != null) {
      newImage = File(result.files.single.path!);
      setState(() => hasChanges = true);
    }
  }

  // CONFIRM CLOSE
  Future<bool> _confirmClose() async {
    if (!hasChanges) return true;

    final result = await showDialog<bool>(
      context: context,
      barrierDismissible: false,
      builder: (_) {
        return AlertDialog(
          title: const Text("Imate nesačuvane promjene"),
          content: const Text(
            "Da li ste sigurni da želite zatvoriti bez spremanja?",
          ),
          actionsAlignment: MainAxisAlignment.center,
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context, false),
              child: const Text("Ostani"),
            ),
            ElevatedButton(
              onPressed: () => Navigator.pop(context, true),
              style: ElevatedButton.styleFrom(backgroundColor: Colors.red),
              child: const Text("Izađi"),
            ),
          ],
        );
      },
    );

    return result ?? false;
  }

  // SAVE CHANGES
  Future<void> _saveChanges() async {
    if (!_validate()) return;

    final userProvider = Provider.of<UserProvider>(context, listen: false);

    try {
      await userProvider.update(widget.user.id, {
        "firstName": _ime.text,
        "lastName": _prezime.text,
        "email": _email.text,
        "username": _username.text,
        "phoneNumber": "+387${_phone.text.trim()}",
        "dateBirth": _parseBirthDate(_birthDate.text)?.toIso8601String(),
      });

      if (newImage != null) {
        if (widget.user.imageUrl != null && widget.user.imageUrl!.isNotEmpty) {
          await userProvider.deleteUserImage(widget.user.id);
        }

        await userProvider.addUserImage(
          UserImageRequest(userId: widget.user.id, image: newImage!),
        );

        widget.user.imageUrl = "";
      }

      _showToast("Profil uspješno ažuriran.");
      Navigator.pop(context, true);
    } catch (e) {
      _showToast("Greška pri ažuriranju profila.");
      debugPrint("Greška pri ažuriranju profila: $e");
    }
  }

  // TOAST
  void _showToast(String msg) {
    final overlay = Overlay.of(context);
    if (overlay == null) return;

    late OverlayEntry entry;
    entry = OverlayEntry(
      builder:
          (_) => Positioned(
            bottom: 25,
            right: 25,
            child: Material(
              color: Colors.transparent,
              child: Container(
                padding: const EdgeInsets.symmetric(
                  horizontal: 18,
                  vertical: 12,
                ),
                decoration: BoxDecoration(
                  color: Colors.blue,
                  borderRadius: BorderRadius.circular(12),
                ),
                child: Text(
                  msg,
                  style: const TextStyle(
                    color: Colors.white,
                    fontSize: 16,
                    fontWeight: FontWeight.w600,
                  ),
                ),
              ),
            ),
          ),
    );

    overlay.insert(entry);
    Future.delayed(const Duration(seconds: 3), entry.remove);
  }

  @override
  Widget build(BuildContext context) {
    return WillPopScope(
      onWillPop: _confirmClose,
      child: Dialog(
        insetPadding: const EdgeInsets.symmetric(horizontal: 180, vertical: 80),
        backgroundColor: Colors.white,
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(18)),
        child: SizedBox(
          height: 650,
          width: 780,
          child: Column(
            children: [
              Container(
                height: 50,
                decoration: const BoxDecoration(
                  color: Color(0xff67B1E5),
                  borderRadius: BorderRadius.vertical(top: Radius.circular(18)),
                ),
                child: Stack(
                  children: [
                    Center(
                      child: Text(
                        "Uredi profil",
                        style: GoogleFonts.openSans(
                          fontSize: 18,
                          color: Colors.white,
                          fontWeight: FontWeight.w600,
                        ),
                      ),
                    ),
                    Positioned(
                      right: 12,
                      top: 10,
                      child: GestureDetector(
                        onTap: () async {
                          if (await _confirmClose()) {
                            Navigator.pop(context);
                          }
                        },
                        child: const Icon(
                          Icons.close,
                          color: Colors.white,
                          size: 24,
                        ),
                      ),
                    ),
                  ],
                ),
              ),

              Expanded(
                child: Padding(
                  padding: const EdgeInsets.symmetric(
                    horizontal: 30,
                    vertical: 20,
                  ),
                  child: Row(
                    children: [
                      // LEFT IMAGE SIDE
                      SizedBox(
                        width: 220,
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.spaceAround,
                          children: [
                            Container(
                              width: 180,
                              height: 180,
                              decoration: BoxDecoration(
                                shape: BoxShape.circle,
                                border: Border.all(
                                  color: Colors.black54,
                                  width: 2,
                                ),
                              ),
                              child: ClipOval(
                                child:
                                    newImage != null
                                        ? Image.file(
                                          newImage!,
                                          fit: BoxFit.cover,
                                        )
                                        : (widget.user.imageUrl == null ||
                                            widget.user.imageUrl!.isEmpty)
                                        ? const Icon(Icons.person, size: 120)
                                        : Image.network(
                                          "${ApiConfig.imagesUsers}/${widget.user.imageUrl}",
                                          fit: BoxFit.cover,
                                        ),
                              ),
                            ),

                            const SizedBox(height: 10),

                            ElevatedButton(
                              onPressed: _pickNewImage,
                              style: ElevatedButton.styleFrom(
                                backgroundColor: const Color(0xff67B1E5),
                                padding: const EdgeInsets.symmetric(
                                  horizontal: 20,
                                  vertical: 10,
                                ),
                                shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(12),
                                ),
                              ),
                              child: const Text(
                                "zamijeni sliku",
                                style: TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.w600,
                                ),
                              ),
                            ),

                            InkWell(
                              onTap: () async {
                                final result = await showDialog(
                                  context: context,
                                  barrierDismissible: false,
                                  builder: (_) => const ChangePasswordPopup(),
                                );

                                if (result == true) {
                                  _showToast("Lozinka uspješno promijenjena.");
                                }
                              },
                              child: MouseRegion(
                                cursor: SystemMouseCursors.click,
                                child: Text(
                                  "Želite li promijeniti lozinku?",
                                  style: TextStyle(
                                    color: Colors.black87,
                                    decoration: TextDecoration.underline,
                                  ),
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),

                      const SizedBox(width: 30),

                      // RIGHT FORM SIDE
                      Expanded(
                        child: SingleChildScrollView(
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              _field("Ime", _ime, firstNameError),
                              _field("Prezime", _prezime, lastNameError),
                              _field("Email", _email, emailError),
                              _field("Username", _username, usernameError),
                              _phoneField(),

                              GestureDetector(
                                onTap: _pickBirthDate,
                                child: AbsorbPointer(
                                  child: _field(
                                    "Datum rođenja",
                                    _birthDate,
                                    birthDateError,
                                  ),
                                ),
                              ),
                            ],
                          ),
                        ),
                      ),
                    ],
                  ),
                ),
              ),

              if (hasChanges)
                Padding(
                  padding: const EdgeInsets.only(bottom: 20),
                  child: ElevatedButton(
                    onPressed: _saveChanges,
                    style: ElevatedButton.styleFrom(
                      backgroundColor: const Color(0xff67B1E5),
                      padding: const EdgeInsets.symmetric(
                        horizontal: 40,
                        vertical: 14,
                      ),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(30),
                      ),
                    ),
                    child: const Text(
                      "Ažuriraj profil",
                      style: TextStyle(color: Colors.white, fontSize: 16),
                    ),
                  ),
                ),
            ],
          ),
        ),
      ),
    );
  }

  // FIELD UI
  Widget _field(String label, TextEditingController ctrl, String? error) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 14),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            label,
            style: GoogleFonts.openSans(
              fontSize: 14,
              fontWeight: FontWeight.w600,
            ),
          ),
          const SizedBox(height: 5),
          TextField(
            controller: ctrl,
            decoration: InputDecoration(
              filled: true,
              fillColor: const Color(0xffEDEDED),
              errorText: error,
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

  Widget _phoneField() {
    return Padding(
      padding: const EdgeInsets.only(bottom: 14),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            "Telefon",
            style: GoogleFonts.openSans(
              fontSize: 14,
              fontWeight: FontWeight.w600,
            ),
          ),

          const SizedBox(height: 5),

          Container(
            decoration: BoxDecoration(
              color: const Color(0xffEDEDED),
              borderRadius: BorderRadius.circular(8),
              border: Border.all(
                color: phoneError == null ? Colors.transparent : Colors.red,
                width: phoneError == null ? 1 : 2,
              ),
            ),
            child: Row(
              children: [
                // PREFIX
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 12),
                  child: Text(
                    "+387",
                    style: GoogleFonts.openSans(
                      fontSize: 14,
                      fontWeight: FontWeight.w600,
                      color: Colors.black87,
                    ),
                  ),
                ),

                // LINE
                Container(height: 32, width: 1, color: Colors.black38),

                // INPUT — BEZ errorText !!!
                Expanded(
                  child: TextField(
                    controller: _phone,
                    keyboardType: TextInputType.number,
                    style: GoogleFonts.openSans(fontSize: 14),
                    decoration: InputDecoration(
                      filled: true,
                      fillColor: const Color(0xffEDEDED),
                      border: InputBorder.none,
                      contentPadding: const EdgeInsets.symmetric(
                        horizontal: 12,
                        vertical: 14,
                      ),
                      hintText: "unesite broj",
                      hintStyle: GoogleFonts.openSans(color: Colors.black54),
                    ),
                  ),
                ),
              ],
            ),
          ),

          // ERROR TEKST – PRIKAZUJEMO GA SAMO OVDJE
          if (phoneError != null)
            Padding(
              padding: const EdgeInsets.only(top: 4, left: 4),
              child: Text(
                phoneError!,
                style: const TextStyle(color: Colors.red, fontSize: 12.5),
              ),
            ),
        ],
      ),
    );
  }
}
