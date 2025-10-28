using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class OfferAdminDetailResponse : BaseOfferDetailResponse
    {
        public string County { get; set; }

        public string City { get; set; }
    }
}