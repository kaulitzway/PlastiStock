using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlastiStock.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string NumeroDocumento { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [MaxLength(100)]
        public string Contraseña { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        //  Relación con TipoDocumento
        [ForeignKey("TipoDocumento")]
        public int TipoDocumentoId { get; set; }
        public TipoDocumento TipoDocumento { get; set; }

        // Relación con Rol
        [ForeignKey("Rol")]
        public int RolId { get; set; }
        public Rol Rol { get; set; }
    }
}



