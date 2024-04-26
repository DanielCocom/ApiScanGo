namespace api_scango.Domain.Dtos;
public  class VentasTiendaDTO
{
     public int IdVenta { get; set; }

    public DateTime? FechaVenta { get; set; }

    public decimal? TotalPagado { get; set; }
    public string? NombreTienda  {get;set;}

    public string? IdTransaccion {get; set;}
}