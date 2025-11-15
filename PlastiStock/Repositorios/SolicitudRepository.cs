using Microsoft.EntityFrameworkCore;
using PlastiStock.Data;
using PlastiStock.Models;
using PlastiStock.Repositorios.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlastiStock.Repositories
{
    public class SolicitudRepository : ISolicitudRepository
    {
        private readonly AppDbContext _context;

        public SolicitudRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Solicitud>> GetAllAsync()
        {
            return await _context.Solicitudes
                .Include(s => s.UsuarioSolicitante)
                .Include(s => s.UsuarioAfectado)
                .Include(s => s.RolSolicitado)
                .ToListAsync();
        }

        public async Task<Solicitud> GetByIdAsync(int id)
        {
            return await _context.Solicitudes
                .Include(s => s.UsuarioSolicitante)
                .Include(s => s.UsuarioAfectado)
                .Include(s => s.RolSolicitado)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Solicitud> CreateAsync(Solicitud solicitud)
        {
            _context.Solicitudes.Add(solicitud);
            await _context.SaveChangesAsync();
            return solicitud;
        }

        public async Task UpdateEstadoAsync(int id, string nuevoEstado, string observaciones)
        {
            var solicitud = await _context.Solicitudes.FindAsync(id);
            if (solicitud == null) return;

            solicitud.Estado = nuevoEstado;
            solicitud.Observaciones = observaciones;
            solicitud.FechaRespuesta = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}

