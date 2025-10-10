using System.Collections.Generic;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class OfferDetailsResponse : OfferResponse
    {
       public string Description { get; set; }

       public List<OfferHotelResponse> Hotels { get; set; }

       public List<OfferPlanDayResponse> TravelPlan { get; set; }
    }
}