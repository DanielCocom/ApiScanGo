using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/paypal")]
public class PaypalController : ControllerBase
{
    private readonly PaypalService _paypalService;

    public PaypalController(PaypalService paypalService)
    {
        _paypalService = paypalService;
    }

    [HttpGet("token")]
    public async Task<IActionResult> GetAccessToken()
    {
            var accessToken = await _paypalService.GetAccessToken();
            return Ok(new { AccessToken = accessToken });
       
    }
}
