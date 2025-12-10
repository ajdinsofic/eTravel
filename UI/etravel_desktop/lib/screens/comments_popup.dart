// FULL UPDATED ReviewPopup.dart WITHOUT IMAGE HEADER

import 'dart:async';
import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/models/comment.dart';
import 'package:etravel_desktop/models/offer.dart';
import 'package:etravel_desktop/providers/comment_provider.dart';
import 'package:etravel_desktop/providers/user_provider.dart';
import 'package:etravel_desktop/providers/offer_provider.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

class ReviewPopup extends StatefulWidget {
  final int offerId;
  final bool openInEditMode;


  const ReviewPopup({super.key, required this.offerId, this.openInEditMode = false});

  @override
  State<ReviewPopup> createState() => _ReviewPopupState();
}

class _ReviewPopupState extends State<ReviewPopup> {
  late CommentProvider _commentProvider;
  late UserProvider _userProvider;

  bool alreadyEdited = false;


  List<Comment> comments = [];
  bool isLoading = true;
  bool isEditMode = false;

  Map<int, String> userNames = {};

  /// PAGING
  int currentPage = 0;
  int totalPages = 1;
  final int pageSize = 5;

  /// SEARCH
  String searchQuery = "";
  Timer? _searchDebounce;

  @override
  void initState() {
    super.initState();
    _commentProvider = Provider.of<CommentProvider>(context, listen: false);
    _userProvider = Provider.of<UserProvider>(context, listen: false);
    isEditMode = widget.openInEditMode;
    _loadInitialPaging();
  }

  bool get hasEditedComments => comments.any((c) => c.isEdited == true);

  // -----------------------------------------------------------
  // INITIAL LOAD
  // -----------------------------------------------------------
  Future<void> _loadInitialPaging() async {
    try {
      await _recalculatePaging();
      await _loadPage(0);
    } catch (e) {
      debugPrint("Initial load error: $e");
    }

    setState(() => isLoading = false);
  }

  // -----------------------------------------------------------
  // RE-CALCULATE PAGE COUNT (USED IN SEARCH)
  // -----------------------------------------------------------
  Future<void> _recalculatePaging() async {
    final all = await _commentProvider.get(
      filter: {
        "offerId": widget.offerId,
        "retrieveAll": true,
        if (searchQuery.isNotEmpty) "pearsonName": searchQuery,
      },
    );

    int totalCount = all.count ?? all.items.length;

    totalPages = (totalCount / pageSize).ceil();
    if (totalPages == 0) totalPages = 1;

    currentPage = 0;
  }

  Future<bool> _confirmDeleteComment() async {
    return await showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) {
        return AlertDialog(
          title: const Text(
            "Izbrisati komentar?",
            style: TextStyle(fontWeight: FontWeight.bold),
            textAlign: TextAlign.center,
          ),
          content: const Text(
            "Da li ste sigurni da želite obrisati ovaj komentar?\n"
            "Nakon brisanja, komentar neće biti moguće vratiti.",
            textAlign: TextAlign.center,
          ),
          actions: [
            // ❌ OTKAZI
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

            // 🗑️ DA, IZBRIŠI
            ElevatedButton(
              onPressed: () => Navigator.pop(context, true),
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.redAccent,
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: const Text("Da, izbriši"),
            ),
          ],
        );
      },
    );
  }

  void _showUnsavedWarning() {
  showDialog(
    context: context,
    builder: (context) {
      return AlertDialog(
        title: const Text(
          "Sačuvajte promjene",
          style: TextStyle(fontWeight: FontWeight.bold),
        ),
        content: const Text(
          "Ne možete se vratiti na detalje dok ne sačuvate izmjene.",
        ),
        actions: [
          ElevatedButton(
            onPressed: () => Navigator.pop(context),
            style: ElevatedButton.styleFrom(
              backgroundColor: Colors.blueAccent,
              foregroundColor: Colors.white,
            ),
            child: const Text("U redu"),
          )
        ],
      );
    },
  );
}


  // -----------------------------------------------------------
  // LOAD SPECIFIC PAGE
  // -----------------------------------------------------------
  Future<void> _loadPage(int page) async {
    try {
      final paged = await _commentProvider.get(
        filter: {
          "offerId": widget.offerId,
          "page": page,
          "pageSize": pageSize,
          if (searchQuery.isNotEmpty) "pearsonName": searchQuery,
        },
      );

      comments = paged.items;

      for (var c in comments) {
        c.isEdited = false;
      }

      // Load user names
      for (var c in comments) {
        try {
          final u = await _userProvider.getById(c.userId);
          userNames[c.userId] = "${u.firstName} ${u.lastName}";
        } catch (_) {
          userNames[c.userId] = "Korisnik ${c.userId}";
        }
      }

      setState(() {});
    } catch (e) {
      debugPrint("Load page error: $e");
    }
  }

  // -----------------------------------------------------------
  // SEARCH LOGIC
  // -----------------------------------------------------------
  Future<void> _searchComments(String query) async {
    searchQuery = query.trim();

    if (_searchDebounce?.isActive ?? false) _searchDebounce!.cancel();

    _searchDebounce = Timer(const Duration(milliseconds: 300), () async {
      await _recalculatePaging();
      await _loadPage(0);
    });
  }

  // -----------------------------------------------------------
  // SAVE CHANGES
  // -----------------------------------------------------------
  Future<void> _saveChanges() async {
    final edited = comments.where((c) => c.isEdited).toList();

    if (edited.isEmpty) return;

    for (var c in edited) {
      await _commentProvider.update(c.id, {
        "id": c.id,
        "userId": c.userId,
        "offerId": c.offerId,
        "comment": c.comment,
        "starRate": c.starRate,
      });
    }

    for (var c in edited) {
      c.isEdited = false;
    }

    setState(() {});
    alreadyEdited = false;
    _showSaveToast();
  }

  // -----------------------------------------------------------
  // SUCCESS TOAST
  // -----------------------------------------------------------
  void _showSaveToast() {
    final overlay = Overlay.of(context);
    if (overlay == null) return;

    late OverlayEntry entry;

    entry = OverlayEntry(
      builder:
          (context) => Positioned(
            bottom: 20,
            right: 20,
            child: Material(
              color: Colors.transparent,
              child: Container(
                padding: const EdgeInsets.symmetric(
                  horizontal: 16,
                  vertical: 12,
                ),
                decoration: BoxDecoration(
                  color: Colors.green.shade600,
                  borderRadius: BorderRadius.circular(10),
                ),
                child: const Text(
                  "✓ Uspješno sačuvano",
                  style: TextStyle(color: Colors.white, fontSize: 16),
                ),
              ),
            ),
          ),
    );

    overlay.insert(entry);
    Future.delayed(const Duration(seconds: 3), () => entry.remove());
  }

  // -----------------------------------------------------------
  // BUILD ROOT
  // -----------------------------------------------------------
  @override
  Widget build(BuildContext context) {
    return Dialog(
      insetPadding: const EdgeInsets.symmetric(horizontal: 60, vertical: 40),
      backgroundColor: Colors.transparent,
      child:
          isLoading
              ? const Center(child: CircularProgressIndicator())
              : _content(),
    );
  }

  Widget _content() {
    return Container(
      width: 800,
      child: Column(children: [_topBar(), Expanded(child: _commentsSection())]),
    );
  }

  // -----------------------------------------------------------
  // TOP BAR
  // -----------------------------------------------------------
  Widget _topBar() {
    return Container(
      height: 60,
      decoration: const BoxDecoration(
        color: Color(0xff67B1E5),
        borderRadius: BorderRadius.vertical(top: Radius.circular(14)),
      ),
      child: Row(
        children: [
          const SizedBox(width: 20),

          GestureDetector(
  onTap: () {
    if (hasEditedComments) {
      _showUnsavedWarning(); // <-- blokada + popup
      return;
    }
    setState(() => isEditMode = false);
  },
  child: Text(
    "Detalji",
    style: GoogleFonts.openSans(
      fontSize: 20,
      fontWeight: FontWeight.w600,
      color: (!isEditMode || hasEditedComments)
          ? Colors.white
          : Colors.white70,
    ),
  ),
),


          const SizedBox(width: 20),

          GestureDetector(
            onTap: () => setState(() => isEditMode = true),
            child: Text(
              "Uredi",
              style: GoogleFonts.openSans(
                fontSize: 20,
                color: isEditMode ? Colors.white : Colors.white70,
                fontWeight: FontWeight.w600,
              ),
            ),
          ),

          const Spacer(),
          IconButton(
            icon: const Icon(Icons.close, color: Colors.white, size: 28),
            onPressed: () => Navigator.pop(context),
          ),
        ],
      ),
    );
  }

  // -----------------------------------------------------------
  // COMMENTS SECTION
  // -----------------------------------------------------------
  Widget _commentsSection() {
    return Container(
      color: const Color(0xFFD9D9D9),
      child: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(22),
          child: Container(
            decoration: BoxDecoration(
              color: const Color(0xFFF5F5F5),
              borderRadius: BorderRadius.circular(14),
            ),
            child: Padding(
              padding: const EdgeInsets.all(22),
              child: Column(
                children: [
                  Text(
                    "KOMENTARI",
                    style: GoogleFonts.openSans(
                      fontSize: 22,
                      fontWeight: FontWeight.bold,
                    ),
                  ),

                  const SizedBox(height: 20),

                  /// SEARCH
                  TextField(
                    onChanged: _searchComments,
                    decoration: InputDecoration(
                      hintText: "Pretraži po imenu korisnika...",
                      prefixIcon: const Icon(Icons.search),
                      filled: true,
                      fillColor: Colors.white,
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(12),
                      ),
                    ),
                  ),

                  const SizedBox(height: 20),

                  ...comments.map(_commentCard).toList(),

                  const SizedBox(height: 20),

                  if (!isEditMode) _pagingBar(),

                  if (isEditMode)
                    ElevatedButton(
                      onPressed: hasEditedComments ? _saveChanges : null,
                      style: ElevatedButton.styleFrom(
                        backgroundColor: Colors.blueAccent,
                        minimumSize: const Size(double.infinity, 55),
                      ),
                      child: const Text(
                        "Sačuvaj promjene",
                        style: TextStyle(color: Colors.white, fontSize: 18),
                      ),
                    ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }

  // -----------------------------------------------------------
  // PAGING BAR
  // -----------------------------------------------------------
  Widget _pagingBar() {
    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        IconButton(
          icon: const Icon(Icons.chevron_left, size: 32),
          onPressed:
              currentPage > 0
                  ? () {
                    currentPage--;
                    _loadPage(currentPage);
                  }
                  : null,
        ),
        Text(
          "${currentPage + 1} / $totalPages",
          style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
        ),
        IconButton(
          icon: const Icon(Icons.chevron_right, size: 32),
          onPressed:
              currentPage < totalPages - 1
                  ? () {
                    currentPage++;
                    _loadPage(currentPage);
                  }
                  : null,
        ),
      ],
    );
  }

  // -----------------------------------------------------------
  // COMMENT CARD
  // -----------------------------------------------------------
  Widget _commentCard(Comment c) {
    final controller = TextEditingController(text: c.comment);

    controller.addListener(() {
      if (controller.text != c.comment) {
        c.comment = controller.text;
        c.isEdited = true;
        if (alreadyEdited == false) {
          setState(() {});
          alreadyEdited = true;
        }
      }
    });

    return Container(
      margin: const EdgeInsets.only(bottom: 18),
      padding: const EdgeInsets.all(20),
      decoration: BoxDecoration(
        color: Colors.white,
        border: Border.all(color: Colors.grey.shade300),
        borderRadius: BorderRadius.circular(14),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            children: [
              Text(
                userNames[c.userId] ?? "Korisnik",
                style: const TextStyle(
                  fontSize: 18,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(width: 10),
              Row(
                children: List.generate(
                  5,
                  (i) => Icon(
                    i < c.starRate ? Icons.star : Icons.star_border,
                    size: 18,
                    color: Colors.amber,
                  ),
                ),
              ),
            ],
          ),

          const SizedBox(height: 12),

          TextField(
            controller: controller,
            maxLines: null,
            readOnly: !isEditMode,
            decoration: InputDecoration(
              filled: true,
              fillColor: Colors.grey.shade100,
              border: OutlineInputBorder(
                borderRadius: BorderRadius.circular(12),
              ),
            ),
          ),

          const SizedBox(height: 12),

          Align(
            alignment: Alignment.centerRight,
            child: ElevatedButton(
              onPressed:
                  isEditMode
                      ? () async {
                        final confirmed = await _confirmDeleteComment();
                        if (!confirmed) return;

                        try {
                          await _commentProvider.delete(c.id);
                        } catch (e) {
                          debugPrint("Greška pri brisanju: $e");
                        }

                        await _recalculatePaging();
                        await _loadPage(currentPage);

                        setState(() {});
                      }
                      : null,

              style: ElevatedButton.styleFrom(
                backgroundColor: isEditMode ? Colors.red : Colors.grey.shade400,
              ),
              child: const Text(
                "izbriši komentar",
                style: TextStyle(color: Colors.white),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
