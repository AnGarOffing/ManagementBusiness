using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class Pago : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Monto { get; set; }

        public int MetodoPagoId { get; set; }

        public int? FacturaId { get; set; }

        public int? CompraId { get; set; }

        public int? GastoId { get; set; }

        // Propiedades de navegación
        public virtual MetodoPago MetodoPago { get; set; } = null!;
        public virtual Factura? Factura { get; set; }
        public virtual Compra? Compra { get; set; }
        public virtual Gasto? Gasto { get; set; }
    }
}
