using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Services.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/paypal")]
public class PayPalController : ControllerBase
{
    private readonly PayPalService _paypal;

    public PayPalController(PayPalService paypal)
    {
        _paypal = paypal;
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder([FromBody] PayPalCreateRequest request)
    {
        var order = await _paypal.CreateOrder(request.Amount);
        return Ok(order);
    }

    [HttpPost("capture")]
    public async Task<IActionResult> Capture([FromBody] PayPalCaptureRequest request)
    {
        var result = await _paypal.CaptureOrder(request.OrderId);
        return Ok(result);
    }
}
