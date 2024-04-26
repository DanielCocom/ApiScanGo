using System;
using System.Collections.Generic;

namespace api_scango.Domain.Entities;

public partial class Venta
{
    public int IdVenta { get; set; }

    public DateTime? FechaVenta { get; set; }

    public decimal? TotalPagado { get; set; }

    public int? IdEstablecimiento { get; set; }
    public string? IdTransaccion { get; set; }


    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Establecimiento? IdEstablecimientoNavigation { get; set; }

    
}
