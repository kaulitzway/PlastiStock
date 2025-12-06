using PlastiStock.Models;

namespace PlastiStock.Repositorios.Interfaces
{
    public interface IMermaRepository
    {
        Task<IEnumerable<Merma>> GetAllAsync();
        Task<Merma> GetByIdAsync(int id);
        Task<Merma> CreateAsync(Merma merma);
        Task<Merma> UpdateAsync(Merma merma);
        Task<bool> DeleteAsync (int id);
    }
}
