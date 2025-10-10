using eTravelAgencija.Services.Database;

namespace eTravelAgencija.Services.Database
{
    public class OfferSubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OfferCategoryId { get; set; }
        public OfferCategory OfferCategory { get; set; }
    }
}   