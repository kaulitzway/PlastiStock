using Microsoft.AspNetCore.Mvc;
using PlastiStock.Models;
using PlastiStock.Repositories;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // ✅ GET: api/usuarios
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            if (usuarios == null || !usuarios.Any())
                return NotFound("No se encontraron usuarios registrados.");

            return Ok(usuarios);
        }

        // ✅ GET: api/usuarios/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
                return NotFound($"No se encontró un usuario con el ID {id}.");

            return Ok(usuario);
        }

        // ✅ POST: api/usuarios
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var creado = await _usuarioRepository.AddAsync(usuario);

            if (!creado)
                return StatusCode(StatusCodes.Status500InternalServerError, "No se pudo crear el usuario.");

            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        // ✅ PUT: api/usuarios/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null || usuario.Id != id)
                return BadRequest("El ID del usuario no coincide con el de la solicitud.");

            var actualizado = await _usuarioRepository.UpdateAsync(usuario);

            if (!actualizado)
                return NotFound($"No se pudo actualizar el usuario con el ID {id}.");

            return Ok("Usuario actualizado correctamente.");
        }

        // ✅ DELETE: api/usuarios/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _usuarioRepository.DeleteAsync(id);

            if (!eliminado)
                return NotFound($"No se encontró el usuario con el ID {id}.");

            return Ok("Usuario eliminado correctamente.");
        }
    }
}


