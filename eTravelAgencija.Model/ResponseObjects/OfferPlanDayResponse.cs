using System.Collections.Generic;

namespace eTravelAgencija.Model.ResponseObjects
{ 
    public class OfferPlanDayResponse
    {
        public int DayNumber { get; set; } // Its gonna be like day 1,2,3,4 and we will add days in flutter app

        public string Title { get; set; }

        public string Description { get; set; }
    }
}