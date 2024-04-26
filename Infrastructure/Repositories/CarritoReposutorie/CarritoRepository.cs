using System.Threading.Tasks;
using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using api_scango.Domain.Dtos;
using System.Collections.ObjectModel;

namespace api_scango.Infrastructure.Data.Repositories;

public class CarritoRepository
{
    private readonly ClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    private readonly ProductoRepository _productoRepository;
    private readonly ScanGoDbContext _context;


    public CarritoRepository(IMapper mapper, ScanGoDbContext scanGoDbContext, ClienteRepository clienteRepository, ProductoRepository productoRepository)
    {

        _context = scanGoDbContext;
        _mapper = mapper;
        _clienteRepository = clienteRepository;
        _productoRepository = productoRepository;
    }
    public async Task AddFruta(string numero, string idFruta, int pesoGramos)
    {
        var fruta = await _context.Producto
    .Include(p => p.IdTipoProductoNavigation) // Incluye la propiedad de navegación IdTipoProductoNavigation
    .FirstOrDefaultAsync(fruta => fruta.IdProducto == idFruta);

        if (fruta == null)
        {
            throw new Exception("Fruta no encontrada");
        }
        var cliente = await _clienteRepository.GetById(numero);

        var carrito = await _context.Carrito
           .Include(c => c.ProductosEnCarrito) // Asegúrate de incluir los productos en el carrito
           .FirstOrDefaultAsync(c => c.IdCarrito == cliente.IdCarrito);


        decimal precioTotal = (decimal)((fruta.IdTipoProductoNavigation!.PrecioPorKilo * pesoGramos) / 1000)!;
        var productoEnCarrito = new ProductosEnCarrito
        {
            IdProducto = fruta.IdProducto,
            NombreProducto = fruta.Nombre,
            Cantidad = 1, // Por defecto, agregamos 1 unidad de la fruta al carrito
            Total = precioTotal // El precio total de la fruta
        };

        carrito!.ProductosEnCarrito.Add(productoEnCarrito);

        // Guardar los cambios en la base de datos
        await _context.SaveChangesAsync();
    }
    public async Task AddProducto(string numero, string codigoBarras, int idEstablecimiento, int cantidad)
    {
        var establecimiento = await _context.Establecimiento
        .Include(id => id.IdInventarioNavigation!.ProductoInventario)
        .FirstOrDefaultAsync(tienda => tienda.IdEstablecimiento == idEstablecimiento);

        var inventario = establecimiento!.IdInventarioNavigation!.ProductoInventario;
        var productoInvenario = inventario.FirstOrDefault(p => p.IdProducto == codigoBarras);
        // esta mal evaluado


        if(productoInvenario!.Cantidad <= 0)
        {
            throw new Exception("Producto sin Stokc");
        }

        if(productoInvenario != null){

            productoInvenario.Cantidad -= cantidad;
        }
        else if(productoInvenario!.Cantidad < cantidad)
        {
            throw new Exception("Cantidad no disponible");
            

        }


        var cliente = await _clienteRepository.GetById(numero);
        if (cliente == null)
        {
            throw new Exception("Cliente no encontrado");
        }

        var carrito = await _context.Carrito
            .Include(c => c.ProductosEnCarrito) 
            .FirstOrDefaultAsync(c => c.IdCarrito == cliente.IdCarrito);

        if (carrito == null)
        {
            throw new Exception("Carrito no encontrado");
        }

        var productoEnCarrito = carrito.ProductosEnCarrito.FirstOrDefault(p => p.IdProducto == codigoBarras);

        var producto = await _productoRepository.GetbyId(codigoBarras, idEstablecimiento);
        if (producto == null)
        {
            throw new Exception("No se encontró el producto");
        }

        if (productoEnCarrito != null)
        {
            // Si el producto ya está en el carrito, actualizar la cantidad y el total
            productoEnCarrito.Cantidad += cantidad;
            productoEnCarrito.Total += producto.Precio * cantidad;
        }
        else
        {
            // Si el producto no está en el carrito, agregarlo
            carrito.ProductosEnCarrito.Add(new ProductosEnCarrito
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.Nombre,
                Cantidad = cantidad,
                Total = producto.Precio * cantidad
            });
        }

        await _context.SaveChangesAsync();
    }

    public async Task EliminarProducto(string telefono, string idProducto)
    {
        var cliente = await _clienteRepository.GetById(telefono);
        var carrito = await _context.Carrito
            .Include(c => c.ProductosEnCarrito)  // Asegúrate de incluir los productos en el carrito
            .FirstOrDefaultAsync(c => c.IdCarrito == cliente.IdCarrito);

        if (carrito == null)
        {
            throw new Exception("Carrito not found");
        }

        var productoEnCarrito = carrito.ProductosEnCarrito.FirstOrDefault(p => p.IdProducto == idProducto);
        if (productoEnCarrito == null)
        {
            throw new Exception("Product not found");
        }

        if (productoEnCarrito.Cantidad > 1)
        {
            productoEnCarrito.Cantidad--; // Decrementa la cantidad del producto en 1
                                          // Descontar total del producto
            productoEnCarrito.Total = productoEnCarrito.Total / (productoEnCarrito.Cantidad + 1) * productoEnCarrito.Cantidad;
        }
        else
        {
            carrito.ProductosEnCarrito.Remove(productoEnCarrito);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<ProductosEnCarrito>> GetProductos(string numerodetelefono)
    {
        var cliente = await _context.Cliente
            .Include(c => c.IdCarritoNavigation)
                .ThenInclude(c => c!.ProductosEnCarrito)
                    .ThenInclude(pc => pc.IdProductoNavigation) // Asegúrate de incluir la entidad de Producto
            .FirstOrDefaultAsync(c => c.NumeroTelefono == numerodetelefono);

        if (cliente == null || cliente.IdCarritoNavigation == null)
        {
            throw new Exception("Cliente o carrito no encontrado");
        }

        var productosEnCarrito = cliente.IdCarritoNavigation.ProductosEnCarrito;

        if (productosEnCarrito == null) 
        {
            throw new Exception("Productos en carrito no encontrados");
        }

        // var productosEnCarritoDto = _mapper.Map<List<ProductoEnCarritoDto>>(productosEnCarrito);

        return productosEnCarrito;
    }
    public async Task VaciarCarrito(string numeroTelefono)
    {
        var cliente = await _clienteRepository.GetById(numeroTelefono);

        var carrito = await _context.Carrito
            .Include(c => c.ProductosEnCarrito)
            .FirstOrDefaultAsync(c => c.IdCarrito == cliente.IdCarrito);

        if (carrito != null)
        {
            carrito.ProductosEnCarrito.Clear(); // Elimina todos los productos del carrito
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }
    }




}