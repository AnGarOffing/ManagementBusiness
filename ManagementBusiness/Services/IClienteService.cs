using ManagementBusiness.Models;
using ManagementBusiness.Services.Validation;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Interfaz para el servicio de gestión de clientes
    /// </summary>
    public interface IClienteService
    {
        /// <summary>
        /// Obtiene todos los clientes activos
        /// </summary>
        /// <returns>Lista de clientes activos</returns>
        Task<IEnumerable<Cliente>> GetAllAsync();

        /// <summary>
        /// Obtiene todos los clientes (activos e inactivos)
        /// </summary>
        /// <returns>Lista de todos los clientes</returns>
        Task<IEnumerable<Cliente>> GetAllIncludingInactiveAsync();

        /// <summary>
        /// Obtiene un cliente por su ID
        /// </summary>
        /// <param name="id">ID del cliente</param>
        /// <returns>Cliente encontrado o null si no existe</returns>
        Task<Cliente?> GetByIdAsync(int id);

        /// <summary>
        /// Busca clientes por nombre
        /// </summary>
        /// <param name="nombre">Nombre o parte del nombre a buscar</param>
        /// <returns>Lista de clientes que coinciden con la búsqueda</returns>
        Task<IEnumerable<Cliente>> SearchByNameAsync(string nombre);

        /// <summary>
        /// Busca clientes por email
        /// </summary>
        /// <param name="email">Email a buscar</param>
        /// <returns>Cliente encontrado o null si no existe</returns>
        Task<Cliente?> GetByEmailAsync(string email);

        /// <summary>
        /// Busca clientes por RFC
        /// </summary>
        /// <param name="rfc">RFC a buscar</param>
        /// <returns>Cliente encontrado o null si no existe</returns>
        Task<Cliente?> GetByRFCAsync(string rfc);

        /// <summary>
        /// Obtiene clientes por estado (activo/inactivo)
        /// </summary>
        /// <param name="esActivo">Estado del cliente</param>
        /// <returns>Lista de clientes con el estado especificado</returns>
        Task<IEnumerable<Cliente>> GetByStatusAsync(bool esActivo);

        /// <summary>
        /// Crea un nuevo cliente
        /// </summary>
        /// <param name="cliente">Datos del cliente a crear</param>
        /// <returns>Cliente creado con ID asignado</returns>
        Task<Cliente> CreateAsync(Cliente cliente);

        /// <summary>
        /// Actualiza un cliente existente
        /// </summary>
        /// <param name="cliente">Datos del cliente a actualizar</param>
        /// <returns>Cliente actualizado</returns>
        Task<Cliente> UpdateAsync(Cliente cliente);

        /// <summary>
        /// Elimina un cliente (soft delete)
        /// </summary>
        /// <param name="id">ID del cliente a eliminar</param>
        /// <returns>True si se eliminó correctamente</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Reactiva un cliente eliminado
        /// </summary>
        /// <param name="id">ID del cliente a reactivar</param>
        /// <returns>True si se reactivó correctamente</returns>
        Task<bool> ReactivateAsync(int id);

        /// <summary>
        /// Valida los datos de un cliente
        /// </summary>
        /// <param name="cliente">Cliente a validar</param>
        /// <returns>Resultado de la validación</returns>
        Task<ValidationResult> ValidateAsync(Cliente cliente);

        /// <summary>
        /// Obtiene estadísticas básicas de clientes
        /// </summary>
        /// <returns>Estadísticas de clientes</returns>
        Task<ClienteStatistics> GetStatisticsAsync();

        /// <summary>
        /// Verifica si un email ya está en uso
        /// </summary>
        /// <param name="email">Email a verificar</param>
        /// <param name="excludeId">ID del cliente a excluir de la verificación</param>
        /// <returns>True si el email ya está en uso</returns>
        Task<bool> IsEmailInUseAsync(string email, int? excludeId = null);

        /// <summary>
        /// Verifica si un RFC ya está en uso
        /// </summary>
        /// <param name="rfc">RFC a verificar</param>
        /// <param name="excludeId">ID del cliente a excluir de la verificación</param>
        /// <returns>True si el RFC ya está en uso</returns>
        Task<bool> IsRFCInUseAsync(string rfc, int? excludeId = null);
    }

    /// <summary>
    /// Estadísticas de clientes
    /// </summary>
    public class ClienteStatistics
    {
        public int TotalClientes { get; set; }
        public int ClientesActivos { get; set; }
        public int ClientesInactivos { get; set; }
        public int NuevosEsteMes { get; set; }
        public int NuevosEsteAno { get; set; }
        public decimal PromedioFacturacionMensual { get; set; }
        public Cliente? ClienteMasFacturacion { get; set; }
    }
}
