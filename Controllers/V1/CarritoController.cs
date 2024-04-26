using api_scango.Domain.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("v1/[controller]")]
public class CarritoController : ControllerBase
{
    private readonly CarritoService _carritoService;
    private readonly IMapper _mapper;

    public CarritoController(CarritoService carritoService, IMapper mapper)
    {
        _carritoService = carritoService;
        _mapper = mapper;
    }

    [HttpPost("AddProducto")]
    public async Task<IActionResult> AddProducto([FromBody] CarritoAddProducto modelo)
    {
        // try
        // {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        else
        {

        }

        await _carritoService.AddProducto(modelo.Numerodetelefono!, modelo.Codigodebarras!, modelo.idEstablecimiento!, modelo.cantidada);
        return Ok(new { message = "Producto agregado al carrito exitosamente" });
        // }
        // catch (Exception ex)
        // {
        //     // Loguea el error
        //     return StatusCode(500, new { error = "Error interno del servidor", message = ex.Message });
        // }
    }
    [HttpPost("AddFruta")]
    public async Task<IActionResult> AddFruta([FromBody] FrutaCarritoDTO modelo, string numerodetelefono)
    {
        // try
        // {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _carritoService.AddFruta(numerodetelefono, modelo.IdProducto!, modelo.PesoGramos!);
        return Ok(new { message = "Producto agregado al carrito exitosamente" });
        // }
        // catch (Exception ex)
        // {
        //     // Loguea el error
        //     return StatusCode(500, new { error = "Error interno del servidor", message = ex.Message });
        // }
    }

    [HttpGet("Productos")]
    public async Task<IActionResult> GetProductosCarrito(string numerodetelefono)
    {
        try
        {
            var productoEnCarrito = await _carritoService.GetProductos(numerodetelefono);
            var productosEnCarritoDto = _mapper.Map<List<ProductoEnCarritoDto>>(productoEnCarrito);

            return Ok(productosEnCarritoDto);
        }
        catch (Exception ex)
        {
            // Loguea el error
            return StatusCode(500, new { error = "Error interno del servidor", message = ex.Message });
        }
    }
    [HttpPost("Delete")]
    public async Task<ActionResult> Descontar(string numerodetelefono, string Codigodebarras)
    {
        await _carritoService.DeleteProduct(numerodetelefono, Codigodebarras);
        return Ok();
    }
    [HttpPost("VaciarCarro")]
    public async Task<ActionResult> VaciarCarro(string numerodetelefono)
    {
        await _carritoService.VaciarCarrito(numerodetelefono);
        return Ok();
    }
}
