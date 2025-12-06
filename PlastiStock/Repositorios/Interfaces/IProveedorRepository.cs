using PlastiStock.Models;

namespace PlastiStock.Repositorios.Interfaces
{
    public interface IProveedorRepository
    {
        Task<IEnumerable<Proveedor>> GetAllAsync();
        Task<Proveedor?> GetByIdAsync(int id);
        Task<Proveedor> CreateAsync(Proveedor proveedor);
        Task<Proveedor> UpdateAsync(Proveedor proveedor);
        Task<bool> DeleteAsync(int id);
    }
}


