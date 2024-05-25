using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    internal class TipoPersonalConfig : IEntityTypeConfiguration<TipoPersonal>
    {
        public void Configure(EntityTypeBuilder<TipoPersonal> builder)
        {
            // Configuración de TipoPersonal
            builder.ToTable("TipoPersonal");

            builder.HasKey(tipoPersonal => tipoPersonal.IdTipoPersonal)
                .HasName("PK_tipo_personal");

            builder.Property(tipoPersonal => tipoPersonal.IdTipoPersonal)
                .HasColumnName("id_tipo_personal");

            builder.Property(tipoPersonal => tipoPersonal.Descripcion)
                .IsRequired()
                .HasColumnName("descripcion")
                .HasMaxLength(50);

            builder.HasIndex(tipoPersonal => tipoPersonal.Descripcion)
                .HasDatabaseName("idx_descripcion")
                .IsUnique();

            builder.Property(tipoPersonal => tipoPersonal.SueldoMinimo)
                .HasColumnType("money")
                .HasDefaultValue(0)
                .HasColumnName("sueldo_minimo");

            builder.Property(tipoPersonal => tipoPersonal.SueldoMaximo)
                .HasColumnType("money")
                .HasDefaultValue(0)
                .HasColumnName("sueldo_maximo");


            builder.Property(tipoPersonal => tipoPersonal.IdentificadorDeControl)
                .HasColumnType("char")
                .HasMaxLength(1)
                .HasDefaultValue("")
                .HasColumnName("identificador_de_control");
        }
    }
}
