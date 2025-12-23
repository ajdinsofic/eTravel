using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;
using eTravelAgencija.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserVoucherController : BaseController<Model.model.UserVoucher, UserVoucherSearchObject>
    {

        public UserVoucherController(ILogger<BaseController<Model.model.UserVoucher, UserVoucherSearchObject>> logger, IUserVoucherService userVoucherService)
        :base(logger, userVoucherService)
        {
            
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyVoucher([FromBody] BuyVoucherRequest request)
        {
            await (_service as IUserVoucherService).BuyVoucherAsync(request);
            return Ok(new { message = "Vaučer uspješno kupljen." });
        }

        [HttpPut("mark-as-used")]
    public async Task<IActionResult> MarkAsUsed(
        [FromBody] MarkVoucherUsedRequest request)
    {
        try
        {
            await (_service as IUserVoucherService).MarkAsUsed(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    }
}
