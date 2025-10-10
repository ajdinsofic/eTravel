using System.Collections.Generic;

namespace eTravelAgencija.Model.ResponseObjects
{ 
    public class OfferHotelResponse
    {
        public string HotelName { get; set; }

        public int Stars { get; set; }

        public string RoomType { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public List<string> HotelAssets { get; set; }
    }
}