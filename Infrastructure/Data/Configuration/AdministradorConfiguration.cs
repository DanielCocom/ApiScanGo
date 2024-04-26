
using api_scango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class AdministradorConfiguration : IEntityTypeConfiguration<Administrador>
{
    public void Configure(EntityTypeBuilder<Administrador> builder)
    {
        builder.ToTable("Administrador");
        builder.HasKey(e => e.IdAdministrador).HasName("PK__Administ__0FE822AA0ABFEC6D");

            builder.Property(e => e.IdAdministrador).HasColumnName("id_administrador");
            builder.Property(e => e.Contraseña)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            builder.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            builder.Property(e => e.IdEstablecimiento).HasColumnName("id_establecimiento");
            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasOne(d => d.IdEstablecimientoNavigation).WithMany(p => p.Administrador)
                .HasForeignKey(d => d.IdEstablecimiento)
                .HasConstraintName("FK__Administr__id_es__59063A47");
    }
}