namespace api_scango.Domain.Dtos;

public class FrutaCreateDTO
{
    public int IdFruta { get; set; }
    public string? Nombre { get; set; }
    public string? Imagen { get; set; }
    public int idTipo { get; set; }
    public int idDescuento { get; set; }
}