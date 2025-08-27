using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class Factura : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [MaxLength(50)]
        public string NumeroFactura { get; set; } = string.Empty;

        public int ClienteId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Total { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = string.Empty;

        // Propiedades de navegación
        public virtual Cliente Cliente { get; set; } = null!;
        public virtual ICollection<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
        public virtual Venta? Venta { get; set; }
    }
}
