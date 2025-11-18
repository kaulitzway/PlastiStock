using System.ComponentModel.DataAnnotations;

namespace PlastiStock.Models
{
    public class InicioSesionRequest
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }
    }
}
