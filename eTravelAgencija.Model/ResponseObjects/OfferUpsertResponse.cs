using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class OfferUpsertResponse
    {
        public int Id { get; set; }
        public string OfferName { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int DaysInTotal { get; set; }
        public string WayOfTravel { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}