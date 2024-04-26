
using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_scango.Infrastructure.Data.Configurations;

public class CarritoConfiguration : IEntityTypeConfiguration<Carrito>
{
    public void Configure(EntityTypeBuilder<Carrito> builder)
    {
        builder.ToTable("Carrito");
                  builder.HasKey(e => e.IdCarrito).HasName("PK__Carrito__83A2AD9CBE547550");

            builder.Property(e => e.IdCarrito).HasColumnName("id_carrito");
            builder.Property(e => e.TotalArticulos).HasColumnName("total_articulos");
            builder.Property(e => e.TotalPagar)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_pagar");



    }
}
