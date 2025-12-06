using PlastiStock.Models;

namespace PlastiStock.Repositorios.Interfaces
{
    public interface ISalidaInventarioRepository
    {
        Task <IEnumerable<SalidaInventario>> GetAllAsync();
        Task<SalidaInventario> GetByIdAsync(int id);
        Task<SalidaInventario> CreateAsync(SalidaInventario salidaInventario);
        Task<SalidaInventario> UpdateAsync(SalidaInventario salidaInventario);
        Task<bool> DeleteAsync(int id);


    }
}
