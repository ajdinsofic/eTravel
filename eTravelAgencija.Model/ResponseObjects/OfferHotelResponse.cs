using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class OfferHotelResponse
    {
        public int OfferId { get; set; }
        public int HotelId { get; set; }
        public DateTime Departuredate { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}