using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace eTravelAgencija.Model.RequestObjects
{
    public class OfferUpdateRequest
    {
        public string Title { get; set; }
        public int DaysInTotal { get; set; }
        public string WayOfTravel { get; set; }
        public decimal MinimalPrice { get; set; }
        public decimal TravelInsuranceTotal { get; set; }
        public decimal ResidenceTotal { get; set; }
        public decimal ResidenceTaxPerDay { get; set; }  
        public int SubCategoryId { get; set; } = -1;
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}