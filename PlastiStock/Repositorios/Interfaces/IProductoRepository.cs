using PlastiStock.Models;

namespace PlastiStock.Repositories
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAll();
        Task<Producto?> GetById(int id);
        Task<Producto> Create(Producto producto);
        Task<bool> Update(Producto producto);
        Task<bool> Delete(int id);
    }
}

