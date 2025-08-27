using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class DetalleFactura : BaseModel
    {
        public int Id { get; set; }

        public int FacturaId { get; set; }

        public int? ProductoId { get; set; }

        public int? ServicioId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PrecioUnitario { get; set; }

        // Propiedades de navegación
        public virtual Factura Factura { get; set; } = null!;
        public virtual Producto? Producto { get; set; }
        public virtual Servicio? Servicio { get; set; }
    }
}
