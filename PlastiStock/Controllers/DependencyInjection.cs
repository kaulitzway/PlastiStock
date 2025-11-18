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

            // Aquí se registra el ÚNICO contexto válido
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Repositorios reales
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ITipoDocumentoRepository, TipoDocumentoRepository>();

            return services;
        }
    }
}

