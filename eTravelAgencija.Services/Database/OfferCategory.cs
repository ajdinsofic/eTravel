using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTravelAgencija.Services.Database
{
    public class OfferCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<OfferSubCategory> SubCategories { get; set; } = new List<OfferSubCategory>();
    }
}
