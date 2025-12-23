import 'package:etravel_app/helper/date_converter.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:etravel_app/widgets/headerIFooterAplikacije/eTravelFooter.dart';
import '../providers/user_provider.dart';
import '../screens/loginPage.dart';

class RegisterPage extends StatefulWidget {
  const RegisterPage({super.key});

  @override
  State<RegisterPage> createState() => _RegisterPageState();
}

class _RegisterPageState extends State<RegisterPage> {
  // =========================
  // PROVIDER
  // =========================
  late UserProvider _userProvider;

  // =========================
  // CONTROLLERS
  // =========================
  final TextEditingController _firstNameController = TextEditingController();
  final TextEditingController _lastNameController = TextEditingController();
  final TextEditingController _phoneController = TextEditingController();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _userNameController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _birthDateController = TextEditingController();

  DateTime? _selectedBirthDate;

  // =========================
  // ERRORS
  // =========================
  String? firstNameError;
  String? lastNameError;
  String? usernameError;
  String? emailError;
  String? phoneError;
  String? birthDateError;
  String? passwordError;

  bool isLoading = false;

  @override
  void initState() {
    super.initState();
    _userProvider = Provider.of<UserProvider>(context, listen: false);
  }

  @override
  void dispose() {
    _firstNameController.dispose();
    _lastNameController.dispose();
    _phoneController.dispose();
    _emailController.dispose();
    _passwordController.dispose();
    _birthDateController.dispose();
    _userNameController.dispose();
    super.dispose();
  }

  // =========================
  // DATE PICKER
  // =========================
  Future<void> _pickBirthDate() async {
    final now = DateTime.now();

    final picked = await showDatePicker(
      context: context,
      initialDate: DateTime(now.year - 18),
      firstDate: DateTime(1900),
      lastDate: now,
      locale: const Locale("bs"),
    );

    if (picked == null) return;

    setState(() {
      _selectedBirthDate = picked;
      _birthDateController.text =
          "${picked.day.toString().padLeft(2, '0')}."
          "${picked.month.toString().padLeft(2, '0')}."
          "${picked.year}";
    });
  }

  // =========================
  // VALIDATION (TVOJ PRIMJER)
  // =========================
  bool _validate() {
    setState(() {
      firstNameError =
          _firstNameController.text.trim().isEmpty ? "Ime je obavezno." : null;

      lastNameError =
          _lastNameController.text.trim().isEmpty
              ? "Prezime je obavezno."
              : null;

      // USERNAME
      if (_userNameController.text.trim().isEmpty) {
        usernameError = "Username je obavezan.";
      } else if (_userNameController.text.length < 6) {
        usernameError = "Minimum 6 karaktera.";
      } else if (!RegExp(
        r'^(?=.*\d).{6,}$',
      ).hasMatch(_userNameController.text)) {
        usernameError = "Mora sadržavati barem jedan broj.";
      } else {
        usernameError = null;
      }

      // EMAIL
      if (_emailController.text.trim().isEmpty) {
        emailError = "Email je obavezan.";
      } else if (!RegExp(
        r"^[\w\.-]+@[\w\.-]+\.\w+$",
      ).hasMatch(_emailController.text.trim())) {
        emailError = "Email nije validan.";
      } else {
        emailError = null;
      }

      // TELEFON (BH)
      if (_phoneController.text.trim().isEmpty) {
        phoneError = "Broj telefona je obavezan.";
      } else {
        final digits = _phoneController.text.trim();

        if (!RegExp(r'^\d+$').hasMatch(digits)) {
          phoneError = "Broj smije sadržavati samo cifre.";
        } else if (!digits.startsWith("6")) {
          phoneError = "BH broj mora početi sa 6 (60, 61 ili 62).";
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

      if (_passwordController.text.isEmpty) {
        passwordError = "Lozinka je obavezna.";
      } else if (!RegExp(
        r'^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,20}$',
      ).hasMatch(_passwordController.text)) {
        passwordError =
            "6–10 znakova, veliko i malo slovo, broj i specijalni znak.";
      } else {
        passwordError = null;
      }

      birthDateError =
          _birthDateController.text.isEmpty
              ? "Datum rođenja je obavezan."
              : null;
    });

    return firstNameError == null &&
        lastNameError == null &&
        usernameError == null &&
        emailError == null &&
        phoneError == null &&
        passwordError == null &&
        birthDateError == null;
  }

  // =========================
  // REGISTER
  // =========================
  Future<void> _handleRegister() async {
    if (!_validate()) return;

    final request = {
      "firstName": _firstNameController.text.trim(),
      "lastName": _lastNameController.text.trim(),
      "email": _emailController.text.trim(),
      "username": _userNameController.text.trim(),
      "dateBirth": DateConverter.toUtcIsoFromDate(_selectedBirthDate!),
      "phoneNumber": "+387${_phoneController.text.trim()}",
      "password": _passwordController.text,
      "roleId": 1,
    };

    setState(() => isLoading = true);

    try {
      await _userProvider.insert(request);

      _showSuccessToast(context);

      await Future.delayed(const Duration(milliseconds: 500));

      if (!mounted) return;

      Navigator.pushReplacement(
        context,
        MaterialPageRoute(builder: (_) => const LoginPage()),
      );
    } catch (e) {
      debugPrint("Register error: $e");
    } finally {
      setState(() => isLoading = false);
    }
  }


  // =========================
  // BUILD
  // =========================
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color(0xFFF2F2F2),
      body: GestureDetector(
        behavior: HitTestBehavior.translucent,
        onTap: () {
          FocusScope.of(context).unfocus();
        },
        child: CustomScrollView(
          slivers: [
            SljedecaDestinacijaIMenuBar(daLijeKliknuo: false),

            SliverFillRemaining(
              hasScrollBody: false,
              child: Column(
                children: [
                  Expanded(
                    child: Center(
                      child: Container(
                        width: 380,
                        padding: const EdgeInsets.all(24),
                        decoration: BoxDecoration(
                          color: Colors.white,
                          borderRadius: BorderRadius.circular(20),
                          boxShadow: const [
                            BoxShadow(
                              color: Colors.black12,
                              blurRadius: 20,
                              offset: Offset(0, 10),
                            ),
                          ],
                        ),
                        child: _registerForm(),
                      ),
                    ),
                  ),
                  const eTravelFooter(),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }

  // =========================
  // FORM (DIZAJN OSTAO ISTI)
  // =========================
  Widget _registerForm() {
    return Container(
      padding: const EdgeInsets.all(24),
      decoration: BoxDecoration(
        color: const Color(0xFFF5F5F5),
        borderRadius: BorderRadius.circular(20),
      ),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Center(
            child: Text(
              "REGISTRACIJA",
              style: TextStyle(fontSize: 22, fontWeight: FontWeight.bold),
            ),
          ),
          const SizedBox(height: 24),

          _label("Ime"),
          _inputField(
            _firstNameController,
            "unesite ime",
            error: firstNameError,
          ),

          _label("Prezime"),
          _inputField(
            _lastNameController,
            "unesite prezime",
            error: lastNameError,
          ),

          _phoneField(),

          _label("Datum rođenja"),
          _dateField(),

          _label("Email"),
          _inputField(
            _emailController,
            "unesite email",
            keyboardType: TextInputType.emailAddress,
            error: emailError,
          ),

          _label("Username"),
          _inputField(
            _userNameController,
            "unesite username",
            error: usernameError,
          ),

          _label("Lozinka"),
          _inputField(
            _passwordController,
            "unesite lozinku",
            obscure: true,
            error: passwordError,
          ),

          const SizedBox(height: 24),

          Align(
            alignment: Alignment.centerRight,
            child: ElevatedButton(
              onPressed: isLoading ? null : _handleRegister,
              style: ElevatedButton.styleFrom(
                backgroundColor: const Color(0xFF6FB7E9),
                padding: const EdgeInsets.symmetric(
                  horizontal: 26,
                  vertical: 12,
                ),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(25),
                ),
              ),
              child:
                  isLoading
                      ? const CircularProgressIndicator(color: Colors.white)
                      : const Text(
                        "registrujte se",
                        style: TextStyle(color: Colors.white),
                      ),
            ),
          ),
        ],
      ),
    );
  }

  void _showSuccessToast(BuildContext context) {
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
                  color: Colors.green.shade600,
                  borderRadius: BorderRadius.circular(12),
                ),
                child: const Text(
                  "✓ Uspješna registracija, molimo prijavite se",
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

  // =========================
  // HELPERS
  // =========================
  Widget _label(String text) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 6, top: 12),
      child: Text(text, style: const TextStyle(fontWeight: FontWeight.w600)),
    );
  }

  Widget _inputField(
    TextEditingController controller,
    String hint, {
    bool obscure = false,
    TextInputType keyboardType = TextInputType.text,
    String? error,
  }) {
    return TextField(
      controller: controller,
      obscureText: obscure,
      keyboardType: keyboardType,
      decoration: _inputDecoration(hint).copyWith(errorText: error),
    );
  }

  Widget _phoneField() {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        _label("Broj telefona"),
        Container(
          decoration: BoxDecoration(
            border: Border.all(
              color: phoneError == null ? Colors.black12 : Colors.red,
              width: phoneError == null ? 1 : 2,
            ),
            borderRadius: BorderRadius.circular(12),
          ),
          child: Row(
            children: [
              const Padding(
                padding: EdgeInsets.symmetric(horizontal: 12),
                child: Text(
                  "+387",
                  style: TextStyle(fontWeight: FontWeight.w600),
                ),
              ),
              Container(height: 32, width: 1, color: Colors.black38),
              Expanded(
                child: TextField(
                  controller: _phoneController,
                  keyboardType: TextInputType.number,
                  decoration: const InputDecoration(
                    border: InputBorder.none,
                    hintText: "unesite broj",
                    contentPadding: EdgeInsets.symmetric(
                      horizontal: 12,
                      vertical: 14,
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
        if (phoneError != null)
          Padding(
            padding: const EdgeInsets.only(top: 4),
            child: Text(
              phoneError!,
              style: const TextStyle(color: Colors.red, fontSize: 12),
            ),
          ),
      ],
    );
  }

  Widget _dateField() {
    return TextField(
      controller: _birthDateController,
      readOnly: true,
      onTap: _pickBirthDate,
      decoration: _inputDecoration("unesite datum").copyWith(
        errorText: birthDateError,
        suffixIcon: const Icon(Icons.calendar_today),
      ),
    );
  }

  InputDecoration _inputDecoration(String hint) {
    return InputDecoration(
      hintText: hint,
      contentPadding: const EdgeInsets.symmetric(vertical: 14, horizontal: 16),
      border: OutlineInputBorder(borderRadius: BorderRadius.circular(12)),
      enabledBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: const BorderSide(color: Colors.black12),
      ),
      focusedBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: const BorderSide(color: Color(0xFF6FB7E9)),
      ),
    );
  }
}
