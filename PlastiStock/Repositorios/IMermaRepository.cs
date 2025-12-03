using PlastiStock.Models;

namespace PlastiStock.Repositorios.Interfaces
{
    public interface IMermaRepository
    {
        Task<IEnumerable<Merma>> GetAll();
        Task<Merma> GetById(int id);
        Task<Merma> Create(Merma merma);
        Task<Merma> Update(Merma merma);
        Task<bool> Delete(int id);
    }
}
