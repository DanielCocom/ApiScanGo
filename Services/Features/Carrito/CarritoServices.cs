using api_scango.Infrastructure.Data.Repositories;
using api_scango.Domain.Dtos;
using AutoMapper;
using api_scango.Domain.Entities;


public class CarritoService
{
    private readonly CarritoRepository _repository;
    public CarritoService(CarritoRepository carritoRepository)
    {
        _repository = carritoRepository;
    }
    public async Task AddProducto(string numero, string codigoBarras, int idEstablecimiento, int cantidad)
    {
        await _repository.AddProducto(numero, codigoBarras, idEstablecimiento, cantidad);
    }
     public async Task AddFruta(string numero, string codigoBarras, int pesoGramos)
    {
        await _repository.AddFruta(numero, codigoBarras, pesoGramos);
    }
    public async Task<ICollection<ProductosEnCarrito>> GetProductos(string numerodetelefono)
    {
        return await _repository.GetProductos(numerodetelefono);
    }

    public async Task DeleteProduct(string telefono, string idProducto)
    {
        await _repository.EliminarProducto(telefono, idProducto);
    }
     public async Task VaciarCarrito(string telefono)
    {
        await _repository.VaciarCarrito(telefono);
    }
}