using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PlastiStock.Data;
using PlastiStock.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context; // contexto de la base de datos

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // obtener todos los productos (cualquier usuario autenticado)
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Producto>>> GetAll()
        {
            var lista = await _context.Productos.ToListAsync();
            return Ok(lista);
        }

        // obtener un producto por ID (cualquier usuario autenticado)
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Producto>> GetById(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound("El producto no existe.");

            return Ok(producto);
        }

        // crear producto (Administrador o Supervisor)
        [HttpPost]
        [Authorize(Roles = "Administrador,Supervisor")]
        public async Task<ActionResult<Producto>> Create(Producto producto)
        {
            if (producto == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }

        // actualizar producto (Administrador o Supervisor)
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Supervisor")]
        public async Task<IActionResult> Update(int id, Producto producto)
        {
            if (producto == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            if (id != producto.Id)
                return BadRequest("El ID no coincide.");

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Productos.Any(e => e.Id == id))
                    return NotFound("Producto no encontrado.");
                else
                    throw;
            }

            return Ok("Producto actualizado correctamente.");
        }

        // eliminar producto (solo Administrador)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound("Producto no encontrado.");

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok("Producto eliminado correctamente.");
        }
    }
}




