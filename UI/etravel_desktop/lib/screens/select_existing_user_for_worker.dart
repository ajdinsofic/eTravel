import 'dart:async';
import 'package:etravel_desktop/models/paging_info.dart';
import 'package:etravel_desktop/models/user.dart';
import 'package:etravel_desktop/providers/user_provider.dart';
import 'package:etravel_desktop/providers/user_role_provider.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

class SelectExistingUserPopup extends StatefulWidget {
  const SelectExistingUserPopup({super.key});

  @override
  State<SelectExistingUserPopup> createState() => _SelectExistingUserPopupState();
}

class _SelectExistingUserPopupState extends State<SelectExistingUserPopup> {
  late UserRoleProvider _userRoleProvider;
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
    _userRoleProvider = Provider.of<UserRoleProvider>(context, listen: false);
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

    _debounce = Timer(const Duration(milliseconds: 380), () {
      page = 0;
      _loadTotal();
    });
  }

  // ------------------- LOAD TOTAL -------------------
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

    paging = PagingInfo(
      totalCount: result.count,
      pageSize: 5,
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
    builder: (context) => Positioned(
      bottom: 25,
      right: 25,
      child: Material(
        color: Colors.transparent,
        child: Container(
          padding: const EdgeInsets.symmetric(horizontal: 18, vertical: 12),
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


  // ------------------- LOAD PAGE -------------------
  Future<void> _loadPage() async {
    final result = await _userProvider.get(
      filter: {
        "onlyUsers": true,
        "page": page,
        "pageSize": 5,
        "personNameSearch": _searchController.text.trim(),
        "CheckMoreRoles":true
      },
    );

    paging!.currentPage = page;

    setState(() {
      users = result.items;
      isLoading = false;
    });
  }

  

  // ------------------- ENABLE WORKER ROLE -------------------
  Future<void> _enableWorkerRole(User user) async {
  try {
    await _userRoleProvider.insert({
      "userId": user.id,
      "roleId": 2, // 2 = Radnik
    });

    _showSuccessToast("✓ ${user.firstName} ${user.lastName} je sada radnik");

    // Pošalji signal parent screenu da je završeno
    Navigator.pop(context, {
      "userId": user.id,
      "fullName": "${user.firstName} ${user.lastName}"
    });

  } catch (e) {
    debugPrint("Greška: $e");
  }
}


  // ------------------------------------------------------------------
  //                               UI
  // ------------------------------------------------------------------

  @override
  Widget build(BuildContext context) {
    return Dialog(
      insetPadding: const EdgeInsets.symmetric(horizontal: 120, vertical: 70),
      backgroundColor: Colors.white,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
      child: SizedBox(
        width: 850,
        height: 600,
        child: Column(
          children: [
            _header(),
            Expanded(
              child: Padding(
                padding: const EdgeInsets.all(28),
                child: Column(
                  children: [
                    _searchBar(),
                    const SizedBox(height: 30),

                    if (isLoading)
                      const Center(child: CircularProgressIndicator())
                    else if (users.isEmpty)
                      Text(
                        "Nema korisnika pod tim pojmom.",
                        style: GoogleFonts.openSans(
                          fontSize: 18,
                          fontWeight: FontWeight.w600,
                        ),
                      )
                    else
                      Expanded(
                        child: Column(
                          children: [
                            Expanded(
                              child: ListView(
                                children: [
                                  ...users.map((u) => _userItem(u)),
                                ],
                              ),
                            ),
                            _pagingWidget(),
                          ],
                        ),
                      ),
                  ],
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }

  // ------------------- HEADER -------------------
  Widget _header() {
    return Container(
      height: 55,
      decoration: const BoxDecoration(
        color: Color(0xff67B1E5),
        borderRadius: BorderRadius.vertical(top: Radius.circular(16)),
      ),
      child: Stack(
        children: [
          Center(
            child: Text(
              "Pronađeni korisnici",
              style: GoogleFonts.openSans(
                fontSize: 20,
                color: Colors.white,
                fontWeight: FontWeight.w700,
              ),
            ),
          ),
          Positioned(
            right: 12,
            top: 12,
            child: GestureDetector(
              onTap: () => Navigator.pop(context),
              child: const Icon(Icons.close, color: Colors.white, size: 24),
            ),
          )
        ],
      ),
    );
  }

  // ------------------- SEARCH -------------------
  Widget _searchBar() {
    return Container(
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
                border: InputBorder.none,
                hintStyle: GoogleFonts.openSans(
                  color: Colors.grey.shade600,
                  fontSize: 16,
                ),
              ),
            ),
          ),
          Padding(
            padding: const EdgeInsets.only(right: 16),
            child: Icon(Icons.search, color: Colors.grey.shade600, size: 20),
          )
        ],
      ),
    );
  }

  // ------------------- USER ITEM -------------------
  Widget _userItem(User user) {
  final fullName = "${user.firstName} ${user.lastName}";

  return Container(
    margin: const EdgeInsets.only(bottom: 16),
    height: 80,
    decoration: BoxDecoration(
      color: const Color(0xffD9D9D9),
      borderRadius: BorderRadius.circular(40),
    ),
    child: Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Padding(
          padding: const EdgeInsets.symmetric(horizontal: 30),
          child: Text(
            fullName,
            style: GoogleFonts.openSans(
              fontSize: 20,
              fontWeight: FontWeight.bold,
            ),
          ),
        ),

        // --------------- BUTTON ---------------
        Padding(
          padding: const EdgeInsets.only(right: 20),
          child: ElevatedButton(
            onPressed: () => _enableWorkerRole(user),
            style: ElevatedButton.styleFrom(
              backgroundColor: Colors.green,
              padding: const EdgeInsets.symmetric(horizontal: 22, vertical: 12),
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(30),
              ),
            ),
            child: Text(
              "omogući rolu radnika",
              style: GoogleFonts.openSans(
                color: Colors.white,
                fontSize: 16,
              ),
            ),
          ),
        )
      ],
    ),
  );
}


  // ------------------- PAGING -------------------
  Widget _pagingWidget() {
    if (paging == null) return const SizedBox.shrink();

    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        IconButton(
          onPressed: paging!.hasPrevious
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
          onPressed: paging!.hasNext
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
