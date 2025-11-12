using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.RequestObjects
{
    public class OfferImageUpsertRequest
    {
        [Required]
        public int OfferId { get; set; }
        
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsMain { get; set; } = false;
    }
}