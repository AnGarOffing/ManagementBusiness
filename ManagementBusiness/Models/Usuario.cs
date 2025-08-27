using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class Usuario : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Rol { get; set; } = string.Empty;

        public int SucursalId { get; set; }

        // Propiedades de navegación
        public virtual Sucursal Sucursal { get; set; } = null!;
    }
}
