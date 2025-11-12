using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.SearchObjects
{
    public class HotelSearchObject : BaseSearchObject
    {
        [Required]
        public int? OfferId { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int? RoomId { get; set; }
        public bool isMainImage { get; set; } = true;
    }
}