using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class HotelDetailsResponse
    {
        public string Address { get; set; }

        public List<string> HotelImages { get; set; }

    }
}