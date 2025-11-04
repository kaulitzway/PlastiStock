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

        public async Task<List<Usuarios>> ObtenerUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            try
            {
                var UsuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);    
                if (UsuarioExistente == null)
                {
                    return false; // Usuario no encontrado
                    throw new Exception("Usuario para eliminar no encontrado");
                }

                _context.Usuarios.Remove(UsuarioExistente);
                await _context.SaveChangesAsync();
                return true;


            }

            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<bool> ActualizarUsuario(Usuarios usuario, int id)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
                if (usuarioExistente == null)
                {
                    return false; // Usuario no encontrado
                    throw new Exception("Usuario para actualizar no encontrado");
                    
                }

                usuarioExistente.Nombre = usuario.Nombre;
                usuarioExistente.Apellido = usuario.Apellido;
                usuarioExistente.TipoDocumentoId = usuario.TipoDocumentoId;
                usuarioExistente.NumeroDocumento = usuario.NumeroDocumento;
                usuarioExistente.Correo = usuario.Correo;
                usuarioExistente.Contraseña = usuario.Contraseña;


                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                return false;
                throw new Exception(ex.Message.ToString());  

            }
        }

        public async Task<bool> CrearUsuario(Usuarios usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
