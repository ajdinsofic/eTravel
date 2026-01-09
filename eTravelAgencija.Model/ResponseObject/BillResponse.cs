using System;

namespace eTravelAgencija.Model.ResponseObject
{
    public class BillResponse
    {
        public int ReservationId { get; set; }
        public DateTime CreatedAt { get; set; }

        // 👤
        public string UserFullName { get; set; }

        // 🧳
        public string OfferTitle { get; set; }
        public string HotelName { get; set; }
        public int HotelStars { get; set; }
        public string RoomType { get; set; }

        // 💰
        public decimal TravelPrice { get; set; }
        public decimal ResidenceTax { get; set; }
        public decimal Insurance { get; set; }

        public bool IsDiscountUsed { get; set; }
        public double DiscountPercent { get; set; }

        public decimal Total { get; set; }
    }

}