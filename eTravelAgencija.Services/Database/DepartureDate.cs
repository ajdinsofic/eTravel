using System;
using System.Collections.Generic;

namespace eTravelAgencija.Services.Database
{
    public class DepartureDate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
