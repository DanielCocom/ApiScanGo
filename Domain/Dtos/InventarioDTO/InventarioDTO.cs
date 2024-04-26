namespace api_scango.Domain.Dtos;

public class InventarioDTO
{
    public string? IdProducto { get; set; }
    public string? NombreProducto { get; set; }
    public string? Imagen { get; set; }
    public decimal? Precio {get; set;}

    public int? Cantidad {get; set;}

}