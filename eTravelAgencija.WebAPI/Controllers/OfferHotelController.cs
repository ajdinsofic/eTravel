using System;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class OfferHotelController : ControllerBase
    {
        private readonly IOfferHotelService _offerHotelService;

        public OfferHotelController(IOfferHotelService offerHotelService)
        {
            _offerHotelService = offerHotelService;
        }

        [HttpPost("link")]
        public async Task<ActionResult<OfferHotelResponse>> LinkHotelToOffer(int hotelId, int offerId, DateTime departureDate, DateTime returnDate)
        {
            var result = await _offerHotelService.LinkHotelToOffer(hotelId, offerId, departureDate, returnDate);
            return Ok(result);
        }

        [HttpPut("dates")]
        public async Task<ActionResult<OfferHotelResponse>> UpdateDates(int hotelId, int offerId, DateTime departureDate, DateTime returnDate)
        {
            var result = await _offerHotelService.PutOfferHotelDates(hotelId, offerId, departureDate, returnDate);
            return Ok(result);
        }
    }
}
