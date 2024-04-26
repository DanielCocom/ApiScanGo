
using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_scango.Infrastructure.Data.Configurations;

public class DetalleCompraConfiguration : IEntityTypeConfiguration<CompraDetalles>
{
    public void Configure(EntityTypeBuilder<CompraDetalles> builder)
    {
        builder.ToTable("CompraDetalles");
         builder.HasKey(e => e.IdDetalleCompra).HasName("PK__CompraDe__BD16E279EE9D9862");

            builder.Property(e => e.IdDetalleCompra)
                // .ValueGeneratedNever()
                .HasColumnName("id_detalle_compra");
            builder.Property(e => e.Cantidad).HasColumnName("cantidad");
            builder.Property(e => e.IdCompra).HasColumnName("id_compra");
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

            builder.HasOne(d => d.IdCompraNavigation).WithMany(p => p.CompraDetalles)
                .HasForeignKey(d => d.IdCompra)
                .HasConstraintName("FK__CompraDet__id_co__5535A963");

            builder.HasOne(d => d.IdProductoNavigation).WithMany(p => p.CompraDetalles)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__CompraDet__id_pr__5629CD9C");

    }
}
