using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class Servicio : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Precio { get; set; }

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        public int ImpuestoId { get; set; }

        // Propiedades de navegación
        public virtual Impuesto Impuesto { get; set; } = null!;
        public virtual ICollection<DetalleFactura> DetallesFactura { get; set; } = new List<DetalleFactura>();
    }
}
