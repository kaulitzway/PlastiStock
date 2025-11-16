using Microsoft.EntityFrameworkCore;
using PlastiStock.Data;
using PlastiStock.Models;
using PlastiStock.Repositorios.Interfaces;

namespace PlastiStock.Repositories
{
    public class MateriaPrimaRepository : IMateriaPrimaRepository
    {
        private readonly AppDbContext _context;

        public MateriaPrimaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MateriaPrima>> GetAllAsync()
        {
            return await _context.MateriasPrimas.ToListAsync();
        }

        public async Task<MateriaPrima?> GetByIdAsync(int id)
        {
            return await _context.MateriasPrimas.FindAsync(id);
        }

        public async Task<MateriaPrima> CreateAsync(MateriaPrima materiaPrima)
        {
            _context.MateriasPrimas.Add(materiaPrima);
            await _context.SaveChangesAsync();
            return materiaPrima;
        }

        public async Task<bool> UpdateAsync(MateriaPrima materiaPrima)
        {
            _context.Entry(materiaPrima).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var materia = await _context.MateriasPrimas.FindAsync(id);
            if (materia == null) return false;

            _context.MateriasPrimas.Remove(materia);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

