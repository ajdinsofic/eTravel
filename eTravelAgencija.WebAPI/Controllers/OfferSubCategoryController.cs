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
    public class OfferSubCategoryController : BaseController<Model.model.OfferSubCategory, OfferSubCategorySearchObject>
    {
        public OfferSubCategoryController(ILogger<BaseController<Model.model.OfferSubCategory, OfferSubCategorySearchObject>> logger, IOfferSubCategoryService offerSubCategoryService) : base(logger, offerSubCategoryService)
        {
            
        }
    }
}