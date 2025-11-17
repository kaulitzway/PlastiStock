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
        public required string Nombre { get; set; }

        [MaxLength(200)]
        public string? Descripcion { get; set; }

        public ICollection<RolPermiso>? RolesPermiso { get; set; }
    }
}


