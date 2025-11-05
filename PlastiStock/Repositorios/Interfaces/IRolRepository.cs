using PlastiStock.Models;

namespace PlastiStock.Interfaces
{
    public interface IRolRepository
    {
        Task<List<Rol>> ObtenerRoles();
        Task<Rol> ObtenerRol(int id);
        Task<bool> CrearRol(Rol rol);
        Task<bool> ActualizarRol(Rol rol, int id);
        Task<bool> EliminarRol(int id);
    }
}
