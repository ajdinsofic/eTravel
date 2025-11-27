using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.WebAPI.Controllers
{
    public class OfferCategoryController : BaseController<Model.model.OfferCategory, BaseSearchObject>
    {
        public OfferCategoryController(ILogger<BaseController<Model.model.OfferCategory, BaseSearchObject>> logger, IOfferCategoryService offerCategoryService) : base(logger, offerCategoryService)
        {
            
        }
    }
}