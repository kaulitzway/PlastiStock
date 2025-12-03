using PlastiStock.Models;

namespace PlastiStock.Repositorios.Interfaces
{
    public interface IProveedorRepository
    {
        Task<IEnumerable<Proveedor>> GetAll();
        Task<Proveedor> GetById(int id);
        Task<Proveedor> Create(Proveedor proveedor);
        Task<Proveedor> Update(Proveedor proveedor);
        Task<bool> Delete(int id);
    }
}

