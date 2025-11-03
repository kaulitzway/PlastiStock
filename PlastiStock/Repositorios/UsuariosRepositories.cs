using Microsoft.EntityFrameworkCore;
using PlastiStock.Contest;
using PlastiStock.Models;

namespace PlastiStock.Repositorios
{
    public class UsuariosRepositories : IUsuariosRepositories
    {
        // Implementación de los métodos definidos en la interfaz IUsuariosRepositories
        private readonly PlasticStockContext _context;

        public UsuariosRepositories(PlasticStockContext context)
        {
            _context = context;
        }

        public async Task<Usuarios> ObtenerUsuario(int id)
        {
               return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ActualizarUsuario(Usuarios usuario, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CrearUsuario(Usuarios usuario)
        {
            throw new NotImplementedException();
        }
    }
}
