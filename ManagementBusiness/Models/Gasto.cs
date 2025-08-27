using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class Gasto : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Monto { get; set; }

        [Required]
        [MaxLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        public int TipoGastoId { get; set; }

        public int? ProveedorId { get; set; }

        // Propiedades de navegación
        public virtual TipoGasto TipoGasto { get; set; } = null!;
        public virtual Proveedor? Proveedor { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
