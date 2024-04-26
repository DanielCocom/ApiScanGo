using System;
using System.Collections.Generic;

namespace api_scango.Domain.Entities;

public partial class CompraDetalles
{
    public int IdDetalleCompra { get; set; }

    public int? IdCompra { get; set; }

    public string? IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Total { get; set; }

    public virtual Compra? IdCompraNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
