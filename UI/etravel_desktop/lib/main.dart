import 'package:flutter/material.dart';
import 'screens/login_screen.dart';
import 'screens/offer_screen.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,

      // početni ekran
      initialRoute: "/login",

      routes: {
        "/login": (context) => const LoginScreen(),
        "/offer": (context) => const OfferScreen(),   
      },
    );
  }
}
