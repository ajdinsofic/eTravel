using System;
using System.Collections.Generic;
using System.Reflection;

namespace eTravelAgencija.Model.RequestObjects
{
    public class OfferRequest
    {
        public string OfferName { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public List<DateTime> DepartureDates { get; set; }

        public List<
    }
}