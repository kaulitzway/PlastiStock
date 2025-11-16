using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlastiStock.Data;
using PlastiStock.Models;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoTerminadoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductoTerminadoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductoTerminado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoTerminado>>> GetProductoTerminado()
        {
            return await _context.ProductoTerminado.ToListAsync();
        }

        // GET: api/ProductoTerminado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoTerminado>> GetProductoTerminado(int id)
        {
            var productoTerminado = await _context.ProductoTerminado.FindAsync(id);

            if (productoTerminado == null)
            {
                return NotFound(new { mensaje = "Producto terminado no encontrado." });
            }

            return productoTerminado;
        }

        // POST: api/ProductoTerminado
        [HttpPost]
        public async Task<ActionResult> PostProductoTerminado(ProductoTerminado productoTerminado)
        {
            _context.ProductoTerminado.Add(productoTerminado);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Producto terminado creado exitosamente.",
                producto = productoTerminado
            });
        }

        // PUT: api/ProductoTerminado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductoTerminado(int id, ProductoTerminado productoTerminado)
        {
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

        // DELETE: api/ProductoTerminado/5
        [HttpDelete("{id}")]
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


