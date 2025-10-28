using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class BaseOfferResponse
    {
        public int Id { get; set; }
        public string OfferName { get; set; }
        public string MainImage { get; set; }
    }
}