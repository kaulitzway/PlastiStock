using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlastiStock.Models
{
    public class Solicitud
    {
        [Key]
        public int Id { get; set; }

        // Usuario que hace la solicitud (ej: Administrador Secundario)
        [Required]
        public int UsuarioSolicitanteId { get; set; }

        [ForeignKey("UsuarioSolicitanteId")]
        public Usuario UsuarioSolicitante { get; set; }

        // Usuario al que se le quiere cambiar el rol
        [Required]
        public int UsuarioAfectadoId { get; set; }

        [ForeignKey("UsuarioAfectadoId")]
        public Usuario UsuarioAfectado { get; set; }

        // Rol solicitado (ej: Supervisor)
        [Required]
        public int RolSolicitadoId { get; set; }

        [ForeignKey("RolSolicitadoId")]
        public Rol RolSolicitado { get; set; }

        // Estado de la solicitud
        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "Pendiente";
        // Pendiente | Aprobada | Rechazada

        // Fechas
        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;
        public DateTime? FechaRespuesta { get; set; }

        // Observaciones del administrador principal al aprobar/rechazar
        [MaxLength(300)]
        public string Observaciones { get; set; }
    }
}

