using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class VwAlumnoConfig : IEntityTypeConfiguration<VwAlumno>
    {
        public void Configure(EntityTypeBuilder<VwAlumno> builder)
        {
            builder.HasNoKey();
            builder.ToView("VwAlumno");
            builder.Property(e => e.NombreCompleto).HasColumnName("NombreCompleto");
            builder.Property(e => e.Correo).HasColumnName("Correo");
            builder.Property(e => e.NumeroControl).HasColumnName("NumeroControl");
            builder.Property(e => e.TipoPersonal).HasColumnName("TipoPersonal");
            builder.Property(e => e.GradoEscolar).HasColumnName("GradoEscolar");
        }
    }
}
