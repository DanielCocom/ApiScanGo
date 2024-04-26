
using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_scango.Infrastructure.Data.Configurations;

public class ProductoEnCarritoConfiguration : IEntityTypeConfiguration<ProductosEnCarrito>
{
    public void Configure(EntityTypeBuilder<ProductosEnCarrito> builder)
    {
        builder.ToTable("ProductosEnCarrito");
        builder.HasKey(e => e.IdProductoEncarrito).HasName("PK__Producto__65992ACE0AA2451B");

            builder.Property(e => e.IdProductoEncarrito).HasColumnName("id_productoEncarrito");
            builder.Property(e => e.Cantidad).HasColumnName("cantidad");
            builder.Property(e => e.IdCarrito).HasColumnName("id_carrito");
            builder.Property(e => e.IdProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id_producto");
            builder.Property(e => e.NombreProducto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre_producto");
            builder.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            builder.HasOne(d => d.IdCarritoNavigation).WithMany(p => p.ProductosEnCarrito)
                .HasForeignKey(d => d.IdCarrito)
                .HasConstraintName("FK__Productos__id_ca__4AB81AF0");

            builder.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductosEnCarrito)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Productos__id_pr__49C3F6B7");

    }
}
