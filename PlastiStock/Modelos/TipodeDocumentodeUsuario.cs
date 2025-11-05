using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlastiStock.Models
{
    public class TipoDocumento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }  // Ejemplo: "Cédula de ciudadanía"

        [Required]
        [MaxLength(10)]
        public string Abreviatura { get; set; }  // Ejemplo: "CC"

        // 🔹 Relación: un tipo de documento tiene muchos usuarios
        public ICollection<Usuario> Usuarios { get; set; }
    }
}



