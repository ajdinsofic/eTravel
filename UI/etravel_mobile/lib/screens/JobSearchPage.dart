import 'dart:io';

import 'package:etravel_app/config/api_config.dart';
import 'package:etravel_app/models/user.dart';
import 'package:etravel_app/models/work_application_insert.dart';
import 'package:etravel_app/providers/user_provider.dart';
import 'package:etravel_app/providers/work_application_provider.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:etravel_app/widgets/headerIFooterAplikacije/eTravelFooter.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

class JobSearchPage extends StatefulWidget {
  const JobSearchPage({super.key});

  @override
  State<JobSearchPage> createState() => _JobSearchPageState();
}

class _JobSearchPageState extends State<JobSearchPage> {
  late UserProvider _userProvider;
  late WorkApplicationProvider _workApplicationProvider;

  User? _user;
  bool _isUserLoading = true;

  File? _cvFile;
  bool _isSubmitting = false;
  String? _cvError;
  String? _letterError;

  final TextEditingController _letterController = TextEditingController();

  @override
  void initState() {
    super.initState();
    _userProvider = Provider.of<UserProvider>(context, listen: false);
    _workApplicationProvider = Provider.of<WorkApplicationProvider>(context, listen: false);
    _loadUser();
  }

  @override
  void dispose() {
    _letterController.dispose();
    super.dispose();
  }

  Future<void> _loadUser() async {
    try {
      final result = await _userProvider.getById(Session.userId!);
      if (!mounted) return;
      setState(() {
        _user = result;
        _isUserLoading = false;
      });
    } catch (_) {
      if (!mounted) return;
      setState(() => _isUserLoading = false);
    }
  }

  // ================= PICK CV =================
  Future<void> _pickCvDocument() async {
    final result = await FilePicker.platform.pickFiles(
      type: FileType.custom,
      allowedExtensions: ['pdf', 'doc', 'docx'],
    );

    if (result != null && result.files.single.path != null) {
      setState(() {
        _cvFile = File(result.files.single.path!);
        _cvError = null; // čim se izabere dokument, skidamo error
      });
    }
  }

  // ================= SUBMIT =================
  Future<void> _submitApplication() async {
    bool isValid = true;

    setState(() {
      _cvError = null;
      _letterError = null;

      if (_cvFile == null) {
        _cvError = "Molimo priložite CV dokument.";
        isValid = false;
      }

      if (_letterController.text.trim().isEmpty) {
        _letterError = "Motivaciono pismo je obavezno.";
        isValid = false;
      }
    });

    if (!isValid) return;

    setState(() => _isSubmitting = true);

    try {
      final request = WorkApplicationInsert(
        userId: Session.userId!,
        letter: _letterController.text.trim(),
        cvFile: _cvFile!,
      );

      await _workApplicationProvider.insertApplication(request);

      if (!mounted) return;
      _showSuccessDialog();

      setState(() {
        _cvFile = null;
        _letterController.clear();
        _cvError = null;
        _letterError = null;
      });
    } catch (e) {
      if (!mounted) return;
      _showMessage("Došlo je do greške prilikom slanja prijave.${e}");
    } finally {
      if (mounted) {
        setState(() => _isSubmitting = false);
      }
    }
  }

  Widget _buildCvLoadingOverlay() {
  return Container(
    color: Colors.black.withOpacity(0.4),
    width: double.infinity,
    height: double.infinity,
    child: Center(
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          const SizedBox(
            width: 80,
            height: 80,
            child: CircularProgressIndicator(
              strokeWidth: 6,
              color: Color(0xFF67B1E5),
            ),
          ),
          const SizedBox(height: 20),
          Text(
            "Slanje prijave...",
            style: GoogleFonts.openSans(
              color: Colors.white,
              fontSize: 16,
              fontWeight: FontWeight.bold,
            ),
          ),
        ],
      ),
    ),
  );
}


  void _showMessage(String text) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(content: Text(text), backgroundColor: Colors.black),
    );
  }

  void _showSuccessDialog() {
    showDialog(
      context: context,
      barrierDismissible: false,
      builder:
          (_) => AlertDialog(
            shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
            title: const Icon(
              Icons.check_circle_outline,
              color: Color(0xFF67B1E5),
              size: 48,
            ),
            content: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Text(
                  "Prijava uspješno poslana",
                  textAlign: TextAlign.center,
                  style: GoogleFonts.openSans(
                    fontWeight: FontWeight.bold,
                    fontSize: 16,
                  ),
                ),
                const SizedBox(height: 10),
                Text(
                  "Naši uposlenici će Vašu prijavu pregledati u najkraćem mogućem roku.",
                  textAlign: TextAlign.center,
                  style: GoogleFonts.openSans(fontSize: 14),
                ),
              ],
            ),
            actionsAlignment: MainAxisAlignment.center,
            actions: [
              ElevatedButton(
                style: ElevatedButton.styleFrom(
                  backgroundColor: const Color(0xFF67B1E5),
                  shape: const StadiumBorder(),
                ),
                onPressed: () {
                  Navigator.of(context).pop();
                },
                child: const Text("U redu", style: TextStyle(color: Colors.white)),
              ),
            ],
          ),
    );
  }

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    return Stack(
      children: [
        Scaffold(
          backgroundColor: Colors.white,
          body: GestureDetector(
            behavior: HitTestBehavior.translucent,
            onTap: () => FocusScope.of(context).unfocus(),
            child: CustomScrollView(
              slivers: [
                SljedecaDestinacijaIMenuBar(daLijeKliknuo: false),

                SliverToBoxAdapter(
                  child: Column(
                    children: [
                      _buildSectionHeader("Tražite posao?"),

                      SizedBox(height: screenHeight * 0.03),

                      // ====== SEKCIJA: TRENUTNO TRAŽIMO ======
                      Padding(
                        padding: EdgeInsets.symmetric(horizontal: screenWidth * 0.06),
                        child: Align(
                          alignment: Alignment.centerLeft,
                          child: Text(
                            "Trenutno tražimo",
                            style: GoogleFonts.openSans(
                              fontWeight: FontWeight.bold,
                              fontSize: screenWidth * 0.045,
                            ),
                          ),
                        ),
                      ),

                      SizedBox(height: screenHeight * 0.02),

                      Padding(
                        padding: EdgeInsets.symmetric(horizontal: screenWidth * 0.05),
                        child: Column(
                          children: [
                            _buildJobCard(
                              title: "Turističke vodiče",
                              description:
                                  "Tražimo komunikativne i odgovorne turističke vodiče koji vole rad s ljudima. "
                                  "Posao uključuje vođenje grupa, predstavljanje destinacija, brigu o putnicima "
                                  "i stvaranje nezaboravnog iskustva tokom putovanja.",
                              imagePath: "assets/images/santoriniPonuda.jpg",
                              imageOnRight: false,
                            ),

                            SizedBox(height: screenHeight * 0.02),

                            _buildJobCard(
                              title: "Operatere",
                              description:
                                  "Tražimo operatere koji će biti prva kontakt tačka sa našim klijentima. "
                                  "Zaduženja uključuju komunikaciju s putnicima, davanje informacija o ponudama, "
                                  "rezervacijama i podršku prije, tokom i nakon putovanja.",
                              imagePath: "assets/images/coverSantorini.jpg",
                              imageOnRight: true,
                            ),
                          ],
                        ),
                      ),

                      SizedBox(height: screenHeight * 0.03),

                      _buildInfoBar(
                        "Ako mislite da ste se pronašli u nekim od poslova "
                        "pošaljite nam vaše podatke i CV, te javit ćemo Vam se "
                        "putem maila",
                      ),

                      SizedBox(height: screenHeight * 0.03),

                      // ====== FORMA: PRIJAVA ======
                      _buildApplicationCard(),

                      SizedBox(height: screenHeight * 0.05),

                      const eTravelFooter(),
                    ],
                  ),
                ),
              ],
            ),
          ),
        ),

        // ===== LOADING OVERLAY =====
        if (_isSubmitting) _buildCvLoadingOverlay(),
      ],
    );
  }

  // ================= HEADER =================
  Widget _buildSectionHeader(String title) {
    return Container(
      width: double.infinity,
      padding: EdgeInsets.symmetric(vertical: screenHeight * 0.02),
      color: const Color(0xFF67B1E5),
      child: Center(
        child: Text(
          title,
          style: GoogleFonts.openSans(
            color: Colors.white,
            fontWeight: FontWeight.bold,
            fontSize: screenWidth * 0.05,
          ),
        ),
      ),
    );
  }

  // ================= INFO BAR =================
  Widget _buildInfoBar(String text) {
    return Container(
      width: double.infinity,
      padding: EdgeInsets.symmetric(
        vertical: screenHeight * 0.025,
        horizontal: screenWidth * 0.08,
      ),
      color: const Color(0xFF67B1E5),
      child: Text(
        text,
        textAlign: TextAlign.center,
        style: GoogleFonts.openSans(
          color: Colors.white,
          fontWeight: FontWeight.bold,
          fontSize: screenWidth * 0.04,
          height: 1.3,
        ),
      ),
    );
  }

  // ================= JOB CARD =================
 Widget _buildJobCard({
  required String title,
  required String description,
  required String imagePath,
  required bool imageOnRight,
}) {
  final image = ClipRRect(
    borderRadius: BorderRadius.circular(12),
    child: Image.asset(
      imagePath,
      width: screenWidth * 0.3,
      height: screenWidth * 0.3,
      fit: BoxFit.cover,
    ),
  );

  final text = Expanded(
    child: Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        Text(
          title,
          textAlign: TextAlign.center,
          style: GoogleFonts.openSans(
            fontWeight: FontWeight.bold,
            fontSize: screenWidth * 0.04,
          ),
        ),
        SizedBox(height: screenHeight * 0.005),
        Text(
          description,
          textAlign: TextAlign.center,
          style: GoogleFonts.openSans(
            color: const Color(0xFF67B1E5),
            fontSize: screenWidth * 0.03,
            height: 1.3,
          ),
        ),
      ],
    ),
  );

  return Container(
    padding: EdgeInsets.all(screenWidth * 0.04),
    decoration: BoxDecoration(
      color: const Color(0xFFF5F5F5),
      border: Border.all(color: const Color(0xFF67B1E5)),
      borderRadius: BorderRadius.circular(16),
    ),
    child: Row(
      children: imageOnRight
          ? [text, SizedBox(width: screenWidth * 0.04), image]
          : [image, SizedBox(width: screenWidth * 0.04), text],
    ),
  );
}


  // ================= APPLICATION CARD =================
  Widget _buildApplicationCard() {
    return SizedBox(
      width: screenWidth * 0.9,
      child: Card(
        color: const Color(0xFFF5F5F5),
        elevation: 2,
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
        child: Padding(
          padding: const EdgeInsets.all(14),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Center(
                child: Text(
                  "PRIJAVA ZA POSAO",
                  style: GoogleFonts.openSans(
                    fontWeight: FontWeight.bold,
                    fontSize: 14,
                  ),
                ),
              ),

              const SizedBox(height: 14),

              Text(
                "Prijavljeni profil",
                style: GoogleFonts.openSans(fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 10),

              Container(
                padding: const EdgeInsets.all(14),
                decoration: BoxDecoration(
                  color: const Color(0xFFD9D9D9),
                  borderRadius: BorderRadius.circular(40),
                  border: Border.all(color: const Color(0xFF67B1E5)),
                ),
                child: Row(
                  children: [
                    CircleAvatar(
                      radius: 26,
                      backgroundImage:
                          _isUserLoading
                              ? const AssetImage("assets/images/person1.jpg")
                              : (_user?.imageUrl != null &&
                                      (_user!.imageUrl!.isNotEmpty))
                                  ? NetworkImage(
                                        "${ApiConfig.imagesUsers}/${_user!.imageUrl}",
                                      )
                                      as ImageProvider
                                  : const AssetImage("assets/images/person1.jpg"),
                    ),
                    const SizedBox(width: 12),
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: [
                          Text(
                            _isUserLoading
                                ? "Učitavanje..."
                                : "${_user?.firstName ?? ""} ${_user?.lastName ?? ""}",
                            textAlign: TextAlign.center,
                            style: GoogleFonts.openSans(
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                          const SizedBox(height: 4),
                          Text(
                            "svi neophodni podaci biće preuzeti s profila",
                            textAlign: TextAlign.center,
                            style: GoogleFonts.openSans(
                              fontWeight: FontWeight.bold,
                              fontSize: 12,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
              ),

              const SizedBox(height: 20),

              Text(
                "Dodajte vaš CV",
                style: GoogleFonts.openSans(fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 8),

              GestureDetector(
                onTap: _pickCvDocument,
                child: Container(
                  padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
                  decoration: BoxDecoration(
                    color: const Color(0xFF67B1E5),
                    borderRadius: BorderRadius.circular(12),
                  ),
                  child: Row(
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      const Icon(Icons.upload_file, color: Colors.white),
                      const SizedBox(width: 8),
                      Text(
                        _cvFile != null ? "dokument priložen" : "postavite dokument",
                        style: GoogleFonts.openSans(
                          color: Colors.white,
                          fontWeight: FontWeight.bold,
                          fontSize: 12,
                        ),
                      ),
                    ],
                  ),
                ),
              ),

              if (_cvError != null)
                Padding(
                  padding: const EdgeInsets.only(top: 6, left: 6),
                  child: Text(
                    _cvError!,
                    style: const TextStyle(
                      color: Colors.red,
                      fontSize: 12,
                      fontWeight: FontWeight.w600,
                    ),
                  ),
                ),

              const SizedBox(height: 20),

              Text(
                "Zašto želite da radite kod nas?",
                style: GoogleFonts.openSans(fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 8),

              TextField(
                controller: _letterController,
                maxLines: 4,
                onChanged: (_) {
                  if (_letterError != null) {
                    setState(() => _letterError = null);
                  }
                },
                decoration: InputDecoration(
                  hintText: "unesite text ovdje",
                  filled: true,
                  fillColor: Colors.white,
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(12),
                    borderSide: BorderSide(
                      color: _letterError != null ? Colors.red : Colors.transparent,
                    ),
                  ),
                  enabledBorder: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(12),
                    borderSide: BorderSide(
                      color: _letterError != null ? Colors.red : Colors.transparent,
                    ),
                  ),
                ),
              ),

              if (_letterError != null)
                Padding(
                  padding: const EdgeInsets.only(top: 6, left: 6),
                  child: Text(
                    _letterError!,
                    style: const TextStyle(
                      color: Colors.red,
                      fontSize: 12,
                      fontWeight: FontWeight.w600,
                    ),
                  ),
                ),

              const SizedBox(height: 20),

              SizedBox(
                width: double.infinity,
                child: ElevatedButton(
                  onPressed: _isSubmitting ? null : _submitApplication,
                  style: ElevatedButton.styleFrom(
                    backgroundColor: const Color(0xFF67B1E5),
                    shape: const StadiumBorder(),
                  ),
                  child: Text(
                    _isSubmitting ? "SLANJE..." : "POŠALJITE PRIJAVU",
                    style: GoogleFonts.openSans(
                      color: Colors.white,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
