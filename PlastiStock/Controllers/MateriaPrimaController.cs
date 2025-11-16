using Microsoft.AspNetCore.Mvc;
using PlastiStock.Models;
using PlastiStock.Repositorios.Interfaces;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaPrimaController : ControllerBase
    {
        private readonly IMateriaPrimaRepository _repository;

        public MateriaPrimaController(IMateriaPrimaRepository repository)
        {
            _repository = repository;
        }

        // GET: api/MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaPrima>>> GetAll()
        {
            var lista = await _repository.GetAllAsync();
            return Ok(lista);
        }

        // GET: api/MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaPrima>> GetById(int id)
        {
            var materia = await _repository.GetByIdAsync(id);

            if (materia == null)
                return NotFound();

            return Ok(materia);
        }

        // POST: api/MateriaPrima
        [HttpPost]
        public async Task<ActionResult<MateriaPrima>> Create(MateriaPrima materiaPrima)
        {
            var nueva = await _repository.CreateAsync(materiaPrima);
            return CreatedAtAction(nameof(GetById), new { id = nueva.Id }, nueva);
        }

        // PUT: api/MateriaPrima/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MateriaPrima materiaPrima)
        {
            if (id != materiaPrima.Id)
                return BadRequest("El ID no coincide.");

            var actualizado = await _repository.UpdateAsync(materiaPrima);

            if (!actualizado)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _repository.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}

