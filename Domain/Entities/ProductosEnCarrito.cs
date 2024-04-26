using System;
using System.Collections.Generic;

namespace api_scango.Domain.Entities;

public partial class ProductosEnCarrito
{
    public int IdProductoEncarrito { get; set; }

    public string? IdProducto { get; set; }

    public int? IdCarrito { get; set; }

    public string? NombreProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Total { get; set; }

    public virtual Carrito? IdCarritoNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
