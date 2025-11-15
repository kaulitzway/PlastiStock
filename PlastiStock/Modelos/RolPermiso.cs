using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlastiStock.Models
{
    public class RolPermiso
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Rol")]
        public int RolId { get; set; }
        public Rol Rol { get; set; }

        [ForeignKey("Rol")]
        public int PermisoId { get; set; }
        public Permiso Permiso { get; set; }
    }
}

