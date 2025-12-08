using Microsoft.EntityFrameworkCore;
using PlastiStock.Data;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;

namespace PlastiStock.Repositories
{
    public class SalidaInventarioRepository : ISalidaInventarioRepository
    {
        private readonly AppDbContext _context;
        public SalidaInventarioRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SalidaInventario>> GetAllAsync()
        {
            return await _context.SalidasInventarios
                .Include(s => s.MateriaPrima)
                .ToListAsync();
        }
        public async Task<SalidaInventario> GetByIdAsync(int id)
        {
            return await _context.SalidasInventarios
                .Include(s => s.MateriaPrima)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<SalidaInventario> CreateAsync(SalidaInventario salidaInventario)
        {
            _context.SalidasInventarios.Add(salidaInventario);
            await _context.SaveChangesAsync();
            return salidaInventario;
        }
        public async Task<SalidaInventario> UpdateAsync(SalidaInventario salidaInventario)
        {
            _context.SalidasInventarios.Update(salidaInventario);
            await _context.SaveChangesAsync();
            return salidaInventario;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var salidaInventario = await _context.SalidasInventarios.FindAsync(id);
            if (salidaInventario == null) return false;
            _context.SalidasInventarios.Remove(salidaInventario);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
