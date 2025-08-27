using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class ReporteMensual : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 12)]
        public int Mes { get; set; }

        [Required]
        [Range(2000, 2100)]
        public int Anio { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalVentas { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalGastos { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal UtilidadNeta { get; set; }

        public int SucursalId { get; set; }

        // Propiedades de navegación
        public virtual Sucursal Sucursal { get; set; } = null!;
    }
}
