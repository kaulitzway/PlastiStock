using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlastiStock.Models;
using PlastiStock.Repositories;
using PlastiStock.Repositorios.Interfaces;
using System.Threading.Tasks;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly ISolicitudRepository _repository; // repositorio de solicitudes

        public SolicitudController(ISolicitudRepository repository)
        {
            _repository = repository;
        }

        // ver todas las solicitudes (solo Administrador)
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _repository.GetAllAsync();
            return Ok(lista);
        }

        // crear una solicitud (Administrador, Supervisor u Operario)
        [HttpPost]
        [Authorize(Roles = "Administrador,Supervisor,Operario")]
        public async Task<IActionResult> Create(Solicitud s)
        {
            if (s == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var nueva = await _repository.CreateAsync(s);
            return Ok(nueva);
        }

        // cambiar estado de la solicitud (solo Administrador)
        [HttpPut("{id}/estado")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] EstadoSolicitudDTO dto)
        {
            if (dto == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            await _repository.UpdateEstadoAsync(id, dto.Estado, dto.Observaciones);
            return Ok("Estado actualizado correctamente");
        }
    }

    public class EstadoSolicitudDTO
    {
        public string Estado { get; set; }     // Aprobada | Rechazada
        public string Observaciones { get; set; }
    }
}


