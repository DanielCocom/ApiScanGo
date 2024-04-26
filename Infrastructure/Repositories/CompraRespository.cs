using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using api_scango.Domain.Dtos;
using System.Collections;
using System.Globalization;

namespace api_scango.Infrastructure.Data.Repositories;

public class CompraRepository
{
    private readonly ScanGoDbContext _context;
    private readonly VentaRepository _repsoitory;
    private readonly EmailService _emailServices;
    public CompraRepository(ScanGoDbContext scanGoDbContext, VentaRepository ventaRepository, EmailService emailService)
    {
        _context = scanGoDbContext;
        _repsoitory = ventaRepository;
        _emailServices = emailService;

    }
    public async Task<ICollection<Compra>> GetCompras(string numeroTelefono)
    {
        var compras = await _context.Compra
        .Include(c => c.IdEstablecimientoNavigation)
        .Where(c => c.NumeroTelefono == numeroTelefono)
        .OrderByDescending(c => c.FechaCompra)
        .Take(5)
        .ToListAsync();
        return compras;
    }
    public async Task<Venta> RealizarCompra(int idEstablecimiento, string numerodetelefono, string idTransaccion)
    {
        var cliente = await _context.Cliente
         .Include(c => c.IdCarritoNavigation)
         .ThenInclude(carrito => carrito!.ProductosEnCarrito)
             .ThenInclude(producto => producto.IdProductoNavigation)
                .FirstOrDefaultAsync(c => c.NumeroTelefono == numerodetelefono);

        if (cliente == null || cliente.IdCarritoNavigation == null)
        {
            throw new Exception("Cliente no encontrado o carrito vacío");
        }

        // Calcular el total pagado sumando los totales de los productos en el carrito
        decimal totalPagado = (decimal)cliente.IdCarritoNavigation.ProductosEnCarrito.Sum(pc => pc.Total)!;

        string fechaStr = DateTime.Now.ToString("dd/MM/yyyy");

        // Convertir la cadena a un objeto DateTime
        var fechaVenta = DateTime.ParseExact(fechaStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        // Crear la nueva venta
        var nuevaVenta = new Venta
        {
            FechaVenta = fechaVenta,
            TotalPagado = totalPagado,
            IdEstablecimiento = idEstablecimiento,
            IdTransaccion = idTransaccion

        };

        // Agregar la nueva venta al contexto
        _context.Venta.Add(nuevaVenta);
        await _context.SaveChangesAsync();

        // Registrar el detalle de la venta
        var productosEnCarrito = cliente.IdCarritoNavigation.ProductosEnCarrito;
        foreach (var productoEnCarrito in productosEnCarrito)
        {
            var nuevoDetalleVenta = new DetalleVenta
            {
                IdVenta = nuevaVenta.IdVenta,
                IdProducto = productoEnCarrito.IdProducto,
                Cantidad = productoEnCarrito.Cantidad,
                PrecioUnitario = productoEnCarrito.IdProductoNavigation!.Precio, // Obtener el precio del producto
                Total = productoEnCarrito.Total
            };
            _context.DetalleVenta.Add(nuevoDetalleVenta);
        }

        // Limpiar el carrito del cliente
        // cliente.IdCarritoNavigation.ProductosEnCarrito.Clear();
        await _emailServices.SendEmail(cliente, productosEnCarrito, nuevaVenta);
        await RegistrarCompra(nuevaVenta, cliente);
        await _context.SaveChangesAsync();

        return nuevaVenta;
    }

    private async Task RegistrarCompra(Venta nuevaVenta, Cliente cliente)
    {
        // MApepar esta entidad para que diga el nombre la sucursal que compro en el dto
        var nuevaCompra = new Compra
        {
            NumeroTelefono = cliente.NumeroTelefono,
            TotalPagado = nuevaVenta.TotalPagado,
            TotalProductos = nuevaVenta.DetalleVenta.Sum(p => p.Cantidad),
            FechaCompra = nuevaVenta.FechaVenta!,
            IdEstablecimiento = nuevaVenta.IdEstablecimiento

        };
        _context.Compra.Add(nuevaCompra);
        await _context.SaveChangesAsync();

        foreach (var productoEnCarrito in cliente.IdCarritoNavigation!.ProductosEnCarrito)
        {
            var nuevaCompraDetalles = new CompraDetalles
            {
                IdCompra = nuevaCompra.IdCompra,
                IdProducto = productoEnCarrito.IdProducto,
                NombreProducto = productoEnCarrito.NombreProducto,
                Cantidad = productoEnCarrito.Cantidad,
                Total = productoEnCarrito.Total
            };
            _context.CompraDetalles.Add(nuevaCompraDetalles);
        }
        await _context.SaveChangesAsync();
    }
}
