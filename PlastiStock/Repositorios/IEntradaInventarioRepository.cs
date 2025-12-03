using PlastiStock.Models;

namespace PlastiStock.Repositorios.Interfaces
{
    public interface IEntradaInventarioRepository
    {
        Task<IEnumerable<EntradaInventario>> GetAll();
        Task<EntradaInventario> GetById(int id);
        Task<EntradaInventario> Create(EntradaInventario entradaInventario);
        Task<EntradaInventario> Update(EntradaInventario entradaInventario);
        Task<bool> Delete(int id);
    }
}
