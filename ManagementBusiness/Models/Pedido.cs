using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class Pedido : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public int ClienteId { get; set; }

        public int? PresupuestoId { get; set; }

        public int SucursalId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = string.Empty;

        // Propiedades de navegación
        public virtual Cliente Cliente { get; set; } = null!;
        public virtual Presupuesto? Presupuesto { get; set; }
        public virtual Sucursal Sucursal { get; set; } = null!;
        public virtual ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
        public virtual Venta? Venta { get; set; }
    }
}
