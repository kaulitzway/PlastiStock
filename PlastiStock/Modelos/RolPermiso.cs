using System.ComponentModel.DataAnnotations;

namespace PlastiStock.Models
{
    public class RolPermiso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RolId { get; set; }
        public Rol? Rol { get; set; }

        [Required]
        public int PermisoId { get; set; }
        public Permiso? Permiso { get; set; }
    }
}



