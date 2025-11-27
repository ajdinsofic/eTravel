// import 'package:etravel_desktop/providers/offer_provider.dart';
// import 'package:etravel_desktop/providers/hotel_provider.dart';

// class OfferCleanupHelper {
//   static final OfferProvider _offerProvider = OfferProvider();
//   static final HotelProvider _hotelProvider = HotelProvider();

//   /// Obriši sve hotele i ponudu jer je korisnik napustio kreiranje
//   static Future<void> cleanupOffer(int offerId) async {
//     try {
//       // 1. Dohvati ponudu sa OfferHotels
//       final offers = await _offerProvider.getOffers(
//         page: 0,
//         pageSize: 1,
//         isMainImage: false,
//         isOfferHotels: true,
//       );

//       final offer = offers.items.firstWhere(
//         (o) => o.offerId == offerId,
//         orElse: () => throw Exception("Ponuda ne postoji."),
//       );

//       // 2. Ako ponuda nema hotela → nema šta brisati
//       if (offer.offerHotels.isEmpty) {
//         await _offerProvider.deleteOffer(offerId);
//         return;
//       }

//       // 3. Obriši sve hotele (cascade)
//       for (var oh in offer.offerHotels) {
//         await _hotelProvider.deleteHotel(oh.hotelId);
//       }

//       // 4. Obriši ponudu
//       await _offerProvider.deleteOffer(offerId);
//     } catch (e) {
//       print("❌ cleanupOffer greška: $e");
//     }
//   }
// }
