using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlastiStock.Data;
using PlastiStock.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoTerminadoController : ControllerBase
    {
        private readonly AppDbContext _context; // contexto de la base de datos

        public ProductoTerminadoController(AppDbContext context)
        {
            _context = context;
        }

        // obtener todos los productos terminados (cualquier usuario autenticado)
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductoTerminado>>> GetProductoTerminado()
        {
            var lista = await _context.ProductoTerminado.ToListAsync();
            return Ok(lista);
        }

        // obtener un producto terminado por id (cualquier usuario autenticado)
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductoTerminado>> GetProductoTerminado(int id)
        {
            var productoTerminado = await _context.ProductoTerminado.FindAsync(id);

            if (productoTerminado == null)
                return NotFound(new { mensaje = "Producto terminado no encontrado." });

            return Ok(productoTerminado);
        }

        // crear producto terminado (Administrador o Supervisor)
        [HttpPost]
        [Authorize(Roles = "Administrador,Supervisor")]
        public async Task<ActionResult> PostProductoTerminado(ProductoTerminado productoTerminado)
        {
            if (productoTerminado == null)
                return BadRequest(new { mensaje = "El cuerpo de la solicitud está vacío." });

            _context.ProductoTerminado.Add(productoTerminado);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Producto terminado creado exitosamente.",
                producto = productoTerminado
            });
        }

        // actualizar producto terminado (Administrador o Supervisor)
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Supervisor")]
        public async Task<IActionResult> PutProductoTerminado(int id, ProductoTerminado productoTerminado)
        {
            if (productoTerminado == null)
                return BadRequest(new { mensaje = "El cuerpo de la solicitud está vacío." });

            if (id != productoTerminado.ProductoTerminadoId)
                return BadRequest(new { mensaje = "El ID no coincide." });

            _context.Entry(productoTerminado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ProductoTerminado.Any(e => e.ProductoTerminadoId == id))
                    return NotFound(new { mensaje = "Producto terminado no encontrado." });

                throw;
            }

            return Ok(new { mensaje = "Producto terminado actualizado correctamente." });
        }

        // eliminar producto terminado (solo Administrador)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteProductoTerminado(int id)
        {
            var productoTerminado = await _context.ProductoTerminado.FindAsync(id);

            if (productoTerminado == null)
                return NotFound(new { mensaje = "Producto terminado no encontrado." });

            _context.ProductoTerminado.Remove(productoTerminado);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Producto terminado eliminado exitosamente." });
        }
    }
}



