using api_scango.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using PayPalCheckoutSdk.Core;
using PayPalHttp;


// public class PaypalServices{
//     private readonly string _clientId;
//     private readonly string _clientSecret;
//     public PaypalServices(IConfiguration configuration){
//         _clientId = configuration["Paypal:ClientId"]!;
//         _clientSecret = configuration["Paypal:ClientSecret"]!;

//     }
//     // devuelve la instancia con mis credenciales, para poder hacer peticiones a la api paypal
//      public PayPalHttpClient GetPayPalHttpClient()
//     {
//         return new PayPalHttpClient(new SandboxEnvironment(_clientId, _clientSecret));
//     }
// }

// IPaypalService.cs


// PaypalService.cs
public class PaypalService 
{
    private readonly PaypalRepository _paypalRepository;

    public PaypalService(PaypalRepository paypalRepository)
    {
        _paypalRepository = paypalRepository;
    }

    public async Task<string> GetAccessToken()
    {
        return await _paypalRepository.GetAccessToken();
    }

    // Implementa los métodos CreateOrder y CaptureOrder aquí...
}
