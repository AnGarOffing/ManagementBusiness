using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class MetodoPago : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        // Propiedades de navegación
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
