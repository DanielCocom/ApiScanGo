using Microsoft.AspNetCore.Mvc;
using api_scango.Domain.Entities;
using AutoMapper;
using api_scango.Domain.Dtos;
using System;
using System.Threading.Tasks;
using api_scango.Services.Fetures.producto;
using api_scango.Services.stripe;

namespace api_scango.Controllers.V1;
[ApiController]
[Route("v1/[controller]")]
public class StripeController : ControllerBase
{
    private readonly StripeServices _services;

    public StripeController(StripeServices stripeServices)
    {
        _services = stripeServices;
    }
    [HttpPost()]
    public async Task<IActionResult> CheckoutProducts(string numeroTelefono, int idEstablecimiento)
    {
        try
        {
            var urlCheckOut = await _services.getCheckout(numeroTelefono, idEstablecimiento);
            return Ok(urlCheckOut);

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", message = ex.Message });
        }
    }
}