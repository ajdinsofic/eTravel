using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.RequestObjects
{
    public class ReservationPreviewRequest
    {
        public int UserId { get; set; }
        public int OfferId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        // public string OfferTitle { get; set; }
        // public string HotelTitle { get; set; }
        // public string HotelStars { get; set; }
        // public string RoomType { get; set; }
        public decimal BasePrice { get; set; }
        // Za Resindence i insurance ce pisati "Boravisna taksa" i "Putnicko zdravstveno osiguranje -> ovdje ce biti i checkmark"
        // public decimal ResidenceTaxTotal { get; set; }
        // public decimal Insurance { get; set; } 
        public bool IncludeInsurance { get; set; }
        public string VoucherCode { get; set; }
         
    }
}