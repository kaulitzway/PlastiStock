using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using PlastiStock.Contest;
using PlastiStock.Repositorios;
using PlastiStock.Repositorios.Interfaces;

namespace PlastiStock.Controllers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration _configuration)
        {
            // Aquí puedes registrar otros servicios si es necesario
            // services.AddScoped<IOtroServicio, OtroServicioImplementacion>();

            string connectionString = "";
            connectionString = _configuration["ConnectionString:SQLConectionStrngs"];

            services.AddDbContext<PlasticStockContext>(options => options.UseSqlServer(connectionString)); // Configura el contexto de la base de datos con SQL Server con la cadena de conexión proporcionada
            services.AddScoped<IUsuariosRepositories, UsuariosRepositories>();// Registra el repositorio de usuarios como un servicio de alcance (scoped) en el contenedor de inyección de dependencias.
            services.AddScoped<ITipoDocumentoRepository, TipoDocumentoRepository>();// Registra el repositorio de tipos de documento como un servicio de alcance (scoped) en el contenedor de inyección de dependencias.

            return services;



        }
    }
}
