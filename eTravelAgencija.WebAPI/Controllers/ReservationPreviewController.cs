using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using eTravelAgencija.Services.Services;
using eTravelAgencija.Model.RequestObjects;

namespace eTravelAgencija.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}
