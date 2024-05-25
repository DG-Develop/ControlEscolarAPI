using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    internal class PersonalConfig : IEntityTypeConfiguration<Personal>
    {
        public void Configure(EntityTypeBuilder<Personal> builder)
        {
            // Configuración de Personal
            builder.ToTable("Personal");

            builder.Property(personal => personal.Sueldo)
                .HasColumnType("money")
                .HasColumnName("sueldo")
                .IsRequired();
        }
    }
}
