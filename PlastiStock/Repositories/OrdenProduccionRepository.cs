using Microsoft.EntityFrameworkCore;
using PlastiStock.Data;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;

namespace PlastiStock.Repositories
{

    public class OrdenProduccionRepository : IOrdenProduccionRepository
    {
        private readonly AppDbContext _context;
        public OrdenProduccionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrdenProduccion>> GetAllAsync()
            => await _context.OrdenesProduccion.ToListAsync();
        public async Task<OrdenProduccion> GetByIdAsync(int id)
            => await _context.OrdenesProduccion.FindAsync(id);
        public async Task<OrdenProduccion> CreateAsync(OrdenProduccion ordenProduccion)
        {
            _context.OrdenesProduccion.Add(ordenProduccion);
            await _context.SaveChangesAsync();
            return ordenProduccion;
        }
        public async Task<OrdenProduccion> UpdateAsync(OrdenProduccion ordenProduccion)
        {
            _context.OrdenesProduccion.Update(ordenProduccion);
            await _context.SaveChangesAsync();
            return ordenProduccion;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var ordenProduccion = await _context.OrdenesProduccion.FindAsync(id);
            if (ordenProduccion == null)
                return false;
            _context.OrdenesProduccion.Remove(ordenProduccion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}