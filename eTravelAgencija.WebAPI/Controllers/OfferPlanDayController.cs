using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model.model;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;
using eTravelAgencija.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.WebAPI.Controllers
{
    public class OfferPlanDayController : BaseCRUDController<Model.model.OfferPlanDay,OfferPlanDaySearchObject,OfferPlanDayInsertRequest,OfferPlanDayUpdateRequest>
    {
        public OfferPlanDayController(ILogger<BaseCRUDController<Model.model.OfferPlanDay,OfferPlanDaySearchObject,OfferPlanDayInsertRequest,OfferPlanDayUpdateRequest>> logger,
            IOfferPlanDayService offerPlanDayService): base(logger, offerPlanDayService)
        {
        }

        public override Task<ActionResult<OfferPlanDay>> GetById(int id)
        {
            return Task.FromResult<ActionResult<Model.model.OfferPlanDay>>(NotFound());
        }

        [Authorize(Roles = "Korisnik,Radnik,Direktor")]
        [HttpGet("{offerDetailsId}/{dayNumber}")]
        public async Task<Model.model.OfferPlanDay?> GetByCompositeKey(int offerDetailsId, int dayNumber)
        {
            return await _service.GetByCompositeKeysAsync(offerDetailsId, dayNumber);
        }

        [Authorize(Roles = "Radnik,Direktor")]
        [HttpPut("{offerDetailsId}/{dayNumber}")]
        public async Task<Model.model.OfferPlanDay?> UpdateByCompositeKey(int offerDetailsId, int dayNumber, OfferPlanDayUpdateRequest plan)
        {
            return await _service.UpdateCompositeAsync([offerDetailsId,dayNumber], plan);
        }

        [Authorize(Roles = "Radnik,Direktor")]
        [HttpDelete("{offerDetailsId}/{dayNumber}")]
        public async Task<bool> DeleteByCompositeKey(int offerDetailsId, int dayNumber)
        {
            return await _service.DeleteCompositeAsync(offerDetailsId, dayNumber);
        }
    }
}