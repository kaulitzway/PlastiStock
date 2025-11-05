using PlastiStock.Models;

namespace PlastiStock.Repositorios
{
    public interface IUsuariosRepositories
    {
        Task<List<Usuarios>> ObtenerUsuarios();

        Task<Usuarios> ObtenerUsuario(int id);

        Task<bool> CrearUsuario(Usuarios usuario);

        Task<bool> ActualizarUsuario(Usuarios usuario, int id);

        Task<bool> EliminarUsuario(int id);
    }
}
