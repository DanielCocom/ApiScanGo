namespace api_scango.Domain.Dtos;
public class VentaMesDTO
{
    public int? Anio { get; set; }
    public string? Mes { get; set; }

    public decimal? Ganancias { get; set; }
    public int? TotalProductosVendidos { get; set; }

    public int? idEstablecimiento { get; set; }
}