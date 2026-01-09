using eTravelAgencija.Model.model;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.RequestObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eTravelAgencija.Services.Services;
using eTravelAgencija.Services.Interfaces;
using eTravelAgencija.Model;

namespace eTravelAgencija.WebAPI.Controllers
{

    public class HotelController 
        : BaseCRUDController<
            Hotel,
            HotelSearchObject,
            HotelUpsertRequest,
            HotelUpsertRequest>
    {


        public HotelController(
            ILogger<BaseCRUDController<Hotel, HotelSearchObject, HotelUpsertRequest, HotelUpsertRequest>> logger,
            IHotelService hotelService)
            : base(logger, hotelService)
        {
            
        }

        [HttpGet("offers/{offerId}/recommendedHotel")]
        public async Task<ActionResult<RecommendedHotelRoomDto>> GetRecommendedHotelRoomForOffer(
            int offerId,
            [FromQuery] int userId)
        {
            var result = await (_service as IHotelService).RecommendHotelRoomForOfferAsync(offerId, userId);
            if (result == null) return NotFound("No suitable hotel/room found for this offer.");
            return Ok(result);
        }

    }
}
