import 'package:etravel_app/models/login_request.dart';
import 'package:etravel_app/providers/auth_provider.dart';
import 'package:etravel_app/screens/StartingPage.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../widgets/SljedecaDestinacijaIMenuBar.dart';
import '../widgets/headerIFooterAplikacije/eTravelFooter.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({super.key});

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  final AuthProvider _authProvider = AuthProvider();

  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();

  String? usernameError;
  String? passwordError;
  String? globalError;

  bool isLoading = false;


  @override
  void dispose() {
    _usernameController.dispose();
    _passwordController.dispose();
    super.dispose();
  }

  bool _isValidUsername(String username) {
  // final usernameRegex = RegExp(
  //   r'^(?=.*\d)[A-Za-z\d]{6,}$',
  // );
  // return usernameRegex.hasMatch(username);
  return true;
}


  bool _isValidPassword(String password) {
    // 6-10 karaktera, 1 veliko, 1 malo, 1 broj, 1 specijalan
    final passwordRegex = RegExp(
      r'^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,20}$',
    );
    return passwordRegex.hasMatch(password);
  }

  Future<void> _handleLogin() async {
    setState(() {
      usernameError = null;
      passwordError = null;
      globalError = null;
    });

    final username = _usernameController.text.trim();
    final password = _passwordController.text;

    bool valid = true;

    if (!_isValidUsername(username)) {
      usernameError = "Unesite ispravan username (min 3 karaktera).";
      valid = false;
    }

    if (!_isValidPassword(password)) {
      passwordError =
          "Lozinka: 6–10 znakova, veliko i malo slovo, broj i specijalan znak.";
      valid = false;
    }

    if (!valid) {
      setState(() {});
      return;
    }

    setState(() => isLoading = true);

    final result = await _authProvider.prijava(
      LoginRequest(username: username, password: password),
    );

    setState(() => isLoading = false);

    if (!mounted) return;

    if (result == "OK") {
      Navigator.of(context).pushReplacement(
        MaterialPageRoute(builder: (_) => const StartingPage()),
      );
      return;
    }

    setState(() {
      if (result == "NEISPRAVNO") {
        globalError = "Pogrešan username ili lozinka.";
      } else if (result == "ZABRANJENO") {
        globalError = "Nemate pravo pristupa aplikaciji.";
      } else {
        globalError = "Došlo je do greške. Pokušajte ponovo.";
      }
    });
  }

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
            /// SLIVER APP BAR (HEADER)
            SljedecaDestinacijaIMenuBar(daLijeKliknuo: false),
        
            /// LOGIN + FOOTER
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
                        child: _loginForm(),
                      ),
                    ),
                  ),
        
                  /// FOOTER
                  const eTravelFooter(),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget _loginForm() {
    return Container(
      padding: const EdgeInsets.all(24),
      decoration: BoxDecoration(
        color: const Color(0xFFF5F5F5),
        borderRadius: BorderRadius.circular(20),
        boxShadow: const [
          BoxShadow(
            color: Colors.black12,
            blurRadius: 12,
            offset: Offset(0, 6),
          ),
        ],
      ),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Center(
            child: Text(
              "LOGIN",
              style: TextStyle(fontSize: 22, fontWeight: FontWeight.bold),
            ),
          ),
          const SizedBox(height: 24),

          const Text("Username", style: TextStyle(fontWeight: FontWeight.w600)),
          const SizedBox(height: 6),
          _inputField(
            controller: _usernameController,
            hint: "Unesite username",
            icon: Icons.person_outline,
            errorText: usernameError,
            textInputAction: TextInputAction.next,
          ),

          const SizedBox(height: 16),

          const Text("Lozinka", style: TextStyle(fontWeight: FontWeight.w600)),
          const SizedBox(height: 6),
          _inputField(
            controller: _passwordController,
            hint: "Unesite lozinku",
            icon: Icons.lock_outline,
            obscure: true,
            errorText: passwordError,
            textInputAction: TextInputAction.done,
            onSubmitted: (_) => _handleLogin(),
          ),

          if (globalError != null) ...[
            const SizedBox(height: 12),
            Text(
              globalError!,
              style: const TextStyle(color: Colors.red, fontSize: 12),
            ),
          ],

          const SizedBox(height: 20),

          Row(
            children: [
              TextButton(
                onPressed: () {
                  // TODO: forgot password flow
                },
                child: const Text(
                  "zaboravili ste staru lozinku?",
                  style: TextStyle(fontSize: 12),
                ),
              ),
              const Spacer(),
              ElevatedButton(
                onPressed: isLoading ? null : _handleLogin,
                style: ElevatedButton.styleFrom(
                  backgroundColor: const Color(0xFF6FB7E9),
                  padding: const EdgeInsets.symmetric(
                    horizontal: 26,
                    vertical: 12,
                  ),
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(25),
                  ),
                  elevation: 0,
                ),
                child: isLoading
                    ? const SizedBox(
                        width: 18,
                        height: 18,
                        child: CircularProgressIndicator(
                          strokeWidth: 2,
                          color: Colors.white,
                        ),
                      )
                    : const Text(
                        "login",
                        style: TextStyle(color: Colors.white),
                      ),
              ),
            ],
          ),
        ],
      ),
    );
  }

  static Widget _inputField({
    required TextEditingController controller,
    required String hint,
    required IconData icon,
    bool obscure = false,
    String? errorText,
    TextInputAction? textInputAction,
    void Function(String)? onSubmitted,
  }) {
    return TextField(
      controller: controller,
      obscureText: obscure,
      textInputAction: textInputAction,
      onSubmitted: onSubmitted,
      decoration: InputDecoration(
        hintText: hint,
        errorText: errorText,
        prefixIcon: Icon(icon, size: 20),
        contentPadding: const EdgeInsets.symmetric(vertical: 14, horizontal: 16),
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(12),
          borderSide: const BorderSide(color: Colors.black12),
        ),
        enabledBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(12),
          borderSide: const BorderSide(color: Colors.black12),
        ),
        focusedBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(12),
          borderSide: const BorderSide(color: Color(0xFF6FB7E9)),
        ),
      ),
    );
  }
}
