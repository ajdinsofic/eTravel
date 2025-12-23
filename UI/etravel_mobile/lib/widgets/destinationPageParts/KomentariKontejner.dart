import 'package:etravel_app/models/comment.dart';
import 'package:etravel_app/models/user.dart';
import 'package:etravel_app/providers/comment_provider.dart';
import 'package:etravel_app/providers/user_provider.dart';
import 'package:etravel_app/widgets/startingPageParts/postaviWidthIHeight.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class KomentariKontejner extends StatefulWidget {
  final int offerId;

  const KomentariKontejner({
    super.key,
    required this.offerId,
  });

  @override
  State<KomentariKontejner> createState() => _KomentariKontejnerState();
}

class _KomentariKontejnerState extends State<KomentariKontejner> {
  late CommentProvider _commentProvider;
  late UserProvider _userProvider;

  PageController? _pageController;

  bool isLoading = true;

  /// SVI komentari (backend paging IZBAČEN)
  final List<_CommentUI> comments = [];

  /// cache korisnika
  final Map<int, String> _userCache = {};

  int currentPage = 0;

  @override
  void initState() {
    super.initState();
    _commentProvider = Provider.of<CommentProvider>(context, listen: false);
    _userProvider = Provider.of<UserProvider>(context, listen: false);
    _pageController = PageController(initialPage: 0);

    _loadComments();
  }

  @override
  void dispose() {
    _pageController?.dispose();
    super.dispose();
  }

  Future<void> _loadComments() async {
    try {
      final result = await _commentProvider.get(
        filter: {
          "offerId": widget.offerId,
        },
      );

      for (final Comment c in result.items) {
        if (!_userCache.containsKey(c.userId)) {
          final User user = await _userProvider.getById(c.userId);
          _userCache[c.userId] =
              "${user.firstName} ${user.lastName}";
        }

        comments.add(
          _CommentUI(
            fullName: _userCache[c.userId]!,
            comment: c.comment,
            starRate: c.starRate,
          ),
        );
      }

      setState(() => isLoading = false);
    } catch (e) {
      debugPrint("Greška pri učitavanju komentara: $e");
      setState(() => isLoading = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    postaviWidthIHeight(context);

    if (isLoading) {
      return const Padding(
        padding: EdgeInsets.all(24),
        child: CircularProgressIndicator(),
      );
    }

    if (comments.isEmpty) {
      return const Padding(
        padding: EdgeInsets.all(24),
        child: Text("Još nema komentara za ovu ponudu."),
      );
    }

    return Column(
      children: [
        SizedBox(
          height: screenHeight * 0.45,
          child: PageView.builder(
            controller: _pageController!,
            itemCount: comments.length,
            onPageChanged: (index) {
              setState(() => currentPage = index);
            },
            itemBuilder: (context, index) {
              return _buildCommentCard(comments[index]);
            },
          ),
        ),
        const SizedBox(height: 12),
        Text(
          "Stranica ${currentPage + 1} / ${comments.length}",
          style: const TextStyle(fontWeight: FontWeight.bold),
        ),
      ],
    );
  }

  /// ⬇⬇⬇ TVOJ DIZAJN – NEDIRNUT ⬇⬇⬇
  Widget _buildCommentCard(_CommentUI item) {
  return Container(
    width: screenWidth * 0.85,
    margin: EdgeInsets.symmetric(vertical: screenHeight * 0.02),
    decoration: BoxDecoration(
      color: Colors.white,
      borderRadius: BorderRadius.circular(16),
      border: Border.all(color: Colors.blue.shade100),
    ),
    child: Column(
      children: [
        const SizedBox(height: 16),

        // AVATAR
        CircleAvatar(
          radius: screenWidth * 0.08,
          child: Text(
            item.fullName[0],
            style: const TextStyle(fontSize: 24),
          ),
        ),

        const SizedBox(height: 12),

        // IME
        Text(
          item.fullName,
          style: TextStyle(
            fontWeight: FontWeight.bold,
            fontSize: screenWidth * 0.045,
          ),
        ),

        const SizedBox(height: 16),

        // PLAVI BOX
        Container(
          width: screenWidth * 0.85,
          padding: EdgeInsets.symmetric(
            vertical: screenHeight * 0.025,
            horizontal: screenWidth * 0.04,
          ),
          decoration: BoxDecoration(
            color: const Color(0xFF67B1E5),
            borderRadius: BorderRadius.circular(16),
          ),
          child: Column(
            children: [
              Text(
                item.comment,
                textAlign: TextAlign.center,
                style: TextStyle(
                  fontSize: screenWidth * 0.038,
                  color: Colors.white,
                ),
              ),

              const SizedBox(height: 12),

              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: List.generate(
                  5,
                  (index) => Icon(
                    index < item.starRate
                        ? Icons.star
                        : Icons.star_border,
                    color: const Color(0xFFDAB400),
                    size: screenWidth * 0.05,
                  ),
                ),
              ),
            ],
          ),
        ),

        const SizedBox(height: 16),
      ],
    ),
  );
}

}

/// UI helper
class _CommentUI {
  final String fullName;
  final String comment;
  final int starRate;

  _CommentUI({
    required this.fullName,
    required this.comment,
    required this.starRate,
  });
}
