using PlastiStock.Models;
using System.ComponentModel.DataAnnotations;

public class ProductoTerminado
{
    [Key]
    public int ProductoTerminadoId { get; set; }

    [Required]
    public required string Nombre { get; set; }

    public string? Descripcion { get; set; }

    [Required]
    public int Cantidad { get; set; }

    [Required]
    public int ProductoEnProcesoId { get; set; }

    public ProductoEnProceso? ProductoEnProceso { get; set; }
}


