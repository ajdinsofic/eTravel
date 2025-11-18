using System;

namespace eTravelAgencija.Services.Recommendation
{
    public class HotelEntry
    {
        public uint HotelID { get; set; }
        public uint CoHotelID { get; set; }
        public float Label { get; set; } = 1;
    }
}
