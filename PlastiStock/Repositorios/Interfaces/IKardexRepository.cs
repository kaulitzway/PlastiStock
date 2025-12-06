using PlastiStock.Models;

namespace PlastiStock.Repositorios.Interfaces
{
    public interface IKardexRepository
    {
        Task<IEnumerable<Kardex>> GetAllAsync();
        Task<Kardex> GetByIdAsync(int id);
        Task<Kardex> CreateAsync(Kardex kardex);
        Task<Kardex> UpdateAsync(Kardex kardex);
        Task<bool> DeleteAsync (int id);
    }
}
