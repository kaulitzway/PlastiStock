using PlastiStock.Models;

namespace PlastiStock.Repositorios.Interfaces
{
    public interface IOrdenProduccionRepository
    {
        Task<IEnumerable<OrdenProduccion>> GetAll();
        Task<OrdenProduccion> GetById(int id);
        Task<OrdenProduccion> Create(OrdenProduccion ordenProduccion);
        Task<OrdenProduccion> Update(OrdenProduccion ordenProduccion);
        Task<bool> Delete(int id);
    }
}
