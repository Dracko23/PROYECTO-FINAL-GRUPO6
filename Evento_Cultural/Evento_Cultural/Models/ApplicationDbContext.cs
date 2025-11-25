using Microsoft.EntityFrameworkCore;

namespace Evento_Cultural.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Participacion> Participaciones { get; set; }
        public DbSet<Ganador> Ganadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales si son necesarias
            base.OnModelCreating(modelBuilder);
        }
    }
}