using System.Net;
using System.Text.Json;
using api_scango.Domain.Dtos;
using api_scango.Domain.Entities;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;


namespace api_scango.Infrastructure.Data.Repositories;

public class ProductoRepository
{
    private readonly ScanGoDbContext _context;
    private readonly EstablecimientoRepository _establecimientoRepo;

    private readonly CloudinaryServices _cloudinaryServices;


    public ProductoRepository(ScanGoDbContext scanGoDbContext, EstablecimientoRepository establecimientoRepository, CloudinaryServices cloudinary)
    {
        _context = scanGoDbContext;
        _establecimientoRepo = establecimientoRepository;
        _cloudinaryServices = cloudinary;
    }
    public async Task<Producto> GetbyId(string idProducto, int idEstablecimiento)
    {
        var establecimiento = await _context.Establecimiento.FirstOrDefaultAsync(tienda => tienda.IdEstablecimiento == idEstablecimiento);
        var exitProductOnInventory = await _context.ProductoInventario.FirstOrDefaultAsync(producto => producto.IdProducto == idProducto && producto.IdInventario == establecimiento.IdInventario);
        if (exitProductOnInventory != null)
        {
            Producto? producto = await _context.Producto.FirstOrDefaultAsync(producto => producto.IdProducto == idProducto);
            return producto!;
        }
        return null;
    }
    public async Task<List<FrutaDTO>> GetFrutas()
    {
        var productos = await _context.Producto
     .Include(p => p.IdTipoProductoNavigation)
     .Where(p => p.IdTipoProducto != 1)
     .ToListAsync();


        //deberia usar autommapper
        var frutasDto = productos.Select(p => new FrutaDTO
        {
            IdFruta = p.IdProducto,
            Nombre = p.Nombre,
            Imagen = p.Imagen,
            PrecioKilo = p.IdTipoProductoNavigation != null ? p.IdTipoProductoNavigation.PrecioPorKilo : 0,
            // idTipo = p.IdTipoProducto
        }).ToList();

        return frutasDto;
    }

    public async Task AddProducto(Producto producto, IFormFile image)
    {
        string imageUrl = await _cloudinaryServices.UploadImageOfProduct(image);

        producto.Imagen = imageUrl;
        await _context.Producto.AddAsync(producto);
        await _context.SaveChangesAsync();


    }
    public async Task AddProductotienda(int establishmentId, Producto product, IFormFile image, int quantity)
    {
        // Verificar si el producto ya existe en la base de datos
        var existingProduct = await _context.Producto.FirstOrDefaultAsync(p => p.IdProducto == product.IdProducto);

        // Obtener el establecimiento y su inventario asociado
        var establishment = await _context.Establecimiento
            .Include(e => e.IdInventarioNavigation)
            .FirstOrDefaultAsync(e => e.IdEstablecimiento == establishmentId);

        // Verificar si el producto ya está en el inventario del establecimiento
        var inventoryProduct = await _context.ProductoInventario.FirstOrDefaultAsync(pi => pi.IdProducto == product.IdProducto && pi.IdInventario == establishment.IdInventario);

        // Si el producto no existe en la base de datos, agregarlo
        if (existingProduct == null)
        {
            await AddProducto(product, image);
            existingProduct = product; // Actualizar la referencia al producto recién agregado
        }

        // Si el producto no está en el inventario, crear una nueva entrada
        if (inventoryProduct == null)
        {
            var newInventoryProduct = new ProductoInventario
            {
                IdProducto = existingProduct.IdProducto,
                Cantidad = quantity,
                IdInventario = establishment.IdInventario // Asignar el ID del inventario del establecimiento
            };

            _context.ProductoInventario.Add(newInventoryProduct);
        }
        else
        {
            // Si el producto ya está en el inventario, actualizar la cantidad
            inventoryProduct.Cantidad += quantity;
        }

        // Guardar los cambios en la base de datos
        await _context.SaveChangesAsync();
    }
    public async Task UpdateProduct(int IdEstablecimiento, ProductoUpdateDTO producto, IFormFile file)
    {
        var tienda = await _context.Establecimiento
            .Include(p => p.IdInventarioNavigation)
            .FirstOrDefaultAsync(tienda => tienda.IdEstablecimiento == IdEstablecimiento);

        if (tienda == null)
        {
            throw new Exception("La tienda no existe");
        }

        var inventario = await _context.ProductoInventario
            .Include(productos => productos.IdProductoNavigation)
            .FirstOrDefaultAsync(inventario => inventario.IdInventario == tienda!.IdInventario);

        if (inventario == null)
        {
            throw new Exception("El producto no existe en el inventario de la tienda");
        }

        var product = await _context.Producto.FirstOrDefaultAsync(p => p.IdProducto == producto.IdProducto);
        if (product == null)
        {
            throw new Exception("El producto no existe");
        }

        if (product.IdProducto == producto.IdProducto)
        {
            var ProductoInventario = await _context.ProductoInventario.FirstOrDefaultAsync(p => p.IdProducto == producto.IdProducto);
            string imgUrl = await _cloudinaryServices.UploadImageOfProduct(file);
            product.Imagen = imgUrl;
            ProductoInventario!.Cantidad = producto.stock;
           _context.Entry(product).CurrentValues.SetValues(producto);
            await _context.SaveChangesAsync();
        }
    }
     public async Task Delete(string id)
    {
        var tienda = await _context.Producto.FirstOrDefaultAsync(producto => producto.IdProducto == id);

        
        if (tienda != null)
        {
            _context.Producto.Remove(tienda);
            
            await _context.SaveChangesAsync();
        }
    }




}