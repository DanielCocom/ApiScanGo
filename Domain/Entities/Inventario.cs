using System;
using System.Collections.Generic;

namespace api_scango.Domain.Entities;

public partial class Inventario
{
    public int IdInventario { get; set; }

    public virtual ICollection<Establecimiento> Establecimiento { get; set; } = new List<Establecimiento>();

    public virtual ICollection<ProductoInventario> ProductoInventario { get; set; } = new List<ProductoInventario>();
}
