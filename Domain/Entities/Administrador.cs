using System;
using System.Collections.Generic;

namespace api_scango.Domain.Entities;

public partial class Administrador
{
    public int IdAdministrador { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Contraseña { get; set; }

    public int? IdEstablecimiento { get; set; }

    public virtual Establecimiento? IdEstablecimientoNavigation { get; set; }
}
