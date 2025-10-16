using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eTravelAgencija.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpGet("specijalne-ponude")]
        public async Task<IActionResult> GetSpecialOffers([FromQuery] OfferCategoryAndSubcategoryRequest request)
        {
            var offers = await _offerService.GetSpecialOffers(request);

            return Ok(offers);
        }

        [HttpGet("praznicna-putovanja")]

        public async Task<IActionResult> GetHolidayOffers(OfferCategoryAndSubcategoryRequest request)
        {
            var offers = await _offerService.GetHolidayOffers(request);

            return Ok(offers);
        }

        [HttpGet("osjetite-svaki-mjesec")]

        public async Task<IActionResult> GetFeelTheMonth(OfferCategoryAndSubcategoryRequest request)
        {
            var offers = await _offerService.GetFeelTheMonth(request);

            return Ok(offers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfferById(int id)
        {
            var offer = await _offerService.GetOfferById(id);

            if (offer == null)
            {
                return NotFound();
            }

            return Ok(offer);
        }



    }
}