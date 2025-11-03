using Microsoft.EntityFrameworkCore;
using PlastiStock.Models;

namespace PlastiStock.Contest
{
    public class PlasticStockContext : DbContext
    {
        public PlasticStockContext(DbContextOptions<PlasticStockContext> options)
            : base(options)
        {
        }

        // Estas son las tablas (entidades)
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<TipoDocumento> TiposDeDocumento { get; set; }

        // Configuraciones adicionales
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Relación uno a muchos entre TipoDocumento y Usuario
            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey (e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Contraseña).IsRequired().HasMaxLength(255);
                entity.HasOne(e => e.TipoDocumento)
                      .WithMany(t => t.Usuarios)
                      .HasForeignKey(e => e.TipoDocumentoId);



            } );

        }


    }

}

