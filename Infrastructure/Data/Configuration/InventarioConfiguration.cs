using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_scango.Infrastructure.Data.Configurations;


public class InventarioConfiguration : IEntityTypeConfiguration<Inventario>
{
    public void Configure(EntityTypeBuilder<Inventario> builder)
    {
        builder.ToTable("Inventario");
       builder.HasKey(e => e.IdInventario).HasName("PK__Inventar__013AEB517298FE6E");

            builder.Property(e => e.IdInventario).HasColumnName("id_inventario");
    } 
}