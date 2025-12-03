namespace PlastiStock.Models
{
    public class Merma
    {
        public int Id { get; set; }
        public int ProductoTerminadoId { get; set; }
        public decimal Cantidad { get; set; }
        public string Motivo { get; set; }
        public DateTime Fecha { get; set; }
    }
}

