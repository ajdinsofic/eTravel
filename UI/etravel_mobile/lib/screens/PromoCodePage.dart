import 'package:etravel_app/models/voucher.dart';
import 'package:etravel_app/providers/user_token_provider.dart';
import 'package:etravel_app/providers/user_voucher_provider.dart';
import 'package:etravel_app/providers/voucher_provider.dart';
import 'package:etravel_app/utils/session.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

import 'package:etravel_app/widgets/SljedecaDestinacijaIMenuBar.dart';
import 'package:etravel_app/widgets/headerIFooterAplikacije/eTravelFooter.dart';

class VoucherPage extends StatefulWidget {
  const VoucherPage({super.key});

  @override
  State<VoucherPage> createState() => _VoucherPageState();
}

class _VoucherPageState extends State<VoucherPage> {
  late UserTokenProvider _userTokenProvider;
  late UserVoucherProvider _userVoucherProvider;
  late VoucherProvider _voucherProvider;

  int userTokens = 0;

  /// voucherId → voucherCode
  final Map<int, String> unlockedVouchers = {};

  /// voucherId → isUsed
  final Map<int, bool> usedVouchers = {};

  bool isLoading = true;

  @override
  void initState() {
    super.initState();

    _userTokenProvider = Provider.of<UserTokenProvider>(context, listen: false);
    _userVoucherProvider = Provider.of<UserVoucherProvider>(
      context,
      listen: false,
    );
    _voucherProvider = Provider.of<VoucherProvider>(context, listen: false);

    _loadUserTokens().then((_) {
      _loadUserVouchers();
    });
  }

  /// ============================
  /// LOAD USER TOKENS
  /// ============================
  Future<void> _loadUserTokens() async {
    final result = await _userTokenProvider.get(
      filter: {"userId": Session.userId},
    );

    if (result.items.isNotEmpty) {
      userTokens = result.items.first.equity;
    }

    setState(() => isLoading = false);
  }

  /// ============================
  /// LOAD USER VOUCHERS
  /// ============================
  Future<void> _loadUserVouchers() async {
  final result = await _userVoucherProvider.get(
    filter: {"userId": Session.userId},
  );

  for (var userVoucher in result.items) {
    final Voucher voucher = await _voucherProvider.getById(
      userVoucher.voucherId,
    );

    unlockedVouchers[userVoucher.voucherId] = voucher.voucherCode;
    usedVouchers[userVoucher.voucherId] = userVoucher.isUsed;
  }

  setState(() {});
}


  /// ============================
  /// BUY VOUCHER
  /// ============================
  Future<void> _buyVoucher({
    required int voucherId,
    required int priceInTokens,
  }) async {
    try {
      await _userVoucherProvider.buyVoucher(
        dynamicRequest: {
          "userId": Session.userId,
          "voucherId": voucherId,
          "numberOfTokens": priceInTokens,
        },
      );

      await _loadUserTokens();

      final voucherResult = await _voucherProvider.get(
        filter: {"voucherId": voucherId},
      );

      final Voucher voucher = voucherResult.items.first;

      setState(() {
        unlockedVouchers[voucherId] = voucher.voucherCode;
        usedVouchers[voucherId] = false;
      });
    } catch (e) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text(e.toString())));
    }
  }

  @override
  Widget build(BuildContext context) {
    if (isLoading) {
      return const Scaffold(body: Center(child: CircularProgressIndicator()));
    }

    return Scaffold(
      body: CustomScrollView(
        slivers: [
          SljedecaDestinacijaIMenuBar(daLijeKliknuo: false),

          SliverToBoxAdapter(
            child: Padding(
              padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 30),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  Text(
                    "Kupite vaučere tokenima",
                    style: GoogleFonts.openSans(
                      fontSize: 22,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  const SizedBox(height: 8),
                  Text(
                    "Stanje: $userTokens TT",
                    style: GoogleFonts.openSans(
                      fontSize: 14,
                      fontWeight: FontWeight.w600,
                      color: Colors.grey[700],
                    ),
                  ),
                  const SizedBox(height: 30),

                  _voucherCard(
                    voucherId: 1,
                    title: "Vaučer 20% popusta",
                    price: 40,
                  ),
                  const SizedBox(height: 20),
                  _voucherCard(
                    voucherId: 2,
                    title: "Vaučer 50% popusta",
                    price: 80,
                  ),
                  const SizedBox(height: 20),
                  _voucherCard(
                    voucherId: 3,
                    title: "Vaučer 70% popusta",
                    price: 120,
                  ),
                ],
              ),
            ),
          ),

          SliverFillRemaining(hasScrollBody: false, child: eTravelFooter()),
        ],
      ),
    );
  }

  /// ============================
  /// VOUCHER CARD
  /// ============================
  Widget _voucherCard({
    required int voucherId,
    required String title,
    required int price,
  }) {
    final isUnlocked = unlockedVouchers.containsKey(voucherId);
    final isUsed = usedVouchers[voucherId] == true;

    return Container(
      padding: const EdgeInsets.all(18),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(20),
        border: Border.all(color: const Color(0xFF67B1E5), width: 1.5),
      ),
      child: Row(
        children: [
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Text(
                  title,
                  textAlign: TextAlign.center,
                  style: GoogleFonts.openSans(
                    fontSize: 14,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 12),
                Container(
                  padding: const EdgeInsets.symmetric(
                    horizontal: 14,
                    vertical: 10,
                  ),
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(25),
                    border: Border.all(color: Colors.grey),
                  ),
                  child: Column(
                    children: [
                      Text(
                        isUnlocked
                            ? unlockedVouchers[voucherId]!
                            : "**********",
                        style: TextStyle(
                          color: isUsed ? Colors.grey : Colors.black,
                          fontWeight: FontWeight.bold,
                          decoration:
                              isUsed
                                  ? TextDecoration.lineThrough
                                  : TextDecoration.none,
                        ),
                      ),
                    ],
                  ),
                ),
                if (isUsed)
                  const Padding(
                    padding: EdgeInsets.only(top: 4),
                    child: Text(
                      "KOD ISKORIŠTEN",
                      style: TextStyle(
                        color: Colors.red,
                        fontSize: 12,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),
              ],
            ),
          ),
          const SizedBox(width: 15),
          ElevatedButton(
            onPressed:
                isUnlocked || userTokens < price
                    ? null
                    : () =>
                        _buyVoucher(voucherId: voucherId, priceInTokens: price),
            style: ElevatedButton.styleFrom(
              backgroundColor: const Color(0xFF67B1E5),
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(8),
              ),
            ),
            child: Text(
              isUnlocked ? "Otkriveno" : "otkrijte za $price TT",
              style: const TextStyle(color: Colors.white),
            ),
          ),
        ],
      ),
    );
  }
}
