
import 'package:etravel_desktop/providers/user_provider.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class ForgotPasswordPopup extends StatefulWidget {
  const ForgotPasswordPopup({super.key});

  @override
  State<ForgotPasswordPopup> createState() => _ForgotPasswordPopupState();
}

class _ForgotPasswordPopupState extends State<ForgotPasswordPopup> {
  final TextEditingController _emailController = TextEditingController();
  late UserProvider _userProvider;

  String? error;
  String? success;
  bool isLoading = false;

  @override
  void initState() {
    super.initState();
    _userProvider = Provider.of<UserProvider>(context, listen: false);
  }

  @override
  void dispose() {
    _emailController.dispose();
    super.dispose();
  }

  bool _isValidEmail(String email) {
    final regex = RegExp(r'^[^@]+@[^@]+\.[^@]+');
    return regex.hasMatch(email);
  }

  Future<void> _submit() async {
  setState(() {
    error = null;
    success = null;
  });

  final email = _emailController.text.trim();

  if (!_isValidEmail(email)) {
    setState(() => error = "Unesite ispravan email.");
    return;
  }

  setState(() => isLoading = true);

  try {
    await _userProvider.forgotPassword(email);

    if (!mounted) return;

    setState(() {
      success =
          "Nalog pronadjen, poslan je email sa izgenerisanom novom lozinkom.";
    });
  } catch (e) {
    if (!mounted) return;

    setState(() {
      error = "Nijedan korisnički nalog se ne poklapa sa ovim emailom";
    });
  } finally {
    setState(() => isLoading = false);
  }
}


  @override
  Widget build(BuildContext context) {
    return Dialog(
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(18)),
      child: SizedBox(
        width: 480,
        child: Padding(
          padding: const EdgeInsets.all(20),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Center(
                child: Text(
                  "ZABORAVLJENA LOZINKA",
                  style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                ),
              ),
              const SizedBox(height: 16),
        
              const Text(
                "Unesite email povezan sa vašim nalogom.",
                style: TextStyle(fontSize: 13),
              ),
              const SizedBox(height: 12),
        
              TextField(
                controller: _emailController,
                keyboardType: TextInputType.emailAddress,
                decoration: InputDecoration(
                  hintText: "Email",
                  prefixIcon: const Icon(Icons.email_outlined),
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(12),
                  ),
                ),
              ),
        
              if (error != null) ...[
                const SizedBox(height: 10),
                Text(
                  error!,
                  style: const TextStyle(color: Colors.red, fontSize: 12),
                ),
              ],
        
              if (success != null) ...[
                const SizedBox(height: 10),
                Text(
                  success!,
                  style: const TextStyle(color: Colors.green, fontSize: 12),
                ),
              ],
        
              const SizedBox(height: 18),
        
              Row(
                children: [
                  TextButton(
                    onPressed: () => Navigator.of(context).pop(),
                    child: const Text("Zatvori"),
                  ),
                  const Spacer(),
                  ElevatedButton(
                    onPressed: isLoading ? null : _submit,
                    style: ElevatedButton.styleFrom(
                      backgroundColor: const Color(0xFF6FB7E9),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(20),
                      ),
                    ),
                    child:
                        isLoading
                            ? const SizedBox(
                              width: 16,
                              height: 16,
                              child: CircularProgressIndicator(
                                strokeWidth: 2,
                                color: Colors.white,
                              ),
                            )
                            : const Text(
                              "Pošalji link",
                              style: TextStyle(color: Colors.white),
                            ),
                  ),
                ],
              ),
            ],
          ),
        ),
      ),
    );
  }
}
