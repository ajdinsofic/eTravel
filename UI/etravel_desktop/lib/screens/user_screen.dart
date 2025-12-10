import 'dart:async';
import 'package:etravel_desktop/models/paging_info.dart';
import 'package:etravel_desktop/models/user.dart';
import 'package:etravel_desktop/providers/user_provider.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

class UserScreen extends StatefulWidget {
  const UserScreen({super.key});

  @override
  State<UserScreen> createState() => _UserScreenState();
}

class _UserScreenState extends State<UserScreen> {
  late UserProvider _userProvider;
  final TextEditingController _searchController = TextEditingController();
  Timer? _debounce;

  bool isLoading = true;
  List<User> users = [];

  int page = 0;
  int pageSize = 5;

  PagingInfo? paging;

  @override
  void initState() {
    super.initState();
    _userProvider = Provider.of<UserProvider>(context, listen: false);
    _searchController.addListener(_onSearchChanged);
    _loadTotal();
  }

  @override
  void dispose() {
    _searchController.dispose();
    _debounce?.cancel();
    super.dispose();
  }

  // ------------------- SEARCH DEBOUNCE -------------------
  void _onSearchChanged() {
    if (_debounce?.isActive ?? false) _debounce!.cancel();
    _debounce = Timer(const Duration(milliseconds: 400), () {
      page = 0;
      _loadTotal();
    });
  }

  // ------------------- LOAD TOTAL COUNT -------------------
  Future<void> _loadTotal() async {
    setState(() => isLoading = true);

    final result = await _userProvider.get(
      filter: {
        "onlyUsers": true,
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
        "onlyUsers": true,
        "page": page,
        "pageSize": 5,
        "personNameSearch": _searchController.text.trim(),
      },
    );

    paging!.currentPage = page;

    setState(() {
      users = result.items;
      isLoading = false;
    });
  }

  // ------------------- CONFIRM BLOCK -------------------
  Future<bool> _confirmBlockUser(String fullName) async {
    return await showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) {
        return AlertDialog(
          title: const Text(
            "Blokirati korisnika?",
            style: TextStyle(fontWeight: FontWeight.bold),
            textAlign: TextAlign.center,
          ),
          content: Text(
            "Da li ste sigurni da želite blokirati korisnika:\n"
            "👤 $fullName\n"
            "Korisnik neće moći pristupiti aplikaciji.",
            textAlign: TextAlign.center,
          ),
          actions: [
            OutlinedButton(
              onPressed: () => Navigator.pop(context, false),
              style: OutlinedButton.styleFrom(
                backgroundColor: Colors.white,
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
              child: const Text("Da, blokiraj"),
            ),
          ],
        );
      },
    );
  }

  // ------------------- CONFIRM UNBLOCK -------------------
  Future<bool> _confirmUnblockUser(String fullName) async {
    return await showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) {
        return AlertDialog(
          title: const Text(
            "Odblokirati korisnika?",
            style: TextStyle(fontWeight: FontWeight.bold),
            textAlign: TextAlign.center,
          ),
          content: Text(
            "Da li ste sigurni da želite odblokirati korisnika:\n"
            "👤 $fullName\n"
            "Korisnik će ponovo imati pristup aplikaciji.",
            textAlign: TextAlign.center,
          ),
          actions: [
            OutlinedButton(
              onPressed: () => Navigator.pop(context, false),
              style: OutlinedButton.styleFrom(
                backgroundColor: Colors.white,
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
                backgroundColor: Colors.green,
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: const Text("Da, odblokiraj"),
            ),
          ],
        );
      },
    );
  }

  void _showToast(String message, {required bool isBlockAction}) {
  final overlay = Overlay.of(context);
  if (overlay == null) return;

  late OverlayEntry entry;

  entry = OverlayEntry(
    builder: (context) => Positioned(
      bottom: 25,
      right: 25,
      child: Material(
        color: Colors.transparent,
        child: Container(
          padding: const EdgeInsets.symmetric(horizontal: 18, vertical: 12),
          decoration: BoxDecoration(
            color: isBlockAction ? Colors.redAccent : Colors.green,
            borderRadius: BorderRadius.circular(12),
          ),
          child: Text(
            message,
            style: const TextStyle(
              color: Colors.white,
              fontSize: 16,
              fontWeight: FontWeight.w500,
            ),
          ),
        ),
      ),
    ),
  );

  overlay.insert(entry);

  Future.delayed(const Duration(seconds: 3), () {
    entry.remove();
  });
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
                      _searchBar(),
                      const SizedBox(height: 45),
                      _tableHeader(),
                      const SizedBox(height: 25),

                      if (isLoading)
                        const Center(child: CircularProgressIndicator())
                      else if (users.isEmpty)
                        Center(
                          child: Text(
                            "Nema pronađenih korisnika.",
                            style: GoogleFonts.openSans(
                              fontSize: 18,
                              fontWeight: FontWeight.w600,
                            ),
                          ),
                        )
                      else
                        Column(
                          children: [
                            ...users.map((u) => _userItem(u)),
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
            "Korisnici",
            style: GoogleFonts.openSans(
              fontSize: 40,
              fontWeight: FontWeight.bold,
              color: Colors.white,
            ),
          ),
          const SizedBox(height: 8),
          Text(
            "upravljanje korisnicima",
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
                  controller: _searchController,
                  decoration: InputDecoration(
                    hintText: "pretražite korisnike",
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
            "Ime korisnika",
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

  // --------------------- USER ITEM ---------------------
  Widget _userItem(User user) {
    final bool blocked = user.isBlocked == true;
    final fullName = "${user.firstName} ${user.lastName}";

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
              /// ---------------- IME KORISNIKA ----------------
              Text(
                fullName,
                style: GoogleFonts.openSans(
                  fontSize: 22,
                  fontWeight: FontWeight.bold,
                  color: blocked ? Colors.grey.shade600 : Colors.black,
                  decoration: blocked ? TextDecoration.lineThrough : null,
                ),
              ),

              /// ---------------- DUGME ----------------
              ElevatedButton(
                onPressed: () async {
                  if (blocked) {
                    final confirm = await _confirmUnblockUser(fullName);
                    if (!confirm) return;

                    await _userProvider.unblockUser(user.id);
                    _showToast(
                      "✓ Uspješno ste odblokirali korisnika $fullName",
                      isBlockAction: false,
                    );
                    _loadTotal();
                  } else {
                    final confirm = await _confirmBlockUser(fullName);
                    if (!confirm) return;

                    await _userProvider.blockUser(user.id);
                    _showToast("✓ Uspješno ste blokirali korisnika $fullName",
                        isBlockAction: true);
                    _loadTotal();
                  }
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor: blocked ? Colors.green : Colors.redAccent,
                  padding: const EdgeInsets.symmetric(
                    horizontal: 22,
                    vertical: 12,
                  ),
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(30),
                  ),
                ),
                child: Text(
                  blocked ? "odblokiraj korisnika" : "blokiraj korisnika",
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
