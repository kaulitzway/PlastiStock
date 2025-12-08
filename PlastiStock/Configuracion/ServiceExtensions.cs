using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlastiStock.Data;
using PlastiStock.Repositories;
using PlastiStock.Repositories.Interfaces;

namespace PlastiStock
{
    public static class ConfigServiceExtensions
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration configuration)
        {
            // Conexión a la base de datos 
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Registro de repositorios
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRolRepository, RolRepository>();

            return services;
        }
    }
}
