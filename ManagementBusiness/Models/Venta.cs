using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class Venta : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public int PedidoId { get; set; }

        public int MetodoPagoId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Total { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = string.Empty;

        public int? FacturaId { get; set; }

        // Propiedades de navegación
        public virtual Pedido Pedido { get; set; } = null!;
        public virtual MetodoPago MetodoPago { get; set; } = null!;
        public virtual Factura? Factura { get; set; }
        public virtual ICollection<MovimientoInventario> MovimientosInventario { get; set; } = new List<MovimientoInventario>();
    }
}
