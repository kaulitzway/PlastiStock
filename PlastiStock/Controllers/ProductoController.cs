using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PlastiStock.Data;
using PlastiStock.Models;
using Microsoft.EntityFrameworkCore;

namespace PlastiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // ============================
        //           GET ALL
        // ============================
        [HttpGet]
        [Authorize(Roles = "AdminPrincipal,AdminSecundario")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetAll()
        {
            return await _context.Productos.ToListAsync();
        }

        // ============================
        //        GET BY ID
        // ============================
        [HttpGet("{id}")]
        [Authorize(Roles = "AdminPrincipal,AdminSecundario")]
        public async Task<ActionResult<Producto>> GetById(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound("El producto no existe.");

            return producto;
        }

        // ============================
        //           CREATE
        // ============================
        [HttpPost]
        [Authorize(Roles = "AdminPrincipal,AdminSecundario")]
        public async Task<ActionResult<Producto>> Create(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }

        // ============================
        //           UPDATE
        // ============================
        [HttpPut("{id}")]
        [Authorize(Roles = "AdminPrincipal,AdminSecundario")]
        public async Task<IActionResult> Update(int id, Producto producto)
        {
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

        // ============================
        //           DELETE
        // ============================
        [HttpDelete("{id}")]
        [Authorize(Roles = "AdminPrincipal")]   // SOLO EL PRINCIPAL
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


