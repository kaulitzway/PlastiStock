using System.Collections.Generic;

namespace PlastiStock.Models
{
    public class TipoDocumento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }               // Ejemplo: "Cédula de ciudadanía"
        public string Abreviatura { get; set; }          // Ejemplo: "CC"

        // Relación: un tipo de documento tiene muchos usuarios
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}

    

