
using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class ProductoInventariorConfiguration : IEntityTypeConfiguration<ProductoInventario>
{
    public void Configure(EntityTypeBuilder<ProductoInventario> builder)

    {
        builder.ToTable("ProductoInventario");
        builder.HasKey(e => e.IdProductoInventario).HasName("PK__Producto__7362DAA55F93E78C");

            builder.Property(e => e.IdProductoInventario).HasColumnName("id_producto_inventario");
            builder.Property(e => e.Cantidad).HasColumnName("cantidad");
            builder.Property(e => e.IdInventario).HasColumnName("id_inventario");
            builder.Property(e => e.IdProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id_producto");

            builder.HasOne(d => d.IdInventarioNavigation).WithMany(p => p.ProductoInventario)
                .HasForeignKey(d => d.IdInventario)
                .HasConstraintName("FK__ProductoI__id_in__4222D4EF");

            builder.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoInventario)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__ProductoI__id_pr__412EB0B6");
    }
}