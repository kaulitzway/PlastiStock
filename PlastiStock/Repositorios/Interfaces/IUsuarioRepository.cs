using PlastiStock.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlastiStock.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task<bool> AddAsync(Usuario usuario);
        Task<bool> UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(int id);
        Task DeleteAsync(Usuario usuario);
    }
}
