using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class DetalleCompra : BaseModel
    {
        public int Id { get; set; }

        public int CompraId { get; set; }

        public int ProductoId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PrecioUnitario { get; set; }

        // Propiedades de navegación
        public virtual Compra Compra { get; set; } = null!;
        public virtual Producto Producto { get; set; } = null!;
    }
}
