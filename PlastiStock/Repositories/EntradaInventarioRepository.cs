using Microsoft.EntityFrameworkCore;
using PlastiStock.Data;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;


namespace PlastiStock.Repositories
{
    public class EntradaInventarioRepository : IEntradaInventarioRepository
    {
        private readonly AppDbContext _context;

        public EntradaInventarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EntradaInventario>> GetAllAsync()
        {
            return await _context.EntradasInventario
                .Include(e => e.MateriaPrima)
                .ToListAsync();
        }

        public async Task<EntradaInventario> GetByIdAsync(int id)
        {
            return await _context.EntradasInventario
                .Include(e => e.MateriaPrima)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<EntradaInventario> CreateAsync(EntradaInventario entradaInventario)
        {
            _context.EntradasInventario.Add(entradaInventario);
            await _context.SaveChangesAsync();
            return entradaInventario;
        }

        public async Task<bool> UpdateAsync(EntradaInventario entradaInventario)
        {
            _context.EntradasInventario.Update(entradaInventario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entradaInventario = await _context.EntradasInventario.FindAsync(id);
            if (entradaInventario == null) return false;

            _context.EntradasInventario.Remove(entradaInventario);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}