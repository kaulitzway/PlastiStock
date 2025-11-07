using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlastiStock.Models;

namespace PlastiStock.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerarToken(Usuario usuario)
        {
            // Llave secreta (definida en appsettings.json)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            // Credenciales de firma
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Lista de claims (información dentro del token)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Nombre ?? "Desconocido"),
                new Claim("id", usuario.Id.ToString()),
                new Claim("Correo", usuario.Correo ?? "sin_email"),
                new Claim("Rol", usuario.Rol?.Nombre ?? "Usuario"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Crear el token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            //Convertirlo a cadena
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
