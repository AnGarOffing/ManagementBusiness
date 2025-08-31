using ManagementBusiness.Models.DTOs;
using ManagementBusiness.Services.Validation;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Servicio de prueba para demostrar el funcionamiento de las operaciones CRUD de clientes
    /// </summary>
    public class ClienteCRUDTestService
    {
        private readonly ClienteCRUDService _clienteCRUDService;
        private readonly ILoggingService _loggingService;

        public ClienteCRUDTestService(
            ClienteCRUDService clienteCRUDService,
            ILoggingService loggingService)
        {
            _clienteCRUDService = clienteCRUDService ?? throw new ArgumentNullException(nameof(clienteCRUDService));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        /// <summary>
        /// Ejecuta todas las pruebas CRUD básicas
        /// </summary>
        public async Task<ClienteCRUDTestResult> RunAllCRUDTestsAsync()
        {
            var result = new ClienteCRUDTestResult();
            
            try
            {
                _loggingService.LogInformation("Iniciando pruebas CRUD de clientes");

                // Prueba 1: Crear cliente
                result.CreateTestResult = await TestCreateClienteAsync();

                // Prueba 2: Leer cliente
                if (result.CreateTestResult.Success)
                {
                    result.ReadTestResult = await TestReadClienteAsync(result.CreateTestResult.CreatedClienteId);
                }

                // Prueba 3: Actualizar cliente
                if (result.ReadTestResult?.Success == true)
                {
                    result.UpdateTestResult = await TestUpdateClienteAsync(result.CreateTestResult.CreatedClienteId);
                }

                // Prueba 4: Eliminar cliente
                if (result.UpdateTestResult?.Success == true)
                {
                    result.DeleteTestResult = await TestDeleteClienteAsync(result.CreateTestResult.CreatedClienteId);
                }

                // Prueba 5: Reactivar cliente
                if (result.DeleteTestResult?.Success == true)
                {
                    result.ReactivateTestResult = await TestReactivateClienteAsync(result.CreateTestResult.CreatedClienteId);
                }

                // Prueba 6: Búsqueda y filtrado
                result.SearchTestResult = await TestSearchAndFilterAsync();

                // Prueba 7: Estadísticas
                result.StatisticsTestResult = await TestStatisticsAsync();

                result.OverallSuccess = result.CreateTestResult.Success &&
                                      result.ReadTestResult?.Success == true &&
                                      result.UpdateTestResult?.Success == true &&
                                      result.DeleteTestResult?.Success == true &&
                                      result.ReactivateTestResult?.Success == true &&
                                      result.SearchTestResult.Success &&
                                      result.StatisticsTestResult.Success;

                _loggingService.LogInformation($"Pruebas CRUD completadas. Éxito general: {result.OverallSuccess}");
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error durante la ejecución de las pruebas CRUD", ex);
                result.OverallSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        #region Pruebas Individuales

        /// <summary>
        /// Prueba la operación CREATE
        /// </summary>
        private async Task<ClienteCRUDTestStepResult> TestCreateClienteAsync()
        {
            try
            {
                _loggingService.LogInformation("Probando operación CREATE de cliente");

                var createDto = new CreateClienteDTO
                {
                    Nombre = "Cliente de Prueba CRUD",
                    RFC = "TEST123456789",
                    Email = "test.crud@example.com",
                    Telefono = "1234567890",
                    Direccion = "Dirección de Prueba CRUD 123"
                };

                // Validar DTO antes de crear
                var validationResult = await _clienteCRUDService.ValidateCreateClienteAsync(createDto);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return new ClienteCRUDTestStepResult
                    {
                        Success = false,
                        ErrorMessage = $"Validación fallida: {errors}"
                    };
                }

                // Crear cliente
                var clienteCreado = await _clienteCRUDService.CreateClienteAsync(createDto);

                if (clienteCreado != null && clienteCreado.Id > 0)
                {
                    _loggingService.LogInformation($"Cliente creado exitosamente con ID: {clienteCreado.Id}");
                    return new ClienteCRUDTestStepResult
                    {
                        Success = true,
                        CreatedClienteId = clienteCreado.Id,
                        Message = $"Cliente creado con ID: {clienteCreado.Id}"
                    };
                }
                else
                {
                    return new ClienteCRUDTestStepResult
                    {
                        Success = false,
                        ErrorMessage = "No se pudo crear el cliente"
                    };
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error en prueba CREATE", ex);
                return new ClienteCRUDTestStepResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Prueba la operación READ
        /// </summary>
        private async Task<ClienteCRUDTestStepResult> TestReadClienteAsync(int clienteId)
        {
            try
            {
                _loggingService.LogInformation($"Probando operación READ de cliente con ID: {clienteId}");

                var cliente = await _clienteCRUDService.GetClienteByIdAsync(clienteId);

                if (cliente != null && cliente.Id == clienteId)
                {
                    _loggingService.LogInformation($"Cliente leído exitosamente: {cliente.Nombre}");
                    return new ClienteCRUDTestStepResult
                    {
                        Success = true,
                        Message = $"Cliente leído: {cliente.Nombre}"
                    };
                }
                else
                {
                    return new ClienteCRUDTestStepResult
                    {
                        Success = false,
                        ErrorMessage = "No se pudo leer el cliente creado"
                    };
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error en prueba READ", ex);
                return new ClienteCRUDTestStepResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Prueba la operación UPDATE
        /// </summary>
        private async Task<ClienteCRUDTestStepResult> TestUpdateClienteAsync(int clienteId)
        {
            try
            {
                _loggingService.LogInformation($"Probando operación UPDATE de cliente con ID: {clienteId}");

                var updateDto = new UpdateClienteDTO
                {
                    Id = clienteId,
                    Nombre = "Cliente de Prueba CRUD - ACTUALIZADO",
                    RFC = "TEST123456789",
                    Email = "test.crud.updated@example.com",
                    Telefono = "0987654321",
                    Direccion = "Dirección de Prueba CRUD 123 - ACTUALIZADA",
                    EsActivo = true
                };

                // Validar DTO antes de actualizar
                var validationResult = await _clienteCRUDService.ValidateUpdateClienteAsync(updateDto);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return new ClienteCRUDTestStepResult
                    {
                        Success = false,
                        ErrorMessage = $"Validación fallida: {errors}"
                    };
                }

                // Actualizar cliente
                var clienteActualizado = await _clienteCRUDService.UpdateClienteAsync(updateDto);

                if (clienteActualizado != null && clienteActualizado.Nombre.Contains("ACTUALIZADO"))
                {
                    _loggingService.LogInformation($"Cliente actualizado exitosamente: {clienteActualizado.Nombre}");
                    return new ClienteCRUDTestStepResult
                    {
                        Success = true,
                        Message = $"Cliente actualizado: {clienteActualizado.Nombre}"
                    };
                }
                else
                {
                    return new ClienteCRUDTestStepResult
                    {
                        Success = false,
                        ErrorMessage = "No se pudo actualizar el cliente"
                    };
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error en prueba UPDATE", ex);
                return new ClienteCRUDTestStepResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Prueba la operación DELETE
        /// </summary>
        private async Task<ClienteCRUDTestStepResult> TestDeleteClienteAsync(int clienteId)
        {
            try
            {
                _loggingService.LogInformation($"Probando operación DELETE de cliente con ID: {clienteId}");

                var resultado = await _clienteCRUDService.DeleteClienteAsync(clienteId);

                if (resultado)
                {
                    _loggingService.LogInformation($"Cliente eliminado exitosamente con ID: {clienteId}");
                    return new ClienteCRUDTestStepResult
                    {
                        Success = true,
                        Message = $"Cliente eliminado con ID: {clienteId}"
                    };
                }
                else
                {
                    return new ClienteCRUDTestStepResult
                    {
                        Success = false,
                        ErrorMessage = "No se pudo eliminar el cliente"
                    };
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error en prueba DELETE", ex);
                return new ClienteCRUDTestStepResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Prueba la operación REACTIVATE
        /// </summary>
        private async Task<ClienteCRUDTestStepResult> TestReactivateClienteAsync(int clienteId)
        {
            try
            {
                _loggingService.LogInformation($"Probando operación REACTIVATE de cliente con ID: {clienteId}");

                var resultado = await _clienteCRUDService.ReactivateClienteAsync(clienteId);

                if (resultado)
                {
                    _loggingService.LogInformation($"Cliente reactivado exitosamente con ID: {clienteId}");
                    return new ClienteCRUDTestStepResult
                    {
                        Success = true,
                        Message = $"Cliente reactivado con ID: {clienteId}"
                    };
                }
                else
                {
                    return new ClienteCRUDTestStepResult
                    {
                        Success = false,
                        ErrorMessage = "No se pudo reactivar el cliente"
                    };
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error en prueba REACTIVATE", ex);
                return new ClienteCRUDTestStepResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Prueba las operaciones de búsqueda y filtrado
        /// </summary>
        private async Task<ClienteCRUDTestStepResult> TestSearchAndFilterAsync()
        {
            try
            {
                _loggingService.LogInformation("Probando operaciones de búsqueda y filtrado");

                // Probar búsqueda por nombre
                var searchDto = new ClienteSearchDTO
                {
                    Nombre = "Prueba",
                    PageNumber = 1,
                    PageSize = 10,
                    SortBy = "Nombre",
                    SortDescending = false
                };

                var searchResult = await _clienteCRUDService.SearchClientesAsync(searchDto);

                if (searchResult != null)
                {
                    _loggingService.LogInformation($"Búsqueda exitosa. Total clientes: {searchResult.TotalCount}");
                    return new ClienteCRUDTestStepResult
                    {
                        Success = true,
                        Message = $"Búsqueda exitosa. Total: {searchResult.TotalCount}"
                    };
                }
                else
                {
                    return new ClienteCRUDTestStepResult
                    {
                        Success = false,
                        ErrorMessage = "No se pudo realizar la búsqueda"
                    };
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error en prueba de búsqueda", ex);
                return new ClienteCRUDTestStepResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Prueba las estadísticas
        /// </summary>
        private async Task<ClienteCRUDTestStepResult> TestStatisticsAsync()
        {
            try
            {
                _loggingService.LogInformation("Probando operaciones de estadísticas");

                var statistics = await _clienteCRUDService.GetClientesStatisticsAsync();

                if (statistics != null)
                {
                    _loggingService.LogInformation($"Estadísticas obtenidas. Total clientes: {statistics.TotalClientes}");
                    return new ClienteCRUDTestStepResult
                    {
                        Success = true,
                        Message = $"Estadísticas obtenidas. Total: {statistics.TotalClientes}"
                    };
                }
                else
                {
                    return new ClienteCRUDTestStepResult
                    {
                        Success = false,
                        ErrorMessage = "No se pudieron obtener las estadísticas"
                    };
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error en prueba de estadísticas", ex);
                return new ClienteCRUDTestStepResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        #endregion
    }

    #region Clases de Resultado de Pruebas

    /// <summary>
    /// Resultado general de todas las pruebas CRUD
    /// </summary>
    public class ClienteCRUDTestResult
    {
        public bool OverallSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public ClienteCRUDTestStepResult CreateTestResult { get; set; } = new();
        public ClienteCRUDTestStepResult? ReadTestResult { get; set; }
        public ClienteCRUDTestStepResult? UpdateTestResult { get; set; }
        public ClienteCRUDTestStepResult? DeleteTestResult { get; set; }
        public ClienteCRUDTestStepResult? ReactivateTestResult { get; set; }
        public ClienteCRUDTestStepResult SearchTestResult { get; set; } = new();
        public ClienteCRUDTestStepResult StatisticsTestResult { get; set; } = new();
    }

    /// <summary>
    /// Resultado de una prueba individual
    /// </summary>
    public class ClienteCRUDTestStepResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? ErrorMessage { get; set; }
        public int CreatedClienteId { get; set; }
    }

    #endregion
}
