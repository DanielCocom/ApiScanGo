


using System.Globalization;
using api_scango.Domain.Dtos;
using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_scango.Infrastructure.Data.Repositories;


public class ReporteRepository
{
    private readonly ScanGoDbContext _dbContext;

    private readonly VentaRepository _ventaRepository;


    public ReporteRepository(ScanGoDbContext scanGoDbContext, VentaRepository ventaRepository)
    {
        _dbContext = scanGoDbContext;
        _ventaRepository = ventaRepository;
    }

    public async Task<ReporteMensual> GenerarReporteMes(int idEstablecimiento, DateTime dateTime)
    {
        var establecimiento = await _dbContext.Establecimiento
        .FirstOrDefaultAsync(e => e.IdEstablecimiento == idEstablecimiento) ?? throw new Exception("El establecimiento no existe");

        var totalSalesCurrentMonth = await getSalesByCurrentMonth(idEstablecimiento, dateTime);

        var productsMostSale = GetMostProductSalesByMonth(totalSalesCurrentMonth);

        var productoMenosVendido = GetMenosProductosVendidosPerMonth(totalSalesCurrentMonth);

        decimal? averageRevenuePerDay = GetAverageRevenueByDay(totalSalesCurrentMonth, dateTime);

        var reporteMensual = new ReporteMensual
        {
            NombreEstablecimiento = establecimiento.Nombre,
            Mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month),
            NumeroVentas = totalSalesCurrentMonth.Count(),
            Ingresostotoales = totalSalesCurrentMonth.Sum(g => g.TotalPagado),
            GananciasPromedioPorDia = Math.Round((decimal)averageRevenuePerDay!, 2),
            ProductosMasVendidos = productsMostSale,
            ProductosMenosVendidos = productoMenosVendido

        };

        return reporteMensual;


    }

    private async Task<ICollection<Venta>> getSalesByCurrentMonth(int idEstablecimiento, DateTime date)
    {
        var salesPerMont = await _dbContext.Venta.
                Include(d => d.DetalleVenta)
                .Where(v => v.IdEstablecimiento == idEstablecimiento && v.FechaVenta.Value.Month == date.Month)
                .ToListAsync();

        return salesPerMont;
    }
    private decimal? GetAverageRevenueByDay(ICollection<Venta> salesMonth, DateTime date)
    {
        var totalRevenue = salesMonth.Sum(g => g.TotalPagado);
        var monthsDays = DateTime.DaysInMonth(date.Year, date.Month);

        var averageRevenuebyDay = totalRevenue / monthsDays;

        return averageRevenuebyDay;
    }
    private List<ProductosVendidosDTO> GetMostProductSalesByMonth(ICollection<Venta> salesMonth)
    {
        var productosVendidos = new Dictionary<string, int>();


        foreach (var venta in salesMonth)
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

        var productosMasVendidos = productosVendidos.OrderByDescending(kv => kv.Value
        )
           .Select(async kv => new ProductosVendidosDTO
           {
               IdProducto = kv.Key,
               NombreProducto = await _ventaRepository.GetNameProductAsync(kv.Key),
               CantidadVendida = kv.Value
           })
           .Select(task => task.Result)
            .Take(5) // esperar  a que todas las tareas se completen y obtener los resultados                  //convierte en una lista de objetos y no de tareas
           .ToList();

        return productosMasVendidos;
    }
    public List<ProductosVendidosDTO> GetMenosProductosVendidosPerMonth(ICollection<Venta> ventas)
    {

        var productosVendidos = new Dictionary<string, int>();
        var ventasMes = ventas;

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
        var productoMenosVendido = productosVendidos.OrderBy(kv => kv.Value)
        .Select(async kv => new ProductosVendidosDTO
        {
            IdProducto = kv.Key,
            NombreProducto = await _ventaRepository.GetNameProductAsync(kv.Key),
            CantidadVendida = kv.Value
        })
        .Select(task => task.Result)
        .Take(5)
        .ToList();
        return productoMenosVendido;
    }

}


