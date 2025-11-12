using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTravelAgencija.Services.Database
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public int Stars { get; set; }

        public decimal CalculatedPrice { get; set; }

        // Jedan hotel može imati više soba
        public ICollection<HotelRooms> HotelRooms { get; set; } = new List<HotelRooms>();

        // Jedan hotel može imati više slika
        public ICollection<HotelImages> HotelImages { get; set; } = new List<HotelImages>();

        // Veza sa ponudama u kojima se hotel koristi
        public ICollection<OfferHotels> OfferHotels { get; set; } = new List<OfferHotels>();
    }
}
