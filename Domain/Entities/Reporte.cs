using System.Drawing;
using api_scango.Domain.Dtos;

namespace api_scango.Domain.Entities;
public class ReporteMensual{
    public string? NombreEstablecimiento {get; set;} 
    public string? Mes {get; set;}
    public int NumeroVentas {get; set;}
    public decimal? Ingresostotoales {get; set;}
    public decimal? GananciasPromedioPorDia {get; set;}
    public List <ProductosVendidosDTO> ProductosMasVendidos {get; set;}= new List<ProductosVendidosDTO>();
    public List<ProductosVendidosDTO> ProductosMenosVendidos {get; set;} = new List<ProductosVendidosDTO>();
    

} 