using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class VwPersonalConfig : IEntityTypeConfiguration<VwPersonal>
    {
        public void Configure(EntityTypeBuilder<VwPersonal> builder)
        {
            builder.HasNoKey();
            builder.ToView("VwPersonal");
            builder.Property(e => e.NombreCompleto).HasColumnName("NombreCompleto");
            builder.Property(e => e.Correo).HasColumnName("Correo");
            builder.Property(e => e.NumeroControl).HasColumnName("NumeroControl");
            builder.Property(e => e.TipoPersonal).HasColumnName("TipoPersonal");
            builder.Property(e => e.Sueldo).HasColumnName("Sueldo");
        }
    }
}
