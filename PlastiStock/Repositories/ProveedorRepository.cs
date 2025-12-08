using Microsoft.EntityFrameworkCore;
using PlastiStock.Data;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;

namespace PlastiStock.Repositories

{

    public class ProveedorRepository : IProveedorRepository
    {
        private readonly AppDbContext _context;

        public ProveedorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proveedor>> GetAllAsync()
            => await _context.Proveedores.ToListAsync();

        public async Task<Proveedor> GetByIdAsync(int id)
            => await _context.Proveedores.FindAsync(id);

        public async Task<Proveedor> CreateAsync(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        public async Task<Proveedor> UpdateAsync(Proveedor proveedor)
        {
            _context.Proveedores.Update(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
                return false;

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}