using api_scango.Domain.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("v1/[controller]")]

public class VentaController : ControllerBase
{
    private readonly VentaServices _ventaServices;
    private readonly IMapper _mapper;

    public VentaController(VentaServices ventaServices, IMapper mapper)
    {
        _ventaServices = ventaServices;
        _mapper = mapper;
    }
    [HttpGet("Ventas")]
    public async Task<IActionResult> GetAllVentas()
    {
        var ventas = await _ventaServices.getAll();
        return Ok(ventas);
    }
    [HttpGet("ProductoMasVendido")]
    public async Task<ActionResult> GetProductosMasVendidos(int idEstablecimiento)
    {
        var productosVendidos = await _ventaServices.getProductosMasVendidos(idEstablecimiento);
        return Ok(productosVendidos);
    }
    [HttpGet("VentasTienda")]
    public async Task<ActionResult> GetVentasEstablecimiento(int idEstablecimiento)
    {
        var ventasTienda = await _ventaServices.getVentasEstablecimientoId(idEstablecimiento);
        return Ok(ventasTienda);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetDetalleVentaId(int id)
    {
        var detalleVenta = await _ventaServices.GetDetalleVentaId(id);
        var dto = _mapper.Map<DetalleVentaDTO>(detalleVenta);
        return Ok(dto);
    }
    [HttpGet("ProductoMenosVendido")] 
    public async Task<IActionResult> GetProductoMenosVendido(int idEstablecimiento)
    {
        var productosVendidos = await _ventaServices.getProductosMenosVendidos(idEstablecimiento);
        return Ok(productosVendidos);
    } 
    [HttpGet("UltimasVentas")] 
    public async Task<IActionResult> GetUltimasVentas(int idEstablecimiento)
    {
        var ultimasVentas = await _ventaServices.GetLastFiveSale(idEstablecimiento);


        return Ok(ultimasVentas);
    } 
     [HttpGet("VentasPorMesTienda")] 
    public async Task<IActionResult> GetSalesPerMonthStore(int idEstablecimiento)
    {
        var ultimasVentas = await _ventaServices.GetSalesPerMonthStore(idEstablecimiento);


        return Ok(ultimasVentas);
    } 


}