using ManagementBusiness.Models;
using ManagementBusiness.Models.DTOs;
using ManagementBusiness.Services.Validation;
using ManagementBusiness.Services.Exceptions;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Servicio que implementa las operaciones CRUD básicas de clientes
    /// </summary>
    public class ClienteCRUDService
    {
        private readonly IClienteService _clienteService;
        private readonly ClienteValidator _clienteValidator;
        private readonly ILoggingService _loggingService;

        public ClienteCRUDService(
            IClienteService clienteService,
            ClienteValidator clienteValidator,
            ILoggingService loggingService)
        {
            _clienteService = clienteService ?? throw new ArgumentNullException(nameof(clienteService));
            _clienteValidator = clienteValidator ?? throw new ArgumentNullException(nameof(clienteValidator));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        #region Operaciones CRUD Básicas

        /// <summary>
        /// Crea un nuevo cliente desde un DTO
        /// </summary>
        public async Task<ClienteDisplayDTO> CreateClienteAsync(CreateClienteDTO createDto)
        {
            try
            {
                _loggingService.LogInformation($"Creando nuevo cliente: {createDto.Nombre}");

                // Convertir DTO a entidad
                var cliente = new Cliente
                {
                    Nombre = createDto.Nombre,
                    RFC = createDto.RFC,
                    Email = createDto.Email,
                    Telefono = createDto.Telefono,
                    Direccion = createDto.Direccion,
                    EsActivo = true,
                    FechaRegistro = DateTime.Now
                };

                // Crear cliente usando el servicio
                var clienteCreado = await _clienteService.CreateAsync(cliente);

                // Convertir a DTO de respuesta
                return ConvertToDisplayDTO(clienteCreado);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al crear cliente: {createDto.Nombre}", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene un cliente por ID
        /// </summary>
        public async Task<ClienteDisplayDTO?> GetClienteByIdAsync(int id)
        {
            try
            {
                _loggingService.LogInformation($"Obteniendo cliente con ID: {id}");

                var cliente = await _clienteService.GetByIdAsync(id);
                if (cliente == null)
                {
                    _loggingService.LogWarning($"Cliente no encontrado con ID: {id}");
                    return null;
                }

                return ConvertToDisplayDTO(cliente);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al obtener cliente con ID: {id}", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los clientes activos
        /// </summary>
        public async Task<IEnumerable<ClienteDisplayDTO>> GetAllClientesAsync()
        {
            try
            {
                _loggingService.LogInformation("Obteniendo todos los clientes activos");

                var clientes = await _clienteService.GetAllAsync();
                return clientes.Select(ConvertToDisplayDTO);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error al obtener todos los clientes", ex);
                throw;
            }
        }

        /// <summary>
        /// Actualiza un cliente existente
        /// </summary>
        public async Task<ClienteDisplayDTO> UpdateClienteAsync(UpdateClienteDTO updateDto)
        {
            try
            {
                _loggingService.LogInformation($"Actualizando cliente con ID: {updateDto.Id}");

                // Obtener cliente existente
                var clienteExistente = await _clienteService.GetByIdAsync(updateDto.Id);
                if (clienteExistente == null)
                {
                    throw new InvalidOperationException($"Cliente con ID {updateDto.Id} no encontrado");
                }

                // Actualizar propiedades
                clienteExistente.Nombre = updateDto.Nombre;
                clienteExistente.RFC = updateDto.RFC;
                clienteExistente.Email = updateDto.Email;
                clienteExistente.Telefono = updateDto.Telefono;
                clienteExistente.Direccion = updateDto.Direccion;
                clienteExistente.EsActivo = updateDto.EsActivo;

                // Actualizar cliente usando el servicio
                var clienteActualizado = await _clienteService.UpdateAsync(clienteExistente);

                return ConvertToDisplayDTO(clienteActualizado);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al actualizar cliente con ID: {updateDto.Id}", ex);
                throw;
            }
        }

        /// <summary>
        /// Elimina un cliente (soft delete)
        /// </summary>
        public async Task<bool> DeleteClienteAsync(int id)
        {
            try
            {
                _loggingService.LogInformation($"Eliminando cliente con ID: {id}");

                var resultado = await _clienteService.DeleteAsync(id);
                if (resultado)
                {
                    _loggingService.LogInformation($"Cliente con ID {id} eliminado exitosamente");
                }
                else
                {
                    _loggingService.LogWarning($"No se pudo eliminar el cliente con ID {id}");
                }

                return resultado;
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
        public async Task<bool> ReactivateClienteAsync(int id)
        {
            try
            {
                _loggingService.LogInformation($"Reactivando cliente con ID: {id}");

                var resultado = await _clienteService.ReactivateAsync(id);
                if (resultado)
                {
                    _loggingService.LogInformation($"Cliente con ID {id} reactivado exitosamente");
                }
                else
                {
                    _loggingService.LogWarning($"No se pudo reactivar el cliente con ID {id}");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al reactivar cliente con ID: {id}", ex);
                throw;
            }
        }

        #endregion

        #region Operaciones de Búsqueda y Filtrado

        /// <summary>
        /// Busca clientes con filtros y paginación
        /// </summary>
        public async Task<ClientePagedResultDTO> SearchClientesAsync(ClienteSearchDTO searchDto)
        {
            try
            {
                _loggingService.LogInformation($"Buscando clientes con filtros: {searchDto.Nombre ?? "Todos"}");

                // Obtener todos los clientes activos
                var clientes = await _clienteService.GetAllAsync();

                // Aplicar filtros
                var filteredClientes = ApplyFilters(clientes, searchDto);

                // Aplicar ordenamiento
                var sortedClientes = ApplySorting(filteredClientes, searchDto.SortBy, searchDto.SortDescending);

                // Aplicar paginación
                var pagedResult = ApplyPagination(sortedClientes, searchDto.PageNumber, searchDto.PageSize);

                // Convertir a DTOs
                var clientesDTO = pagedResult.Select(ConvertToDisplayDTO);

                return new ClientePagedResultDTO
                {
                    Clientes = clientesDTO,
                    TotalCount = filteredClientes.Count(),
                    PageNumber = searchDto.PageNumber,
                    PageSize = searchDto.PageSize,
                    TotalPages = (int)Math.Ceiling((double)filteredClientes.Count() / searchDto.PageSize),
                    HasPreviousPage = searchDto.PageNumber > 1,
                    HasNextPage = searchDto.PageNumber < (int)Math.Ceiling((double)filteredClientes.Count() / searchDto.PageSize)
                };
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error al buscar clientes", ex);
                throw;
            }
        }

        /// <summary>
        /// Busca clientes por nombre
        /// </summary>
        public async Task<IEnumerable<ClienteDisplayDTO>> SearchClientesByNameAsync(string nombre)
        {
            try
            {
                _loggingService.LogInformation($"Buscando clientes por nombre: {nombre}");

                var clientes = await _clienteService.SearchByNameAsync(nombre);
                return clientes.Select(ConvertToDisplayDTO);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error al buscar clientes por nombre: {nombre}", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene estadísticas de clientes
        /// </summary>
        public async Task<ClienteStatistics> GetClientesStatisticsAsync()
        {
            try
            {
                _loggingService.LogInformation("Obteniendo estadísticas de clientes");

                return await _clienteService.GetStatisticsAsync();
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error al obtener estadísticas de clientes", ex);
                throw;
            }
        }

        #endregion

        #region Métodos de Validación

        /// <summary>
        /// Valida un DTO de creación de cliente
        /// </summary>
        public async Task<ValidationResult> ValidateCreateClienteAsync(CreateClienteDTO createDto)
        {
            try
            {
                var cliente = new Cliente
                {
                    Nombre = createDto.Nombre,
                    RFC = createDto.RFC,
                    Email = createDto.Email,
                    Telefono = createDto.Telefono,
                    Direccion = createDto.Direccion
                };

                return await _clienteValidator.ValidateAsync(cliente);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error al validar DTO de creación de cliente", ex);
                throw;
            }
        }

        /// <summary>
        /// Valida un DTO de actualización de cliente
        /// </summary>
        public async Task<ValidationResult> ValidateUpdateClienteAsync(UpdateClienteDTO updateDto)
        {
            try
            {
                var cliente = new Cliente
                {
                    Id = updateDto.Id,
                    Nombre = updateDto.Nombre,
                    RFC = updateDto.RFC,
                    Email = updateDto.Email,
                    Telefono = updateDto.Telefono,
                    Direccion = updateDto.Direccion,
                    EsActivo = updateDto.EsActivo
                };

                return await _clienteValidator.ValidateAsync(cliente);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error al validar DTO de actualización de cliente", ex);
                throw;
            }
        }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Convierte una entidad Cliente a ClienteDisplayDTO
        /// </summary>
        private ClienteDisplayDTO ConvertToDisplayDTO(Cliente cliente)
        {
            return new ClienteDisplayDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                RFC = cliente.RFC,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                EsActivo = cliente.EsActivo,
                FechaRegistro = cliente.FechaRegistro,
                TotalFacturas = cliente.Facturas?.Count ?? 0,
                TotalPedidos = cliente.Pedidos?.Count ?? 0,
                TotalPresupuestos = cliente.Presupuestos?.Count ?? 0
            };
        }

        /// <summary>
        /// Aplica filtros a la colección de clientes
        /// </summary>
        private IEnumerable<Cliente> ApplyFilters(IEnumerable<Cliente> clientes, ClienteSearchDTO searchDto)
        {
            var filtered = clientes.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(searchDto.Nombre))
            {
                filtered = filtered.Where(c => c.Nombre.Contains(searchDto.Nombre, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(searchDto.Email))
            {
                filtered = filtered.Where(c => c.Email.Contains(searchDto.Email, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(searchDto.RFC))
            {
                filtered = filtered.Where(c => !string.IsNullOrEmpty(c.RFC) && c.RFC.Contains(searchDto.RFC, StringComparison.OrdinalIgnoreCase));
            }

            if (searchDto.EsActivo.HasValue)
            {
                filtered = filtered.Where(c => c.EsActivo == searchDto.EsActivo.Value);
            }

            if (searchDto.FechaRegistroDesde.HasValue)
            {
                filtered = filtered.Where(c => c.FechaRegistro >= searchDto.FechaRegistroDesde.Value);
            }

            if (searchDto.FechaRegistroHasta.HasValue)
            {
                filtered = filtered.Where(c => c.FechaRegistro <= searchDto.FechaRegistroHasta.Value);
            }

            return filtered;
        }

        /// <summary>
        /// Aplica ordenamiento a la colección de clientes
        /// </summary>
        private IEnumerable<Cliente> ApplySorting(IEnumerable<Cliente> clientes, string? sortBy, bool sortDescending)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return clientes;

            var sorted = sortBy.ToLower() switch
            {
                "nombre" => sortDescending ? clientes.OrderByDescending(c => c.Nombre) : clientes.OrderBy(c => c.Nombre),
                "email" => sortDescending ? clientes.OrderByDescending(c => c.Email) : clientes.OrderBy(c => c.Email),
                "fecharegistro" => sortDescending ? clientes.OrderByDescending(c => c.FechaRegistro) : clientes.OrderBy(c => c.FechaRegistro),
                "esactivo" => sortDescending ? clientes.OrderByDescending(c => c.EsActivo) : clientes.OrderBy(c => c.EsActivo),
                _ => clientes.OrderBy(c => c.Nombre)
            };

            return sorted;
        }

        /// <summary>
        /// Aplica paginación a la colección de clientes
        /// </summary>
        private IEnumerable<Cliente> ApplyPagination(IEnumerable<Cliente> clientes, int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            return clientes.Skip(skip).Take(pageSize);
        }

        #endregion
    }
}
