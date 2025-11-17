using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlastiStock.Models
{
    public class MateriaPrima
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public required string Nombre { get; set; }

        [Required]
        public int CantidadDisponible { get; set; }

        [Required]
        [MaxLength(50)]
        public required string UnidadMedida { get; set; }

        public ICollection<ProductoEnProceso>? ProductosEnProceso { get; set; }
    }
}



