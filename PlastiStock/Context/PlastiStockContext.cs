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
                entity.HasKey(e => e.Id).HasName("Id");
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100).HasColumnName("Nombre");
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100).HasColumnName("Apellido");
                entity.Property(e => e.TipoDocumentoId).IsRequired().HasColumnName("TipoDocumentoId");
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(150).HasColumnName("Correo");
                entity.Property(e => e.Contraseña).IsRequired().HasMaxLength(255).HasColumnName("Contraseña");
                entity.HasOne(e => e.TipoDocumento)
                      .WithMany(t => t.Usuarios)
                      .HasForeignKey(e => e.TipoDocumentoId);
                entity.ToTable("Usuarios");



            } );

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100).HasColumnName("Nombre");
                entity.Property(e => e.Abreviatura).IsRequired().HasMaxLength(10).HasColumnName("Abreviatura");
                entity.ToTable("TipoDocumento");
            } );

            base.OnModelCreating(modelBuilder);

        }


    }

}

