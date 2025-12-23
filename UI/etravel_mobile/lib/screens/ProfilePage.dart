import 'dart:io';

import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/helper/date_converter.dart';
import 'package:etravel_app/models/user.dart';
import 'package:etravel_app/models/user_image_request.dart';
import 'package:etravel_app/providers/user_provider.dart';
import 'package:etravel_app/providers/user_token_provider.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:etravel_app/widgets/headerIFooterAplikacije/eTravelFooter.dart';
import 'package:etravel_app/widgets/profilePageParts/reservationUniversalView.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class profilePage extends StatefulWidget {
  const profilePage({super.key});

  @override
  State<profilePage> createState() => _profilePageState();
}

class _profilePageState extends State<profilePage> {
  late UserProvider _userProvider;
  late UserTokenProvider _userTokenProvider;

  int? _userEquity;
  bool _isLoadingTokens = true;

  User? user;

  // CONTROLLERS
  late TextEditingController ime;
  late TextEditingController prezime;
  late TextEditingController phone;
  late TextEditingController birthDate;
  late TextEditingController email;
  late TextEditingController username;
  late TextEditingController currentPasswordCtrl;
  late TextEditingController newPasswordCtrl;
  late TextEditingController repeatNewPasswordCtrl;

  String? currentPasswordError;
  String? newPasswordError;
  String? repeatNewPasswordError;
  String? firstNameError;
  String? lastNameError;
  String? usernameError;
  String? emailError;
  String? phoneError;
  String? birthDateError;

  File? newImage;
  bool hasChanges = false;
  bool isLoading = false;
  bool showChangePassword = false;

  @override
  void initState() {
    super.initState();
    _userProvider = Provider.of<UserProvider>(context, listen: false);
    _userTokenProvider = Provider.of<UserTokenProvider>(context, listen: false);
    currentPasswordCtrl = TextEditingController();
    newPasswordCtrl = TextEditingController();
    repeatNewPasswordCtrl = TextEditingController();

    _loadUser();
    _loadUserTokens();
  }

  Future<void> _loadUserTokens() async {
    try {
      final userId = Session.userId!;
      final token = await _userTokenProvider.getById(userId);

      setState(() {
        _userEquity = token.equity;
        _isLoadingTokens = false;
      });
    } catch (e) {
      debugPrint("Greška pri učitavanju tokena: $e");
      setState(() {
        _isLoadingTokens = false;
        _userEquity = 0;
      });
    }
  }

  Future<void> _loadUser() async {
    user = await _userProvider.getById(Session.userId!);

    ime = TextEditingController(text: user!.firstName);
    prezime = TextEditingController(text: user!.lastName);
    phone = TextEditingController(
      text: user!.phoneNumber.replaceAll("+387", ""),
    );
    email = TextEditingController(text: user!.email);
    username = TextEditingController(text: user!.username);
    birthDate = TextEditingController(text: _formatDate(user!.dateBirth));

    for (var c in [ime, prezime, phone, email, username, birthDate]) {
      c.addListener(_onChanged);
    }

    if (!mounted) return;
    setState(() => isLoading = false);
  }

  Future<bool> _confirmExit() async {
    if (!hasChanges) return true;

    final result = await showDialog<bool>(
      context: context,
      barrierDismissible: false,
      builder: (context) {
        return AlertDialog(
          title: const Text(
            "Nesačuvane promjene",
            textAlign: TextAlign.center,
            style: TextStyle(fontWeight: FontWeight.bold),
          ),
          content: const Text(
            "Ako izađete sada, sve promjene će biti izgubljene",
            textAlign: TextAlign.center,
          ),
          actions: [
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                // ❌ OSTANI
                OutlinedButton(
                  onPressed: () => Navigator.pop(context, false),
                  style: OutlinedButton.styleFrom(
                    backgroundColor: Colors.white,
                    foregroundColor: Colors.blue,
                    side: const BorderSide(color: Colors.blue, width: 2),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(10),
                    ),
                  ),
                  child: const Text("Ostani"),
                ),

                const SizedBox(width: 16), // razmak između dugmadi
                // 🚪 IZAĐI
                ElevatedButton(
                  onPressed: () => Navigator.pop(context, true),
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.redAccent,
                    foregroundColor: Colors.white,
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(10),
                    ),
                  ),
                  child: const Text("Izađi"),
                ),
              ],
            ),
          ],
        );
      },
    );

    return result ?? false;
  }

  void _onChanged() {
    setState(() => hasChanges = true);
  }

  String _formatDate(DateTime? dt) {
    if (dt == null) return "";
    return "${dt.day.toString().padLeft(2, '0')}."
        "${dt.month.toString().padLeft(2, '0')}."
        "${dt.year}";
  }

  Future<void> _pickBirthDate() async {
    final picked = await showDatePicker(
      context: context,
      initialDate: user?.dateBirth ?? DateTime(1995),
      firstDate: DateTime(1900),
      lastDate: DateTime.now(),
      locale: const Locale("bs"),
    );

    if (picked != null) {
      birthDate.text = _formatDate(picked);
      _onChanged();
    }
  }

  Future<void> _pickImage() async {
    final result = await FilePicker.platform.pickFiles(type: FileType.image);
    if (result != null) {
      newImage = File(result.files.single.path!);
      setState(() => hasChanges = true);
    }
  }

  bool _validatePasswordChange() {
    setState(() {
      // TRENUTNA
      currentPasswordError =
          currentPasswordCtrl.text.isEmpty
              ? "Trenutna lozinka je obavezna."
              : null;

      // NOVA
      if (newPasswordCtrl.text.isEmpty) {
        newPasswordError = "Nova lozinka je obavezna.";
      } else if (!RegExp(
        r'^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,20}$',
      ).hasMatch(newPasswordCtrl.text)) {
        newPasswordError =
            "6–20 znakova, veliko i malo slovo, broj i specijalni znak.";
      } else {
        newPasswordError = null;
      }

      // PONOVI NOVU
      if (repeatNewPasswordCtrl.text.isEmpty) {
        repeatNewPasswordError = "Ponovite novu lozinku.";
      } else if (repeatNewPasswordCtrl.text != newPasswordCtrl.text) {
        repeatNewPasswordError = "Lozinke se ne poklapaju.";
      } else {
        repeatNewPasswordError = null;
      }
    });

    return currentPasswordError == null &&
        newPasswordError == null &&
        repeatNewPasswordError == null;
  }

  bool _validateProfileUpdate() {
    setState(() {
      firstNameError = null;
      lastNameError = null;
      usernameError = null;
      emailError = null;
      phoneError = null;
      birthDateError = null;

      // IME
      if (ime.text.trim().isEmpty) {
        firstNameError = "Ime je obavezno.";
      }

      // PREZIME
      if (prezime.text.trim().isEmpty) {
        lastNameError = "Prezime je obavezno.";
      }

      // USERNAME
      if (username.text.trim().isEmpty) {
        usernameError = "Username je obavezan.";
      } else if (username.text.length < 6) {
        usernameError = "Minimum 6 karaktera.";
      } else if (!RegExp(r'^(?=.*\d).{6,}$').hasMatch(username.text)) {
        usernameError = "Mora sadržavati barem jedan broj.";
      }

      // EMAIL
      if (email.text.trim().isEmpty) {
        emailError = "Email je obavezan.";
      } else if (!RegExp(
        r"^[\w\.-]+@[\w\.-]+\.\w+$",
      ).hasMatch(email.text.trim())) {
        emailError = "Email nije validan.";
      }

      // TELEFON (BH)
      final digits = phone.text.trim();
      if (digits.isEmpty) {
        phoneError = "Broj telefona je obavezan.";
      } else if (!RegExp(r'^\d+$').hasMatch(digits)) {
        phoneError = "Broj smije sadržavati samo cifre.";
      } else if (!digits.startsWith("6")) {
        phoneError = "BH broj mora početi sa 6 (60, 61 ili 62).";
      } else {
        final prefix = digits.substring(0, 2);
        if ((prefix == "61" || prefix == "62") && digits.length != 8) {
          phoneError = "Mora imati 6 cifara nakon $prefix.";
        } else if (prefix == "60" && digits.length != 9) {
          phoneError = "60 mreža mora imati 7 cifara nakon 60.";
        }
      }

      // DATUM ROĐENJA
      if (birthDate.text.trim().isEmpty) {
        birthDateError = "Datum rođenja je obavezan.";
      }
    });

    return firstNameError == null &&
        lastNameError == null &&
        usernameError == null &&
        emailError == null &&
        phoneError == null &&
        birthDateError == null;
  }

  Future<void> _changePassword() async {
    if (!_validatePasswordChange()) return;

    // 1️⃣ Provjera trenutne lozinke
    final isValid = await _userProvider.checkCurrentPassword({
      "userId": user!.id,
      "currentPassword": currentPasswordCtrl.text,
    });

    if (!isValid) {
      setState(() {
        currentPasswordError = "Trenutna lozinka nije ispravna.";
      });
      return;
    }

    // 2️⃣ Update nove lozinke
    await _userProvider.updateNewPassword({
      "userId": user!.id,
      "newPassword": newPasswordCtrl.text,
    });

    // 3️⃣ Reset
    currentPasswordCtrl.clear();
    newPasswordCtrl.clear();
    repeatNewPasswordCtrl.clear();

    setState(() {
      currentPasswordError = null;
      newPasswordError = null;
      repeatNewPasswordError = null;
    });

    _showPasswordChangeSuccessToast(context);
  }

  Future<void> _saveChanges() async {
    if (!hasChanges || user == null) return;

    // 🔴 VALIDACIJA PRIJE AŽURIRANJA
    if (!_validateProfileUpdate()) return;

    final oldUsername = user!.username;
    final newUsername = username.text.trim();

    await _userProvider.update(user!.id, {
      "firstName": ime.text.trim(),
      "lastName": prezime.text.trim(),
      "email": email.text.trim(),
      "username": newUsername,
      "phoneNumber": "+387${phone.text.trim()}",
      "dateBirth": DateConverter.toUtcIso(birthDate.text),
    });

    // 🟡 AKO JE USERNAME PROMIJENJEN → UPDATE SESSION
    if (oldUsername != newUsername) {
      Session.username = newUsername;
    }

    // IMAGE
    if (newImage != null) {
      await _userProvider.addUserImage(
        UserImageRequest(userId: user!.id, image: newImage!),
      );
    }

    _showUpdateSuccessToast(context);
    setState(() {
      hasChanges = false;
      user!.username = newUsername; // lokalno ažuriranje modela
    });
  }

  void _showPasswordChangeSuccessToast(BuildContext context) {
    final overlay = Overlay.of(context);
    if (overlay == null) return;

    late OverlayEntry entry;

    entry = OverlayEntry(
      builder:
          (_) => Positioned(
            bottom: 20,
            right: 20,
            child: Material(
              color: Colors.transparent,
              child: Container(
                padding: const EdgeInsets.symmetric(
                  horizontal: 18,
                  vertical: 12,
                ),
                decoration: BoxDecoration(
                  color: const Color(0xFF4CAF50), // 🟢 zeleno – uspjeh
                  borderRadius: BorderRadius.circular(12),
                  boxShadow: const [
                    BoxShadow(
                      color: Colors.black26,
                      blurRadius: 6,
                      offset: Offset(0, 3),
                    ),
                  ],
                ),
                child: const Text(
                  "✓ Lozinka je uspješno promijenjena",
                  style: TextStyle(
                    color: Colors.white,
                    fontSize: 15,
                    fontWeight: FontWeight.w600,
                  ),
                ),
              ),
            ),
          ),
    );

    overlay.insert(entry);

    Future.delayed(const Duration(seconds: 3), () {
      entry.remove();
    });
  }

  void _showUpdateSuccessToast(BuildContext context) {
    final overlay = Overlay.of(context);
    if (overlay == null) return;

    late OverlayEntry entry;

    entry = OverlayEntry(
      builder:
          (_) => Positioned(
            bottom: 20,
            right: 20,
            child: Material(
              color: Colors.transparent,
              child: Container(
                padding: const EdgeInsets.symmetric(
                  horizontal: 18,
                  vertical: 12,
                ),
                decoration: BoxDecoration(
                  color: const Color(0xFFF9A825), // 🟡 žuta (brand)
                  borderRadius: BorderRadius.circular(12),
                  boxShadow: const [
                    BoxShadow(
                      color: Colors.black26,
                      blurRadius: 6,
                      offset: Offset(0, 3),
                    ),
                  ],
                ),
                child: const Text(
                  "✓ Uspješno ažuriranje podataka",
                  style: TextStyle(
                    color: Colors.white,
                    fontSize: 15,
                    fontWeight: FontWeight.w600,
                  ),
                ),
              ),
            ),
          ),
    );

    overlay.insert(entry);

    Future.delayed(const Duration(seconds: 3), () {
      entry.remove();
    });
  }

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    if (user == null) {
      return const Scaffold(body: Center(child: CircularProgressIndicator()));
    }

    final bool hasDefaultImage =
        user!.imageUrl == null || user!.imageUrl == "test.jpg";

    return Scaffold(
      backgroundColor: Colors.white,
      body: GestureDetector(
        behavior: HitTestBehavior.translucent,
        onTap: () {
          FocusScope.of(context).unfocus();
        },
        child: CustomScrollView(
          slivers: [
            SljedecaDestinacijaIMenuBar(
              daLijeKliknuo: false,
              onBeforeNavigate: () async {
                if (!hasChanges) return true;
                return await _confirmExit();
              },
            ),

            SliverToBoxAdapter(
              child: Column(
                children: [
                  SizedBox(
                    height: screenHeight * 0.13,
                    child: Center(
                      child: Text(
                        'Moj profil',
                        style: TextStyle(
                          fontFamily: 'AROneSans',
                          fontWeight: FontWeight.bold,
                          fontSize: screenWidth * 0.06,
                        ),
                      ),
                    ),
                  ),

                  // IMAGE
                  CircleAvatar(
                    radius: screenWidth * 0.2,
                    backgroundImage:
                        newImage != null
                            ? FileImage(newImage!)
                            : NetworkImage(
                                  "${ApiConfig.imagesUsers}/${user!.imageUrl}",
                                )
                                as ImageProvider,
                  ),

                  SizedBox(height: screenHeight * 0.02),

                  GestureDetector(
                    onTap: _pickImage,
                    child: Container(
                      width: screenWidth * 0.5,
                      height: screenHeight * 0.06,
                      alignment: Alignment.center,
                      color: const Color(0xFF67B1E5),
                      child: Text(
                        hasDefaultImage ? "Dodaj sliku" : "Zamijeni sliku",
                        style: TextStyle(
                          color: Colors.white,
                          fontFamily: 'AROneSans',
                          fontWeight: FontWeight.bold,
                          fontSize: screenWidth * 0.04,
                        ),
                      ),
                    ),
                  ),

                  SizedBox(height: screenHeight * 0.04),

                  // TRAVEL TOKENI
                  // TRAVEL TOKENI
                  Container(
                    width: screenWidth,
                    height: screenHeight * 0.05,
                    alignment: Alignment.center,
                    child: Text(
                      'UKUPAN BROJ TRAVEL TOKENA:',
                      style: TextStyle(
                        fontFamily: 'AROneSans',
                        fontWeight: FontWeight.bold,
                        fontSize: screenWidth * 0.045,
                      ),
                    ),
                  ),
                  Container(
                    width: screenWidth,
                    height: screenHeight * 0.1,
                    alignment: Alignment.center,
                    color: const Color(0xFF67B1E5),
                    child:
                        _isLoadingTokens
                            ? const CircularProgressIndicator(
                              color: Colors.white,
                            )
                            : Text(
                              '${_userEquity ?? 0}',
                              style: TextStyle(
                                color: Colors.white,
                                fontFamily: 'AROneSans',
                                fontWeight: FontWeight.bold,
                                fontSize: screenWidth * 0.12,
                              ),
                            ),
                  ),

                  SizedBox(height: screenHeight * 0.04),

                  // INFO BOX
                  Container(
                    width: screenWidth * 0.9,
                    padding: EdgeInsets.all(screenWidth * 0.04),
                    decoration: BoxDecoration(
                      color: const Color(0xFFF5F5F5),
                      borderRadius: BorderRadius.circular(screenWidth * 0.05),
                    ),
                    child: Column(
                      children: [
                        _field("Ime", ime, errorText: firstNameError),
                        _field("Prezime", prezime, errorText: lastNameError),
                        _phoneField(),
                        GestureDetector(
                          onTap: _pickBirthDate,
                          child: AbsorbPointer(
                            child: _field(
                              "Datum rođenja",
                              birthDate,
                              errorText: birthDateError,
                            ),
                          ),
                        ),
                        _field("Email", email, errorText: emailError),
                        _field("Username", username, errorText: usernameError),

                        SizedBox(height: screenHeight * 0.02),

                        // CHANGE PASSWORD
                        Container(
                          width: double.infinity,
                          padding: EdgeInsets.all(screenWidth * 0.04),
                          decoration: BoxDecoration(
                            color: const Color(0xFF67B1E5),
                            borderRadius: BorderRadius.circular(
                              screenWidth * 0.05,
                            ),
                          ),
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Text(
                                'Želite li promijeniti lozinku?',
                                style: TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.bold,
                                  fontSize: screenWidth * 0.045,
                                ),
                              ),
                              SizedBox(height: screenHeight * 0.015),

                              // TRENUTNA
                              _buildPasswordField(
                                label: "Trenutna lozinka",
                                controller: currentPasswordCtrl,
                                error: currentPasswordError,
                              ),

                              // NOVA
                              _buildPasswordField(
                                label: "Nova lozinka",
                                controller: newPasswordCtrl,
                                error: newPasswordError,
                              ),

                              // PONOVI NOVU
                              _buildPasswordField(
                                label: "Ponovi novu lozinku",
                                controller: repeatNewPasswordCtrl,
                                error: repeatNewPasswordError,
                              ),

                              SizedBox(height: screenHeight * 0.015),

                              Align(
                                alignment: Alignment.centerRight,
                                child: ElevatedButton(
                                  onPressed: _changePassword,
                                  style: ElevatedButton.styleFrom(
                                    backgroundColor: const Color(0xFF67B1E5),
                                    foregroundColor: Colors.white,
                                    side: const BorderSide(
                                      color: Colors.white,
                                      width: 2,
                                    ),
                                  ),
                                  child: Text(
                                    'ažuriraj',
                                    style: TextStyle(
                                      fontSize: screenWidth * 0.035,
                                    ),
                                  ),
                                ),
                              ),
                            ],
                          ),
                        ),

                        SizedBox(height: screenHeight * 0.025),

                        ElevatedButton(
                          onPressed: hasChanges ? _saveChanges : null,
                          style: ElevatedButton.styleFrom(
                            backgroundColor:
                                hasChanges
                                    ? const Color(0xFF67B1E5)
                                    : Colors.grey,
                            shape: const StadiumBorder(),
                          ),
                          child: Text(
                            'ažuriraj podatke',
                            style: TextStyle(
                              color: Colors.white,
                              fontSize: screenWidth * 0.04,
                            ),
                          ),
                        ),
                      ],
                    ),
                  ),

                  SizedBox(height: screenHeight * 0.05),

                  Container(
                    width: screenWidth * 0.95,
                    decoration: BoxDecoration(
                      border: Border.all(color: const Color(0xFF67B1E5)),
                      borderRadius: BorderRadius.circular(20),
                    ),
                    child: Column(
                      children: [
                        ReservationUniversalView(
                          imeRezervacija: 'Aktivne rezervacije',
                          activationStatus: true,
                        ),
                        ReservationUniversalView(
                          imeRezervacija: 'Završena putovanja',
                          activationStatus: false,
                        ),
                      ],
                    ),
                  ),

                  SizedBox(height: screenHeight * 0.05),
                  const eTravelFooter(),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget _field(
    String label,
    TextEditingController ctrl, {
    bool enabled = true,
    TextInputType keyboardType = TextInputType.text,
    String? errorText,
  }) {
    return Padding(
      padding: EdgeInsets.symmetric(vertical: screenHeight * 0.01),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            label,
            style: TextStyle(
              fontWeight: FontWeight.bold,
              fontSize: screenWidth * 0.035,
            ),
          ),
          SizedBox(height: screenHeight * 0.005),
          TextField(
            controller: ctrl,
            enabled: enabled,
            keyboardType: keyboardType,
            decoration: InputDecoration(
              filled: true,
              fillColor: Colors.white,
              errorText: errorText,
              border: OutlineInputBorder(
                borderRadius: BorderRadius.circular(screenWidth * 0.02),
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildPasswordField({
    required String label,
    required TextEditingController controller,
    String? error,
  }) {
    return Padding(
      padding: EdgeInsets.only(bottom: screenHeight * 0.01),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            label,
            style: const TextStyle(
              color: Colors.white,
              fontWeight: FontWeight.bold,
            ),
          ),
          const SizedBox(height: 4),
          TextField(
            controller: controller,
            obscureText: true,
            decoration: InputDecoration(
              filled: true,
              fillColor: Colors.white,
              border: OutlineInputBorder(
                borderRadius: BorderRadius.circular(10),
                borderSide: BorderSide(
                  color: error != null ? Colors.red : Colors.transparent,
                ),
              ),
            ),
          ),
          if (error != null)
            Padding(
              padding: const EdgeInsets.only(top: 4),
              child: Text(
                error,
                style: const TextStyle(color: Colors.red, fontSize: 12),
              ),
            ),
        ],
      ),
    );
  }

  Widget _buildTextField(
    String label,
    String hint, {
    bool obscureText = false,
    TextInputType keyboardType = TextInputType.text,
  }) {
    return Padding(
      padding: EdgeInsets.symmetric(vertical: screenHeight * 0.01),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            '$label',
            style: TextStyle(
              fontWeight: FontWeight.bold,
              fontSize: screenWidth * 0.035,
            ),
          ),
          SizedBox(height: screenHeight * 0.005),
          TextField(
            obscureText: obscureText,
            keyboardType: keyboardType,
            decoration: InputDecoration(
              hintText: hint,
              hintStyle: TextStyle(fontSize: screenWidth * 0.035),
              filled: true,
              fillColor: Colors.white,
              border: OutlineInputBorder(
                borderRadius: BorderRadius.circular(screenWidth * 0.02),
              ),
              contentPadding: EdgeInsets.symmetric(
                horizontal: screenWidth * 0.03,
                vertical: screenHeight * 0.015,
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _phoneField() {
    return Padding(
      padding: EdgeInsets.symmetric(vertical: screenHeight * 0.01),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            'Broj telefona',
            style: TextStyle(
              fontWeight: FontWeight.bold,
              fontSize: screenWidth * 0.035,
            ),
          ),
          SizedBox(height: screenHeight * 0.005),

          Container(
            decoration: BoxDecoration(
              color: Colors.white,
              borderRadius: BorderRadius.circular(screenWidth * 0.02),
              border: Border.all(
                color: phoneError != null ? Colors.red : Colors.black12,
                width: phoneError != null ? 1.5 : 1,
              ),
            ),
            child: Row(
              children: [
                // PREFIX +387 (FIXNO)
                Padding(
                  padding: EdgeInsets.symmetric(horizontal: screenWidth * 0.03),
                  child: Text(
                    "+387",
                    style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: screenWidth * 0.035,
                    ),
                  ),
                ),

                // VERTIKALNA LINIJA
                Container(
                  height: screenHeight * 0.04,
                  width: 1,
                  color: Colors.black26,
                ),

                // INPUT
                Expanded(
                  child: TextField(
                    controller: phone,
                    keyboardType: TextInputType.number,
                    decoration: InputDecoration(
                      hintText: "unesite broj",
                      hintStyle: TextStyle(fontSize: screenWidth * 0.035),
                      border: InputBorder.none,
                      contentPadding: EdgeInsets.symmetric(
                        horizontal: screenWidth * 0.03,
                        vertical: screenHeight * 0.015,
                      ),
                    ),
                  ),
                ),
              ],
            ),
          ),

          // ❌ ERROR TEKST
          if (phoneError != null)
            Padding(
              padding: const EdgeInsets.only(top: 6, left: 6),
              child: Text(
                phoneError!,
                style: const TextStyle(
                  color: Colors.red,
                  fontSize: 12,
                  fontWeight: FontWeight.w500,
                ),
              ),
            ),
        ],
      ),
    );
  }
}
