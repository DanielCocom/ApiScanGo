
using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_scango.Infrastructure.Data.Configurations;

public class EstablecimientoConfiguration : IEntityTypeConfiguration<Establecimiento>
{
    public void Configure(EntityTypeBuilder<Establecimiento> builder)
    {
        builder.ToTable("Establecimiento");
        builder.HasKey(e => e.IdEstablecimiento).HasName("PK__Establec__AFEAEA2088B7779A");

            builder.Property(e => e.IdEstablecimiento).HasColumnName("id_establecimiento");
            builder.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("direccion");
            builder.Property(e => e.IdInventario).HasColumnName("id_inventario");
            builder.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagen");
            builder.Property(e => e.Latitud)
                .HasColumnType("decimal(10, 6)")
                .HasColumnName("latitud");
            builder.Property(e => e.Longitud)
                .HasColumnType("decimal(10, 6)")
                .HasColumnName("longitud");
            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            builder.HasOne(d => d.IdInventarioNavigation).WithMany(p => p.Establecimiento)
                .HasForeignKey(d => d.IdInventario)
                .HasConstraintName("FK__Estableci__id_in__44FF419A");

    }
}
