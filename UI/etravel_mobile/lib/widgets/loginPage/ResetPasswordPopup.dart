import 'package:etravel_app/providers/user_provider.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class ResetPasswordPopup extends StatefulWidget {
  final String token;

  const ResetPasswordPopup({
    super.key,
    required this.token,
  });

  @override
  State<ResetPasswordPopup> createState() => _ResetPasswordPopupState();
}

class _ResetPasswordPopupState extends State<ResetPasswordPopup> {
  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _confirmController = TextEditingController();
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
    _passwordController.dispose();
    _confirmController.dispose();
    super.dispose();
  }

  bool _isValidPassword(String password) {
    final passwordRegex = RegExp(
      r'^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,20}$',
    );
    return passwordRegex.hasMatch(password);
  }

  Future<void> _submit() async {
    setState(() {
      error = null;
      success = null;
    });

    final password = _passwordController.text;
    final confirm = _confirmController.text;

    if (!_isValidPassword(password)) {
      setState(() {
        error =
            "Lozinka mora imati 6–20 znakova, veliko i malo slovo, broj i specijalan znak.";
      });
      return;
    }

    if (password != confirm) {
      setState(() {
        error = "Lozinke se ne poklapaju.";
      });
      return;
    }

    setState(() => isLoading = true);

    try {
      await _userProvider.resetPassword(
            token: widget.token,
            newPassword: password,
          );

      if (!mounted) return;

      setState(() {
        success = "Lozinka je uspješno promijenjena.";
      });

      // mali delay pa zatvori popup
      await Future.delayed(const Duration(seconds: 1));
      if (mounted) Navigator.of(context).pop(true);
    } catch (e) {
      setState(() {
        error = "Token nije validan ili je istekao.";
      });
    } finally {
      setState(() => isLoading = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Dialog(
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(18),
      ),
      child: Padding(
        padding: const EdgeInsets.all(20),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const Center(
              child: Text(
                "RESET LOZINKE",
                style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
              ),
            ),
            const SizedBox(height: 16),

            TextField(
              controller: _passwordController,
              obscureText: true,
              decoration: InputDecoration(
                hintText: "Nova lozinka",
                prefixIcon: const Icon(Icons.lock_outline),
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
              ),
            ),

            const SizedBox(height: 12),

            TextField(
              controller: _confirmController,
              obscureText: true,
              decoration: InputDecoration(
                hintText: "Potvrdite lozinku",
                prefixIcon: const Icon(Icons.lock_outline),
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
              ),
            ),

            if (error != null) ...[
              const SizedBox(height: 10),
              Text(error!,
                  style: const TextStyle(color: Colors.red, fontSize: 12)),
            ],

            if (success != null) ...[
              const SizedBox(height: 10),
              Text(success!,
                  style: const TextStyle(color: Colors.green, fontSize: 12)),
            ],

            const SizedBox(height: 18),

            Row(
              children: [
                TextButton(
                  onPressed: () => Navigator.of(context).pop(),
                  child: const Text("Otkaži"),
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
                  child: isLoading
                      ? const SizedBox(
                          width: 16,
                          height: 16,
                          child: CircularProgressIndicator(
                            strokeWidth: 2,
                            color: Colors.white,
                          ),
                        )
                      : const Text(
                          "Promijeni lozinku",
                          style: TextStyle(color: Colors.white),
                        ),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
