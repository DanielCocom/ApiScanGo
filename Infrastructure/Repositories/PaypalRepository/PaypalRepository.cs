
using System.Net.Http.Headers;
using Newtonsoft.Json;
using PayPalCheckoutSdk.Core;


namespace api_scango.Infrastructure.Data.Repositories;

public class PaypalRepository{

     private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    public PaypalRepository(IConfiguration configuration, IHttpClientFactory httpClientFactory){
        _clientId = configuration["PaypalSettings:ClientId"]!;
        _clientSecret = configuration["PaypalSettings:ClientSecret"]!;
         _httpClient = httpClientFactory.CreateClient();
         _httpClient.BaseAddress = new Uri("https://api-m.sandbox.paypal.com"); // url para desarrollo
         _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
     public async Task<string> GetAccessToken()
    {
        var credenciales = $"{_clientId}:{_clientSecret}"; // credebcuales de la peticion
        var requestBody = new List<KeyValuePair<string, string>>{
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        };
        using (var request = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress+"v1/oauth2/token"))
    {
        request.Content = new FormUrlEncodedContent(requestBody);
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credenciales); // faltaba el auth?

        using (var response = await _httpClient.SendAsync(request))
        {
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
                return tokenResponse.AccessToken;
            }
            else
            {
                throw new Exception("Failed to obtain access token from PayPal API");
            }
        }
    }
    }
}

internal class TokenResponse
{
    public string? AccessToken { get; set; }
}