import 'package:etravel_desktop/models/age_group_stats_response.dart';
import 'package:etravel_desktop/models/destination_stats_response.dart';
import 'package:etravel_desktop/providers/report_provider.dart';
import 'package:fl_chart/fl_chart.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

class ReportScreen extends StatefulWidget {
  const ReportScreen({super.key});

  @override
  State<ReportScreen> createState() => _ReportScreenState();
}

class _ReportScreenState extends State<ReportScreen> {
  String selectedRange = "dan"; // dan, sedmica, mjesec

  late ReportProvider _reportProvider;

  List<DestinationStatsResponse> topDestinations = [];
  List<AgeGroupStatsResponse> ageGroups = [];
  bool loadingDestinations = true;
  bool loadingAges = true;

  @override
  void initState() {
    super.initState();
    _reportProvider = Provider.of<ReportProvider>(context, listen: false);
    _loadTopDestinations();
    _loadAgeReport();
  }

  Future<void> _loadAgeReport() async {
  setState(() => loadingAges = true);
  
  try {
    ageGroups = await _reportProvider.getAgeReport(selectedRange);
  } catch (e) {
    debugPrint("Age report error: $e");
  }

  setState(() => loadingAges = false);
}
  
  Future<void> _loadTopDestinations() async {
    try {
      topDestinations = await _reportProvider.getTopDestinations();
    } catch (e) {
      debugPrint("Top destinations error: $e");
    }
  
    setState(() => loadingDestinations = false);
  }



  @override
  Widget build(BuildContext context) {
    return Scaffold(
  backgroundColor: const Color(0xfff7f7f7),
  body: SingleChildScrollView(
    child: Column(
      children: [
        
        /// HEADER — bez paddinga, full width, scrolla se
        _header(),

        /// Ostatak sadržaja — sa paddingom
        Padding(
          padding: const EdgeInsets.all(24),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const SizedBox(height: 20),
              _ageReportCard(),
              const SizedBox(height: 30),
              _popularDestinationsCard(),
            ],
          ),
        ),

      ],
    ),
  ),
);


  }

  // ----------------------------------------------------------
  // HEADER
  Widget _header() {
  return Container(
    height: 260,
    width: double.infinity,
    alignment: Alignment.center,   // <— centriraj sve horizontalno i vertikalno
    decoration: const BoxDecoration(
      color: Color(0xFFD9D9D9),
    ),
    child: Column(
      mainAxisAlignment: MainAxisAlignment.center,  // <— centriraj u Column
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        Text(
          "Izvještaji",
          style: GoogleFonts.openSans(
            fontSize: 48,
            fontWeight: FontWeight.bold,
            color: Colors.white,
          ),
        ),
        const SizedBox(height: 6),
        Text(
          "Statistikom do uspjeha",
          textAlign: TextAlign.center,
          style: GoogleFonts.openSans(
            fontSize: 15,
            color: Colors.white,
          ),
        ),
      ],
    ),
  );
}


  // ----------------------------------------------------------
  // AGE REPORT
  Widget _ageReportCard() {
  if (loadingAges) {
    return Center(child: CircularProgressIndicator());
  }

  if (ageGroups.isEmpty) {
    return Center(child: Text("Nema podataka za odabrani period."));
  }

  // izračun širine bara (dinamično)
  double maxPercent = ageGroups.map((e) => e.percentage).reduce((a, b) => a > b ? a : b);

  return Center(
    child: Container(
      width: 700,
      child: _card(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // HEADER
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                const Text(
                  "Starosna dob korisnika",
                  style: TextStyle(
                    fontWeight: FontWeight.w700,
                    fontSize: 24,
                  ),
                ),

                DropdownButton<String>(
                  value: selectedRange,
                  onChanged: (value) {
                    setState(() => selectedRange = value!);
                    _loadAgeReport();
                  },
                  items: const [
                    DropdownMenuItem(value: "dan", child: Text("dan")),
                    DropdownMenuItem(value: "sedmica", child: Text("sedmica")),
                    DropdownMenuItem(value: "mjesec", child: Text("mjesec")),
                  ],
                ),
              ],
            ),

            const SizedBox(height: 35),

            // GRAF
            Column(
              children: ageGroups.map((item) {
                final double widthValue = (item.percentage / maxPercent) * 390;

                return Padding(
                  padding: const EdgeInsets.only(bottom: 26),
                  child: Row(
                    children: [
                      SizedBox(
                        width: 90,
                        child: Text(
                          item.ageGroup,
                          style: const TextStyle(
                            fontSize: 20,
                            fontWeight: FontWeight.w600,
                          ),
                        ),
                      ),

                      Container(
                        height: 26,
                        width: widthValue,
                        decoration: BoxDecoration(
                          color: Color(0xff67B1E5),
                          borderRadius: BorderRadius.circular(8),
                        ),
                      ),

                      const SizedBox(width: 18),

                      Text(
                        "${item.percentage}%",
                        style: const TextStyle(
                          fontSize: 22,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ],
                  ),
                );
              }).toList(),
            ),
          ],
        ),
      ),
    ),
  );
}




  // ----------------------------------------------------------
  // POPULAR DESTINATIONS (DONUT CHART)
  Widget _popularDestinationsCard() {
  if (loadingDestinations) {
    return const Center(
      child: CircularProgressIndicator(),
    );
  }

  if (topDestinations.isEmpty) {
    return const Center(
      child: Text("Nema dostupnih podataka za ovu godinu."),
    );
  }

  // COLORS
  final List<Color> legendColors = [
    Colors.teal,
    Colors.pink,
    Colors.blue,
    Colors.orange,
    Colors.deepOrange,
    Colors.yellow[700]!,
  ];

  // CHART SECTIONS
  final List<PieChartSectionData> sections = List.generate(
    topDestinations.length,
    (i) => PieChartSectionData(
      value: topDestinations[i].percentage,
      color: legendColors[i],
      radius: 70,
      showTitle: false,
    ),
  );

  return Center(
    child: Container(
      width: 900,
      child: _card(
        child: Column(
          children: [
            const SizedBox(height: 10),

            const Text(
              "Popularne destinacije u godini",
              style: TextStyle(
                fontSize: 26,
                fontWeight: FontWeight.bold,
              ),
            ),

            const SizedBox(height: 30),

            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                // ------------------ TABLE ------------------
                Expanded(
                  flex: 3,
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      // HEADER
                      Row(
                        children: const [
                          SizedBox(
                            width: 150,
                            child: Text(
                              "Destinacija",
                              style: TextStyle(
                                fontSize: 18,
                                fontWeight: FontWeight.w600,
                              ),
                            ),
                          ),
                          SizedBox(
                            width: 90,
                            child: Text(
                              "vrijednost",
                              textAlign: TextAlign.right,
                              style: TextStyle(
                                fontSize: 18,
                                fontWeight: FontWeight.w600,
                              ),
                            ),
                          ),
                          SizedBox(
                            width: 90,
                            child: Text(
                              "%",
                              textAlign: TextAlign.right,
                              style: TextStyle(
                                fontSize: 18,
                                fontWeight: FontWeight.w600,
                              ),
                            ),
                          ),
                        ],
                      ),

                      const SizedBox(height: 8),

                      Container(
                        height: 2,
                        width: 360,
                        color: Colors.black.withOpacity(0.15),
                      ),

                      const SizedBox(height: 16),

                      // ROWS
                      ...List.generate(topDestinations.length, (i) {
                        final item = topDestinations[i];

                        return Padding(
                          padding: const EdgeInsets.symmetric(vertical: 10),
                          child: Row(
                            children: [
                              SizedBox(
                                width: 150,
                                child: Row(
                                  children: [
                                    Container(
                                      width: 14,
                                      height: 14,
                                      decoration: BoxDecoration(
                                        color: legendColors[i],
                                        shape: BoxShape.circle,
                                      ),
                                    ),
                                    const SizedBox(width: 10),
                                    Expanded(
                                      child: Text(
                                        item.destinationName,
                                        style: const TextStyle(
                                          fontSize: 18,
                                          fontWeight: FontWeight.w500,
                                        ),
                                      ),
                                    ),
                                  ],
                                ),
                              ),
                              SizedBox(
                                width: 60,
                                child: Text(
                                  "${item.count}",
                                  textAlign: TextAlign.right,
                                  style: const TextStyle(
                                    fontSize: 18,
                                    fontWeight: FontWeight.w600,
                                  ),
                                ),
                              ),
                              SizedBox(
                                width: 140,
                                child: Text(
                                  "${item.percentage}%",
                                  textAlign: TextAlign.right,
                                  style: const TextStyle(
                                    fontSize: 18,
                                    fontWeight: FontWeight.w600,
                                  ),
                                ),
                              ),
                            ],
                          ),
                        );
                      }),
                    ],
                  ),
                ),

                // ------------------ DONUT GRAPH ------------------
                Expanded(
                  flex: 2,
                  child: SizedBox(
                    height: 300,
                    child: PieChart(
                      PieChartData(
                        sections: sections,
                        centerSpaceRadius: 70,
                        sectionsSpace: 2,
                      ),
                    ),
                  ),
                )
              ],
            ),

            const SizedBox(height: 20),
          ],
        ),
      ),
    ),
  );
}


  Widget _destinationTable(List tableData) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: tableData.map((item) {
        return Padding(
          padding: const EdgeInsets.symmetric(vertical: 6),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Expanded(child: Text(item["label"])),
              Text("${item['value']}"),
              const SizedBox(width: 10),
              Text(item["percent"]),
            ],
          ),
        );
      }).toList(),
    );
  }

  // ----------------------------------------------------------
  // CARD CONTAINER
  Widget _card({required Widget child}) {
    return Container(
      width: double.infinity,
      padding: const EdgeInsets.all(22),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(18),
        border: Border.all(color: Colors.grey.shade300),
      ),
      child: child,
    );
  }
}
