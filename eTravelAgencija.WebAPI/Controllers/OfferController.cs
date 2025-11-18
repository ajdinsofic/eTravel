using eTravelAgencija.Model.model;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.RequestObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eTravelAgencija.Services.Services;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.WebAPI.Controllers
{

    public class OfferController 
        : BaseCRUDController<
            Offer,
            OfferSearchObject,
            OfferInsertRequest,
            OfferUpdateRequest>
    {

        public OfferController(
            ILogger<BaseCRUDController<Offer, OfferSearchObject, OfferInsertRequest, OfferUpdateRequest>> logger,
            IOfferService offerService)
            : base(logger, offerService)
        {
            
        }

        [HttpGet("{offerId}/recommended")]
        public IActionResult GetRecommendedOffers(int offerId)
        {
            var result = (_service as IOfferService).RecommendOffers(offerId);
            return Ok(result);
        }

    }
}




