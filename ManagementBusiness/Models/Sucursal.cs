using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class Sucursal : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Direccion { get; set; } = string.Empty;

        public bool EsPrincipal { get; set; } = false;

        // Propiedades de navegación
        public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
        public virtual ICollection<MovimientoInventario> MovimientosInventario { get; set; } = new List<MovimientoInventario>();
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public virtual ICollection<ReporteMensual> ReportesMensuales { get; set; } = new List<ReporteMensual>();
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
