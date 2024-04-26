using Microsoft.AspNetCore.Mvc;
using api_scango.Domain.Entities;
using AutoMapper;
using api_scango.Domain.Dtos;
using System;
using System.Threading.Tasks;
using api_scango.Services.Fetures.producto;

namespace api_scango.Controllers.V1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _productoService;
        private readonly IMapper _mapper;

        public ProductoController(ProductoService productoService, IMapper mapper)
        {
            _productoService = productoService ?? throw new ArgumentNullException(nameof(productoService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("Frutas")]
        public async Task<IActionResult> GetAllFrutas()
        {
            // try
            // {
            var Frutas = await _productoService.GetFrutas();
            return Ok(Frutas);
            // }
            // catch (Exception ex)
            // {
            //     return StatusCode(500, new { error = "Internal Server Error", message = ex.Message });
            // }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, int idtienda)
        {
            try
            {
                var producto = await _productoService.GetbyId(id, idtienda);
                if (producto.IdProducto == null)
                {
                    return NotFound(new { error = "Producto no encontrado", message = "No se encontró el Producto" });
                }

                var dto = _mapper.Map<ProductoDto>(producto);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error", message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(int idEstablecimiento, [FromForm] ProductoCreateDto productoCreateDto, IFormFile image, int cantidad)
        {
            try
            {
                ProductoCreateDto productoCreate = productoCreateDto;
                productoCreate.IdTipoProducto = 1;
                productoCreate.IdDescuento = 1;
                // Mapear el DTO de producto a la entidad de producto
                var entity = _mapper.Map<Producto>(productoCreate);

                // Agregar el producto al establecimiento utilizando el servicio
                await _productoService.AddProductoestablecimiento(idEstablecimiento, entity, image, cantidad);

                // Mapear la entidad del producto a un DTO de producto
                var dto = _mapper.Map<ProductoDto>(entity);

                // Devolver una respuesta con el DTO del producto creado
                return Ok("Producto agregado");
            }
            catch (Exception ex)
            {
                //     // Manejar cualquier excepción que pueda ocurrir durante el proceso de creación del producto
                return StatusCode(500, "Ocurrió un error al agregar el producto al establecimiento. Por favor, inténtelo de nuevo más tarde." + ex);
            }
        }
        [HttpPut("v1/ActualizarProducto/{IdEstablecimiento}")]
        public async Task<IActionResult> UpdateProduct(int IdEstablecimiento, [FromForm] ProductoUpdateDTO producto, IFormFile file)
        {
            try
            {
                await _productoService.UpdateProducto(IdEstablecimiento, producto, file);
                return Ok("Producto actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el producto: " + ex.Message);
            }
        }
        [HttpDelete("V1/Eliminar")]
        public async Task<IActionResult> DeleteProducto(string codigoBarras)
        {
            try
            {
               await _productoService.Delete(codigoBarras);    
               return Ok("ProductoEliminado");
            }
            catch (Exception ex){
                return BadRequest();


            }
        }


        }
    }
