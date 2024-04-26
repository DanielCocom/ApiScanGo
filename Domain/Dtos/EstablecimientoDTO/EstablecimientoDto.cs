namespace api_scango.Domain.Dtos;

public class EstablecimientoDTO
{
    public int? idSuper {get; set;}
    public string? Nombre { get; set; }

    public string? Imagen { get; set; }
    public int? IdInventar {get; set;}

    public string? Direccion { get; set; }

    public decimal? Longitud { get; set; }

    public decimal? Latitud { get; set; }

}