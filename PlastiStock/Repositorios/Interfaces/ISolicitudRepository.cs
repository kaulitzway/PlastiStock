using PlastiStock.Models;


namespace PlastiStock.Repositorios.Interfaces
{
    public interface ISolicitudRepository
    {
        Task<IEnumerable<Solicitud>> GetAllAsync();
        Task<Solicitud> GetByIdAsync(int id);
        Task<Solicitud> CreateAsync(Solicitud solicitud);
        Task UpdateAsync(int id, string nuevoEstado, string observaciones);

    }
}

