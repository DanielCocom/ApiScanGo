using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class VentaConfiguration : IEntityTypeConfiguration<Venta>
{
    public void Configure(EntityTypeBuilder<Venta> builder)
    {
        builder.ToTable("Venta");
       builder.HasKey(e => e.IdVenta).HasName("PK__Venta__459533BF3B9DC6DA");

            builder.Property(e => e.IdVenta).HasColumnName("id_venta");
            builder.Property(e => e.FechaVenta)
                .HasColumnType("datetime")
                .HasColumnName("fecha_venta");
            builder.Property(e => e.IdEstablecimiento).HasColumnName("id_establecimiento");
            builder.Property(e => e.IdTransaccion)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("idTransaccion");
            builder.Property(e => e.TotalPagado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_pagado");

            builder.HasOne(d => d.IdEstablecimientoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdEstablecimiento)
                .HasConstraintName("FK__Venta__id_establ__5BE2A6F2");
    }
}