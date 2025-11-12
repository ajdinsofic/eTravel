using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTravelAgencija.Services.Database
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OfferId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public decimal BookedPrice { get; set; }
        public bool IsActive { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public string addedNeeds { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        // OfferTitle u offer servisu
        // Boravisnu i putno osiguranje offerDetails servis
        // HotelName u hotel servisu
        // RoomType preko _contexta
        // Datume preko OfferHotel servisa;
    }

}
