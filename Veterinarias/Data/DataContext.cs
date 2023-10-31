using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Veterinarias.Modelos;
namespace Veterinarias.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Animales> Animales { get; set; }
        public DbSet<Razas> Razas { get; set; }
        public DbSet<Personas> Personas { get; set; }
        public DbSet<Veterinarios> Veterinarios { get; set; }
        public DbSet<Mascotas> Mascotas { get; set; }
        public DbSet<Bandeja> Bandeja { get; set; }
        public DbSet<Visitas> Visitas { get; set; }
        

        public DbSet<PR_RAZAS_S01> PR_RAZAS_S01 { get; set; }
        public DbSet<PR_PERSONAS_S01> PR_PERSONAS_S01 { get; set; }
        public DbSet<PR_VETERINARIOS_S01> PR_VETERINARIOS_S01 { get; set; }
        public DbSet<PR_MASCOTAS_S01> PR_MASCOTAS_S01 { get; set; }
        public DbSet<PR_VISITAS_S01> PR_VISITAS_S01 { get; set; }
    }
}