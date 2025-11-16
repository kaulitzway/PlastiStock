using Microsoft.EntityFrameworkCore;
using PlastiStock.Data;
using PlastiStock.Models;
using PlastiStock.Repositorios.Interfaces;

namespace PlastiStock.Repositories
{
    public class ProductoEnProcesoRepository : IProductoEnProcesoRepository
    {
        private readonly AppDbContext _context;

        public ProductoEnProcesoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoEnProceso>> GetAllAsync()
        {
            return await _context.ProductosEnProceso
                .Include(p => p.MateriaPrima)
                .ToListAsync();
        }

        public async Task<ProductoEnProceso> GetByIdAsync(int id)
        {
            return await _context.ProductosEnProceso
                .Include(p => p.MateriaPrima)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProductoEnProceso> CreateAsync(ProductoEnProceso entity)
        {
            _context.ProductosEnProceso.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(ProductoEnProceso entity)
        {
            _context.ProductosEnProceso.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ProductosEnProceso.FindAsync(id);
            if (entity == null) return false;

            _context.ProductosEnProceso.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

