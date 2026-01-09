import 'dart:async';
import 'dart:io';
import 'package:etravel_desktop/models/paging_info.dart';
import 'package:etravel_desktop/models/user.dart';
import 'package:etravel_desktop/models/work_application.dart';
import 'package:etravel_desktop/providers/user_provider.dart';
import 'package:etravel_desktop/providers/work_application_provider.dart';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:open_filex/open_filex.dart';
import 'package:provider/provider.dart';

class WorkApplicationsScreen extends StatefulWidget {
  const WorkApplicationsScreen({super.key});

  @override
  State<WorkApplicationsScreen> createState() => _WorkApplicationsScreenState();
}

class _WorkApplicationsScreenState extends State<WorkApplicationsScreen> {
  late WorkApplicationProvider _workApplicationProvider;
  late UserProvider _userProvider;

  Timer? _debounce;
  final TextEditingController _search = TextEditingController();

  bool isLoading = true;

  List<WorkApplication> applications = [];
  Map<int, User> usersCache = {}; // cache user-a

  int page = 0;
  int pageSize = 5;
  PagingInfo? paging;

  @override
  void initState() {
    super.initState();
    _workApplicationProvider = Provider.of<WorkApplicationProvider>(
      context,
      listen: false,
    );
    _userProvider = Provider.of<UserProvider>(context, listen: false);

    _search.addListener(_onSearchChanged);
    _loadTotal();
  }

  @override
  void dispose() {
    _search.dispose();
    _debounce?.cancel();
    super.dispose();
  }

  // ----------------------- SEARCH DEBOUNCE -----------------------
  void _onSearchChanged() {
    if (_debounce?.isActive ?? false) _debounce!.cancel();
    _debounce = Timer(const Duration(milliseconds: 400), () {
      page = 0;
      _loadTotal();
    });
  }

  // ----------------------- LOAD TOTAL -----------------------
  Future<void> _loadTotal() async {
    setState(() => isLoading = true);

    final result = await _workApplicationProvider.get(
      filter: {
        "IncludeTotalCount": true,
        "retrieveAll": true,
        "personName": _search.text.trim(),
      },
    );

    paging = PagingInfo(
      totalCount: result.count,
      pageSize: pageSize,
      currentPage: 0,
    );

    page = 0;
    await _loadPage();
  }

  void _showSuccessToast(String message) {
    final overlay = Overlay.of(context);
    if (overlay == null) return;

    late OverlayEntry entry;

    entry = OverlayEntry(
      builder:
          (context) => Positioned(
            bottom: 25,
            right: 25,
            child: Material(
              color: Colors.transparent,
              child: Container(
                padding: const EdgeInsets.symmetric(
                  horizontal: 18,
                  vertical: 12,
                ),
                decoration: BoxDecoration(
                  color: Colors.green,
                  borderRadius: BorderRadius.circular(12),
                ),
                child: Text(
                  message,
                  style: const TextStyle(
                    color: Colors.white,
                    fontSize: 16,
                    fontWeight: FontWeight.w600,
                  ),
                ),
              ),
            ),
          ),
    );

    overlay.insert(entry);
    Future.delayed(const Duration(seconds: 3), () => entry.remove());
  }

  void _showErrorToast(String message) {
    final overlay = Overlay.of(context);
    if (overlay == null) return;

    late OverlayEntry entry;

    entry = OverlayEntry(
      builder:
          (context) => Positioned(
            bottom: 25,
            right: 25,
            child: Material(
              color: Colors.transparent,
              child: Container(
                padding: const EdgeInsets.symmetric(
                  horizontal: 18,
                  vertical: 12,
                ),
                decoration: BoxDecoration(
                  color: Colors.redAccent,
                  borderRadius: BorderRadius.circular(12),
                ),
                child: Text(
                  message,
                  style: const TextStyle(
                    color: Colors.white,
                    fontSize: 16,
                    fontWeight: FontWeight.w600,
                  ),
                ),
              ),
            ),
          ),
    );

    overlay.insert(entry);
    Future.delayed(const Duration(seconds: 3), () => entry.remove());
  }

  // ----------------------- LOAD PAGE -----------------------
  Future<void> _loadPage() async {
    final result = await _workApplicationProvider.get(
      filter: {
        "page": page,
        "pageSize": pageSize,
        "personName": _search.text.trim(),
      },
    );

    applications = result.items;

    // Cache usera za brzo prikazivanje
    for (var app in applications) {
      if (!usersCache.containsKey(app.userId)) {
        final user = await _userProvider.getById(app.userId);
        usersCache[app.userId] = user;
      }
    }

    paging!.currentPage = page;

    setState(() => isLoading = false);
  }

  // Formatiranje datuma
  String formatDate(DateTime dt) {
    return "${dt.day.toString().padLeft(2, '0')}.${dt.month.toString().padLeft(2, '0')}.${dt.year}";
  }

  // ------------------------------ UI ------------------------------
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color(0xffF5F5F5),
      body: SingleChildScrollView(
        child: Column(
          children: [
            _header(),
            Padding(
              padding: const EdgeInsets.symmetric(vertical: 40),
              child: Center(
                child: SizedBox(
                  width: 900,
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [_searchBar()],
                      ),
                      const SizedBox(height: 45),
                      _tableHeader(),
                      const SizedBox(height: 25),

                      if (isLoading)
                        const Center(child: CircularProgressIndicator())
                      else if (applications.isEmpty)
                        Center(
                          child: Text(
                            "Nema prijava.",
                            style: GoogleFonts.openSans(
                              fontSize: 18,
                              fontWeight: FontWeight.w600,
                            ),
                          ),
                        )
                      else
                        Column(
                          children: [
                            ...applications.map((a) => _applicationItem(a)),
                            const SizedBox(height: 20),
                            _pagingWidget(),
                          ],
                        ),
                    ],
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }

  // ----------------------- HEADER -----------------------
  Widget _header() {
    return Container(
      height: 220,
      width: double.infinity,
      alignment: Alignment.center,
      color: const Color(0xFFD9D9D9),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Text(
            "Aplikanti",
            style: GoogleFonts.openSans(
              fontSize: 40,
              fontWeight: FontWeight.bold,
              color: Colors.white,
            ),
          ),
          const SizedBox(height: 8),
          Text(
            "spremni smo za nove radne pobjede",
            style: GoogleFonts.openSans(fontSize: 16, color: Colors.white),
          ),
        ],
      ),
    );
  }

  // ----------------------- SEARCH BAR -----------------------
  Widget _searchBar() {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Padding(
          padding: const EdgeInsets.only(left: 12),
          child: Text(
            "Koga tražite?",
            style: GoogleFonts.openSans(
              fontSize: 20,
              fontWeight: FontWeight.w700,
            ),
          ),
        ),
        const SizedBox(height: 14),

        Container(
          width: 350,
          height: 42,
          decoration: BoxDecoration(
            color: const Color(0xffF3ECF8),
            borderRadius: BorderRadius.circular(30),
          ),
          child: Row(
            children: [
              const SizedBox(width: 18),
              Expanded(
                child: TextField(
                  controller: _search,
                  decoration: InputDecoration(
                    hintText: "pretražite aplikante",
                    hintStyle: GoogleFonts.openSans(
                      color: Colors.grey.shade600,
                    ),
                    border: InputBorder.none,
                  ),
                ),
              ),
              Padding(
                padding: const EdgeInsets.only(right: 16),
                child: Icon(
                  Icons.search,
                  color: Colors.grey.shade600,
                  size: 20,
                ),
              ),
            ],
          ),
        ),
      ],
    );
  }

  // ----------------------- TABLE HEADER -----------------------
  Widget _tableHeader() {
    return Row(
      children: [
        const SizedBox(width: 30),
        SizedBox(
          width: 300,
          child: Text(
            "Ime aplikanta",
            style: GoogleFonts.openSans(
              fontSize: 20,
              fontWeight: FontWeight.w700,
            ),
          ),
        ),
        SizedBox(
          width: 200,
          child: Text(
            "Datum prijave",
            style: GoogleFonts.openSans(
              fontSize: 20,
              fontWeight: FontWeight.w700,
            ),
          ),
        ),
        const Spacer(),
        SizedBox(
          width: 180,
          child: Text(
            "Akcija",
            textAlign: TextAlign.center,
            style: GoogleFonts.openSans(
              fontSize: 20,
              fontWeight: FontWeight.w700,
            ),
          ),
        ),
        const SizedBox(width: 30),
      ],
    );
  }

  // ----------------------- APPLICATION ITEM -----------------------
  Widget _applicationItem(WorkApplication app) {
    final user = usersCache[app.userId];

    final fullName =
        user == null ? "Učitavanje..." : "${user.firstName} ${user.lastName}";

    return Padding(
      padding: const EdgeInsets.only(bottom: 25),
      child: Container(
        height: 90,
        decoration: BoxDecoration(
          color: const Color(0xffD9D9D9),
          borderRadius: BorderRadius.circular(45),
        ),
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 35),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              // IME
              SizedBox(
                width: 300,
                child: Text(
                  fullName,
                  style: GoogleFonts.openSans(
                    fontSize: 22,
                    fontWeight: FontWeight.bold,
                  ),
                ),
              ),

              // DATUM
              SizedBox(
                width: 200,
                child: Text(
                  formatDate(app.appliedAt),
                  style: GoogleFonts.openSans(
                    fontSize: 18,
                    fontWeight: FontWeight.w600,
                  ),
                ),
              ),

              // ACTION BUTTON
              ElevatedButton(
                onPressed: () async {
                  await showDialog(
                    context: context,
                    barrierDismissible: true,
                    builder: (_) => _applicationDetailsPopup(app, user),
                  );
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor: const Color(0xff67B1E5),
                  padding: const EdgeInsets.symmetric(
                    horizontal: 22,
                    vertical: 12,
                  ),
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(30),
                  ),
                ),
                child: Text(
                  "pogledaj aplikaciju",
                  style: GoogleFonts.openSans(
                    color: Colors.white,
                    fontSize: 16,
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  // ----------------------- POPUP ZA DETALJE -----------------------
  Widget _applicationDetailsPopup(WorkApplication app, User? user) {
    final cvName =
        app.cvFileName.isNotEmpty ? app.cvFileName : "CV nije dostavljen";

    return Dialog(
      backgroundColor: Color(0xffD9D9D9),
      insetPadding: const EdgeInsets.symmetric(horizontal: 60, vertical: 40),
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(20)),
      child: Stack(
        children: [
          // ---------------- INNER BACKGROUND ----------------
          Container(
            margin: const EdgeInsets.all(30),
            padding: const EdgeInsets.all(35),
            decoration: BoxDecoration(
              color: const Color(0xffF5F5F5),
              borderRadius: BorderRadius.circular(20),
            ),

            child: SingleChildScrollView(
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  // TITLE
                  Text(
                    "Prijava za posao",
                    style: GoogleFonts.openSans(
                      fontSize: 26,
                      fontWeight: FontWeight.w700,
                    ),
                  ),
                  const SizedBox(height: 30),

                  Row(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      // ---------------- LEFT SIDE ----------------
                      Expanded(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            _label("Ime"),
                            _readonlyBox(user?.firstName ?? "—"),

                            const SizedBox(height: 16),
                            _label("Prezime"),
                            _readonlyBox(user?.lastName ?? "—"),

                            const SizedBox(height: 16),
                            _label("Email"),
                            _readonlyBox(user?.email ?? "—"),

                            const SizedBox(height: 16),
                            _label("Broj telefona"),
                            _readonlyBox(user?.phoneNumber ?? "—"),

                            const SizedBox(height: 16),
                            _label("Datum rođenja"),
                            _readonlyBox(
                              user?.dateBirth != null
                                  ? formatDate(user!.dateBirth!)
                                  : "—",
                            ),
                          ],
                        ),
                      ),

                      const SizedBox(width: 40),

                      // ---------------- RIGHT SIDE ----------------
                      Expanded(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            _label("CV"),

                            Container(
                              height: 48,
                              decoration: BoxDecoration(
                                color: Colors.white,
                                border: Border.all(color: Colors.grey.shade400),
                                borderRadius: BorderRadius.circular(8),
                              ),
                              padding: const EdgeInsets.symmetric(
                                horizontal: 14,
                              ),
                              child: Row(
                                mainAxisAlignment:
                                    MainAxisAlignment.spaceBetween,
                                children: [
                                  Row(
                                    children: [
                                      // 🔵 ORIGINALNA IKONICA (plava)
                                      const Icon(
                                        Icons.insert_drive_file,
                                        color: Color(0xff67B1E5),
                                        size: 22,
                                      ),

                                      const SizedBox(width: 10),

                                      Text(
                                        cvName,
                                        style: GoogleFonts.openSans(
                                          fontSize: 15,
                                        ),
                                      ),
                                    ],
                                  ),

                                  ElevatedButton(
                                    onPressed: () async {
                                      final bytes =
                                          await _workApplicationProvider
                                              .downloadCv(app.id);

                                      if (bytes == null) {
                                        _showErrorToast(
                                          "Greška pri preuzimanju fajla",
                                        );
                                        return;
                                      }

                                      // Originalni naziv fajla sa ekstenzijom (npr: test.pdf, radna_biografija.docx)
                                      final originalName = app.cvFileName;

                                      // Save dialog
                                      String? savePath = await FilePicker
                                          .platform
                                          .saveFile(
                                            dialogTitle: "Sačuvaj dokument",
                                            fileName:
                                                originalName, // <- ovdje backend vec šalje npr. "test.docx"
                                          );

                                      if (savePath == null) return;

                                      // Ekstraktujemo ekstenziju
                                      final extension =
                                          originalName.contains(".")
                                              ? originalName.substring(
                                                originalName.lastIndexOf("."),
                                              )
                                              : "";

                                      // Ako korisnik NE upiše ekstenziju — dodaj je
                                      if (!savePath.toLowerCase().endsWith(
                                        extension.toLowerCase(),
                                      )) {
                                        savePath = "$savePath$extension";
                                      }

                                      final file = File(savePath);
                                      await file.writeAsBytes(bytes);

                                      _showSuccessToast(
                                        "Dokument je uspješno preuzet.",
                                      );
                                    },

                                    style: ElevatedButton.styleFrom(
                                      backgroundColor: const Color(0xff67B1E5),
                                      padding: const EdgeInsets.symmetric(
                                        horizontal: 14,
                                        vertical: 8,
                                      ),
                                      shape: RoundedRectangleBorder(
                                        borderRadius: BorderRadius.circular(6),
                                      ),
                                    ),
                                    child: Text(
                                      "preuzmi",
                                      style: GoogleFonts.openSans(
                                        color: Colors.white,
                                        fontWeight: FontWeight.w600,
                                      ),
                                    ),
                                  ),
                                ],
                              ),
                            ),

                            const SizedBox(height: 24),

                            _label("Zašto želite da radite kod nas?"),

                            Container(
                              height: 150,
                              padding: const EdgeInsets.all(12),
                              decoration: BoxDecoration(
                                color: Colors.white,
                                border: Border.all(color: Colors.grey.shade400),
                                borderRadius: BorderRadius.circular(8),
                              ),
                              child: Text(
                                app.letter ?? "—",
                                style: GoogleFonts.openSans(fontSize: 15),
                              ),
                            ),

                            const SizedBox(height: 24),

                            Row(
                              mainAxisAlignment: MainAxisAlignment.center,
                              children: [
                                ElevatedButton(
                                  onPressed: () async {
                                    try {
                                      // 1️⃣ pošalji mail + označi poziv
                                      await _workApplicationProvider
                                          .inviteToInterview(app.id);

                                      // 2️⃣ HARD DELETE prijave
                                      await _workApplicationProvider.delete(
                                        app.id,
                                      );

                                      // 3️⃣ zatvori popup
                                      Navigator.pop(context);

                                      // 4️⃣ REFRESH LISTE
                                      await _loadTotal();

                                      _showSuccessToast(
                                        "Poziv na sastanak je poslan.",
                                      );

                                    } catch (e) {
                                      _showErrorToast(
                                        "Greška pri slanju poziva.",
                                      );
                                    }
                                  },
                                  style: ElevatedButton.styleFrom(
                                    backgroundColor: const Color(0xff67B1E5),
                                    padding: const EdgeInsets.symmetric(
                                      horizontal: 30,
                                      vertical: 14,
                                    ),
                                    shape: RoundedRectangleBorder(
                                      borderRadius: BorderRadius.circular(30),
                                    ),
                                  ),
                                  child: Text(
                                    "pozovi na sastanak",
                                    style: GoogleFonts.openSans(
                                      color: Colors.white,
                                      fontSize: 16,
                                      fontWeight: FontWeight.w600,
                                    ),
                                  ),
                                ),

                                const SizedBox(width: 20),

                                ElevatedButton(
                                  onPressed: () async {
                                    await _workApplicationProvider.delete(
                                        app.id,
                                      );
                                      // 3️⃣ zatvori popup
                                      Navigator.pop(context);

                                      _loadTotal();

                                      _showErrorToast("Prijava odbijena");

                                  },
                                  style: ElevatedButton.styleFrom(
                                    backgroundColor: const Color(0xffE26D8A),
                                    padding: const EdgeInsets.symmetric(
                                      horizontal: 30,
                                      vertical: 14,
                                    ),
                                    shape: RoundedRectangleBorder(
                                      borderRadius: BorderRadius.circular(30),
                                    ),
                                  ),
                                  child: Text(
                                    "odbij prijavu",
                                    style: GoogleFonts.openSans(
                                      color: Colors.white,
                                      fontSize: 16,
                                      fontWeight: FontWeight.w600,
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ],
                        ),
                      ),
                    ],
                  ),

                  // ---------------- BUTTONS ----------------
                ],
              ),
            ),
          ),

          // ---------------- CLOSE BUTTON (WHITE X) ----------------
          Positioned(
            top: 5,
            right: 5,
            child: GestureDetector(
              onTap: () => Navigator.pop(context),
              child: Container(
                padding: const EdgeInsets.all(6),
                child: const Icon(Icons.close, size: 26, color: Colors.white),
              ),
            ),
          ),
        ],
      ),
    );
  }

  // --------- HELPERI ---------
  Widget _label(String text) {
    return Text(
      text,
      style: GoogleFonts.openSans(fontSize: 17, fontWeight: FontWeight.w700),
    );
  }

  Widget _readonlyBox(String text) {
    return Container(
      height: 45,
      width: double.infinity,
      padding: const EdgeInsets.symmetric(horizontal: 12),
      alignment: Alignment.centerLeft,
      decoration: BoxDecoration(
        color: Colors.white,
        border: Border.all(color: Colors.grey.shade400),
        borderRadius: BorderRadius.circular(8),
      ),
      child: Text(text, style: GoogleFonts.openSans(fontSize: 15)),
    );
  }

  // ----------------------- PAGING -----------------------
  Widget _pagingWidget() {
    if (paging == null) return const SizedBox.shrink();

    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        IconButton(
          onPressed:
              paging!.hasPrevious
                  ? () {
                    setState(() => page--);
                    _loadPage();
                  }
                  : null,
          icon: const Icon(Icons.chevron_left),
        ),
        ...paging!.pageNumbers.map((p) {
          final isSelected = (p == page);
          return GestureDetector(
            onTap: () {
              setState(() => page = p);
              _loadPage();
            },
            child: Container(
              margin: const EdgeInsets.symmetric(horizontal: 4),
              padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
              decoration: BoxDecoration(
                color: isSelected ? Colors.blue : Colors.white,
                borderRadius: BorderRadius.circular(8),
                border: Border.all(color: Colors.grey.shade400),
              ),
              child: Text(
                "${p + 1}",
                style: TextStyle(
                  color: isSelected ? Colors.white : Colors.black,
                ),
              ),
            ),
          );
        }),
        IconButton(
          onPressed:
              paging!.hasNext
                  ? () {
                    setState(() => page++);
                    _loadPage();
                  }
                  : null,
          icon: const Icon(Icons.chevron_right),
        ),
      ],
    );
  }
}
