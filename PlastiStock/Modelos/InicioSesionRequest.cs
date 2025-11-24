using System.ComponentModel.DataAnnotations;

namespace PlastiStock.Models
{
    public class InicioSesionRequest
    {
        [Required]
        public string Correo { get; set; } = string.Empty;


        [Required]
        public string Contrasena { get; set; } = string.Empty;  
    }
}
