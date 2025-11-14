using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.model
{
    public partial class Offer
    {
        public int OfferId { get; set; }
        public string Title { get; set; }
        public int DaysInTotal { get; set; }
        public string WayOfTravel { get; set; }
        public int SubCategoryId { get; set; }
        public OfferSubCategory SubCategory { get; set;}
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public decimal MinimalPrice { get; set; }
        public decimal ResidenceTaxPerDay { get; set; }  
        public decimal TravelInsuranceTotal { get; set; }
        public decimal ResidenceTotal { get; set; }
        public virtual ICollection<OfferImage> OfferImages { get; set; } = new List<OfferImage>();
        public virtual ICollection<OfferPlanDay> OfferPlanDays { get; set; } = new List<OfferPlanDay>();
        public virtual ICollection<OfferHotels> OfferHotels { get; set; } = new List<OfferHotels>();
    }
}