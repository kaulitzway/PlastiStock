using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlastiStock.Repositorios;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")] //http://localhost:5000/api/usuarios
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepositories _usuariosRepositories; // variable de solo lectura para el repositorio usuarios del tipo global

        public UsuariosController(IUsuariosRepositories usuariosRepositories)
        {
            _usuariosRepositories = usuariosRepositories;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] //indica que la respuesta exitosa devuelve un código 200
        [ProducesResponseType(StatusCodes.Status404NotFound)] //indica que la respuesta fallida devuelve un código 404
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] //indica que la respuesta fallida devuelve un código 500
        public async Task<IActionResult> GetAllUsuarios() //IActionResult es una interfaz que representa el resultado de una acción de un controlador en ASP.NET Core
        {
            try
            {
                var usuarios = await _usuariosRepositories.ObtenerUsuarios();

                if (usuarios == null || !usuarios.Any()) //verifica si la lista está vacía
                {
                    return NotFound("No se encontraron usuarios."); //devuelve un código 404 con un mensaje
                }
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                //Aqui puedes registrar el error si es necesario
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los usuarios: " + ex.Message);
            }


        }
    }
}

