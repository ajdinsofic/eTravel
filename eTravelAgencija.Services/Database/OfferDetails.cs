using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTravelAgencija.Services.Database
{

    public class OfferDetails
    {
        [Key]
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
        public string Description { get; set; }
        public ICollection<OfferImage> OfferImages { get; set; }
        public ICollection<OfferPlanDay> OfferTravelPlan { get; set; }
        public ICollection<OfferHotels> OfferHotels { get; set; }
    }

}
