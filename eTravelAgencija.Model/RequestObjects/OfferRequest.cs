using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace eTravelAgencija.Model.RequestObjects
{
    public class OfferRequest
{
    public string Title { get; set; }
    public decimal minimalPrice { get; set; }
    public int DaysInTotal { get; set; }
    public string WayOfTravel { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public int SubCategoryId { get; set; } = -1;
    public string Description { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
}
}