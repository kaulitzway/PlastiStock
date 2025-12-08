using PlastiStock.Models;

namespace PlastiStock.Repositories.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto?> GetByIdAsync(int id);
        Task<Producto> CreateAsync(Producto producto);
        Task<bool> UpdateAsync(Producto producto);
        Task<bool> DeleteAsync(int id);
    }
}

