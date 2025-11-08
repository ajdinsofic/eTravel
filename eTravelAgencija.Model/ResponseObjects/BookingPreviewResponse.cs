using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObjects
{
   public class BookingPreviewResponse
    {
        
        public string OfferTitle { get; set; }
        public string HotelTitle { get; set; }
        public string HotelMainImage { get; set; }
        public string RoomName { get; set; }
        public int HotelStars { get; set; }
        public decimal BasePrice { get; set; }                
        public decimal ResidenceTaxTotal { get; set; }        
        public decimal TravelInsurance { get; set; }          
        public bool IncludeInsurance { get; set; }           
        public decimal TotalPrice { get; set; }
    }
}