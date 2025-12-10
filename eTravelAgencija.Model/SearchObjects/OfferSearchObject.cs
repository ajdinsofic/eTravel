using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.SearchObjects
{
    public class OfferSearchObject : BaseSearchObject
    {
        public int? offerId { get; set; }
        public int SubCategoryId { get; set; }
        public bool isMainImage { get; set; } = false;
        public bool isOfferHotels { get; set; } = false;
        public bool isOfferPlanDays { get; set; } = false;
        public string? FTS { get; set; }

    }
}