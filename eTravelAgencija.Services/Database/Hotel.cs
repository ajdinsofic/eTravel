using System.Collections.Generic;
using eTravelAgencija.Services.Database;

namespace eTravelAgencija.Services.Database
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Stars { get; set; }
        public ICollection<HotelRooms> Rooms { get; set; } = new List<HotelRooms>();
        public ICollection<HotelImages> HotelImages { get; set; } = new List<HotelImages>();
        public ICollection<OfferHotels> OfferHotels { get; set; } = new List<OfferHotels>();
    }

    
}