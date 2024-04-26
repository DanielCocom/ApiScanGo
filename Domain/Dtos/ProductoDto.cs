namespace api_scango.Domain.Dtos;


public  class ProductoDto
{
    public string IdProducto { get; set; } = null!;
    

    public string? Nombre { get; set; }

    public decimal? Precio { get; set; }

    // public int? Cantidad { get; set; }

    // public string? Imagen { get; set; }
}