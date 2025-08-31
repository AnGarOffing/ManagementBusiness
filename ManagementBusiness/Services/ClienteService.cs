using Microsoft.EntityFrameworkCore;
using ManagementBusiness.Data;
using ManagementBusiness.Models;
using ManagementBusiness.Services.Validation;
using ManagementBusiness.Services.Helpers;
using ManagementBusiness.Services.Exceptions;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Implementación del servicio de gestión de clientes
    /// </summary>
    public class ClienteService : IClienteService
    {
        private readonly IRepositoryWithSoftDelete<Cliente> _clienteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ClienteValidator _clienteValidator;
        private readonly ILoggingService _loggingService;

        public ClienteService(
            IRepositoryWithSoftDelete<Cliente> clienteRepository,
            IUnitOfWork unitOfWork,
            ClienteValidator clienteValidator,
            ILoggingService loggingService)
        {
            _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _clienteValidator = clienteValidator ?? throw new ArgumentNullException(nameof(clienteValidator));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        /// <summary>
        /// Obtiene todos los clientes activos
        /// </summary>
        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            try
            {
                _loggingService.LogInformation("Obteniendo todos los clientes activos");
                return _clienteRepository.GetActive();
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error al obtener todos los clientes activos", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los clientes (activos e inactivos)
        /// </summary>
        public async Task<IEnumerable<Cliente>> GetAllIncludingInactiveAsync()
        {
            try
            {
                _loggingService.LogInformation("Obteniendo todos los clientes incluyendo inactivos");
                var activos = _clienteRepository.GetActive();
                var inactivos = _clienteRepository.GetDeleted();
                return activos.Concat(inactivos);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error al obtener todos los clientes incluyendo inactivos", ex);
                throw;
            }
            }

        /// <summary>
        /// Obtiene un cliente por su ID
        /// </summary>
        public async Task<Cliente?> GetByIdAsync(int id)
        {
            try
            {
                _loggingService.LogInformation($"Obteniendo cliente con ID: {id}");
                return _clienteRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al obtener cliente con ID: {id}", ex);
                throw;
            }
        }

        /// <summary>
        /// Busca clientes por nombre
        /// </summary>
        public async Task<IEnumerable<Cliente>> SearchByNameAsync(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    _loggingService.LogWarning("Búsqueda por nombre con valor vacío o nulo");
                    return Enumerable.Empty<Cliente>();
                }

                _loggingService.LogInformation($"Buscando clientes por nombre: {nombre}");
                
                var clientes = _clienteRepository.GetActive();
                return clientes.Where(c => c.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al buscar clientes por nombre: {nombre}", ex);
                throw;
            }
        }

        /// <summary>
        /// Busca clientes por email
        /// </summary>
        public async Task<Cliente?> GetByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    _loggingService.LogWarning("Búsqueda por email con valor vacío o nulo");
                    return null;
                }

                _loggingService.LogInformation($"Buscando cliente por email: {email}");
                
                var clientes = _clienteRepository.GetActive();
                return clientes.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al buscar cliente por email: {email}", ex);
                throw;
            }
        }

        /// <summary>
        /// Busca clientes por RFC
        /// </summary>
        public async Task<Cliente?> GetByRFCAsync(string rfc)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(rfc))
                {
                    _loggingService.LogWarning("Búsqueda por RFC con valor vacío o nulo");
                    return null;
                }

                _loggingService.LogInformation($"Buscando cliente por RFC: {rfc}");
                
                var clientes = _clienteRepository.GetActive();
                return clientes.FirstOrDefault(c => !string.IsNullOrEmpty(c.RFC) && c.RFC.Equals(rfc, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al buscar cliente por RFC: {rfc}", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene clientes por estado (activo/inactivo)
        /// </summary>
        public async Task<IEnumerable<Cliente>> GetByStatusAsync(bool esActivo)
        {
            try
            {
                _loggingService.LogInformation($"Obteniendo clientes con estado activo: {esActivo}");
                
                var clientes = _clienteRepository.GetActive();
                return clientes.Where(c => c.EsActivo == esActivo);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al obtener clientes con estado activo: {esActivo}", ex);
                throw;
            }
        }

        /// <summary>
        /// Crea un nuevo cliente
        /// </summary>
        public async Task<Cliente> CreateAsync(Cliente cliente)
        {
            try
            {
                _loggingService.LogInformation($"Creando nuevo cliente: {cliente.Nombre}");

                // Validar cliente antes de crear
                var validationResult = await ValidateAsync(cliente);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    _loggingService.LogWarning($"Validación fallida al crear cliente: {errors}");
                    throw new ValidationException($"Datos del cliente no válidos: {errors}");
                }

                // Verificar que el email no esté en uso
                if (await IsEmailInUseAsync(cliente.Email))
                {
                    _loggingService.LogWarning($"Email ya en uso: {cliente.Email}");
                    throw new InvalidOperationException($"El email {cliente.Email} ya está registrado por otro cliente");
                }

                // Verificar que el RFC no esté en uso (si se proporciona)
                if (!string.IsNullOrEmpty(cliente.RFC) && await IsRFCInUseAsync(cliente.RFC))
                {
                    _loggingService.LogWarning($"RFC ya en uso: {cliente.RFC}");
                    throw new InvalidOperationException($"El RFC {cliente.RFC} ya está registrado por otro cliente");
                }

                // Establecer valores por defecto
                cliente.FechaRegistro = DateTime.Now;
                cliente.EsActivo = true;

                // Crear el cliente
                _clienteRepository.Add(cliente);
                await _unitOfWork.SaveChangesAsync();
                var clienteCreado = cliente;

                _loggingService.LogInformation($"Cliente creado exitosamente con ID: {clienteCreado.Id}");
                return clienteCreado;
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al crear cliente: {cliente.Nombre}", ex);
                throw;
            }
        }

        /// <summary>
        /// Actualiza un cliente existente
        /// </summary>
        public async Task<Cliente> UpdateAsync(Cliente cliente)
        {
            try
            {
                _loggingService.LogInformation($"Actualizando cliente con ID: {cliente.Id}");

                // Validar cliente antes de actualizar
                var validationResult = await ValidateAsync(cliente);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    _loggingService.LogWarning($"Validación fallida al actualizar cliente: {errors}");
                    throw new ValidationException($"Datos del cliente no válidos: {errors}");
                }

                // Verificar que el cliente existe
                var clienteExistente = _clienteRepository.GetById(cliente.Id);
                if (clienteExistente == null)
                {
                    _loggingService.LogWarning($"Cliente no encontrado para actualizar con ID: {cliente.Id}");
                    throw new InvalidOperationException($"Cliente con ID {cliente.Id} no encontrado");
                }

                // Verificar que el email no esté en uso por otro cliente
                if (await IsEmailInUseAsync(cliente.Email, cliente.Id))
                {
                    _loggingService.LogWarning($"Email ya en uso por otro cliente: {cliente.Email}");
                    throw new InvalidOperationException($"El email {cliente.Email} ya está registrado por otro cliente");
                }

                // Verificar que el RFC no esté en uso por otro cliente (si se proporciona)
                if (!string.IsNullOrEmpty(cliente.RFC) && await IsRFCInUseAsync(cliente.RFC, cliente.Id))
                {
                    _loggingService.LogWarning($"RFC ya en uso por otro cliente: {cliente.RFC}");
                    throw new InvalidOperationException($"El RFC {cliente.RFC} ya está registrado por otro cliente");
                }

                // Preservar valores que no deben cambiar
                cliente.FechaRegistro = clienteExistente.FechaRegistro;

                // Actualizar el cliente
                _clienteRepository.Update(cliente);
                await _unitOfWork.SaveChangesAsync();
                var clienteActualizado = cliente;

                _loggingService.LogInformation($"Cliente actualizado exitosamente con ID: {clienteActualizado.Id}");
                return clienteActualizado;
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al actualizar cliente con ID: {cliente.Id}", ex);
                throw;
            }
        }

        /// <summary>
        /// Elimina un cliente (soft delete)
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _loggingService.LogInformation($"Eliminando cliente con ID: {id}");

                var cliente = _clienteRepository.GetById(id);
                if (cliente == null)
                {
                    _loggingService.LogWarning($"Cliente no encontrado para eliminar con ID: {id}");
                    return false;
                }

                // Verificar si el cliente tiene facturas, pedidos o presupuestos
                if (cliente.Facturas.Any() || cliente.Pedidos.Any() || cliente.Presupuestos.Any())
                {
                    _loggingService.LogWarning($"No se puede eliminar cliente con ID {id} porque tiene registros relacionados");
                    throw new InvalidOperationException("No se puede eliminar el cliente porque tiene facturas, pedidos o presupuestos asociados");
                }

                _clienteRepository.SoftDelete(cliente);
                await _unitOfWork.SaveChangesAsync();

                _loggingService.LogInformation($"Cliente eliminado exitosamente con ID: {id}");
                return true;
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al eliminar cliente con ID: {id}", ex);
                throw;
            }
        }

        /// <summary>
        /// Reactiva un cliente eliminado
        /// </summary>
        public async Task<bool> ReactivateAsync(int id)
        {
            try
            {
                _loggingService.LogInformation($"Reactivando cliente con ID: {id}");

                var cliente = _clienteRepository.GetById(id);
                if (cliente == null)
                {
                    _loggingService.LogWarning($"Cliente no encontrado para reactivar con ID: {id}");
                    return false;
                }

                if (cliente.EsActivo)
                {
                    _loggingService.LogInformation($"Cliente con ID {id} ya está activo");
                    return true;
                }

                cliente.EsActivo = true;
                _clienteRepository.Update(cliente);
                await _unitOfWork.SaveChangesAsync();

                _loggingService.LogInformation($"Cliente reactivado exitosamente con ID: {id}");
                return true;
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al reactivar cliente con ID: {id}", ex);
                throw;
            }
        }

        /// <summary>
        /// Valida los datos de un cliente
        /// </summary>
        public async Task<ValidationResult> ValidateAsync(Cliente cliente)
        {
            try
            {
                _loggingService.LogInformation($"Validando cliente: {cliente.Nombre}");
                return await _clienteValidator.ValidateAsync(cliente);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al validar cliente: {cliente.Nombre}", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene estadísticas básicas de clientes
        /// </summary>
        public async Task<ClienteStatistics> GetStatisticsAsync()
        {
            try
            {
                _loggingService.LogInformation("Obteniendo estadísticas de clientes");

                var clientes = _clienteRepository.GetActive();
                var inactivos = _clienteRepository.GetDeleted();

                var ahora = DateTime.Now;
                var inicioMes = new DateTime(ahora.Year, ahora.Month, 1);
                var inicioAno = new DateTime(ahora.Year, 1, 1);

                var estadisticas = new ClienteStatistics
                {
                    TotalClientes = clientes.Count() + inactivos.Count(),
                    ClientesActivos = clientes.Count(),
                    ClientesInactivos = inactivos.Count(),
                    NuevosEsteMes = clientes.Count(c => c.FechaRegistro >= inicioMes),
                    NuevosEsteAno = clientes.Count(c => c.FechaRegistro >= inicioAno),
                    PromedioFacturacionMensual = 0, // TODO: Implementar cálculo real de facturación
                    ClienteMasFacturacion = null // TODO: Implementar cálculo real
                };

                _loggingService.LogInformation($"Estadísticas obtenidas: {estadisticas.TotalClientes} clientes totales");
                return estadisticas;
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error al obtener estadísticas de clientes", ex);
                throw;
            }
        }

        /// <summary>
        /// Verifica si un email ya está en uso
        /// </summary>
        public async Task<bool> IsEmailInUseAsync(string email, int? excludeId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                var clientes = _clienteRepository.GetActive();
                return clientes.Any(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && c.Id != excludeId);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al verificar si email está en uso: {email}", ex);
                throw;
            }
        }

        /// <summary>
        /// Verifica si un RFC ya está en uso
        /// </summary>
        public async Task<bool> IsRFCInUseAsync(string rfc, int? excludeId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(rfc))
                    return false;

                var clientes = _clienteRepository.GetActive();
                return clientes.Any(c => !string.IsNullOrEmpty(c.RFC) && c.RFC.Equals(rfc, StringComparison.OrdinalIgnoreCase) && c.Id != excludeId);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al verificar si RFC está en uso: {rfc}", ex);
                throw;
            }
        }
    }
}
