using Microsoft.EntityFrameworkCore;
using PlastiStock.Data;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;

namespace PlastiStock.Repositories;

public class MermaRepository : IMermaRepository
{
    private readonly AppDbContext _context;
    public MermaRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Merma>> GetAllAsync()
        => await _context.Mermas.ToListAsync();
    public async Task<Merma> GetByIdAsync(int id)
        => await _context.Mermas.FindAsync(id);
    public async Task<Merma> CreateAsync(Merma merma)
    {
        _context.Mermas.Add(merma);
        await _context.SaveChangesAsync();
        return merma;
    }
    public async Task<Merma> UpdateAsync(Merma merma)
    {
        _context.Mermas.Update(merma);
        await _context.SaveChangesAsync();
        return merma;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var merma = await _context.Mermas.FindAsync(id);
        if (merma == null)
            return false;
        _context.Mermas.Remove(merma);
        await _context.SaveChangesAsync();
        return true;
    }
}
