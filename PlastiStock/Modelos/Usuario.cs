using PlastiStock.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Nombre { get; set; }

    [Required]
    public required string Apellido { get; set; }

    [Required]
    public required string NumeroDocumento { get; set; }

    [Required]
    [EmailAddress]
    public required string Correo { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Contraseña { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    [ForeignKey("TipoDocumento")]
    public int TipoDocumentoId { get; set; }
    public TipoDocumento TipoDocumento { get; set; }

    [ForeignKey("Rol")]
    public int RolId { get; set; }
    public Rol Rol { get; set; }
}



