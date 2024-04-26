using System;
using System.Collections.Generic;

namespace api_scango.Domain.Entities;

public partial class DetalleVenta
{
    public int IdDetalleVenta { get; set; }

    public int? IdVenta { get; set; }

    public string? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? Total { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}
