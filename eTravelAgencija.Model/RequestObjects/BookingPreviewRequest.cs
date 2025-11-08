using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.RequestObjects
{
    public class BookingPreviewRequest
    {
        [Required]
        public int OfferId { get; set; }
        [Required]
        public int HotelId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public decimal CalculatedPrice { get; set; }
        public bool IncludeInsurance { get; set; } = true;
    }
}