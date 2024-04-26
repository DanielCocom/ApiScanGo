namespace api_scango.Domain.Dtos;


public partial class ProductoCreateDto
{
    public string IdProducto { get; set; } = null!;

    public string? Nombre { get; set; }

    public decimal? Precio { get; set; }

    // public int? Cantidad { get; set; }

    // public string? Imagen { get; set; }

    public int? IdTipoProducto {get; set;}
    public int? IdDescuento {get; set;} 

}