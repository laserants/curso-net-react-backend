using Curso_Backend_SEGEPLAN.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Curso_Backend_SEGEPLAN.DataContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Beneficiario> Beneficiarios { get; set; }
        public DbSet<Ejecutor> Ejecutores { get; set; }
        public DbSet<ProyectoBeneficiario> ProyectosBeneficiarios { get; set; }
        public DbSet<ProyectoEjecutor> ProyectosEjecutores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<ProyectoBeneficiario>().HasKey(pb => new { pb.ProyectoID, pb.BeneficiarioID });
            builder.Entity<ProyectoEjecutor>().HasKey(pe => new { pe.ProyectoID, pe.EjecutorID }); 
        }
    }
}
