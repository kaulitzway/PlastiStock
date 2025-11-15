using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlastiStock.Models;
using PlastiStock.Repositories;
using PlastiStock.Repositorios.Interfaces;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly ISolicitudRepository _repository;

        public SolicitudController(ISolicitudRepository repository)
        {
            _repository = repository;
        }

        // Ver todas las solicitudes (solo AdminPrincipal)
        [HttpGet]
        [Authorize(Roles = "Administrador_Principal")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        // Crear solicitud (AdminSecundario)
        [HttpPost]
        [Authorize(Roles = "Administrador_Secundario")]
        public async Task<IActionResult> Create(Solicitud s)
        {
            var nueva = await _repository.CreateAsync(s);
            return Ok(nueva);
        }

        // Aprobar o rechazar (solo AdminPrincipal)
        [HttpPut("{id}/estado")]
        [Authorize(Roles = "Administrador_Principal")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] EstadoSolicitudDTO dto)
        {
            await _repository.UpdateEstadoAsync(id, dto.Estado, dto.Observaciones);
            return Ok("Estado actualizado");
        }
    }

    public class EstadoSolicitudDTO
    {
        public string Estado { get; set; }     // Aprobada | Rechazada
        public string Observaciones { get; set; }
    }
}

