using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eTravelAgencija.Services.Services;
using eTravelAgencija.Model.RequestObjects;
using Microsoft.AspNetCore.Authorization;

namespace eTravelAgencija.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferPlanDayController : ControllerBase
    {
        private readonly IOfferPlanDayService _offerPlanDayService;

        public OfferPlanDayController(IOfferPlanDayService offerPlanDayService)
        {
            _offerPlanDayService = offerPlanDayService;
        }

        // ✅ GET: api/OfferPlanDay/{offerId}
        [HttpGet("{offerId}")]
        public async Task<IActionResult> GetByOfferId(int offerId)
        {
            var result = await _offerPlanDayService.GetOfferPlanDaysAsync(offerId);
            return Ok(result);
        }

        // ✅ GET: api/OfferPlanDay/{offerId}/{dayNumber}
        [HttpGet("{offerId}/{dayNumber}")]
        public async Task<IActionResult> GetByKey(int offerId, int dayNumber)
        {
            var result = await _offerPlanDayService.GetByKeyAsync(offerId, dayNumber);
            if (result == null)
                return NotFound($"Dan {dayNumber} za ponudu {offerId} nije pronađen.");
            return Ok(result);
        }

        // ✅ POST: api/OfferPlanDay
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Insert([FromBody] OfferPlanDayUpsertRequest request)
        {
            try
            {
                var result = await _offerPlanDayService.InsertAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        
        [HttpPut("{offerId}/{dayNumber}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int offerId, int dayNumber, [FromBody] OfferPlanDayUpsertRequest request)
        {
            try
            {
                var result = await _offerPlanDayService.UpdateAsync(offerId, dayNumber, request);
                if (result == null)
                    return NotFound($"Dan {dayNumber} za ponudu {offerId} nije pronađen.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ✅ DELETE: api/OfferPlanDay/{offerId}/{dayNumber}
        [HttpDelete("{offerId}/{dayNumber}")]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int offerId, int dayNumber)
        {
            var deleted = await _offerPlanDayService.DeleteAsync(offerId, dayNumber);
            if (!deleted)
                return NotFound($"Dan {dayNumber} za ponudu {offerId} nije pronađen.");
            return Ok(new { message = "Dan uspješno obrisan." });
        }
    }
}
