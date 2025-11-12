using System;
using System.ComponentModel.DataAnnotations;

namespace eTravelAgencija.Model.RequestObjects
{
    public class OfferHotelUpsertRequest
    {
        [Required]
        public int OfferId { get; set; }

        [Required]
        public int HotelId { get; set; }

        public DateTime? DepartureDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
