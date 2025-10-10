using System.Collections.Generic;

namespace eTravelAgencija.Services.Database
{
    public class OfferPlanDay
    {
        public int Id { get; set; }
        public int OfferDetailsId { get; set; }
        public OfferDetails OfferDetails { get; set; }
        public int DayNumber { get; set; }
        public string DayDescription { get; set; }
    }
}
