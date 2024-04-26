using System;
using System.Collections.Generic;

namespace api_scango.Domain.Entities;

public partial class ProductoInventario
{
    public int IdProductoInventario { get; set; }

    public int? IdInventario { get; set; }

    public string? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public virtual Inventario? IdInventarioNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
