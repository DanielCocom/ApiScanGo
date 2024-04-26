
namespace api_scango.Domain.Dtos;
public class ProductoEnCarritoDto
{
    public string? Codigodebarras { get; set; }
    public string? Nombre { get; set; }
    public int? Cantidad { get; set; }
    public int? Precio { get; set; }
    public decimal? Total { get; set; }

}