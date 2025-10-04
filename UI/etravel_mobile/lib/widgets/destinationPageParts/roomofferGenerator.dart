import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';

class Roomoffergenerator extends StatefulWidget {
  final String cijena;
  final String slikaPath;

  const Roomoffergenerator({
    super.key,
    required this.cijena,
    required this.slikaPath,
  });

  @override
  State<Roomoffergenerator> createState() => _RoomoffergeneratorState();
}

class _RoomoffergeneratorState extends State<Roomoffergenerator> {
  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);
    return Container(
      width: screenWidth * 0.9,
      margin: EdgeInsets.all(screenWidth * 0.025),
      decoration: BoxDecoration(
        color: Color(0xFFF5F5F5),
        borderRadius: BorderRadius.circular(15),
        boxShadow: [BoxShadow(color: Colors.grey.shade300, blurRadius: 6)],
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Container(
            decoration: BoxDecoration(
              border: Border.all(color: Color(0xFF67B1E5), width: 1),
              borderRadius: const BorderRadius.vertical(
                top: Radius.circular(15),
              ),
            ),
            child: ClipRRect(
              borderRadius: const BorderRadius.vertical(
                top: Radius.circular(15),
              ),
              child: Image.asset(
                widget.slikaPath, // ili NetworkImage
                height: screenHeight * 0.25,
                width: double.infinity,
                fit: BoxFit.cover,
              ),
            ),
          ),
          Padding(
            padding: EdgeInsets.all(screenWidth * 0.04),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  "Dvokrevetna soba",
                  style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.start,
                  children: [
                    const Text(
                      "Hotel Ragazzi Calcone",
                      style: TextStyle(fontWeight: FontWeight.bold),
                    ),
                    SizedBox(width: 10),
                    Row(
                      children: List.generate(
                        4,
                        (index) => const Icon(
                          Icons.star,
                          color: Color(0xFFDAB400),
                          size: 15,
                        ),
                      ),
                    ),
                  ],
                ),
                const SizedBox(height: 6),
                Row(
                  children: const [
                    Icon(
                      Icons.meeting_room_outlined,
                      size: 16,
                      color: Colors.blue,
                    ),
                    SizedBox(width: 5),
                    Text("Tip sobe: dvokrevetna"),
                  ],
                ),
                const SizedBox(height: 10),
                ElevatedButton(
                  onPressed: () {},
                  style: ElevatedButton.styleFrom(
                    backgroundColor: const Color(0xFFF5F5F5),
                    foregroundColor: Colors.black,
                    elevation: 0,
                    padding: const EdgeInsets.symmetric(horizontal: 24),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(20),
                      side: const BorderSide(
                        color: Color(0xFF67B1E5), // dodani border
                        width: 1.5, // možeš prilagoditi debljinu po želji
                      ),
                    ),
                  ),
                  child: const Text(
                    "Detalji",
                    style: TextStyle(color: Color(0xFF67B1E5)),
                  ),
                ),
                const SizedBox(height: 12),
                const Text(
                  "Termin putovanja",
                  style: TextStyle(fontWeight: FontWeight.bold),
                ),
                const Text(
                  "28.5.2025 - 2.6.2025 | 5 dana",
                  style: TextStyle(color: Color(0xFF67B1E5)),
                ),
                const SizedBox(height: 10),
                TextField(
                  textAlign: TextAlign.center,
                  decoration: InputDecoration(
                    hintStyle: const TextStyle(color: Color(0xFFC7C7C7)),
                    hintText: "unesite broj osoba za ovu sobu",
                    contentPadding: const EdgeInsets.symmetric(
                      horizontal: 10,
                      vertical: 8,
                    ),
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(10),
                    ),
                    enabledBorder: OutlineInputBorder(
                      borderSide: const BorderSide(
                        color: Color(0xFF67B1E5),
                        width: 1.5,
                      ),
                      borderRadius: BorderRadius.circular(10),
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: const BorderSide(
                        color: Color(0xFF67B1E5),
                        width: 2,
                      ),
                      borderRadius: BorderRadius.circular(10),
                    ),
                  ),
                ),

                const SizedBox(height: 12),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    ElevatedButton(
                      onPressed: () {},
                      style: ElevatedButton.styleFrom(
                        backgroundColor: Color(0xFF67B1E5),
                        foregroundColor: Colors.white,
                        padding: const EdgeInsets.symmetric(horizontal: 20),
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(20),
                        ),
                      ),
                      child: const Text("rezerviši"),
                    ),
                    Text(
                      "cijena: ${widget.cijena}",
                      style: const TextStyle(fontWeight: FontWeight.bold),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
