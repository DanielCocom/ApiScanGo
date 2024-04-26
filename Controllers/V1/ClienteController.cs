using System;
using System.Threading.Tasks;
using api_scango.Domain.Dtos;
using api_scango.Domain.Entities;
using api_scango.Domain.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api_scango.Controllers
{
    [ApiController]
    [Route("v1/Cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(ClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerClientePorTelefono(string id)
        {
            try
            {
                var cliente = await _clienteService.ObtenerClientePorTelefonoAsync(id);

                if (cliente.NumeroTelefono != id)
                {
                    return NotFound(new { error = "Cliente no encontrado", message = "El cliente no existe." });
                }
                else
                {
                    var dto = _mapper.Map<ClienteDto>(cliente);

                    return Ok(dto);

                }


            }
            catch (Exception ex)
            { 
                // Loguea el error
                return StatusCode(500, new { error = "Error interno del servidor", message = ex.Message });
            }
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarNuevoCliente([FromBody] ClienteCreateDTO nuevoCliente)
        {

            var entity = _mapper.Map<Cliente>(nuevoCliente);
            await _clienteService.RegistrarNuevoClienteAsync(entity);

            var dto = _mapper.Map<ClienteDto>(entity);

            return CreatedAtAction(nameof(ObtenerClientePorTelefono), new { id = entity.NumeroTelefono }, dto); // Cambiado de Created a Ok

            // Loguea el error

        }
        [HttpPost("login")]
        public async Task<ActionResult<Cliente>> InicioSesion([FromBody] ClienteLoginDTO login)
        {
            try
            {
                var cliente = await _clienteService.InicioSesion(login.Numerodetelefono, login.Contraseña);
                // Resto del código para manejar el inicio de sesión y devolver una respuesta adecuada.
                // ...
                return Ok("Sesion iniciada");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Error de autenticación", message = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cliente>> UpdateUser(string id, ClienteCreateDTO clienteCreateDTO){
            if(id != clienteCreateDTO.NumeroTelefono){
                return BadRequest();
            }
            await _clienteService.UpdateUser(clienteCreateDTO);
            return NoContent();
        } 
    }
}





