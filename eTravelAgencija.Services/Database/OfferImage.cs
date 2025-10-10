using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTravelAgencija.Services.Database
{

    public class OfferImage
    {
        [Key]
        public int Id { get; set; }
        public int OfferId { get; set; }
        public OfferDetails OfferDetails { get; set; }
        public string ImageUrl { get; set; }
    }

    
}
