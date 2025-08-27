using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class Compra : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [MaxLength(50)]
        public string NumeroFactura { get; set; } = string.Empty;

        public int ProveedorId { get; set; }

        public int SucursalId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Total { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = string.Empty;

        // Propiedades de navegación
        public virtual Proveedor Proveedor { get; set; } = null!;
        public virtual Sucursal Sucursal { get; set; } = null!;
        public virtual ICollection<DetalleCompra> Detalles { get; set; } = new List<DetalleCompra>();
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
