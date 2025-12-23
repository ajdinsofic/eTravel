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

        [HttpGet("recommended/OffersForUser/{userId}")]
        public IActionResult GetRecommendedForUser(int userId)
        {
            var result = (_service as IOfferService)
                .RecommendOffersForUser(userId);

            return Ok(result);
        }


        [HttpPut("{offerId}/increaseTotalReservation")]
        public async Task<IActionResult> IncreaseTotalReservation(int offerId)
        {
            await (_service as IOfferService).IncreaseTotalReservation(offerId);
            return Ok(new { message = "TotalCountOfReservations povećan." });
        }

        [HttpPut("{offerId}/decreaseTotalReservation")]
        public async Task<IActionResult> DecreaseTotalReservation(int offerId)
        {
            await (_service as IOfferService).DecreaseTotalReservation(offerId);
            return Ok(new { message = "TotalCountOfReservations smanjen." });
        }

    }
}




