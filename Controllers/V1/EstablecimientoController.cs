using System;
using System.Threading.Tasks;
using api_scango.Domain.Dtos;
using api_scango.Domain.Entities;
using api_scango.Domain.Services;
using api_scango.Services.Fetures.establecimiento;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api_scango.Controllers;

[ApiController]
[Route("v1/Establecimiento")]
public class EstablecimientoController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly EstablecimientoService _service;

    public EstablecimientoController(EstablecimientoService establecimientoService, IMapper mapper)
    {
        this._mapper = mapper;
        this._service = establecimientoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var establecimiento = await _service.getAll();
        var establecimientoDto = _mapper.Map<IEnumerable<EstablecimientoDTO>>(establecimiento);

        return Ok(establecimientoDto);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var esta = await _service.GetById(id);
        if (esta.IdEstablecimiento <= 0)
        {
            return NotFound();

        }
        var dto = _mapper.Map<EstablecimientoDTO>(esta);
        return Ok(dto);
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] EstablecimientoCreateDTO establecimientoDTO, IFormFile file)
    {
        var entity = _mapper.Map<Establecimiento>(establecimientoDTO);
        await _service.Add(entity, file);

        var dto = _mapper.Map<EstablecimientoCreateDTO>(entity);
        // return CreatedAtAction(nameof(GetById), new { id = entity.IdEstablecimiento }, dto);
        return Ok(dto);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,[FromForm] EstablecimientoUpdateDTO tienda, IFormFile image)
    {

        if (id != tienda.id)
        {
            return BadRequest();
        }

        await _service.Update(tienda, image);
        return NoContent();
    }

    //FIXME: NO FUNCIONA ESTA MADRE Y UPDATE TAMPOCO PROBALEMENTE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return NoContent();
    }
    [HttpGet("Inventario")]
    public async Task<IActionResult> GetInventario(int idEstablecimiento)
    {
        var productosEnInventario = await _service.Getinventario(idEstablecimiento);
        return Ok(productosEnInventario);

    }
    [HttpGet("BuscarProducto")]
    public async Task<IActionResult> SearchProduct(string value, int idEstablecimiento)
    {
        var result = await _service.SearchValue(value, idEstablecimiento);
        var dto = _mapper.Map<List<InventarioDTO>>(result);
        return Ok(dto);
    }





}