using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class Proveedor : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(13)]
        public string RFC { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? CuentaBancaria { get; set; }

        // Propiedades de navegación
        public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
        public virtual ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();
    }
}
