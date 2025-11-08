using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTravelAgencija.Services.Database
{
    public class OfferDetails
    {
        [Key]
        [ForeignKey(nameof(Offer))]  // Ova anotacija pokazuje da je OfferId i PK i FK ka Offer (jedan-na-jedan veza)
        public int OfferId { get; set; }

        public Offer Offer { get; set; }

        [Required]
        public string Description { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MinimalPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ResidenceTaxPerDay { get; set; }  // Boravišna taksa po danu
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TravelInsuranceTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ResidenceTotal { get; set; }

        public ICollection<OfferImage> OfferImages { get; set; } = new List<OfferImage>();

        public ICollection<OfferPlanDay> OfferPlanDays { get; set; } = new List<OfferPlanDay>();

        public ICollection<OfferHotels> OfferHotels { get; set; } = new List<OfferHotels>();
    }
}
