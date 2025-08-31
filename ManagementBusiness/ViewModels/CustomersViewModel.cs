using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ManagementBusiness.Models.DTOs;
using ManagementBusiness.Services;
using ManagementBusiness.Services.Validation;
using ManagementBusiness.Services.Helpers;

namespace ManagementBusiness.ViewModels
{
    /// <summary>
    /// ViewModel para la gestión de clientes
    /// </summary>
    public class CustomersViewModel : BaseViewModel
    {
        private readonly ClienteCRUDService _clienteCRUDService;
        private readonly ILoggingService _loggingService;

        #region Propiedades de Datos

        private ObservableCollection<ClienteDisplayDTO> _clientes;
        private ClienteDisplayDTO? _clienteSeleccionado;
        private ClienteDisplayDTO? _clienteEnEdicion;
        private bool _estaEnModoEdicion;
        private bool _estaEnModoCreacion;

        public ObservableCollection<ClienteDisplayDTO> Clientes
        {
            get => _clientes;
            set => SetProperty(ref _clientes, value);
        }

        public ClienteDisplayDTO? ClienteSeleccionado
        {
            get => _clienteSeleccionado;
            set
            {
                if (SetProperty(ref _clienteSeleccionado, value))
                {
                    OnClienteSeleccionadoChanged();
                }
            }
        }

        public ClienteDisplayDTO? ClienteEnEdicion
        {
            get => _clienteEnEdicion;
            set => SetProperty(ref _clienteEnEdicion, value);
        }

        public bool EstaEnModoEdicion
        {
            get => _estaEnModoEdicion;
            set => SetProperty(ref _estaEnModoEdicion, value);
        }

        public bool EstaEnModoCreacion
        {
            get => _estaEnModoCreacion;
            set => SetProperty(ref _estaEnModoCreacion, value);
        }

        #endregion

        #region Propiedades de Filtros y Búsqueda

        private string _filtroNombre = string.Empty;
        private string _filtroEmail = string.Empty;
        private string _filtroRFC = string.Empty;
        private bool? _filtroEsActivo = null;
        private DateTime? _filtroFechaDesde = null;
        private DateTime? _filtroFechaHasta = null;
        private string _ordenamiento = "Nombre";
        private bool _ordenDescendente = false;

        public string FiltroNombre
        {
            get => _filtroNombre;
            set
            {
                if (SetProperty(ref _filtroNombre, value))
                {
                    _ = BuscarClientesAsync();
                }
            }
        }

        public string FiltroEmail
        {
            get => _filtroEmail;
            set
            {
                if (SetProperty(ref _filtroEmail, value))
                {
                    _ = BuscarClientesAsync();
                }
            }
        }

        public string FiltroRFC
        {
            get => _filtroRFC;
            set
            {
                if (SetProperty(ref _filtroRFC, value))
                {
                    _ = BuscarClientesAsync();
                }
            }
        }

        public bool? FiltroEsActivo
        {
            get => _filtroEsActivo;
            set
            {
                if (SetProperty(ref _filtroEsActivo, value))
                {
                    _ = BuscarClientesAsync();
                }
            }
        }

        public DateTime? FiltroFechaDesde
        {
            get => _filtroFechaDesde;
            set
            {
                if (SetProperty(ref _filtroFechaDesde, value))
                {
                    _ = BuscarClientesAsync();
                }
            }
        }

        public DateTime? FiltroFechaHasta
        {
            get => _filtroFechaHasta;
            set
            {
                if (SetProperty(ref _filtroFechaHasta, value))
                {
                    _ = BuscarClientesAsync();
                }
            }
        }

        public string Ordenamiento
        {
            get => _ordenamiento;
            set
            {
                if (SetProperty(ref _ordenamiento, value))
                {
                    _ = BuscarClientesAsync();
                }
            }
        }

        public bool OrdenDescendente
        {
            get => _ordenDescendente;
            set
            {
                if (SetProperty(ref _ordenDescendente, value))
                {
                    _ = BuscarClientesAsync();
                }
            }
        }

        #endregion

        #region Propiedades de Paginación

        private int _paginaActual = 1;
        private int _tamañoPagina = 20;
        private int _totalPaginas = 1;
        private int _totalClientes = 0;
        private bool _tienePaginaAnterior = false;
        private bool _tienePaginaSiguiente = false;

        public int PaginaActual
        {
            get => _paginaActual;
            set
            {
                if (SetProperty(ref _paginaActual, value))
                {
                    _ = BuscarClientesAsync();
                }
            }
        }

        public int TamañoPagina
        {
            get => _tamañoPagina;
            set
            {
                if (SetProperty(ref _tamañoPagina, value))
                {
                    PaginaActual = 1; // Reset a la primera página
                    _ = BuscarClientesAsync();
                }
            }
        }

        public int TotalPaginas
        {
            get => _totalPaginas;
            set => SetProperty(ref _totalPaginas, value);
        }

        public int TotalClientes
        {
            get => _totalClientes;
            set => SetProperty(ref _totalClientes, value);
        }

        public bool TienePaginaAnterior
        {
            get => _tienePaginaAnterior;
            set => SetProperty(ref _tienePaginaAnterior, value);
        }

        public bool TienePaginaSiguiente
        {
            get => _tienePaginaSiguiente;
            set => SetProperty(ref _tienePaginaSiguiente, value);
        }

        #endregion

        #region Propiedades de Estado

        private bool _estaCargando = false;
        private string _mensajeEstado = string.Empty;
        private bool _hayError = false;
        private string _mensajeError = string.Empty;

        public bool EstaCargando
        {
            get => _estaCargando;
            set => SetProperty(ref _estaCargando, value);
        }

        public string MensajeEstado
        {
            get => _mensajeEstado;
            set => SetProperty(ref _mensajeEstado, value);
        }

        public bool HayError
        {
            get => _hayError;
            set => SetProperty(ref _hayError, value);
        }

        public string MensajeError
        {
            get => _mensajeError;
            set => SetProperty(ref _mensajeError, value);
        }

        #endregion

        #region Propiedades de Formulario

        private CreateClienteDTO _nuevoCliente = new();
        private UpdateClienteDTO _clienteParaActualizar = new();
        private ValidationResult? _resultadoValidacion;

        public CreateClienteDTO NuevoCliente
        {
            get => _nuevoCliente;
            set => SetProperty(ref _nuevoCliente, value);
        }

        public UpdateClienteDTO ClienteParaActualizar
        {
            get => _clienteParaActualizar;
            set => SetProperty(ref _clienteParaActualizar, value);
        }

        public ValidationResult? ResultadoValidacion
        {
            get => _resultadoValidacion;
            set => SetProperty(ref _resultadoValidacion, value);
        }

        #endregion

        #region Comandos

        public ICommand CargarClientesCommand { get; }
        public ICommand CrearClienteCommand { get; }
        public ICommand EditarClienteCommand { get; }
        public ICommand GuardarClienteCommand { get; }
        public ICommand CancelarEdicionCommand { get; }
        public ICommand EliminarClienteCommand { get; }
        public ICommand ReactivarClienteCommand { get; }
        public ICommand LimpiarFiltrosCommand { get; }
        public ICommand SiguientePaginaCommand { get; }
        public ICommand PaginaAnteriorCommand { get; }
        public ICommand IrAPaginaCommand { get; }
        public ICommand ExportarClientesCommand { get; }
        public ICommand RefrescarCommand { get; }

        #endregion

        #region Constructor

        public CustomersViewModel(ClienteCRUDService clienteCRUDService, ILoggingService loggingService)
        {
            _clienteCRUDService = clienteCRUDService ?? throw new ArgumentNullException(nameof(clienteCRUDService));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));

            _clientes = new ObservableCollection<ClienteDisplayDTO>();

            // Inicializar comandos
            CargarClientesCommand = new RelayCommand(async (obj) => await CargarClientesAsync(), (obj) => !EstaCargando);
            CrearClienteCommand = new RelayCommand((obj) => IniciarCreacion(), (obj) => !EstaCargando && !EstaEnModoEdicion);
            EditarClienteCommand = new RelayCommand((obj) => IniciarEdicion(), (obj) => !EstaCargando && ClienteSeleccionado != null && !EstaEnModoEdicion);
            GuardarClienteCommand = new RelayCommand(async (obj) => await GuardarClienteAsync(), (obj) => !EstaCargando && (EstaEnModoEdicion || EstaEnModoCreacion));
            CancelarEdicionCommand = new RelayCommand((obj) => CancelarEdicion(), (obj) => !EstaCargando);
            EliminarClienteCommand = new RelayCommand(async (obj) => await EliminarClienteAsync(), (obj) => !EstaCargando && ClienteSeleccionado != null && !EstaEnModoEdicion);
            ReactivarClienteCommand = new RelayCommand(async (obj) => await ReactivarClienteAsync(), (obj) => !EstaCargando && ClienteSeleccionado != null && !EstaEnModoEdicion);
            LimpiarFiltrosCommand = new RelayCommand((obj) => LimpiarFiltros(), (obj) => !EstaCargando);
            SiguientePaginaCommand = new RelayCommand((obj) => PaginaActual++, (obj) => !EstaCargando && TienePaginaSiguiente);
            PaginaAnteriorCommand = new RelayCommand((obj) => PaginaActual--, (obj) => !EstaCargando && TienePaginaAnterior);
            IrAPaginaCommand = new RelayCommand<int>(pagina => PaginaActual = pagina, pagina => !EstaCargando && pagina > 0 && pagina <= TotalPaginas);
            ExportarClientesCommand = new RelayCommand(async (obj) => await ExportarClientesAsync(), (obj) => !EstaCargando && Clientes.Any());
            RefrescarCommand = new RelayCommand(async (obj) => await CargarClientesAsync(), (obj) => !EstaCargando);

            // Cargar clientes al inicializar
            _ = CargarClientesAsync();
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Carga todos los clientes con filtros y paginación
        /// </summary>
        public async Task CargarClientesAsync()
        {
            try
            {
                EstaCargando = true;
                HayError = false;
                MensajeEstado = "Cargando clientes...";

                var searchDto = new ClienteSearchDTO
                {
                    Nombre = string.IsNullOrWhiteSpace(FiltroNombre) ? null : FiltroNombre,
                    Email = string.IsNullOrWhiteSpace(FiltroEmail) ? null : FiltroEmail,
                    RFC = string.IsNullOrWhiteSpace(FiltroRFC) ? null : FiltroRFC,
                    EsActivo = FiltroEsActivo,
                    FechaRegistroDesde = FiltroFechaDesde,
                    FechaRegistroHasta = FiltroFechaHasta,
                    PageNumber = PaginaActual,
                    PageSize = TamañoPagina,
                    SortBy = Ordenamiento,
                    SortDescending = OrdenDescendente
                };

                var resultado = await _clienteCRUDService.SearchClientesAsync(searchDto);

                // Actualizar propiedades de paginación
                TotalClientes = resultado.TotalCount;
                TotalPaginas = resultado.TotalPages;
                TienePaginaAnterior = resultado.HasPreviousPage;
                TienePaginaSiguiente = resultado.HasNextPage;

                // Actualizar colección de clientes
                Clientes.Clear();
                foreach (var cliente in resultado.Clientes)
                {
                    Clientes.Add(cliente);
                }

                MensajeEstado = $"Se cargaron {Clientes.Count} clientes de {TotalClientes} total";
                _loggingService.LogInformation($"Clientes cargados: {Clientes.Count} de {TotalClientes} total");
            }
            catch (Exception ex)
            {
                HayError = true;
                MensajeError = $"Error al cargar clientes: {ex.Message}";
                _loggingService.LogError("Error al cargar clientes", ex);
            }
            finally
            {
                EstaCargando = false;
            }
        }

        /// <summary>
        /// Busca clientes con los filtros actuales
        /// </summary>
        public async Task BuscarClientesAsync()
        {
            if (EstaCargando) return;

            // Reset a la primera página al buscar
            if (PaginaActual != 1)
            {
                PaginaActual = 1;
                return; // La búsqueda se ejecutará automáticamente por el setter
            }

            await CargarClientesAsync();
        }

        #endregion

        #region Métodos de CRUD

        /// <summary>
        /// Inicia el modo de creación de cliente
        /// </summary>
        private void IniciarCreacion()
        {
            try
            {
                EstaEnModoCreacion = true;
                EstaEnModoEdicion = false;
                NuevoCliente = new CreateClienteDTO();
                ClienteEnEdicion = null;
                ResultadoValidacion = null;
                HayError = false;
                MensajeEstado = "Creando nuevo cliente...";

                _loggingService.LogInformation("Iniciando creación de nuevo cliente");
            }
            catch (Exception ex)
            {
                HayError = true;
                MensajeError = $"Error al iniciar creación: {ex.Message}";
                _loggingService.LogError("Error al iniciar creación de cliente", ex);
            }
        }

        /// <summary>
        /// Inicia el modo de edición de cliente
        /// </summary>
        private void IniciarEdicion()
        {
            try
            {
                if (ClienteSeleccionado == null) return;

                EstaEnModoEdicion = true;
                EstaEnModoCreacion = false;
                ClienteEnEdicion = new ClienteDisplayDTO
                {
                    Id = ClienteSeleccionado.Id,
                    Nombre = ClienteSeleccionado.Nombre,
                    RFC = ClienteSeleccionado.RFC,
                    Email = ClienteSeleccionado.Email,
                    Telefono = ClienteSeleccionado.Telefono,
                    Direccion = ClienteSeleccionado.Direccion,
                    EsActivo = ClienteSeleccionado.EsActivo,
                    FechaRegistro = ClienteSeleccionado.FechaRegistro,
                    TotalFacturas = ClienteSeleccionado.TotalFacturas,
                    TotalPedidos = ClienteSeleccionado.TotalPedidos,
                    TotalPresupuestos = ClienteSeleccionado.TotalPresupuestos
                };

                ClienteParaActualizar = new UpdateClienteDTO
                {
                    Id = ClienteSeleccionado.Id,
                    Nombre = ClienteSeleccionado.Nombre,
                    RFC = ClienteSeleccionado.RFC,
                    Email = ClienteSeleccionado.Email,
                    Telefono = ClienteSeleccionado.Telefono,
                    Direccion = ClienteSeleccionado.Direccion,
                    EsActivo = ClienteSeleccionado.EsActivo
                };

                NuevoCliente = new CreateClienteDTO();
                ResultadoValidacion = null;
                HayError = false;
                MensajeEstado = $"Editando cliente: {ClienteSeleccionado.Nombre}";

                _loggingService.LogInformation($"Iniciando edición de cliente: {ClienteSeleccionado.Nombre}");
            }
            catch (Exception ex)
            {
                HayError = true;
                MensajeError = $"Error al iniciar edición: {ex.Message}";
                _loggingService.LogError("Error al iniciar edición de cliente", ex);
            }
        }

        /// <summary>
        /// Guarda el cliente (crear o actualizar)
        /// </summary>
        private async Task GuardarClienteAsync()
        {
            try
            {
                EstaCargando = true;
                HayError = false;
                MensajeEstado = "Guardando cliente...";

                if (EstaEnModoCreacion)
                {
                    await CrearClienteAsync();
                }
                else if (EstaEnModoEdicion)
                {
                    await ActualizarClienteAsync();
                }
            }
            catch (Exception ex)
            {
                HayError = true;
                MensajeError = $"Error al guardar cliente: {ex.Message}";
                _loggingService.LogError("Error al guardar cliente", ex);
            }
            finally
            {
                EstaCargando = false;
            }
        }

        /// <summary>
        /// Crea un nuevo cliente
        /// </summary>
        private async Task CrearClienteAsync()
        {
            try
            {
                // Validar antes de crear
                ResultadoValidacion = await _clienteCRUDService.ValidateCreateClienteAsync(NuevoCliente);
                if (!ResultadoValidacion.IsValid)
                {
                    HayError = true;
                    MensajeError = "El cliente tiene errores de validación";
                    return;
                }

                var clienteCreado = await _clienteCRUDService.CreateClienteAsync(NuevoCliente);

                // Agregar a la colección
                Clientes.Insert(0, clienteCreado);
                TotalClientes++;

                // Limpiar formulario y salir del modo creación
                CancelarEdicion();
                MensajeEstado = $"Cliente '{clienteCreado.Nombre}' creado exitosamente";

                _loggingService.LogInformation($"Cliente creado exitosamente: {clienteCreado.Nombre}");
            }
            catch (Exception ex)
            {
                HayError = true;
                MensajeError = $"Error al crear cliente: {ex.Message}";
                _loggingService.LogError("Error al crear cliente", ex);
            }
        }

        /// <summary>
        /// Actualiza un cliente existente
        /// </summary>
        private async Task ActualizarClienteAsync()
        {
            try
            {
                if (ClienteParaActualizar == null) return;

                // Validar antes de actualizar
                ResultadoValidacion = await _clienteCRUDService.ValidateUpdateClienteAsync(ClienteParaActualizar);
                if (!ResultadoValidacion.IsValid)
                {
                    HayError = true;
                    MensajeError = "El cliente tiene errores de validación";
                    return;
                }

                var clienteActualizado = await _clienteCRUDService.UpdateClienteAsync(ClienteParaActualizar);

                // Actualizar en la colección
                var index = Clientes.IndexOf(Clientes.FirstOrDefault(c => c.Id == clienteActualizado.Id));
                if (index >= 0)
                {
                    Clientes[index] = clienteActualizado;
                }

                // Actualizar cliente seleccionado si es el mismo
                if (ClienteSeleccionado?.Id == clienteActualizado.Id)
                {
                    ClienteSeleccionado = clienteActualizado;
                }

                // Limpiar formulario y salir del modo edición
                CancelarEdicion();
                MensajeEstado = $"Cliente '{clienteActualizado.Nombre}' actualizado exitosamente";

                _loggingService.LogInformation($"Cliente actualizado exitosamente: {clienteActualizado.Nombre}");
            }
            catch (Exception ex)
            {
                HayError = true;
                MensajeError = $"Error al actualizar cliente: {ex.Message}";
                _loggingService.LogError("Error al actualizar cliente", ex);
            }
        }

        /// <summary>
        /// Elimina un cliente
        /// </summary>
        private async Task EliminarClienteAsync()
        {
            try
            {
                if (ClienteSeleccionado == null) return;

                EstaCargando = true;
                HayError = false;
                MensajeEstado = $"Eliminando cliente: {ClienteSeleccionado.Nombre}...";

                var resultado = await _clienteCRUDService.DeleteClienteAsync(ClienteSeleccionado.Id);

                if (resultado)
                {
                    // Remover de la colección
                    Clientes.Remove(ClienteSeleccionado);
                    TotalClientes--;

                    // Limpiar selección
                    ClienteSeleccionado = null;
                    MensajeEstado = "Cliente eliminado exitosamente";

                    _loggingService.LogInformation($"Cliente eliminado exitosamente: {ClienteSeleccionado?.Nombre}");
                }
                else
                {
                    HayError = true;
                    MensajeError = "No se pudo eliminar el cliente";
                }
            }
            catch (Exception ex)
            {
                HayError = true;
                MensajeError = $"Error al eliminar cliente: {ex.Message}";
                _loggingService.LogError("Error al eliminar cliente", ex);
            }
            finally
            {
                EstaCargando = false;
            }
        }

        /// <summary>
        /// Reactiva un cliente eliminado
        /// </summary>
        private async Task ReactivarClienteAsync()
        {
            try
            {
                if (ClienteSeleccionado == null) return;

                EstaCargando = true;
                HayError = false;
                MensajeEstado = $"Reactivando cliente: {ClienteSeleccionado.Nombre}...";

                var resultado = await _clienteCRUDService.ReactivateClienteAsync(ClienteSeleccionado.Id);

                if (resultado)
                {
                    // Actualizar estado en la colección
                    ClienteSeleccionado.EsActivo = true;
                    MensajeEstado = "Cliente reactivado exitosamente";

                    _loggingService.LogInformation($"Cliente reactivado exitosamente: {ClienteSeleccionado.Nombre}");
                }
                else
                {
                    HayError = true;
                    MensajeError = "No se pudo reactivar el cliente";
                }
            }
            catch (Exception ex)
            {
                HayError = true;
                MensajeError = $"Error al reactivar cliente: {ex.Message}";
                _loggingService.LogError("Error al reactivar cliente", ex);
            }
            finally
            {
                EstaCargando = false;
            }
        }

        #endregion

        #region Métodos de Utilidad

        /// <summary>
        /// Cancela la edición o creación
        /// </summary>
        private void CancelarEdicion()
        {
            EstaEnModoEdicion = false;
            EstaEnModoCreacion = false;
            ClienteEnEdicion = null;
            ClienteParaActualizar = new UpdateClienteDTO();
            NuevoCliente = new CreateClienteDTO();
            ResultadoValidacion = null;
            HayError = false;
            MensajeEstado = string.Empty;

            _loggingService.LogInformation("Edición/creación de cliente cancelada");
        }

        /// <summary>
        /// Limpia todos los filtros
        /// </summary>
        private void LimpiarFiltros()
        {
            FiltroNombre = string.Empty;
            FiltroEmail = string.Empty;
            FiltroRFC = string.Empty;
            FiltroEsActivo = null;
            FiltroFechaDesde = null;
            FiltroFechaHasta = null;
            Ordenamiento = "Nombre";
            OrdenDescendente = false;
            PaginaActual = 1;

            _loggingService.LogInformation("Filtros de búsqueda limpiados");
        }

        /// <summary>
        /// Exporta los clientes a un formato específico
        /// </summary>
        private async Task ExportarClientesAsync()
        {
            try
            {
                EstaCargando = true;
                HayError = false;
                MensajeEstado = "Exportando clientes...";

                // TODO: Implementar exportación a Excel/CSV
                await Task.Delay(1000); // Simulación

                MensajeEstado = "Clientes exportados exitosamente";
                _loggingService.LogInformation("Exportación de clientes completada");
            }
            catch (Exception ex)
            {
                HayError = true;
                MensajeError = $"Error al exportar clientes: {ex.Message}";
                _loggingService.LogError("Error al exportar clientes", ex);
            }
            finally
            {
                EstaCargando = false;
            }
        }

        /// <summary>
        /// Se ejecuta cuando cambia el cliente seleccionado
        /// </summary>
        private void OnClienteSeleccionadoChanged()
        {
            // Limpiar formularios cuando se cambia la selección
            if (!EstaEnModoEdicion && !EstaEnModoCreacion)
            {
                CancelarEdicion();
            }
        }

        #endregion
    }
}
