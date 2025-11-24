using Microsoft.EntityFrameworkCore;
using PlastiStock.Models;

namespace PlastiStock.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoDocumento> TiposDocumento { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Producto> Productos { get; set; }

        //  Nuevas tablas
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<RolPermiso> RolesPermisos { get; set; }
        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<MateriaPrima> MateriasPrimas { get; set; }
        public DbSet<ProductoEnProceso> ProductosEnProceso { get; set; }
        public DbSet<ProductoTerminado> ProductoTerminado { get; set; }
        public object TipoDeDocumento { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Nombres de tablas
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<TipoDocumento>().ToTable("TiposDocumento");
            modelBuilder.Entity<Rol>().ToTable("Roles");
            modelBuilder.Entity<Producto>().ToTable("Productos");
            modelBuilder.Entity<Permiso>().ToTable("Permisos");
            modelBuilder.Entity<RolPermiso>().ToTable("RolesPermisos");
            modelBuilder.Entity<MateriaPrima>().ToTable("MateriasPrimas");




            // Relación Usuario → TipoDocumento
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.TipoDocumento)
                .WithMany(t => t.Usuarios)
                .HasForeignKey(u => u.TipoDocumentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Usuario → Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación MUCHOS A MUCHOS entre Rol y Permiso
            modelBuilder.Entity<RolPermiso>()
                .HasKey(rp => new { rp.RolId, rp.PermisoId });

            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Rol)
                .WithMany(r => r.RolesPermiso)
                .HasForeignKey(rp => rp.RolId);

            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Permiso)
                .WithMany(p => p.RolesPermiso)
                .HasForeignKey(rp => rp.PermisoId);

            // Relación Solicitud → Usuario

            modelBuilder.Entity<Solicitud>()
               .HasOne(s => s.UsuarioSolicitante)
               .WithMany()
               .HasForeignKey(s => s.UsuarioSolicitanteId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Solicitud>()
                .HasOne(s => s.UsuarioAfectado)
                .WithMany()
                .HasForeignKey(s => s.UsuarioAfectadoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Solicitud>()
                .HasOne(s => s.RolSolicitado)
                .WithMany()
                .HasForeignKey(s => s.RolSolicitadoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductoEnProceso>().ToTable("ProductosEnProceso");

            modelBuilder.Entity<ProductoEnProceso>()
                .HasOne(p => p.MateriaPrima)
                .WithMany(m => m.ProductosEnProceso)
                .HasForeignKey(p => p.MateriaPrimaId)
                .OnDelete(DeleteBehavior.Restrict);

           modelBuilder.Entity<Producto>()
                .Property(p => p.PrecioUnitario)
                .HasColumnType("decimal(18,2)");





        }
    }
}




