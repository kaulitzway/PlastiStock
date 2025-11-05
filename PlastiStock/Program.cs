using Microsoft.EntityFrameworkCore;
using PlastiStock.Data;
using PlastiStock.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Conexión a la base de datos (ajusta el nombre según tu appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 🔹 Registro de dependencias (repositorios)
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// 🔹 Configuración básica del API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlastiStock API", Version = "v1" });
});

var app = builder.Build();

// 🔹 Swagger (solo en desarrollo)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
