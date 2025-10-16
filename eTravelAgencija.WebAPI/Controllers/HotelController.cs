using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eTravelAgencija.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("{hotelId}/{offerId}")]
        public Task<HotelDetailsResponse> GetHotelById(int hotelId, int offerId)
        {
            var hotel = _hotelService.GetHotelById(hotelId, offerId);

            if (hotel == null)
            {
                return null;
            }

            return hotel;
        }

        [HttpGet("search")]

        public Task<List<HotelResponse>> GetHotel(HotelSearchObject search)
        {
            var hotels = _hotelService.GetHotel(search);

            if (hotels == null)
            {
                return null;
            }

            return hotels;
        }
    }
}