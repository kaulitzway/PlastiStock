using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaPrimaController : ControllerBase
    {
        private readonly IMateriaPrimaRepository _repository; // repositorio de MateriaPrima

        public MateriaPrimaController(IMateriaPrimaRepository repository)
        {
            _repository = repository;
        }

        // obtener todas las materias primas (cualquiera autenticado)
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MateriaPrima>>> GetAll()
        {
            var lista = await _repository.GetAllAsync();
            return Ok(lista);
        }

        // obtener una materia prima por id (cualquiera autenticado)
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<MateriaPrima>> GetById(int id)
        {
            var materia = await _repository.GetByIdAsync(id);

            if (materia == null)
                return NotFound();

            return Ok(materia);
        }

        // crear materia prima (Administrador o Supervisor)
        [HttpPost]
        [Authorize(Roles = "Administrador,Supervisor")]
        public async Task<ActionResult<MateriaPrima>> Create(MateriaPrima materiaPrima)
        {
            if (materiaPrima == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var nueva = await _repository.CreateAsync(materiaPrima);
            return CreatedAtAction(nameof(GetById), new { id = nueva.Id }, nueva);
        }

        // actualizar materia prima (Administrador o Supervisor)
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Supervisor")]
        public async Task<IActionResult> Update(int id, MateriaPrima materiaPrima)
        {
            if (materiaPrima == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            if (id != materiaPrima.Id)
                return BadRequest("El ID no coincide.");

            var actualizado = await _repository.UpdateAsync(materiaPrima);

            if (!actualizado)
                return NotFound();

            return NoContent();
        }

        // eliminar materia prima (solo Administrador)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _repository.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}


