import 'dart:async';

import 'package:app_links/app_links.dart';
import 'package:etravel_app/providers/auth_provider.dart';
import 'package:etravel_app/providers/category_provider.dart';
import 'package:etravel_app/providers/comment_provider.dart';
import 'package:etravel_app/providers/hotel_provider.dart';
import 'package:etravel_app/providers/hotel_room_provider.dart';
import 'package:etravel_app/providers/offer_hotel_provider.dart';
import 'package:etravel_app/providers/offer_image_provider.dart';
import 'package:etravel_app/providers/offer_plan_day_provider.dart';
import 'package:etravel_app/providers/offer_provider.dart';
import 'package:etravel_app/providers/payment_provider.dart';
import 'package:etravel_app/providers/paypal_provider.dart';
import 'package:etravel_app/providers/reservation_preview_provider.dart';
import 'package:etravel_app/providers/reservation_provider.dart';
import 'package:etravel_app/providers/room_provider.dart';
import 'package:etravel_app/providers/sub_category_provider.dart';
import 'package:etravel_app/providers/user_provider.dart';
import 'package:etravel_app/providers/user_token_provider.dart';
import 'package:etravel_app/providers/user_voucher_provider.dart';
import 'package:etravel_app/providers/voucher_provider.dart';
import 'package:etravel_app/providers/work_application_provider.dart';
import 'package:etravel_app/widgets/loginPage/ResetPasswordPopup.dart';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:provider/provider.dart';

import 'package:etravel_app/screens/LoadinPage.dart';

/// 🔑 GLOBALNI NAVIGATOR KEY (za deep link popup)
final GlobalKey<NavigatorState> navigatorKey =
    GlobalKey<NavigatorState>();

StreamSubscription? _deepLinkSub;

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
  @override
  void initState() {
    super.initState();
    _initDeepLinks();
  }

  final AppLinks _appLinks = AppLinks();

void _initDeepLinks() {
  _appLinks.uriLinkStream.listen((Uri? uri) {
    if (uri == null) return;

    if (uri.scheme == "etravel" &&
        uri.host == "reset-password") {
      final token = uri.queryParameters["token"];
      if (token != null) {
        _openResetPasswordPopup(token);
      }
    }
  });
}


  void _openResetPasswordPopup(String token) {
    final context = navigatorKey.currentContext;
    if (context == null) return;

    showDialog(
      context: context,
      barrierDismissible: false,
      builder: (_) => ResetPasswordPopup(token: token),
    );
  }

  @override
  void dispose() {
    _deepLinkSub?.cancel();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => OfferProvider()),
        ChangeNotifierProvider(create: (_) => OfferHotelProvider()),
        ChangeNotifierProvider(create: (_) => HotelRoomProvider()),
        ChangeNotifierProvider(create: (_) => RoomProvider()),
        ChangeNotifierProvider(create: (_) => HotelProvider()),
        ChangeNotifierProvider(create: (_) => CategoryProvider()),
        ChangeNotifierProvider(create: (_) => SubCategoryProvider()),
        ChangeNotifierProvider(create: (_) => OfferPlanDayProvider()),
        ChangeNotifierProvider(create: (_) => ReservationPreviewProvider()),
        ChangeNotifierProvider(create: (_) => UserProvider()),
        ChangeNotifierProvider(create: (_) => UserTokenProvider()),
        ChangeNotifierProvider(create: (_) => ReservationProvider()),
        ChangeNotifierProvider(create: (_) => OfferImageProvider()),
        ChangeNotifierProvider(create: (_) => CommentProvider()),
        ChangeNotifierProvider(create: (_) => PaymentProvider()),
        ChangeNotifierProvider(create: (_) => PayPalProvider()),
        ChangeNotifierProvider(create: (_) => UserVoucherProvider()),
        ChangeNotifierProvider(create: (_) => VoucherProvider()),
        ChangeNotifierProvider(create: (_) => WorkApplicationProvider()),
      ],
      child: MaterialApp(
        navigatorKey: navigatorKey, // 🔑 BITNO
        debugShowCheckedModeBanner: false,
        title: 'eTravel app Demo',

        localizationsDelegates: const [
          GlobalMaterialLocalizations.delegate,
          GlobalWidgetsLocalizations.delegate,
          GlobalCupertinoLocalizations.delegate,
        ],

        supportedLocales: const [
          Locale('bs'),
          Locale('en'),
        ],

        theme: ThemeData(
          colorScheme: ColorScheme.fromSeed(
            seedColor: Colors.deepPurple,
          ),
        ),

        home: LoadingPage(),
      ),
    );
  }
}
