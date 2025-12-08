// ITipoDocumentoRepository.cs
using PlastiStock.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlastiStock.Repositories.Interfaces
{
    public interface ITipoDocumentoRepository
    {
        Task<List<TipoDocumento>> ObtenerTiposDocumento();
        Task<TipoDocumento> ObtenerTipoDocumento(int id); // 👈 CAMBIADO
    }
}
