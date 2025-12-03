using PlastiStock.Models;

namespace PlastiStock.Repositorios.Interfaces
{
    public interface ISalidaInventarioRepository
    {
        Task <IEnumerable<SalidaInventario>> GetAll();
        Task<SalidaInventario> GetById(int id);
        Task<SalidaInventario> Create(SalidaInventario salidaInventario);
        Task<SalidaInventario> Update(SalidaInventario salidaInventario);
        Task<bool> Delete(int id);


    }
}
