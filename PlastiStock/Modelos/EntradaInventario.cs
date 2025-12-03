namespace PlastiStock.Models
{
    public class EntradaInventario
    {
        public int Id { get; set; }
        public int MateriaPrimaId { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}

