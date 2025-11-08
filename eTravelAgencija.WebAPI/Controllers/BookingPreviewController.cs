using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Services.Services;

namespace eTravelAgencija.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingPreviewController : ControllerBase
    {
        private readonly IBookingPreviewService _bookingService;

        public BookingPreviewController(IBookingPreviewService bookingService)
        {
            _bookingService = bookingService;
        }

        
        [HttpGet("preview")]
        public async Task<ActionResult<BookingPreviewResponse>> GetBookingPreview([FromQuery] BookingPreviewRequest request)
        {
            if (request == null)
                return BadRequest("Neispravan zahtjev.");

            try
            {
                var result = await _bookingService.GetBookingPreviewAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
