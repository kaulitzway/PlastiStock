using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Producto
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? CodigoInterno { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecioUnitario { get; set; }

    public int Stock { get; set; }

    public int? CategoriaId { get; set; }
}
