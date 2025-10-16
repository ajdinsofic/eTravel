using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class OfferResponse
    {
        public int Id { get; set; }
        public string OfferName { get; set; }

        public List<string> OfferImage { get; set; }

        public int DaysInTotal { get; set; }

        public int MinimalPrice { get; set; }

        public string WayOfTravel { get; set; } // By buss or by plane

    }
}