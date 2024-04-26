using api_scango.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;


namespace api_scango.Infrastructure.Data.Repositories.administrador;
public class AdministradorRepository
{
    private readonly ScanGoDbContext _dbcontext;

    public AdministradorRepository(ScanGoDbContext scanGoDbContext)
    {
        _dbcontext = scanGoDbContext;
    }
    public async Task<IEnumerable<Administrador>> GetAll()
    {
        var administrador = await _dbcontext.Administrador.ToListAsync();
        return administrador;
    }
    public async Task<Administrador> GetbyId(int id)
    {
        var administrador = await _dbcontext.Administrador.FirstOrDefaultAsync(admi => admi.IdAdministrador == id);
        return administrador!;
    }
    public async Task<Administrador> IniciarSesion(string usuario, string contraseña)
    {
        var administrador = await _dbcontext.Administrador.FirstOrDefaultAsync(exist => exist.Nombre == usuario && exist.Contraseña == contraseña);
        if (administrador == null)
        {
            throw new Exception("No se encontro el usuario");
        }
        return administrador;
    }
    public async Task Add(Administrador administrador)
    {
        await _dbcontext.AddAsync(administrador);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(Administrador administrador)
    {
        var admi = await GetbyId(administrador.IdAdministrador);
        if (admi != null)
        {
            _dbcontext.Entry(administrador).CurrentValues.SetValues(admi);

        }
    }
    public async Task Delete(int id)
    {
        var admi = await _dbcontext.Administrador.FirstOrDefaultAsync(x => x.IdEstablecimiento == id);
        if (admi != null)
        {
            _dbcontext.Administrador.Remove(admi);
            await _dbcontext.SaveChangesAsync();
        }
    }


}