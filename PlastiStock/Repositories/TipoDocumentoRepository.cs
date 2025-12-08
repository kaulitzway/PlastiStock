using Microsoft.EntityFrameworkCore;
using PlastiStock.Data;                       // Aquí está AppDbContext
using PlastiStock.Models;                     // Tus modelos
using PlastiStock.Repositories.Interfaces; // La interfaz

namespace PlastiStock.Repositories
{
    public class TipoDocumentoRepository : ITipoDocumentoRepository
    {
        private readonly AppDbContext _context;

        public TipoDocumentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TipoDocumento>> ObtenerTiposDocumento()
        {
            return await _context.TiposDocumento.ToListAsync();
        }

        public async Task<TipoDocumento?> ObtenerTipoDocumento(int id)
        {
            return await _context.TiposDocumento
                .FirstOrDefaultAsync(td => td.Id == id);
        }
    }
}



