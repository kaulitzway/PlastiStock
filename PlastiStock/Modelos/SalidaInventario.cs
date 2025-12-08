using PlastiStock.Models;

public class SalidaInventario
{
    public int Id { get; set; }
    public int MateriaPrimaId { get; set; }
    public decimal Cantidad { get; set; }
    public DateTime Fecha { get; set; }

    public MateriaPrima MateriaPrima { get; set; }
}


