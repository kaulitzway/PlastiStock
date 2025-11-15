using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlastiStock.Models;
using PlastiStock.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public UsuariosController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("IniciarSesion")]
        [SwaggerOperation(Summary = "Iniciar sesión", Description = "Verifica las credenciales del usuario y genera un token JWT.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginRequest login)
        {
            if (login == null || string.IsNullOrEmpty(login.Correo) || string.IsNullOrEmpty(login.Contraseña))
                return BadRequest("Debe ingresar el correo y la contraseña.");

            var usuarios = await _usuarioRepository.GetAllAsync();
            var usuario = usuarios.FirstOrDefault(u =>
                u.Correo == login.Correo && u.Contraseña == login.Contraseña);

            if (usuario == null)
                return Unauthorized("Credenciales incorrectas.");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "baseWebApiSubject"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", usuario.Id.ToString()),
                new Claim("Correo", usuario.Correo),
                new Claim("Rol", usuario.Rol != null ? usuario.Rol.Nombre : "Usuario")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: singIn
            );

            return Ok(new
            {
                success = true,
                message = "Inicio de sesión exitoso.",
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todos los usuarios", Description = "Devuelve una lista de todos los usuarios registrados.")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            if (usuarios == null || !usuarios.Any())
                return NotFound("No se encontraron usuarios registrados.");

            return Ok(usuarios);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Obtener usuario por ID", Description = "Devuelve la información de un usuario específico.")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
                return NotFound($"No se encontró un usuario con el ID {id}.");

            return Ok(usuario);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crear usuario", Description = "Crea un nuevo usuario en la base de datos.")]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var creado = await _usuarioRepository.AddAsync(usuario);

            if (!creado)
                return StatusCode(StatusCodes.Status500InternalServerError, "No se pudo crear el usuario.");

            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(Summary = "Actualizar usuario", Description = "Actualiza la información de un usuario existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null || usuario.Id != id)
                return BadRequest("El ID del usuario no coincide con el de la solicitud.");

            var actualizado = await _usuarioRepository.UpdateAsync(usuario);

            if (!actualizado)
                return NotFound($"No se pudo actualizar el usuario con el ID {id}.");

            return Ok("Usuario actualizado correctamente.");
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Eliminar usuario", Description = "Elimina un usuario de la base de datos por su ID.")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _usuarioRepository.DeleteAsync(id);

            if (!eliminado)
                return NotFound($"No se encontró el usuario con el ID {id}.");

            return Ok("Usuario eliminado correctamente.");
        }
    }

    public class LoginRequest
    {
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }
}


