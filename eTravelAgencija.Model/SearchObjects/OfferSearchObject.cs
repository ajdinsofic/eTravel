using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.SearchObjects
{
    public class OfferSearchObject : BaseSearchObject
    {
        public int SubCategoryId { get; set; } = -1;
        public int CategoryId { get; set; }

    }
}