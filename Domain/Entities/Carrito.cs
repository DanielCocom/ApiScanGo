using System;
using System.Collections.Generic;

namespace api_scango.Domain.Entities;

public partial class Carrito
{
    public int IdCarrito { get; set; }

    public decimal? TotalPagar { get; set; }

    public int? TotalArticulos { get; set; }

    public virtual ICollection<Cliente> Cliente { get; set; } = new List<Cliente>();

    public virtual ICollection<ProductosEnCarrito> ProductosEnCarrito { get; set; } = new List<ProductosEnCarrito>();
}
