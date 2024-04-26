
using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_scango.Infrastructure.Data.Configurations;

public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        
       builder.ToTable("Producto");
                   builder.HasKey(e => e.IdProducto).HasName("PK__Producto__FF341C0D99FE9EB9");

            builder.Property(e => e.IdProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id_producto");
            builder.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            builder.Property(e => e.IdDescuento).HasColumnName("id_descuento");
            builder.Property(e => e.IdTipoProducto).HasColumnName("id_tipo_producto");
            builder.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagen");
            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            builder.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            builder.HasOne(d => d.IdDescuentoNavigation).WithMany(p => p.Producto)
                .HasForeignKey(d => d.IdDescuento)
                .HasConstraintName("FK__Producto__id_des__3C69FB99");

            builder.HasOne(d => d.IdTipoProductoNavigation).WithMany(p => p.Producto)
                .HasForeignKey(d => d.IdTipoProducto)
                .HasConstraintName("FK__Producto__id_tip__3B75D760");

    }
}
