using System.Net;
using System.Threading.Tasks;
using api_scango.Domain.Dtos;
using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_scango.Infrastructure.Data.Repositories;

public class ClienteRepository
{
    private readonly ScanGoDbContext _context;
    public ClienteRepository(ScanGoDbContext scanGoDbContext)
    {
        _context = scanGoDbContext;
    }
    public async Task<Cliente> GetById(string numerodetelefono)
    {
        var cliente = await _context.Cliente
            .FirstOrDefaultAsync(cliente => cliente.NumeroTelefono == numerodetelefono);

        return cliente ?? new Cliente();
    }
    public async Task AgregarClienteAsync(Cliente cliente)
    {
        var newCarrito = new Carrito();
        _context.Add(newCarrito);
        await _context.SaveChangesAsync();

        cliente.IdCarrito = newCarrito.IdCarrito;

         _context.Add(cliente);
        await _context.SaveChangesAsync();

    }
    public async Task<Cliente> InicioSesion(string telefono, string contraseña)
    {
        var cliente = await GetById(telefono);
        // TODO: POR SI DESEO VALIDAR LA CONTRASELA TAMBIEN
        if (cliente == null)
        {
            throw new Exception("No existe la cuenta con el número de teléfono proporcionado");
        }
        return cliente;
    }
    public async Task Update(ClienteCreateDTO cliente)
    {
        var clienteExist = await _context.Cliente.FirstOrDefaultAsync(cliente => cliente.NumeroTelefono == cliente.NumeroTelefono);
        if (clienteExist != null)
        {
            _context.Cliente.Entry(clienteExist).CurrentValues.SetValues(cliente);
            await _context.SaveChangesAsync();
        }

    }
    public async Task Delete(string numeroTelefono){
        var clienteExist = await _context.Cliente.FirstOrDefaultAsync(cliente => cliente.NumeroTelefono == cliente.NumeroTelefono);
        
        if(clienteExist != null){
            _context.Cliente.Remove(clienteExist);
            await _context.SaveChangesAsync();
        }
    }
}