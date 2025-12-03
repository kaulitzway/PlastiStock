using PlastiStock.Models;

namespace PlastiStock.Repositorios.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable <Categoria>> GetAll();
        Task<Categoria> GetById(int id);
        Task<Categoria> Create(Categoria categoria);
        Task<Categoria> Update(Categoria categoria);
        Task<bool> Delete(int id);

    }
}
