using PlastiStock.Models;

namespace PlastiStock.Interfaces
{
    public interface IRolRepository
    {
        Task<List<Rol>> GetAllAsync();
        Task<Rol> GetByIdAsync(int id);
        Task<bool> CreateAsync(Rol rol);
        Task<bool> UpdateAsync(Rol rol, int id);
        Task<bool> DeleteAsync(int id);
    }
}

