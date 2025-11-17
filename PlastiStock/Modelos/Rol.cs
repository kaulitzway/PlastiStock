using PlastiStock.Models;
using System.ComponentModel.DataAnnotations;

public class Rol
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Nombre { get; set; }

    public string? Descripcion { get; set; }

    public ICollection<Usuario> Usuarios { get; set; }
    public ICollection<RolPermiso> RolesPermiso { get; set; }
}


