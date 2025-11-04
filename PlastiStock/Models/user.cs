    using System.ComponentModel.DataAnnotations;
    using Microsoft.Identity.Client;

    namespace PlastiStock.Models
    {
        public class Usuarios
        {
            [Key]
            public int Id { get; set; }
        
            [Key]
            public int TipoDocumentoId { get; set; }  // Clave foránea

            public string Nombre { get; set; }
            public string Apellido { get; set; }

            public string NumeroDocumento { get; set; }
        public string Correo { get; set; }
            public string Contraseña { get; set; }
            public DateTime FechaRegistro { get; set; }

            // Relación con TipoDocumento
            public TipoDocumento TipoDocumento { get; set; }
        }
    }

    

