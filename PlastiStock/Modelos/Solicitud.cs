using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlastiStock.Models
{
    public class Solicitud
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioSolicitanteId { get; set; }
        public Usuario? UsuarioSolicitante { get; set; }

        [Required]
        public int UsuarioAfectadoId { get; set; }
        public Usuario? UsuarioAfectado { get; set; }

        [Required]
        public int RolSolicitadoId { get; set; }
        public Rol? RolSolicitado { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Estado { get; set; } = "Pendiente";

        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;
        public DateTime? FechaRespuesta { get; set; }

        [MaxLength(300)]
        public string? Observaciones { get; set; }
    }
}

