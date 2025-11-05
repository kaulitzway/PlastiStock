using Microsoft.EntityFrameworkCore;
using PlastiStock.Models;

namespace PlastiStock.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // 🔹 Tablas del sistema
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoDocumento> TiposDocumento { get; set; }
        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Nombres de tablas
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<TipoDocumento>().ToTable("TiposDocumento");
            modelBuilder.Entity<Rol>().ToTable("Roles");

            // 🔹 Relación TipoDocumento → Usuario (1:N)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.TipoDocumento)
                .WithMany(t => t.Usuarios)
                .HasForeignKey(u => u.TipoDocumentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Relación Rol → Usuario (1:N)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

