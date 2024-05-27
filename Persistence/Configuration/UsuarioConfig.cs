using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(usuario => usuario.IdUsuario);

            builder.Property(usuario => usuario.IdUsuario)
                .HasColumnName("id_usuario");

            builder.Property(usuario => usuario.CorreoElectronico)
                .IsRequired()
                .HasColumnName("correo_electronico")
                .HasMaxLength(100);

            builder.HasIndex(usuario => usuario.CorreoElectronico)
                .HasDatabaseName("idx_usario_correo_electronico")
                .IsUnique();

            builder.Property(usuario => usuario.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasMaxLength(18);

            builder.Property(usuario => usuario.TipoUsuario)
                .IsRequired()
                .HasColumnName("tipo_usuario")
                .HasMaxLength(50);
        }
    }
}
