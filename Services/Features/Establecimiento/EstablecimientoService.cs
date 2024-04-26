
using api_scango.Domain.Dtos;
using api_scango.Domain.Entities;
using api_scango.Infrastructure.Data.Repositories;
using Stripe.Radar;

namespace api_scango.Services.Fetures.establecimiento;

public class EstablecimientoService
{
    private readonly EstablecimientoRepository _repository;

    public EstablecimientoService(EstablecimientoRepository establecimientoRepository)
    {
        this._repository = establecimientoRepository;
    }
    public async Task<IEnumerable<Establecimiento>> getAll()
    {
        return await _repository.GetAll();
    }
    public async Task<Establecimiento> GetById(int id)
    {
        return await _repository.GetById(id);
    }
    public async Task Add(Establecimiento establecimiento, IFormFile file)
    {
        await _repository.Add(establecimiento, file);
    }
    public async Task Update(EstablecimientoUpdateDTO estaUpdate, IFormFile file)
    {
        var establecimeinto = await GetById(estaUpdate.id);
        if (establecimeinto.IdEstablecimiento > 0)
        {
            await _repository.Update(estaUpdate, file);
            
        }
    }
    public async Task Delete(int id)
    {
        var esta = await GetById(id);
        if(esta.IdEstablecimiento > 0){
            await _repository.Delete(id);
        }
    }
    public async Task<List<InventarioDTO>> Getinventario(int idEstablecimiento){
        return await _repository.GetInventario(idEstablecimiento);
    }

    public async Task<List<Producto>> SearchValue(string value, int idEstablecimiento){
        return await _repository.SearchProduct(value, idEstablecimiento);
    }
}