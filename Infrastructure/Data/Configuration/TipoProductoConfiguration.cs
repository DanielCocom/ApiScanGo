
using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class TipoProductoConfiguration : IEntityTypeConfiguration<TipoProducto>
{
    public void Configure(EntityTypeBuilder<TipoProducto> builder)
    {
        builder.ToTable("TipoProducto");
        builder.HasKey(e => e.IdTipoProducto).HasName("PK__Tipo_Pro__F5E0BFB83B092371");

            builder.ToTable("Tipo_Producto");

            builder.Property(e => e.IdTipoProducto).HasColumnName("id_tipo_producto");
            builder.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            builder.Property(e => e.PrecioPorKilo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_por_kilo");
    }
}