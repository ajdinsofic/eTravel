namespace eTravelAgencija.Models.Requests
{
    public class BillRequest
    {
        // 🔑 Obavezno – veza sa bazom
        public int ReservationId { get; set; }

        // 👤 Korisnik
        public string UserFullName { get; set; } = string.Empty;

        // 🧳 Putovanje / ponuda
        public string OfferTitle { get; set; } = string.Empty;

        // 🏨 Hotel
        public string HotelName { get; set; } = string.Empty;
        public int HotelStars { get; set; }

        // 🛏️ Soba
        public string RoomType { get; set; } = string.Empty;
    }
}
