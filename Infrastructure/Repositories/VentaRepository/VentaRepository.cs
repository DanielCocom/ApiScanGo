using System.Text.Json;
using api_scango.Domain.Dtos;
using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;


namespace api_scango.Infrastructure.Data.Repositories;

public class VentaRepository
{

    private readonly ScanGoDbContext _context;
    private readonly IMapper _mapper;

    public VentaRepository(ScanGoDbContext scanGoDbContext, IMapper autoMapper)
    {
        _context = scanGoDbContext;
        _mapper = autoMapper;
        // _compraRepository = compraRepository;
    }
    public async Task<IEnumerable<Venta>> GetAll()
    {
        var venta = await _context.Venta.ToListAsync();
        return venta;
    }
    public async Task<Venta> getByid(int idventa)
    {
        var venta = await _context.Venta.FirstOrDefaultAsync(venta => venta.IdVenta == idventa);
        return venta ?? new Venta();
    }
    public async Task<List<VentasTiendaDTO>> getVentasEstablecimiento(int idEstablecimiento)
    {
        var ventas = await _context.Venta
        .Where(v => v.IdEstablecimiento == idEstablecimiento)
        .ProjectTo<VentasTiendaDTO>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return ventas;

    }
    public async Task<List<DetalleVenta>> GetDetalleVentaId(int idVentas)
    {
        var detalleVenta = await _context.DetalleVenta
        .Where(v => v.IdVenta == idVentas)
        .Include(d => d.IdProductoNavigation)
        .ToListAsync();
        return detalleVenta;
    }
    public async Task<List<ProductosVendidosDTO>> getProductoMenosVendido(int establecimiento)
    {
        var productosVendido = await getProductosVendidos(establecimiento);
        var productoMenosVendido = productosVendido.OrderBy(kv => kv.Value)
        .Select(async kv => new ProductosVendidosDTO
        {
            IdProducto = kv.Key,
            NombreProducto = await GetNameProductAsync(kv.Key),
            CantidadVendida = kv.Value
        })
        .Select(task => task.Result)
        .ToList();
        return productoMenosVendido;
    }
    public async Task<List<ProductosVendidosDTO>> getProductMasVendidos(int IdEstablecimiento)
    {

        Dictionary<string, int> productosVendidos = await getProductosVendidos(IdEstablecimiento);

        var productosMasVendidos = productosVendidos.OrderByDescending(kv => kv.Value
        )
           .Select(async kv => new ProductosVendidosDTO
           {
               IdProducto = kv.Key,
               NombreProducto = await GetNameProductAsync(kv.Key),
               CantidadVendida = kv.Value
           })
           .Select(task => task.Result)
            .Take(5) // esperar  a que todas las tareas se completen y obtener los resultados                  //convierte en una lista de objetos y no de tareas
           .ToList();

        return productosMasVendidos;
    }
    public async Task<string> GetNameProductAsync(string IdProducto)
    {
        var nameproducto = await _context.Producto.FirstOrDefaultAsync(p => p.IdProducto == IdProducto);
        return nameproducto!.Nombre;
    }
    private async Task<Dictionary<string, int>> getProductosVendidos(int IdEstablecimiento)
    {
        var ventas = await _context.Venta
           .Include(v => v.DetalleVenta)
           .Where(v => v.IdEstablecimiento == IdEstablecimiento)
           .ToListAsync();

        var productosVendidos = new Dictionary<string, int>();


        foreach (var venta in ventas)
        {
            foreach (var detalleVenta in venta.DetalleVenta)
            {
                if (productosVendidos.ContainsKey(detalleVenta.IdProducto!))
                {
                    productosVendidos[detalleVenta.IdProducto!] += detalleVenta.Cantidad ?? 0;
                }
                else
                {
                    productosVendidos.Add(detalleVenta.IdProducto!, detalleVenta.Cantidad ?? 0);
                }

            }
        }
        return productosVendidos;
    }
    public async Task<List<VentasTiendaDTO>> GetLastFiveSale(int idEstablecimiento)
    {
        var lastFiveSale = await _context.Venta
        .Where(v => v.IdEstablecimiento == idEstablecimiento)
        .ProjectTo<VentasTiendaDTO>(_mapper.ConfigurationProvider)
        .OrderByDescending(v => v.FechaVenta)
        .Take(5)
        .ToListAsync();

        return lastFiveSale;
    }
    public async Task<List<VentaMesDTO>> GetSalesPerMonthStore(int idEstablecimiento)
    {

        var salesPerMont = await _context.Venta.
        Include(d => d.DetalleVenta)
        .Where(v => v.IdEstablecimiento == idEstablecimiento)
        .ToListAsync();
        var salesPerMontGroup = salesPerMont
         .GroupBy(v => new { Mes = v.FechaVenta!.Value.Month, Anio = v.FechaVenta.Value.Year, v.IdEstablecimiento})
         .Select(g => new VentaMesDTO{
            Anio = g.Key.Anio,
            Mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Mes),
            Ganancias = g.Sum(v => v.TotalPagado),
            TotalProductosVendidos = g.SelectMany(v => v.DetalleVenta).Sum(d => d.Cantidad),
            idEstablecimiento = g.Key.IdEstablecimiento
         })
         .ToList();

         return salesPerMontGroup;
    }


}