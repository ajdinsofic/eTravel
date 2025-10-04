import 'package:etravel_app/screens/StartingPage.dart';
import 'package:flutter/material.dart';

class LoadingPage extends StatefulWidget {
  const LoadingPage({super.key});

  @override
  State<LoadingPage> createState() => _LoadingPageState();
}

class _LoadingPageState extends State<LoadingPage> {

  bool isLoading = true;
  
  @override
  void initState() {
    super.initState();
    Future.delayed(Duration(seconds: 8), () {
      setState(() {
        isLoading = false;
      });
      Navigator.pushReplacement(
        context,
        MaterialPageRoute(builder: (context) => StartingPage()), // Drugi ekran
      );
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        body: Stack(
          children: [
            SizedBox.expand(
              child: Image.asset(
                'assets/images/SantoriniProba.jpg',
                fit: BoxFit.cover,
              ),
            ),
            Align(
              alignment: Alignment(0, -0.42),
              child: Text(
                'eTravel',
                style: TextStyle(
                  color: Colors.white,
                  fontSize: 64,
                  fontFamily: 'LeckerliOne',
                ),
              ),
            ),
            Align(
              alignment: Alignment(0, -0.15),
              child: isLoading
              ? CircularProgressIndicator() 
              : Container(), // 
            ), //  /
            Align(
              alignment: Alignment(0, 0),
              child: Text(
                'svijet bez granica',
                style: TextStyle(
                  color: Colors.white,
                  fontSize: 30,
                  fontFamily: 'LeckerliOne',
                ),
              ),
            ),
          ],
        ),
      );
  }
}