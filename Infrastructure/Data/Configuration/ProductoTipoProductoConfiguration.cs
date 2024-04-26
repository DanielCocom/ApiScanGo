
// using api_scango.Domain.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;


// public class ProductoTipoPtoductoConfiguration : IEntityTypeConfiguration<ProductoTipoProducto>
// {
//     public void Configure(EntityTypeBuilder<ProductoTipoProducto> builder)
//     {
//         builder.ToTable("ProductoTipoProducto");
//          builder.HasKey(e => e.IdProductoTipoProducto).HasName("PK__Producto__1F0F156037F09CAD");

//             builder.ToTable("Producto_Tipo_Producto");

//             builder.Property(e => e.IdProductoTipoProducto).HasColumnName("id_producto_tipo_producto");
//             builder.Property(e => e.IdProducto)
//                 .HasMaxLength(50)
//                 .IsUnicode(false)
//                 .HasColumnName("id_producto");
//             builder.Property(e => e.IdTipoProducto).HasColumnName("id_tipo_producto");

//             builder.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoTipoProducto)
//                 .HasForeignKey(d => d.IdProducto)
//                 .HasConstraintName("FK__Producto___id_pr__44FF419A");

//             builder.HasOne(d => d.IdTipoProductoNavigation).WithMany(p => p.ProductoTipoProducto)
//                 .HasForeignKey(d => d.IdTipoProducto)
//                 .HasConstraintName("FK__Producto___id_ti__45F365D3");
//     }
// }