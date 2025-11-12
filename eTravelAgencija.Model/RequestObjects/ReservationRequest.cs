using System;
using System.ComponentModel.DataAnnotations;

namespace eTravelAgencija.Model.RequestObjects
{
    public class ReservationRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int OfferId { get; set; }

        [Required]
        public int HotelId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "BookedPrice mora biti veći od 0.")]
        public decimal BookedPrice { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalPrice mora biti veći od 0.")]
        public decimal TotalPrice { get; set; }

        public string AddedNeeds { get; set; } // npr. "Dječiji krevetić, pogled na more"

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }
    }
}
