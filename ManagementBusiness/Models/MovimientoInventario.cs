using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models
{
    public class MovimientoInventario : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public int ProductoId { get; set; }

        public int SucursalId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [MaxLength(20)]
        public string Tipo { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Referencia { get; set; }

        public int? VentaId { get; set; }

        public int? CompraId { get; set; }

        // Propiedades de navegación
        public virtual Producto Producto { get; set; } = null!;
        public virtual Sucursal Sucursal { get; set; } = null!;
        public virtual Venta? Venta { get; set; }
        public virtual Compra? Compra { get; set; }
    }
}
