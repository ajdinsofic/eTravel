using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.SearchObjects
{
    public class HotelSearchObject
    {
        [Required]
        public int OfferId { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }
        [Required]
        public int RoomId { get; set; }


    }
}