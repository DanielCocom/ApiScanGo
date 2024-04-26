using api_scango.Domain.Entities;
using api_scango.Infrastructure.Data.Repositories;
using api_scango.Infrastructure.Data.Repositories.administrador;

namespace api_scango.Services.Fetures.administrador;

public class AdministradorService
{
    private readonly AdministradorRepository _repository;

    public AdministradorService(AdministradorRepository administradorRepository)
    {
        _repository = administradorRepository;
    }
    public async Task<IEnumerable<Administrador>> getAll()
    {
        return await _repository.GetAll();
    }
    public async Task<Administrador> GetById(int id)
    {
        return await _repository.GetbyId(id);
    }
    public async Task Add(Administrador administrador)
    {

        await _repository.Add(administrador);
    }
    public async Task<Administrador> IniciarSesion(string usuario, string contraseña)
    {
        return await _repository.IniciarSesion(usuario, contraseña);
    }
    // public async Task Update(Administrador estaUpdate)
    // {
    //     var administrador = GetById(estaUpdate.IdEstablecimiento);
    //     if (establecimeinto.Id > 0)
    //     {
    //         await _repository.Update(estaUpdate);
    //     }
    // }

}
