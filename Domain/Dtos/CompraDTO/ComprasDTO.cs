namespace api_scango.Domain.Entities.Dtos;

public partial class CompraDTO
{
   
    public string? Establecimiento { get; set; }

    public int? TotalProductos { get; set; }
    public decimal? TotalPagado { get; set; }

    public DateTime? FechaCompra { get; set; }

}