using PlastiStock.Models;

namespace PlastiStock.Repositories.Interfaces
{
    public interface IProductoEnProcesoRepository
    {
        Task<IEnumerable<ProductoEnProceso>> GetAllAsync();
        Task<ProductoEnProceso> GetByIdAsync(int id);
        Task<ProductoEnProceso> CreateAsync(ProductoEnProceso entity);
        Task<bool> UpdateAsync(ProductoEnProceso entity);
        Task<bool> DeleteAsync(int id);
    }
}

