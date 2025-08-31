using System.ComponentModel.DataAnnotations;

namespace ManagementBusiness.Models.DTOs
{
    /// <summary>
    /// DTO para crear un nuevo cliente
    /// </summary>
    public class CreateClienteDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(13, ErrorMessage = "El RFC no puede exceder 13 caracteres")]
        public string? RFC { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [MaxLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [MaxLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [MaxLength(500, ErrorMessage = "La dirección no puede exceder 500 caracteres")]
        public string Direccion { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para actualizar un cliente existente
    /// </summary>
    public class UpdateClienteDTO
    {
        [Required(ErrorMessage = "El ID es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(13, ErrorMessage = "El RFC no puede exceder 13 caracteres")]
        public string? RFC { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [MaxLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [MaxLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [MaxLength(500, ErrorMessage = "La dirección no puede exceder 500 caracteres")]
        public string Direccion { get; set; } = string.Empty;

        public bool EsActivo { get; set; } = true;
    }

    /// <summary>
    /// DTO para mostrar información de un cliente
    /// </summary>
    public class ClienteDisplayDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? RFC { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public bool EsActivo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int TotalFacturas { get; set; }
        public int TotalPedidos { get; set; }
        public int TotalPresupuestos { get; set; }
    }

    /// <summary>
    /// DTO para búsqueda y filtrado de clientes
    /// </summary>
    public class ClienteSearchDTO
    {
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? RFC { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistroDesde { get; set; }
        public DateTime? FechaRegistroHasta { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? SortBy { get; set; } = "Nombre";
        public bool SortDescending { get; set; } = false;
    }

    /// <summary>
    /// DTO para respuesta paginada de clientes
    /// </summary>
    public class ClientePagedResultDTO
    {
        public IEnumerable<ClienteDisplayDTO> Clientes { get; set; } = Enumerable.Empty<ClienteDisplayDTO>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
