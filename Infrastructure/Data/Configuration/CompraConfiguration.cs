
using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_scango.Infrastructure.Data.Configurations;

public class CompraConfiguration : IEntityTypeConfiguration<Compra>
{
    public void Configure(EntityTypeBuilder<Compra> builder)
    {
        builder.ToTable("Compra");
        builder.HasKey(e => e.IdCompra).HasName("PK__Compra__C4BAA6040B015E15");

            builder.Property(e => e.IdCompra).HasColumnName("id_compra");
            builder.Property(e => e.FechaCompra) 
                .HasColumnType("date")
                .HasColumnName("fecha_compra");
            builder.Property(e => e.IdEstablecimiento).HasColumnName("id_establecimiento");
            builder.Property(e => e.NumeroTelefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("numero_telefono");
            builder.Property(e => e.TotalPagado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_pagado");
            builder.Property(e => e.TotalProductos).HasColumnName("total_productos");

            builder.HasOne(d => d.IdEstablecimientoNavigation).WithMany(p => p.Compra)
                .HasForeignKey(d => d.IdEstablecimiento)
                .HasConstraintName("FK__Compra__id_estab__52593CB8");

            builder.HasOne(d => d.NumeroTelefonoNavigation).WithMany(p => p.Compra)
                .HasForeignKey(d => d.NumeroTelefono)
                .HasConstraintName("FK__Compra__numero_t__5165187F");


    }
}
