import 'package:etravel_app/providers/navigationOpenProvider.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:etravel_app/screens/LoadinPage.dart';
import 'package:provider/provider.dart';



void main() async {
  WidgetsFlutterBinding.ensureInitialized();

  await SystemChrome.setPreferredOrientations([
    DeviceOrientation.portraitUp,
    DeviceOrientation.portraitDown,
  ]);

  // runApp(
  //   ChangeNotifierProvider(
  //     create: (_) => AppState(),
  //     builder: (context, child) {
  //       return const MyApp();
  //     },// ovdje ide tvoja root aplikacija  PROVIDERI I STATE MANAGEMENT LEKCIJA DEBELA
  //   ),
  // );

  runApp(const MyApp());
}

class MyApp extends StatefulWidget {
  const MyApp({super.key});

  @override
  State<MyApp> createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  bool isLoading = true;

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'eTravel app Demo',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
      ),
      home: LoadingPage(),
    );
  }
}
