

using api_scango.Domain.Dtos;
using api_scango.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api_scango.Infrastructure.Data.Repositories;


public class EstablecimientoRepository
{

    private readonly ScanGoDbContext _dbContext;
    private readonly CloudinaryServices _cloudinary;
    public EstablecimientoRepository(ScanGoDbContext scanGoDbContext, CloudinaryServices cloudinary)
    {
        _dbContext = scanGoDbContext;
        _cloudinary = cloudinary;
    }
    public async Task<IEnumerable<Establecimiento>> GetAll()
    {
        var establecimiento = await _dbContext.Establecimiento.ToListAsync();
        return establecimiento;
    }
    public async Task<Establecimiento> GetById(int id)
    {
        var establecimiento = await _dbContext.Establecimiento.FirstOrDefaultAsync(establecimeinto => establecimeinto.IdEstablecimiento == id);

        return establecimiento ?? new Establecimiento();
    }
    public async Task Add(Establecimiento establecimiento, IFormFile file)
    {
        var imageStore = await _cloudinary.UploadImageOfProduct(file);
        establecimiento.Imagen = imageStore;
        await _dbContext.AddAsync(establecimiento);
        await _dbContext.SaveChangesAsync();


        var newInventario = new Inventario();
        _dbContext.Add(newInventario);
        await _dbContext.SaveChangesAsync();

        establecimiento.IdInventario = newInventario.IdInventario;

        await _dbContext.SaveChangesAsync();
    } 


    public async Task Update(EstablecimientoUpdateDTO establecimientoUpdate, IFormFile file)
    {
        var establecimeinto = await _dbContext.Establecimiento
        .FirstOrDefaultAsync(x => x.IdEstablecimiento == establecimientoUpdate.id);
        if (establecimeinto != null)
        {
            string imageUrl = await _cloudinary.UploadImageOfProduct(file);
            establecimeinto.Imagen = imageUrl;

            _dbContext.Entry(establecimeinto).CurrentValues.SetValues(establecimientoUpdate);
            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task Delete(int id)
    {
        var tienda = await _dbContext.Establecimiento.FirstOrDefaultAsync(tienda => tienda.IdEstablecimiento == id);

        
        if (tienda != null)
        {
            _dbContext.Establecimiento.Remove(tienda);
            
            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task<List<InventarioDTO>> GetInventario(int IdEstablecimiento)
    {
        // buscas primero el establecimiento y despues el inventario, para obtener los productos
        var establecimeinto = await _dbContext.Establecimiento
        .Include(inventario => inventario.IdInventarioNavigation)
         .FirstOrDefaultAsync(establecimeinto => establecimeinto.IdEstablecimiento == IdEstablecimiento);


        var inventario = await _dbContext.Inventario
        .Include(c => c.ProductoInventario)
        .FirstOrDefaultAsync(inventario => inventario.IdInventario == establecimeinto.IdInventario);

        var productosEnInventario = await _dbContext.ProductoInventario
        .Where(pi => pi.IdInventario == inventario.IdInventario)
        .Select(pi => new InventarioDTO
        {
            IdProducto = pi.IdProducto,
            NombreProducto = pi.IdProductoNavigation.Nombre, // Ejemplo de cómo acceder al nombre del producto
            Cantidad = pi.Cantidad,
            Imagen = pi.IdProductoNavigation.Imagen,
            Precio = pi.IdProductoNavigation.Precio
        })
        .ToListAsync();

        return productosEnInventario;

    }
  public async Task<List<Producto>> SearchProduct(string valueToSearch, int IdEstablecimiento)
{

    // Buscar el establecimiento por su Id
    var establecimiento = await _dbContext.Establecimiento
        .Include(e => e.IdInventarioNavigation) 
        .FirstOrDefaultAsync(e => e.IdEstablecimiento == IdEstablecimiento)
        ?? throw new Exception("El establecimiento no existe");

    var productosFiltrados = await _dbContext.ProductoInventario
        .Include(pi => pi.IdProductoNavigation) // Incluir la propiedad de navegación para el producto
        .Where(pi => pi.IdInventario == establecimiento.IdInventario && 
                     (pi.IdProducto!.Contains(valueToSearch) || pi.IdProductoNavigation!.Nombre.Contains(valueToSearch)))
                     
        .Select(pi => pi.IdProductoNavigation)
        .Take(4) // Seleccionar el producto asociado
        .ToListAsync();

    return productosFiltrados!;
}

    
   



}