using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class OfferUserResponce : BaseOfferResponse
   {
       public OfferSubCategoryResponse SubCategoryName { get; set; }
       public OfferCategoryResponse CategoryName { get; set; }

   }

}