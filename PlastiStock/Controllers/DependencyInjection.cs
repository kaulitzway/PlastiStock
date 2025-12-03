using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlastiStock.Data;
using PlastiStock.Repositories;
using PlastiStock.Repositorios;
using PlastiStock.Repositorios.Interfaces;

namespace PlastiStock.Controllers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration _configuration)
        {
            string connectionString = _configuration["ConnectionString:SQLConectionStrngs"];

            // Registro del contexto
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Repositorios existentes
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ITipoDocumentoRepository, TipoDocumentoRepository>();

            // Repositorios nuevos
            services.AddScoped<IProveedorRepository, ProveedorRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IEntradaInventarioRepository, EntradaInventarioRepository>();
            services.AddScoped<ISalidaInventarioRepository, SalidaInventarioRepository>();
            services.AddScoped<IKardexRepository, KardexRepository>();
            services.AddScoped<IOrdenProduccionRepository, OrdenProduccionRepository>();
            services.AddScoped<IMermaRepository, MermaRepository>();

            return services;
        }
    }
}

