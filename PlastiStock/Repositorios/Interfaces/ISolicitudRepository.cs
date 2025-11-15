using PlastiStock.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlastiStock.Repositorios.Interfaces
{
    public interface ISolicitudRepository
    {
        Task<IEnumerable<Solicitud>> GetAllAsync();
        Task<Solicitud> GetByIdAsync(int id);
        Task<Solicitud> CreateAsync(Solicitud solicitud);
        Task UpdateEstadoAsync(int id, string nuevoEstado, string observaciones);
    }
}

