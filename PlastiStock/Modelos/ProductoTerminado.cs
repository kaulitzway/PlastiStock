using PlastiStock.Models;

public class ProductoTerminado
{
    public int ProductoTerminadoId { get; set; }

    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Cantidad { get; set; }

    // Relación con ProductoEnProceso (uno a muchos)
    public int ProductoEnProcesoId { get; set; }
    public ProductoEnProceso ProductoEnProceso { get; set; }
}


