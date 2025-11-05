using Microsoft.EntityFrameworkCore;
using PlastiStock.Data;
using PlastiStock.Interfaces;
using PlastiStock.Models;

namespace PlastiStock.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly AppDbContext _context;

        public RolRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rol>> ObtenerRoles() => await _context.Roles.ToListAsync();

        public async Task<Rol> ObtenerRol(int id) => await _context.Roles.FindAsync(id);

        public async Task<bool> CrearRol(Rol rol)
        {
            _context.Roles.Add(rol);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ActualizarRol(Rol rol, int id)
        {
            var existente = await _context.Roles.FindAsync(id);
            if (existente == null) return false;

            existente.Nombre = rol.Nombre;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarRol(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null) return false;

            _context.Roles.Remove(rol);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

