using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eTravelAgencija.Services.Database;

namespace eTravelAgencija.Services.Database
{
    public class Offer
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int DaysInTotal { get; set; }
        [Required]
        public string WayOfTravel { get; set; }
        [ForeignKey(nameof(SubCategory))]
        public int SubCategoryId { get; set; }
        public OfferSubCategory SubCategory { get; set; }
        public OfferDetails Details { get; set; }
    }

}

