
using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class DescuentoConfiguration : IEntityTypeConfiguration<Descuento>
{
    public void Configure(EntityTypeBuilder<Descuento> builder)
    {
        builder.ToTable("Descuento");
         builder.HasKey(e => e.IdDescuento).HasName("PK__Descuent__4F9A1A80336A33DF");

            builder.Property(e => e.IdDescuento).HasColumnName("id_descuento");
            builder.Property(e => e.Porcentaje)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("porcentaje");
    }
}