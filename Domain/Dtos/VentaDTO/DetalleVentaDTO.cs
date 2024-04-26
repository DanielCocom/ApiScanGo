namespace api_scango.Domain.Dtos;

public class DetalleVentaDTO{
     public string? IdProducto { get; set; }

     public string? NombreProducto {get; set;}

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? Total { get; set; }
}