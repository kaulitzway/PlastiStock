using PlastiStock.Models;

namespace PlastiStock.Repositories.Interfaces

{
    public interface ICategoriaRepository
    {
        Task<IEnumerable <Categoria>> GetAllAsync();
        Task<Categoria> GetByIdAsync(int id);
        Task<Categoria> CreateAsync(Categoria categoria);
        Task<bool> UpdateAsync (Categoria categoria);
        Task<bool> DeleteAsync (int id);

    }
}
