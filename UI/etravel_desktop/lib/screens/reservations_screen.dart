import 'dart:async';
import 'package:etravel_desktop/models/paging_info.dart';
import 'package:etravel_desktop/models/user.dart';
import 'package:etravel_desktop/providers/user_provider.dart';
import 'package:etravel_desktop/screens/reservation_popup.dart';
import 'package:etravel_desktop/screens/user_active_reservations_screen.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

class ReservationScreen extends StatefulWidget {
  const ReservationScreen({super.key});

  @override
  State<ReservationScreen> createState() => _ReservationScreenState();
}

class _ReservationScreenState extends State<ReservationScreen> {
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
        "activeReservations": true,
        "personNameSearch": _searchController.text.trim(),
        "IncludeTotalCount": true,
        "retrieveAll": true, // backend vrati ukupan count
      },
    );

    paging = PagingInfo(totalCount: result.count, pageSize: 5, currentPage: 0);

    page = 0;

    await _loadPage();
  }

  // ------------------- LOAD SINGLE PAGE -------------------
  Future<void> _loadPage() async {
    final result = await _userProvider.get(
      filter: {
        "activeReservations": true,
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
                            "Nema korisnika sa aktivnim rezervacijama.",
                            style: GoogleFonts.openSans(
                              fontSize: 18,
                              fontWeight: FontWeight.w600,
                            ),
                          ),
                        )
                      else
                        Column(
                          children: [
                            ...users.map((u) => _reservationItem(u)),
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
            "Rezervacije",
            style: GoogleFonts.openSans(
              fontSize: 40,
              fontWeight: FontWeight.bold,
              color: Colors.white,
            ),
          ),
          const SizedBox(height: 8),
          Text(
            "provjera korisničkih rezervacija",
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

  // --------------------- SINGLE ITEM ---------------------
  Widget _reservationItem(User user) {
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
                "${user.firstName} ${user.lastName}",
                style: GoogleFonts.openSans(
                  fontSize: 22,
                  fontWeight: FontWeight.bold,
                ),
              ),
              ElevatedButton(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (_) => UserActiveReservationsScreen(user: user),
                    ),
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
                  "pogledaj rezervacije",
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

  // ---------------------- PAGING UI ----------------------
  Widget _pagingWidget() {
    if (paging == null) return SizedBox.shrink();

    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        // PREVIOUS
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

        // NUMBERS
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

        // NEXT
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
