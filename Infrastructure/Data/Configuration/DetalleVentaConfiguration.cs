using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DetalleVentaConfiguration : IEntityTypeConfiguration<DetalleVenta>
{
    public void Configure(EntityTypeBuilder<DetalleVenta> builder)
    {
        builder.ToTable("DetalleVenta");
        builder.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__5B265D471211943C");

            builder.Property(e => e.IdDetalleVenta).HasColumnName("id_detalle_venta");
            builder.Property(e => e.Cantidad).HasColumnName("cantidad");
            builder.Property(e => e.IdProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id_producto");
            builder.Property(e => e.IdVenta).HasColumnName("id_venta");
            builder.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");
            builder.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            builder.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DetalleVe__id_pr__5FB337D6");

            builder.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DetalleVe__id_ve__5EBF139D");
    }
}
