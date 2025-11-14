using System;
using System.ComponentModel.DataAnnotations;

namespace eTravelAgencija.Model.RequestObjects
{
    public class ReservationUpsertRequest
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
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalPrice mora biti veći od 0.")]
        public decimal TotalPrice { get; set; }

        public string AddedNeeds { get; set; } 

        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IncludeInsurance { get; set; }
        [Required]
        public bool isFirstRatePaid { get; set;}
        [Required]
        public bool isFullPaid { get; set;}
        [Required]
        public decimal PriceLeftToPay { get; set; }
    }
}
