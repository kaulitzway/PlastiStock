using PlastiStock.Models;

namespace PlastiStock.Repositorios.Interfaces
{
    public interface IEntradaInventarioRepository
    {
        Task<IEnumerable<EntradaInventario>> GetAllAsync();
        Task<EntradaInventario> GetByIdAsync(int id);
        Task<EntradaInventario> CreateAsync(EntradaInventario entradaInventario);
        Task<EntradaInventario> UpdateAsync(EntradaInventario entradaInventario);
        Task<bool> DeleteAsync(int id);
    }
}
