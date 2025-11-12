using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.model
{
   public partial class ReservationPreview
   {
      public int UserId { get; set; }
      public int OfferId { get; set; }
      public int HotelId { get; set; }
      public int RoomId { get; set; }
      public string OfferTitle { get; set; }
      public string HotelTitle { get; set; }
      public string HotelStars { get; set; }
      public string RoomType { get; set; }
      public decimal BasePrice { get; set; }
      public decimal Insurance { get; set; }
      public decimal ResidenceTaxTotal { get; set; }
      public decimal TotalPrice { get; set; }
      public bool IncludeInsurance { get; set; }
      public string VoucherCode { get; set; }
   }

}