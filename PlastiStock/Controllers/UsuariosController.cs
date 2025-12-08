using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlastiStock.Models;
using PlastiStock.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using PlastiStock.Repositories.Interfaces;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository; // repositorio
        private readonly IConfiguration _configuration; // config jwt

        public UsuariosController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        // crear usuario

        [HttpPost("crear")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña); // encriptar clave

            var creado = await _usuarioRepository.AddAsync(usuario);

            if (!creado)
                return StatusCode(500, "No se pudo crear el usuario.");

            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        // listar usuarios
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return Ok(usuarios);
        }

        // obtener usuario
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // actualizar usuario
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if (usuario.Id != id)
                return BadRequest("No coincide el ID.");

            if (!string.IsNullOrEmpty(usuario.Contraseña))
            {
                usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña); // encriptar clave
            }

            var actualizado = await _usuarioRepository.UpdateAsync(usuario);

            if (!actualizado)
                return NotFound();

            return Ok("Usuario actualizado.");
        }

        // eliminar usuario

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _usuarioRepository.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            return Ok("Usuario eliminado.");
        }
    }

    public class LoginRequest
    {
        public string Correo { get; set; }
        public string Contrasena { get; set; } 
    }
}
