using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Services;

namespace eTravelAgencija.WebAPI.Controllers
{
    public class OfferPlanDayController : BaseCRUDController<Model.model.OfferPlanDay,OfferPlanDaySearchObject,OfferPlanDayUpsertRequest,OfferPlanDayUpsertRequest>
    {
        public OfferPlanDayController(ILogger<BaseCRUDController<Model.model.OfferPlanDay,OfferPlanDaySearchObject,OfferPlanDayUpsertRequest,OfferPlanDayUpsertRequest>> logger,
            IOfferPlanDayService offerPlanDayService): base(logger, offerPlanDayService)
        {
            
        }
    }
}