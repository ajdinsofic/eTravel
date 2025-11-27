using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eTravelAgencija.Model.RequestObjects
{
    public class OfferImageUpdateRequest
    {
        [Required]
        public int OfferId { get; set; }
        public bool IsMain { get; set; } = false;
    }
}