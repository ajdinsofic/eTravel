using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;

namespace eTravelAgencija.Model.model
{
    public class OfferCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<OfferSubCategory> SubCategories { get; set; } = new List<OfferSubCategory>();
    }
}