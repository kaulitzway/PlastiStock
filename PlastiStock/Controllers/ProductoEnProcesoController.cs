using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlastiStock.Models;
using PlastiStock.Repositorios.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoEnProcesoController : ControllerBase
    {
        private readonly IProductoEnProcesoRepository _repository; // repositorio de productos en proceso

        public ProductoEnProcesoController(IProductoEnProcesoRepository repository)
        {
            _repository = repository;
        }

        // obtener todos los productos en proceso (cualquier usuario autenticado)
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductoEnProceso>>> GetAll()
        {
            var lista = await _repository.GetAllAsync();
            return Ok(lista);
        }

        // obtener un producto en proceso por id (cualquier usuario autenticado)
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductoEnProceso>> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
                return NotFound("Producto en proceso no encontrado.");

            return Ok(item);
        }

        // crear un producto en proceso (solo Administrador)
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<ProductoEnProceso>> Create(ProductoEnProceso entity)
        {
            if (entity == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var nuevo = await _repository.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
        }

        // actualizar un producto en proceso (Administrador, Supervisor u Operario)
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Supervisor,Operario")]
        public async Task<IActionResult> Update(int id, ProductoEnProceso entity)
        {
            if (entity == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            if (id != entity.Id)
                return BadRequest("El ID no coincide.");

            var ok = await _repository.UpdateAsync(entity);
            if (!ok)
                return NotFound("Producto en proceso no encontrado.");

            return NoContent();
        }

        // eliminar un producto en proceso (solo Administrador)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _repository.DeleteAsync(id);
            if (!ok)
                return NotFound("Producto en proceso no encontrado.");

            return NoContent();
        }
    }
}


