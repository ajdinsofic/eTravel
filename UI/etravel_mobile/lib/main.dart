import 'package:etravel_app/providers/navigationOpenProvider.dart';
import 'package:etravel_app/providers/reservationsProvider.dart';
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
    return MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => NavigationProvider()),
        ChangeNotifierProvider(create: (_) => ReservationsProvider())
      ],
      child: MaterialApp(
        debugShowCheckedModeBanner: false,
        title: 'eTravel app Demo',
        theme: ThemeData(
          colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
        ),
        home: LoadingPage(),
      ),
    );
  }
}
