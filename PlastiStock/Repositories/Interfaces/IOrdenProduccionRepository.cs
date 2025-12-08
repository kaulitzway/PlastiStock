using PlastiStock.Models;

namespace PlastiStock.Repositories.Interfaces
{
    public interface IOrdenProduccionRepository
    {
        Task<IEnumerable<OrdenProduccion>> GetAllAsync();
        Task<OrdenProduccion> GetByIdAsync(int id);
        Task<OrdenProduccion> CreateAsync(OrdenProduccion ordenProduccion);
        Task<OrdenProduccion> UpdateAsync(OrdenProduccion ordenProduccion);
        Task<bool> DeleteAsync(int id);
    }
}
