using PlastiStock.Models;

namespace PlastiStock.Repositorios.Interfaces
{
    public interface IKardexRepository
    {
        Task<IEnumerable<Kardex>> GetAll();
        Task<Kardex> GetById(int id);
        Task<Kardex> Create(Kardex kardex);
        Task<Kardex> Update(Kardex kardex);
        Task<bool> Delete(int id);
    }
}
