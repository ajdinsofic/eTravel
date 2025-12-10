import 'dart:io';
import 'dart:math';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:image_picker/image_picker.dart';

class AddWorkerPopup extends StatefulWidget {
  const AddWorkerPopup({super.key});

  @override
  State<AddWorkerPopup> createState() => _AddWorkerPopupState();
}

class _AddWorkerPopupState extends State<AddWorkerPopup> {
  final TextEditingController firstName = TextEditingController();
  final TextEditingController lastName = TextEditingController();
  final TextEditingController username = TextEditingController();
  final TextEditingController email = TextEditingController();
  final TextEditingController phoneLocal = TextEditingController();
  final TextEditingController birthDate = TextEditingController();
  final TextEditingController generatedPassword = TextEditingController();

  File? uploadedImage;

  // ERROR PORUKE — SADA STRING? (ne bool!)
  String? firstNameError;
  String? lastNameError;
  String? usernameError;
  String? emailError;
  String? phoneError;
  String? birthDateError;

  // COUNTRY CODES
  String selectedDialCode = "+387";
  final List<String> dialCodes = [
    "+387", "+385", "+381", "+389", "+386", "+382"
  ];

  @override
  void initState() {
    super.initState();
    generatedPassword.text = _generatePassword();
  }

  String _generatePassword() {
  const upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
  const lower = "abcdefghijklmnopqrstuvwxyz";
  const numbers = "0123456789";
  const special = "@#%&*\$";
  const all = upper + lower + numbers + special;

  final rnd = Random();

  // obavezni znakovi
  String password = "";
  password += upper[rnd.nextInt(upper.length)];
  password += lower[rnd.nextInt(lower.length)];
  password += numbers[rnd.nextInt(numbers.length)];
  password += special[rnd.nextInt(special.length)];

  // preostali random znakovi (do ukupno 6–10 karaktera)
  int remainingLength = 6 + rnd.nextInt(5) - password.length; // 6–10 ukupno

  for (int i = 0; i < remainingLength; i++) {
    password += all[rnd.nextInt(all.length)];
  }

  // promiješaj znakove da obavezni ne budu uvijek prvi
  List<String> chars = password.split('');
  chars.shuffle(rnd);

  return chars.join();
}


  InputDecoration _input(String hint, {String? error}) {
    return InputDecoration(
      hintText: hint,
      hintStyle:
          GoogleFonts.openSans(fontSize: 13, color: Colors.grey.shade500),
      filled: true,
      fillColor: Colors.white,
      errorText: error,
      contentPadding: const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
      border: OutlineInputBorder(borderRadius: BorderRadius.circular(8)),
    );
  }

  Widget _title(String text) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 3),
      child: Text(
        text,
        style: GoogleFonts.openSans(fontSize: 14, fontWeight: FontWeight.w600),
      ),
    );
  }

  Future<void> _pickImage() async {
    final picker = ImagePicker();
    final XFile? picked = await picker.pickImage(source: ImageSource.gallery);
    if (picked != null) {
      uploadedImage = File(picked.path);
      setState(() {});
    }
  }

  // VALIDACIJA — 100% usklađena sa backendom
  bool _validate() {
    setState(() {
      firstNameError =
          firstName.text.trim().isEmpty ? "Ime je obavezno." : null;

      lastNameError =
          lastName.text.trim().isEmpty ? "Prezime je obavezno." : null;

      if (username.text.trim().isEmpty) {
        usernameError = "Username je obavezan.";
      } else if (username.text.length < 6) {
        usernameError = "Minimum 6 karaktera.";
      } else if (!RegExp(r'^(?=.*\d).{6,}$').hasMatch(username.text)) {
        usernameError = "Mora sadržavati barem jedan broj.";
      } else {
        usernameError = null;
      }

      if (email.text.trim().isEmpty) {
        emailError = "Email je obavezan.";
      } else if (!RegExp(r"^[\w\.-]+@[\w\.-]+\.\w+$")
          .hasMatch(email.text.trim())) {
        emailError = "Email nije validan.";
      } else {
        emailError = null;
      }

      // PHONE VALIDATION
if (phoneLocal.text.trim().isEmpty) {
  phoneError = "Broj telefona je obavezan.";
} else {
  final digits = phoneLocal.text.trim();

  if (!RegExp(r'^\d+$').hasMatch(digits)) {
    phoneError = "Broj smije sadržavati samo cifre.";
  } else if (selectedDialCode == "+387") {
    // +387 validation
    if (!digits.startsWith("6")) {
      phoneError = "BH broj mora početi sa 6 (61, 62 ili 60).";
    } else {
      final prefix = digits.substring(0, 2); // npr. 61, 62, 60

      if (prefix == "61" || prefix == "62") {
        // mora imati ukupno 2 + 6 = 8 cifara
        if (digits.length != 8) {
          phoneError = "Broj mora imati tačno 6 cifara nakon $prefix.";
        } else {
          phoneError = null;
        }
      } else if (prefix == "60") {
        // 60 mreža ima 7 cifara nakon 60 → ukupno 9 cifara
        if (digits.length != 9) {
          phoneError = "Broj mreže 60 mora imati 7 cifara nakon 60.";
        } else {
          phoneError = null;
        }
      } else {
        phoneError = "Nevalidan BH prefiks. Dozvoljeno: 60, 61, 62.";
      }
    }
  } else {
    // General rule for other countries
    if (digits.length < 6) {
      phoneError = "Broj mora imati minimalno 6 cifara.";
    } else {
      phoneError = null;
    }
  }
}


      birthDateError =
          birthDate.text.isEmpty ? "Datum rođenja je obavezan." : null;
    });

    return firstNameError == null &&
        lastNameError == null &&
        usernameError == null &&
        emailError == null &&
        phoneError == null &&
        birthDateError == null;
  }

  @override
  Widget build(BuildContext context) {
    return Dialog(
      insetPadding: const EdgeInsets.symmetric(horizontal: 180, vertical: 80),
      backgroundColor: Colors.white,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(18)),
      child: SizedBox(
        height: 600,
        width: 760,
        child: Column(
          children: [
            // HEADER
            Container(
              height: 48,
              decoration: const BoxDecoration(
                color: Color(0xff67B1E5),
                borderRadius: BorderRadius.vertical(top: Radius.circular(18)),
              ),
              child: Stack(
                children: [
                  Center(
                    child: Text(
                      "dodajte radnika",
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
                      onTap: () => Navigator.pop(context),
                      child: const Icon(Icons.close, size: 24, color: Colors.white),
                    ),
                  ),
                ],
              ),
            ),

            // BODY
            Expanded(
              child: Padding(
                padding: const EdgeInsets.symmetric(horizontal: 30, vertical: 20),
                child: Row(
                  children: [
                    // LEFT — IMAGE
                    SizedBox(
                      width: 200,
                      child: Column(
                        children: [
                          Container(
                            width: 180,
                            height: 180,
                            decoration: BoxDecoration(
                              shape: BoxShape.circle,
                              border: Border.all(color: Colors.black54, width: 2),
                            ),
                            child: ClipOval(
                              child: uploadedImage == null
                                  ? const Icon(Icons.account_circle_outlined,
                                      size: 120, color: Colors.black87)
                                  : Image.file(uploadedImage!,
                                      fit: BoxFit.cover),
                            ),
                          ),
                          const SizedBox(height: 12),
                          SizedBox(
                            width: 150,
                            height: 42,
                            child: ElevatedButton(
                              onPressed: _pickImage,
                              style: ElevatedButton.styleFrom(
                                backgroundColor: Colors.blueAccent,
                                shape: RoundedRectangleBorder(
                                    borderRadius: BorderRadius.circular(18)),
                              ),
                              child: Text(
                                uploadedImage == null
                                    ? "dodaj sliku"
                                    : "zamijeni sliku",
                                style: GoogleFonts.openSans(
                                    color: Colors.white, fontSize: 13),
                              ),
                            ),
                          ),
                        ],
                      ),
                    ),

                    const SizedBox(width: 30),

                    // RIGHT — FORM
                    Expanded(
                      child: SingleChildScrollView(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            _title("Ime"),
                            TextField(
                              controller: firstName,
                              onChanged: (_) => setState(() => firstNameError = null),
                              decoration: _input("unesite ime",
                                  error: firstNameError),
                            ),
                            const SizedBox(height: 8),

                            _title("Prezime"),
                            TextField(
                              controller: lastName,
                              onChanged: (_) => setState(() => lastNameError = null),
                              decoration: _input("unesite prezime",
                                  error: lastNameError),
                            ),
                            const SizedBox(height: 8),

                            _title("Email"),
                            TextField(
                              controller: email,
                              onChanged: (_) => setState(() => emailError = null),
                              decoration: _input("unesite email",
                                  error: emailError),
                            ),
                            const SizedBox(height: 8),

                            _title("Username"),
                            TextField(
                              controller: username,
                              onChanged: (_) => setState(() => usernameError = null),
                              decoration: _input("unesite username",
                                  error: usernameError),
                            ),
                            const SizedBox(height: 8),

                            _title("Broj telefona"),
                            Container(
                              decoration: BoxDecoration(
                                color: Colors.white,
                                borderRadius: BorderRadius.circular(8),
                                border: Border.all(
                                  color: phoneError != null
                                      ? Colors.red
                                      : Colors.grey.shade400,
                                ),
                              ),
                              child: Row(
                                children: [
                                  Padding(
                                    padding: const EdgeInsets.symmetric(horizontal: 8),
                                    child: DropdownButtonHideUnderline(
                                      child: DropdownButton<String>(
                                        value: selectedDialCode,
                                        onChanged: (v) => setState(() {
                                          selectedDialCode = v!;
                                          phoneError = null;
                                        }),
                                        items: dialCodes.map((c) {
                                          return DropdownMenuItem(
                                            value: c,
                                            child: Text(c),
                                          );
                                        }).toList(),
                                      ),
                                    ),
                                  ),

                                  Container(width: 1, height: 30, color: Colors.grey.shade300),

                                  Expanded(
                                    child: TextField(
                                      controller: phoneLocal,
                                      onChanged: (_) => setState(() => phoneError = null),
                                      keyboardType: TextInputType.number,
                                      decoration: const InputDecoration(
                                        hintText: "unesite broj telefona",
                                        border: InputBorder.none,
                                        contentPadding: EdgeInsets.symmetric(
                                            horizontal: 12, vertical: 8),
                                      ),
                                    ),
                                  ),
                                ],
                              ),
                            ),

                            if (phoneError != null)
                              Padding(
                                padding: const EdgeInsets.only(top: 3),
                                child: Text(
                                  phoneError!,
                                  style: TextStyle(
                                      color: Colors.red.shade700, fontSize: 12),
                                ),
                              ),
                            const SizedBox(height: 8),

                            _title("Datum rođenja"),
                            TextField(
                              controller: birthDate,
                              readOnly: true,
                              onTap: () async {
                                FocusScope.of(context).unfocus();
                                setState(() => birthDateError = null);

                                final now = DateTime.now();
                                final picked = await showDatePicker(
                                  context: context,
                                  initialDate: DateTime(now.year - 20),
                                  firstDate: DateTime(1900),
                                  lastDate: now,
                                );

                                if (picked != null) {
                                  birthDate.text =
                                      "${picked.day.toString().padLeft(2, '0')}.${picked.month.toString().padLeft(2, '0')}.${picked.year}";
                                }
                              },
                              decoration: _input(
                                "unesite datum rođenja",
                                error: birthDateError,
                              ),
                            ),
                            const SizedBox(height: 8),

                            _title("Izgenerisana lozinka"),
                            TextField(
                              controller: generatedPassword,
                              readOnly: true,
                              decoration: _input(
                                "automatski generisana lozinka",
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

            // FOOTER BUTTON
            Padding(
              padding: const EdgeInsets.only(bottom: 16),
              child: SizedBox(
                width: 150,
                height: 45,
                child: ElevatedButton(
                  onPressed: () {
                    if (_validate()) {
                      Navigator.pop(context, {
                        "firstName": firstName.text,
                        "lastName": lastName.text,
                        "username": username.text,
                        "email": email.text,
                        "phone": "$selectedDialCode${phoneLocal.text.trim()}",
                        "birthDate": birthDate.text,
                        "password": generatedPassword.text,
                        "imageUrl": "test.jpg",
                        "image": uploadedImage,
                      });
                    }
                  },
                  style: ElevatedButton.styleFrom(
                    backgroundColor: const Color(0xff67B1E5),
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(22)),
                  ),
                  child: Text(
                    "potvrdi",
                    style:
                        GoogleFonts.openSans(fontSize: 15, color: Colors.white),
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
