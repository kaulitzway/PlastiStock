using Microsoft.EntityFrameworkCore;
using PlastiStock.Data;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;

namespace PlastiStock.Repositories
{
    public class KardexRepository : IKardexRepository
    {
        private readonly AppDbContext _context;

        public KardexRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Kardex>> GetAllAsync()
        {
            return await _context.Kardex.ToListAsync();
        }

        public async Task<Kardex> GetByIdAsync(int id)
        {
            return await _context.Kardex.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Kardex> CreateAsync(Kardex kardex)
        {
            _context.Kardex.Add(kardex);
            await _context.SaveChangesAsync();
            return kardex;
        }

        public async Task<Kardex> UpdateAsync(Kardex kardex)
        {
            _context.Kardex.Update(kardex);
            await _context.SaveChangesAsync();
            return kardex;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var kardex = await _context.Kardex.FindAsync(id);
            if (kardex == null)
                return false;

            _context.Kardex.Remove(kardex);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


