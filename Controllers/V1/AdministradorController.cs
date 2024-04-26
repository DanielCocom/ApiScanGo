using System;
using System.Threading.Tasks;
using api_scango.Domain.Dtos;
using api_scango.Domain.Entities;
using api_scango.Domain.Services;
using api_scango.Services.Fetures.administrador;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace api_scango.Controllers;

[ApiController]
[Route("v1/Administrador")]
public class AdministradorController : ControllerBase
{
    private readonly AdministradorService _admiService;
    private readonly IMapper _mapper;
    public AdministradorController(AdministradorService administradorService, IMapper mapper)
    {
        _admiService = administradorService;
        _mapper = mapper;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> getById(int id)
    {
        try
        {
            var administrador = await _admiService.GetById(id);

            if (administrador == null) 
            {
                return NotFound(new { error = "Cliente no encontrado", message = "El administrador no existe." });
            }

            var dto = _mapper.Map<Administrador>(administrador);

            return Ok(dto);
        }
        catch (Exception ex)
        {
            // Loguea el error
            return StatusCode(500, new { error = "Error interno del servidor", message = ex.Message });
        }
    }



    [HttpPost("registrar")]
    public async Task<IActionResult> RegistrarNuevoCliente([FromBody] AdministradorCreateDTO nuevoAdmin)
    {

        var entity = _mapper.Map<Administrador>(nuevoAdmin);
        await _admiService.Add(entity);

        var dto = _mapper.Map<AdministradorCreateDTO>(entity);

        return CreatedAtAction(nameof(getById), new { id = entity.IdAdministrador }, dto); // Cambiado de Created a Ok


    }
    [HttpPost("IniciarSesion")]
    public async Task<IActionResult> IniciarSesion([FromBody] AdministradorLoginDto admi){
         try
            {
                var administrador = await _admiService.IniciarSesion(admi.Nombre, admi.Contraseña);

                if(administrador != null){
                return Ok(administrador);

                }   
                else{
                    return BadRequest("Credenciales Incorrectas");
                }             
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Error de autenticación", message = ex.Message });
            }
    }
}