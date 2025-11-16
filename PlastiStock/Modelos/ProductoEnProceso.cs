using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlastiStock.Models
{
    public class ProductoEnProceso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(100)]
        public string EstadoProceso { get; set; }  // Ej: Mezclado / Secado / Moldeado

        //  Relación con MateriaPrima
        [Required]
        public int MateriaPrimaId { get; set; }

        public MateriaPrima MateriaPrima { get; set; }
    }
}

