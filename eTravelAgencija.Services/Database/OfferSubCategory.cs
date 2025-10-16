using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTravelAgencija.Services.Database
{
    public class OfferSubCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Foreign Key ka OfferCategory
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public OfferCategory Category { get; set; }

        public ICollection<Offer> Offers { get; set; } = new List<Offer>();
    }
}
