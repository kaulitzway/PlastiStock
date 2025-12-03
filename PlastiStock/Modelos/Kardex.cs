namespace PlastiStock.Models
{
    public class Kardex
    {
        public int Id { get; set; }
        public int MateriaPrimaId { get; set; }
        public decimal Cantidad { get; set; }
        public string TipoMovimiento { get; set; } // Entrada / Salida
        public DateTime Fecha { get; set; }
    }
}

