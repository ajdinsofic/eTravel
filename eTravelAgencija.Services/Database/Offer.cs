using System.Collections.Generic;

namespace eTravelAgencija.Services.Database
{
    public class Offer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int DaysInTotal { get; set; }
        public int CategoryId { get; set; }
        public OfferCategory Category { get; set; }
        public ICollection<DepartureDate> DepartureDates { get; set; } = new List<DepartureDate>();
        public OfferDetails Details { get; set; }

    }
}
