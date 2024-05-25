using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    internal class AlumnoConfig : IEntityTypeConfiguration<Alumno>
    {
        public void Configure(EntityTypeBuilder<Alumno> builder)
        {
            builder.ToTable("Alumno");

            builder.Property(alumno => alumno.Grado)
                .HasColumnName("grado")
                .IsRequired();
        }
    }
}
