using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class Presupuesto : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public int ClienteId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Total { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = string.Empty;

        // Propiedades de navegación
        public virtual Cliente Cliente { get; set; } = null!;
        public virtual Pedido? Pedido { get; set; }
    }
}
