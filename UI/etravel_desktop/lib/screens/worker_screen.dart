import 'dart:async';
import 'package:etravel_desktop/models/paging_info.dart';
import 'package:etravel_desktop/models/user.dart';
import 'package:etravel_desktop/models/user_image_request.dart';
import 'package:etravel_desktop/providers/user_provider.dart';
import 'package:etravel_desktop/providers/user_role_provider.dart';
import 'package:etravel_desktop/screens/add_worker_popup.dart';
import 'package:etravel_desktop/screens/choose_worker_type_popup.dart';
import 'package:etravel_desktop/screens/select_existing_user_for_worker.dart';
import 'package:etravel_desktop/screens/view_worker_popup.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

class EmployeeScreen extends StatefulWidget {
  const EmployeeScreen({super.key});

  @override
  State<EmployeeScreen> createState() => _EmployeeScreenState();
}

class _EmployeeScreenState extends State<EmployeeScreen> {
  late UserProvider _userProvider;
  late UserRoleProvider _userRoleProvider;
  final TextEditingController _searchController = TextEditingController();
  Timer? _debounce;

  bool isLoading = true;
  List<User> workers = [];

  int page = 0;
  int pageSize = 5;

  PagingInfo? paging;

  @override
  void initState() {
    super.initState();
    _userProvider = Provider.of<UserProvider>(context, listen: false);
    _userRoleProvider = Provider.of<UserRoleProvider>(context, listen: false);
    _searchController.addListener(_onSearchChanged);
    _loadTotal();
  }

  @override
  void dispose() {
    _searchController.dispose();
    _debounce?.cancel();
    super.dispose();
  }

  Future<void> _fireWorker(User w) async {
    try {
      await _userRoleProvider.deleteComposite(w.id, 2);

      _showSuccessToast("✓ Radnik ${w.firstName} ${w.lastName} je otpušten.");
      _loadTotal(); // reload liste
    } catch (e) {
      debugPrint("Greška otpuštanja: $e");
    }
  }

  Future<bool> _confirmFireWorker(User w) async {
    return await showDialog(
      context: context,
      barrierDismissible: false,
      builder: (_) {
        return AlertDialog(
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(12),
          ),
          title: const Text(
            "Otpustiti radnika?",
            textAlign: TextAlign.center,
            style: TextStyle(fontWeight: FontWeight.bold),
          ),
          content: Text(
            "Da li ste sigurni da želite otpustiti radnika:\n\n"
            "👤 ${w.firstName} ${w.lastName}",
            textAlign: TextAlign.center,
          ),
          actionsAlignment: MainAxisAlignment.center,
          actions: [
            OutlinedButton(
              onPressed: () => Navigator.pop(context, false),
              style: OutlinedButton.styleFrom(
                foregroundColor: Colors.blue,
                side: const BorderSide(color: Colors.blue, width: 2),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: const Text("Otkaži"),
            ),
            ElevatedButton(
              onPressed: () => Navigator.pop(context, true),
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.redAccent,
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: const Text("Da, otpusti"),
            ),
          ],
        );
      },
    );
  }

  // ------------------- SEARCH DEBOUNCE -------------------
  void _onSearchChanged() {
    if (_debounce?.isActive ?? false) _debounce!.cancel();
    _debounce = Timer(const Duration(milliseconds: 400), () {
      page = 0;
      _loadTotal();
    });
  }

  String convertToUtcIso(String dateString) {
    final parts = dateString.split(".");
    if (parts.length != 3) return dateString;

    final day = int.parse(parts[0]);
    final month = int.parse(parts[1]);
    final year = int.parse(parts[2]);

    // Kreiraj lokalni datum, ali kao UTC
    final dt = DateTime.utc(year, month, day);

    return dt.toIso8601String();
  }

  // ------------------- LOAD TOTAL COUNT -------------------
  Future<void> _loadTotal() async {
    setState(() => isLoading = true);

    final result = await _userProvider.get(
      filter: {
        "onlyWorkers": true,
        "personNameSearch": _searchController.text.trim(),
        "IncludeTotalCount": true,
        "retrieveAll": true,
      },
    );

    paging = PagingInfo(totalCount: result.count, pageSize: 5, currentPage: 0);
    page = 0;

    await _loadPage();
  }

  // ------------------- LOAD PAGE -------------------
  Future<void> _loadPage() async {
    final result = await _userProvider.get(
      filter: {
        "onlyWorkers": true,
        "page": page,
        "pageSize": 5,
        "personNameSearch": _searchController.text.trim(),
      },
    );

    paging!.currentPage = page;

    setState(() {
      workers = result.items;
      isLoading = false;
    });
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

  // ---------------------------- UI ----------------------------
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
                        children: [_searchBar(), _topActionRow()],
                      ),
                      const SizedBox(height: 45),
                      _tableHeader(),
                      const SizedBox(height: 25),

                      if (isLoading)
                        const Center(child: CircularProgressIndicator())
                      else if (workers.isEmpty)
                        Center(
                          child: Text(
                            "Nema pronađenih radnika.",
                            style: GoogleFonts.openSans(
                              fontSize: 18,
                              fontWeight: FontWeight.w600,
                            ),
                          ),
                        )
                      else
                        Column(
                          children: [
                            ...workers.map((w) => _workerItem(w)),
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
            "Radnici",
            style: GoogleFonts.openSans(
              fontSize: 40,
              fontWeight: FontWeight.bold,
              color: Colors.white,
            ),
          ),
          const SizedBox(height: 8),
          Text(
            "Uvijek spremni za nove pobjede",
            style: GoogleFonts.openSans(fontSize: 16, color: Colors.white),
          ),
        ],
      ),
    );
  }

  // ----------------------- TOP RIGHT BUTTON -----------------------
  Widget _topActionRow() {
    return Row(
      mainAxisAlignment: MainAxisAlignment.end,
      children: [
        SizedBox(
          height: 40,
          child: ElevatedButton(
            onPressed: () async {
              // 1️⃣ Prvi popup — biramo tip radnika
              final choice = await showDialog(
                context: context,
                barrierDismissible: false,
                builder: (_) => const ChooseWorkerTypePopup(),
              );

              if (choice == null) return;

              // =====================================================================
              // 2️⃣ EXISTING USER — dodaj rolu radnika
              // =====================================================================
              if (choice == "existing") {
                final selectedUser = await showDialog(
                  context: context,
                  barrierDismissible: false,
                  builder: (_) => const SelectExistingUserPopup(),
                );

                // Ako je null → user kliknuo X
                if (selectedUser == null) return;

                // ✔ Refresh liste radnika
                await _loadTotal();

                // ✔ Prikaži toast
                _showSuccessToast(
                  "✓ ${selectedUser["fullName"]} je sada radnik",
                );

                return;
              }

              // =====================================================================
              // 3️⃣ NEW WORKER — kreiranje + upload slike
              // =====================================================================
              if (choice == "new") {
                final newWorker = await showDialog(
                  context: context,
                  barrierDismissible: false,
                  builder: (_) => const AddWorkerPopup(),
                );

                if (newWorker == null) return;

                try {
                  // 3.1 KREIRAJ USERA SA ROLE 2 (radnik)
                  final user = await _userProvider.insert({
                    "firstName": newWorker["firstName"],
                    "lastName": newWorker["lastName"],
                    "username": newWorker["username"],
                    "email": newWorker["email"],
                    "phoneNumber": newWorker["phone"],
                    "dateBirth": convertToUtcIso(newWorker["birthDate"]),
                    "imageUrl": newWorker["imageUrl"],
                    "password": newWorker["password"],
                    "roleId": 2,
                  });

                  // 3.2 UPLOAD SLIKE AKO POSTOJI
                  if (newWorker["image"] != null) {
                    final imageRequest = UserImageRequest(
                      userId: user.id,
                      image: newWorker["image"],
                    );

                    await _userProvider.addUserImage(imageRequest);
                  }

                  _showSuccessToast(
                    "✓ Uspješno dodan radnik ${newWorker["firstName"]} ${newWorker["lastName"]}",
                  );

                  _loadTotal();
                } catch (e) {
                  debugPrint("Greška pri dodavanju radnika: $e");
                }
              }
            },

            style: ElevatedButton.styleFrom(
              backgroundColor: const Color(0xff67B1E5),
              padding: const EdgeInsets.symmetric(horizontal: 22),
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(30),
              ),
            ),
            child: Text(
              "dodaj radnika",
              style: GoogleFonts.openSans(color: Colors.white, fontSize: 16),
            ),
          ),
        ),
      ],
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
                  controller: _searchController,
                  decoration: InputDecoration(
                    hintText: "pretražite radnike",
                    hintStyle: GoogleFonts.openSans(
                      color: Colors.grey.shade600,
                      fontSize: 16,
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

  // --------------------- TABLE HEADER ---------------------
  Widget _tableHeader() {
    return Row(
      children: [
        const SizedBox(width: 30),
        SizedBox(
          width: 300,
          child: Text(
            "Ime radnika",
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

  // --------------------- WORKER ITEM ---------------------
  Widget _workerItem(User worker) {
    final fullName = "${worker.firstName} ${worker.lastName}";

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
              Text(
                fullName,
                style: GoogleFonts.openSans(
                  fontSize: 22,
                  fontWeight: FontWeight.bold,
                ),
              ),

              ElevatedButton(
                onPressed: () async {
                  final result = await showDialog(
                    context: context,
                    barrierDismissible: true,
                    builder: (_) => ViewWorkerPopup(worker: worker),
                  );

                  if (result == "fire") {
                    _fireWorker(worker);
                  }
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
                  "pogledaj radnika",
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

  // ---------------------- PAGING ----------------------
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
