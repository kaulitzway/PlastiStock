using PlastiStock.Models;

namespace PlastiStock.Repositories.Interfaces
{
    public interface IMateriaPrimaRepository
    {
        Task<IEnumerable<MateriaPrima>> GetAllAsync();
        Task<MateriaPrima?> GetByIdAsync(int id);
        Task<MateriaPrima> CreateAsync(MateriaPrima materiaPrima);
        Task<bool> UpdateAsync(MateriaPrima materiaPrima);
        Task<bool> DeleteAsync(int id);
    }
}

