using PlastiStock.Models;

namespace PlastiStock.Repositories.Interfaces
{

    public interface IEntradaInventarioRepository
    {
        Task<IEnumerable<EntradaInventario>> GetAllAsync();
        Task<EntradaInventario> GetByIdAsync(int id);
        Task<EntradaInventario> CreateAsync(EntradaInventario entradaInventario);
        Task<bool> UpdateAsync(EntradaInventario entradaInventario);
        Task<bool> DeleteAsync(int id);
    }
}
