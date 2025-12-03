namespace PlastiStock.Models
{
    public class OrdenProduccion
    {
        public int Id { get; set; }
        public int ProductoTerminadoId { get; set; }
        public int CantidadProgramada { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }
    }
}

