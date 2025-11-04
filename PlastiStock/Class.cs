using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PlastiStock // 
{
    public static class ServiceExtensions
    {
        public static void AddExternal(this IServiceCollection services, IConfiguration configuration)
        {
            // Aquí registras las dependencias o servicios
            string connectionString = configuration.GetConnectionString("SQLConectionStrngs");

            // Ejemplo: registrar tu contexto de base de datos si usas Entity Framework
            // services.AddDbContext<MiDbContext>(options =>
            //     options.UseSqlServer(connectionString));

            
        }
    }
}
