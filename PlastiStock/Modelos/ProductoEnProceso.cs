using System;
using System.ComponentModel.DataAnnotations;

namespace PlastiStock.Models
{
    public class ProductoEnProceso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public required string Nombre { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(100)]
        public required string EstadoProceso { get; set; }

        [Required]
        public int MateriaPrimaId { get; set; }

        public MateriaPrima? MateriaPrima { get; set; }
    }
}


