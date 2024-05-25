using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Data
{
    public class ControlEscolarDBContext : DbContext
    {
        public ControlEscolarDBContext(DbContextOptions<ControlEscolarDBContext> options) : base(options) { }

        public DbSet<MiembroEscolar> MiembrosEscolares { get; set; }
        public DbSet<Personal> Personales { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<TipoPersonal> TiposPersonal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
