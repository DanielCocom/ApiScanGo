using System;
using System.Collections.Generic;

namespace api_scango.Domain.Entities;

public partial class TipoProducto
{
    public int IdTipoProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal? PrecioPorKilo { get; set; }

    public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
}
