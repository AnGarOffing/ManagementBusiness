using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class Producto : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string SKU { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PrecioCosto { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PrecioVenta { get; set; }

        public int StockMinimo { get; set; } = 5;

        public int CategoriaId { get; set; }

        public int ImpuestoId { get; set; }

        // Propiedades de navegación
        public virtual CategoriaProducto Categoria { get; set; } = null!;
        public virtual Impuesto Impuesto { get; set; } = null!;
        public virtual ICollection<DetalleFactura> DetallesFactura { get; set; } = new List<DetalleFactura>();
        public virtual ICollection<DetalleCompra> DetallesCompra { get; set; } = new List<DetalleCompra>();
        public virtual ICollection<DetallePedido> DetallesPedido { get; set; } = new List<DetallePedido>();
        public virtual ICollection<MovimientoInventario> MovimientosInventario { get; set; } = new List<MovimientoInventario>();
    }
}
