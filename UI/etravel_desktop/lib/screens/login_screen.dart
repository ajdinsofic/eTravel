import 'package:etravel_desktop/models/login_request.dart';
import 'package:etravel_desktop/providers/auth_provider.dart';
import 'package:etravel_desktop/screens/offer_screen.dart';
import 'package:etravel_desktop/widgets/master_screen.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  final _usernameController = TextEditingController();
  final _passwordController = TextEditingController();
  bool _isLoading = false;

  void _onLoginPressed() async {
  setState(() => _isLoading = true);

  final auth = AuthProvider();

  final status = await auth.prijava(
    LoginRequest(
      username: _usernameController.text.trim(),
      password: _passwordController.text.trim(),
    ),
  );

  setState(() => _isLoading = false);

  if (status == "OK") {

    Navigator.pushReplacement(
      context,
      MaterialPageRoute(
        builder: (context) => MasterScreen(
          selectedIndex: 0,
          child: OfferScreen(),
        ),
      ),
    );

    return;
  }

  if (status == "ZABRANJENO") {
    ScaffoldMessenger.of(context).showSnackBar(
      const SnackBar(
        content: Text("Nemate privilegije za pristup aplikaciji."),
      ),
    );
    return;
  }

  if (status == "NEISPRAVNO") {
    ScaffoldMessenger.of(context).showSnackBar(
      const SnackBar(
        content: Text("Pogrešno korisničko ime ili lozinka."),
      ),
    );
    return;
  }

  ScaffoldMessenger.of(context).showSnackBar(
    const SnackBar(
      content: Text("Došlo je do greške. Pokušajte ponovo."),
    ),
  );
}



  @override
  Widget build(BuildContext context) {
    const backgroundBlue = Color(0xFF64B5F6);

    return Scaffold(
      backgroundColor: backgroundBlue,
      body: Stack(
        children: [
          Center(
            child: Container(
              width: 420,
              padding: const EdgeInsets.symmetric(horizontal: 32, vertical: 32),
              decoration: BoxDecoration(
                color: Colors.white.withOpacity(0.16),
                borderRadius: BorderRadius.circular(32),
                border: Border.all(
                  color: Colors.white.withOpacity(0.25),
                  width: 1,
                ),
                boxShadow: [
                  BoxShadow(
                    color: Colors.black.withOpacity(0.18),
                    blurRadius: 24,
                    offset: const Offset(0, 12),
                  ),
                ],
              ),
              child: Column(
                mainAxisSize: MainAxisSize.min,
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  
                  // Logo
                  Center(
                    child: Text(
                      'eTravel',
                      style: GoogleFonts.leckerliOne(
                        fontSize: 42,
                        color: Colors.white,
                        letterSpacing: 1.2,
                      ),
                    ),
                  ),

                  const SizedBox(height: 28),

                  // Username field
                  TextField(
                    controller: _usernameController,
                    style: const TextStyle(color: Colors.white),
                    decoration: InputDecoration(
                      filled: true,
                      fillColor: Colors.white.withOpacity(0.12),
                      prefixIcon: const Icon(Icons.person_outline,
                          color: Colors.white70),
                      labelText: 'Username',
                      labelStyle: const TextStyle(
                        color: Colors.white70,
                        fontSize: 14,
                      ),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(18),
                        borderSide:
                            BorderSide(color: Colors.white.withOpacity(0.3)),
                      ),
                      enabledBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(18),
                        borderSide:
                            BorderSide(color: Colors.white.withOpacity(0.25)),
                      ),
                      focusedBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(18),
                        borderSide: const BorderSide(
                          color: Colors.white,
                          width: 1.2,
                        ),
                      ),
                    ),
                  ),

                  const SizedBox(height: 16),

                  // Password field
                  TextField(
                    controller: _passwordController,
                    obscureText: true,
                    style: const TextStyle(color: Colors.white),
                    decoration: InputDecoration(
                      filled: true,
                      fillColor: Colors.white.withOpacity(0.12),
                      prefixIcon: const Icon(Icons.lock_outline,
                          color: Colors.white70),
                      labelText: 'Password',
                      labelStyle: const TextStyle(
                        color: Colors.white70,
                        fontSize: 14,
                      ),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(18),
                        borderSide:
                            BorderSide(color: Colors.white.withOpacity(0.3)),
                      ),
                      enabledBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(18),
                        borderSide:
                            BorderSide(color: Colors.white.withOpacity(0.25)),
                      ),
                      focusedBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(18),
                        borderSide: const BorderSide(
                          color: Colors.white,
                          width: 1.2,
                        ),
                      ),
                    ),
                  ),

                  const SizedBox(height: 24),

                  // Login button
                  SizedBox(
                    height: 46,
                    child: _isLoading
                        ? const Center(
                            child: CircularProgressIndicator(
                              strokeWidth: 2.5,
                              valueColor:
                                  AlwaysStoppedAnimation<Color>(Colors.white),
                            ),
                          )
                        : ElevatedButton(
                            onPressed: _onLoginPressed,
                            style: ElevatedButton.styleFrom(
                              backgroundColor: const Color(0xFF1976D2),
                              foregroundColor: Colors.white,
                              shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(18),
                              ),
                              elevation: 4,
                            ),
                            child: const Text(
                              'Log in',
                              style: TextStyle(
                                fontSize: 16,
                                fontWeight: FontWeight.w600,
                              ),
                            ),
                          ),
                  ),

                  const SizedBox(height: 12),

                  // Forgot password
                  TextButton(
                    onPressed: () {},
                    child: const Text(
                      'Forgot password?',
                      style: TextStyle(
                        color: Colors.white70,
                        fontSize: 13,
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ),

          // Copyright bottom
          Positioned(
            bottom: 16,
            left: 0,
            right: 0,
            child: Center(
              child: Text(
                '© 2025 eTravel. All rights reserved.',
                style: TextStyle(
                  color: Colors.white.withOpacity(0.9),
                  fontSize: 12,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
