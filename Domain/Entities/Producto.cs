using System;
using System.Collections.Generic;

namespace api_scango.Domain.Entities;

public partial class Producto
{
    public string IdProducto { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Imagen { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int? IdTipoProducto { get; set; }

    public int? IdDescuento { get; set; }

    public virtual ICollection<CompraDetalles> CompraDetalles { get; set; } = new List<CompraDetalles>();

    public virtual Descuento? IdDescuentoNavigation { get; set; }

    public virtual TipoProducto? IdTipoProductoNavigation { get; set; }

    public virtual ICollection<ProductoInventario> ProductoInventario { get; set; }  = new List<ProductoInventario>();
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();


    public virtual ICollection<ProductosEnCarrito> ProductosEnCarrito { get; set; } = new List<ProductosEnCarrito>();
}
