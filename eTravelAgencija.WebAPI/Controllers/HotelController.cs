using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.WebAPI.Controllers
{

    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("user")]
        public async Task<ActionResult<PagedResult<HotelResponse>>> GetHotelsForUser([FromQuery] HotelUserSearchObject search)
        {
            var result = await _hotelService.GetHotelsForUserBySearch(search);
            return Ok(result);
        }

        [HttpGet("admin")]
        public async Task<ActionResult<PagedResult<HotelResponse>>> GetHotelsForAdmin([FromQuery] HotelAdminSearchObject search)
        {
            var result = await _hotelService.GetHotelsForAdminByOfferId(search);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<HotelResponse>> CreateHotel([FromBody] HotelUpsertRequest request)
        {
            var created = await _hotelService.PostHotel(request);
            return CreatedAtAction(nameof(CreateHotel), new { id = created.HotelId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HotelResponse>> UpdateHotel(int id, [FromBody] HotelUpsertRequest request)
        {
            var updated = await _hotelService.PutHotel(id, request);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var result = await _hotelService.DeleteHotel(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
