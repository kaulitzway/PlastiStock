using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InicioSesionController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public InicioSesionController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] InicioSesionRequest login)
        {
            if (login == null || string.IsNullOrWhiteSpace(login.Correo) || string.IsNullOrWhiteSpace(login.Contrasena))
                return BadRequest("Debe ingresar correo y contraseña.");

            var usuario = (await _usuarioRepository.GetAllAsync())
                          .FirstOrDefault(u => u.Correo == login.Correo);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(login.Contrasena, usuario.Contraseña))
                return Unauthorized("Credenciales incorrectas.");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol?.Nombre ?? "Usuario")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return Ok(new
            {
                success = true,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                message = "Inicio de sesión exitoso."
            });
        }
    }
}





