using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlastiStock.Models
{
    public class Permiso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }   // Ejemplo: ELIMINAR_PRODUCTO

        [MaxLength(200)]
        public string Descripcion { get; set; }

        // 🔹 Relación muchos a muchos
        public ICollection<RolPermiso> RolesPermiso { get; set; }
    }
}


