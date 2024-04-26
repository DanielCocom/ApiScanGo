using System.Collections;
using api_scango.Domain.Dtos;
using api_scango.Domain.Entities.Dtos;
using api_scango.Services.Fetures.compra;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api_scango.Controllers;

[ApiController]
[Route("v1/[controller]")]

public class CompraController : ControllerBase
{

    private readonly CompraService _compraServices;
    private readonly IMapper _mapper;

    public CompraController(IMapper mapper, CompraService compraServices)
    {
        this._compraServices = compraServices;
        this._mapper = mapper;
    }
    [HttpPost("RealizarCompra")]
    public async Task<IActionResult> RealizarCompra(int idEstablecimiento, string numroTelefono, string idTransaccion)
    {
        // try
        // {
        var venta = await _compraServices.RealizarCompra(idEstablecimiento, numroTelefono, idTransaccion);
        return Ok("Compra realizada exitosamente");

        // }
        // catch (Exception ex)
        // {
        //     return BadRequest(new {erro = "Error al Realizar la compra", message = ex.Message});

        // }
    }
    [HttpGet("ComprasCliente")]
    public async Task<IActionResult> GetComprasCliente(string numeroTelefono)
    {
        try
        {
            var compras = await _compraServices.GetComprasCliente(numeroTelefono);
            var comprasDto = _mapper.Map<ICollection<CompraDTO>> (compras);
            return Ok(comprasDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new {error = "Error al obtener las compras del cliente", message = ex.Message});
        }
    }
}