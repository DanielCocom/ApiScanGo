using api_scango.Infrastructure.Data.Repositories;
using api_scango.Domain.Entities;
using api_scango.Domain.Dtos;

namespace api_scango.Services.Fetures.producto;

public class ProductoService
{
    private readonly ProductoRepository _repository;

    public ProductoService(ProductoRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    public async Task <Producto> GetbyId(string idProducto, int idEstablecimiento)
    {
       return await _repository.GetbyId(idProducto, idEstablecimiento);
    }

    public async Task AddProductoestablecimiento(int idEstablecimiento, Producto producto,IFormFile image, int cantidad)
    {
        await _repository.AddProductotienda(idEstablecimiento, producto,image, cantidad);
    }
    public async Task<List<FrutaDTO>> GetFrutas()
    {
        return await _repository.GetFrutas();
    }
    public async Task UpdateProducto(int idEstablecimiento, ProductoUpdateDTO producto, IFormFile file){
     
        await _repository.UpdateProduct(idEstablecimiento, producto, file);
    }
    public async Task Delete(string idProducto){
        await _repository.Delete(idProducto);
    }
}
