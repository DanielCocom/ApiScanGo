using System;
using System.Collections.Generic;

namespace api_scango.Domain.Entities;

public partial class Cliente
{
    public string NumeroTelefono { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Correo { get; set; }

    public string? Contrasena { get; set; }

    public int? IdCarrito { get; set; }

    public int? IdEstablecimiento { get; set; }

    public virtual ICollection<Compra> Compra { get; set; } = new List<Compra>();

    public virtual Carrito? IdCarritoNavigation { get; set; }

    public virtual Establecimiento? IdEstablecimientoNavigation { get; set; }
}
