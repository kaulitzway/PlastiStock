using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlastiStock.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }  // Ejemplo: "Administrador", "Empleado", etc.

        [MaxLength(200)]
        public string Descripcion { get; set; } // Descripción opcional del rol

        // 🔹 Relación: un rol tiene muchos usuarios
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
