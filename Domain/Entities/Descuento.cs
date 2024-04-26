using System;
using System.Collections.Generic;

namespace api_scango.Domain.Entities;

public partial class Descuento
{
    public int IdDescuento { get; set; }

    public decimal Porcentaje { get; set; }

    public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
}
