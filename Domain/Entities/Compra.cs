using System;
using System.Collections.Generic;

namespace api_scango.Domain.Entities;

public partial class Compra
{
    public int IdCompra { get; set; }

    public string? NumeroTelefono { get; set; }

    public decimal? TotalPagado { get; set; }

    public int? TotalProductos { get; set; }

    public DateTime? FechaCompra { get; set; }

    public int? IdEstablecimiento { get; set; }

    public virtual ICollection<CompraDetalles> CompraDetalles { get; set; } = new List<CompraDetalles>();

    public virtual Establecimiento? IdEstablecimientoNavigation { get; set; }

    public virtual Cliente? NumeroTelefonoNavigation { get; set; }
}
