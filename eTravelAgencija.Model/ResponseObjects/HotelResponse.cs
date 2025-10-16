using System.Collections.Generic;

namespace eTravelAgencija.Model.ResponseObjects
{ 
    public class HotelResponse
    {
        public int HotelId { get; set; }

        public string HotelName { get; set; }

        public int Stars { get; set; }

        public string MainImage { get; set; }

        public List<RoomResponse> Rooms { get; set; }
    }
}