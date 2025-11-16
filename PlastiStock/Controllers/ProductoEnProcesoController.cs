using Microsoft.AspNetCore.Mvc;
using PlastiStock.Models;
using PlastiStock.Repositorios.Interfaces;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoEnProcesoController : ControllerBase
    {
        private readonly IProductoEnProcesoRepository _repository;

        public ProductoEnProcesoController(IProductoEnProcesoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoEnProceso>>> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoEnProceso>> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoEnProceso>> Create(ProductoEnProceso entity)
        {
            var nuevo = await _repository.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductoEnProceso entity)
        {
            if (id != entity.Id) return BadRequest();
            var ok = await _repository.UpdateAsync(entity);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _repository.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}

