using System;
using System.Collections.Generic;

namespace api_scango.Domain.Entities;

public partial class ProductoUpdateDTO
{
    public string IdProducto { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    // public string? Imagen { get; set; }
    public int? stock { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

   
}
