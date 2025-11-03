using Microsoft.EntityFrameworkCore;  // Necesario para ToListAsync() y FirstOrDefaultAsync()
using PlastiStock.Contest;             // Donde está el PlasticStockContext
using PlastiStock.Models;              // Donde está la clase TipoDocumento
using PlastiStock.Repositorios.Interfaces; // Donde está tu interfaz ITipoDocumentoRepository

namespace PlastiStock.Repositorios
{
    public class TipoDocumentoRepository : ITipoDocumentoRepository
    {
        private readonly PlasticStockContext _context;

        public TipoDocumentoRepository(PlasticStockContext context)
        {
            _context = context;
        }

        public async Task<List<TipoDocumento>> ObtenerTiposDocumento()
        {
            return await _context.TiposDeDocumento.ToListAsync();
        }

        public async Task<TipoDocumento> ObtenerTipoDocumento(int id)
        {
            return await _context.TiposDeDocumento.FirstOrDefaultAsync(td => td.Id == id);
        }
    }
}


