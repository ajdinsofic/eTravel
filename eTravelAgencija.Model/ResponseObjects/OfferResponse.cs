using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class OfferResponse
    {
        public string OfferName { get; set; }

        public string Image { get; set; }

        public int DaysInTotal { get; set; }

        public int MinimalPrice { get; set; }

        public string WayOfTravel { get; set; } // By buss or by plane

        public List<DateTime> DepartureDates { get; set; } // Dates of going to a trip

    }
}