
// using api_scango.Domain.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;


// public class ProductoDescuentoConfiguration : IEntityTypeConfiguration<ProductoDescuento>
// {
//     public void Configure(EntityTypeBuilder<ProductoDescuento> builder)
//     {
//         builder.ToTable("ProductoDescuento");
//         builder.HasKey(e => e.IdProductoDescuento).HasName("PK__Producto__10A9692ACF3BE36D");

//             builder.ToTable("Producto_Descuento");

//             builder.Property(e => e.IdProductoDescuento).HasColumnName("id_producto_descuento");
//             builder.Property(e => e.IdDescuento).HasColumnName("id_descuento");
//             builder.Property(e => e.IdProducto)
//                 .HasMaxLength(50)
//                 .IsUnicode(false)
//                 .HasColumnName("id_producto");

//             builder.HasOne(d => d.IdDescuentoNavigation).WithMany(p => p.ProductoDescuento)
//                 .HasForeignKey(d => d.IdDescuento)
//                 .HasConstraintName("FK__Producto___id_de__5535A963");

//             builder.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoDescuento)
//                 .HasForeignKey(d => d.IdProducto)
//                 .HasConstraintName("FK__Producto___id_pr__5441852A");


//     }
// }