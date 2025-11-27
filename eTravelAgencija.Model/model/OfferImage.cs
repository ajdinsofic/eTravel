using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eTravelAgencija.Model.model
{
    public partial class OfferImage
    {
        public int Id { get; set; }
        public int OfferId { get; set; }  
        public string ImageUrl { get; set; }
        public bool isMain { get; set; }
    }
}