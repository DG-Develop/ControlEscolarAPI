using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class MiembroEscolarConfig : IEntityTypeConfiguration<MiembroEscolar>
    {
        public void Configure(EntityTypeBuilder<MiembroEscolar> builder)
        {
            // Configuración de Miembro Escolar
            builder.ToTable("MiembroEscolar");

            builder.HasKey(miembroEscolar => miembroEscolar.IdMiembroEscolar)
                .HasName("PK_miembro_escolar");

            builder.Property(miembroEscolar => miembroEscolar.IdMiembroEscolar)
                .HasColumnName("id_miembro_escolar");

            builder.Property(miembroEscolar => miembroEscolar.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(miembroEscolar => miembroEscolar.Apellidos)
                .HasColumnName("apellidos")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(miembroEscolar => miembroEscolar.CorreoElectronico)
                .HasColumnName("correo_electronico")
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(miembroEscolar => miembroEscolar.CorreoElectronico)
                .HasDatabaseName("idx_correo_electronico")
                .IsUnique();

            builder.Property(miembroEscolar => miembroEscolar.FechaNacimiento)
                .HasColumnType("smalldatetime")
                .HasColumnName("fecha_nacimiento")
                .IsRequired();

            builder.Property(miembroEscolar => miembroEscolar.NumeroControl)
                .HasColumnName("numero_control")
                .IsRequired()
                .HasMaxLength(17);

            builder.Property(miembroEscolar => miembroEscolar.Estatus)
                .IsRequired()
                .HasColumnName("estatus");

            builder.HasOne(miembroEscolar => miembroEscolar.TipoPersonal)
                .WithMany(tipoPersonal => tipoPersonal.MiembrosEscolares)
                .HasForeignKey(miembroEscolar => miembroEscolar.IdTipoPersonal)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
