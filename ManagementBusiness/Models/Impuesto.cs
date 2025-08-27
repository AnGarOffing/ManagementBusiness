using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class Impuesto : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Range(0, 100)]
        public decimal Porcentaje { get; set; }

        [MaxLength(100)]
        public string? Pais { get; set; }

        // Propiedades de navegación
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
        public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
    }
}
