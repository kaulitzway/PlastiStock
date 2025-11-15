using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlastiStock.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(1000)]
        public string Descripcion { get; set; }

        [MaxLength(100)]
        public string CodigoInterno { get; set; }

        public decimal PrecioUnitario { get; set; }

        public int Stock { get; set; }

        public int? CategoriaId { get; set; }
        
    }
}

