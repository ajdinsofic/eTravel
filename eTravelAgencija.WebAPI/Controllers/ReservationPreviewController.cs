using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using eTravelAgencija.Services.Services;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.WebAPI.Controllers
{

    public class ReservationPreviewController : ControllerBase
    {
        private readonly IResevationPreviewService _reservationPreviewService;

        public ReservationPreviewController(IResevationPreviewService reservationPreviewService)
        {
            _reservationPreviewService = reservationPreviewService;
        }


        [Authorize(Roles = "Korisnik")]
        [HttpGet("generate")]
        public async Task<IActionResult> GeneratePreview([FromQuery] ReservationPreviewRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var preview = await _reservationPreviewService.GeneratePreviewAsync(request);
                return Ok(preview);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Korisnik")]
        [HttpGet("check-rate-payment")]
        public async Task<bool> CheckRatePayment(int HotelId)
        {
            var check = await _reservationPreviewService.ApprovingRatePayment(HotelId);
            return check;
        }
    }
}
