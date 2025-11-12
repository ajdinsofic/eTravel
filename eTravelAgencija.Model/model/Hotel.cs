using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using eTravelAgencija.Model.model;

namespace eTravelAgencija.Model.model
{
    public partial class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Stars { get; set; }
        public decimal CalculatedPrice { get; set; }
        public virtual ICollection<HotelRooms> HotelRooms { get; set; } = new List<HotelRooms>();
        public virtual ICollection<HotelImages> HotelImages { get; set; } = new List<HotelImages>();
        public virtual ICollection<OfferHotels> OfferHotels { get; set; } = new List<OfferHotels>();
    }
}
