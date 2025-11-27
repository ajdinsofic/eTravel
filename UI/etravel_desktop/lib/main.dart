import 'package:etravel_desktop/models/hotel_image.dart';
import 'package:etravel_desktop/models/offer_plan_day.dart';
import 'package:etravel_desktop/providers/category_provider.dart';
import 'package:etravel_desktop/providers/hotel_image_provider.dart';
import 'package:etravel_desktop/providers/hotel_provider.dart';
import 'package:etravel_desktop/providers/hotel_room_provider.dart';
import 'package:etravel_desktop/providers/offer_hotel_provider.dart';
import 'package:etravel_desktop/providers/offer_image_provider.dart';
import 'package:etravel_desktop/providers/offer_plan_day_provider.dart';
import 'package:etravel_desktop/providers/offer_provider.dart';
import 'package:etravel_desktop/providers/room_provider.dart';
import 'package:etravel_desktop/providers/sub_category_provider.dart';
import 'package:etravel_desktop/screens/login_screen.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';   // ili login

void main() {
  runApp(MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => OfferProvider()),
        ChangeNotifierProvider(create: (_) => CategoryProvider()),
        ChangeNotifierProvider(create: (_) => SubCategoryProvider()),
        ChangeNotifierProvider(create: (_) => HotelProvider()),
        ChangeNotifierProvider(create: (_) => OfferHotelProvider()),
        ChangeNotifierProvider(create: (_) => HotelImageProvider()),
        ChangeNotifierProvider(create: (_) => OfferImageProvider()),
        ChangeNotifierProvider(create: (_) => HotelRoomProvider()),
        ChangeNotifierProvider(create: (_) => RoomProvider()),
        ChangeNotifierProvider(create: (_) => OfferPlanDayProvider())
      ],
      child: const MyApp(),
    ),);
}

class UserProvider {
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      home: LoginScreen(), 
    );
  }
}
