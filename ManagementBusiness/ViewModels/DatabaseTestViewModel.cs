using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using ManagementBusiness.Services;

namespace ManagementBusiness.ViewModels
{
    /// <summary>
    /// ViewModel para probar la conexión a la base de datos
    /// </summary>
    public class DatabaseTestViewModel : BaseViewModel
    {
        private readonly IDatabaseService _databaseService;
        private string _connectionStatus = "No verificado";
        private bool _isConnected = false;
        private bool _isTesting = false;

        public DatabaseTestViewModel()
        {
            try
            {
                _databaseService = ServiceContainer.GetRequiredService<IDatabaseService>();
            }
            catch (Exception ex)
            {
                ConnectionStatus = $"Error al obtener servicio: {ex.Message}";
            }

            TestConnectionCommand = new RelayCommand(async (obj) => await TestConnectionAsync(), (obj) => !IsTesting);
        }

        #region Propiedades

        public string ConnectionStatus
        {
            get => _connectionStatus;
            set => SetProperty(ref _connectionStatus, value);
        }

        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }

        public bool IsTesting
        {
            get => _isTesting;
            set => SetProperty(ref _isTesting, value);
        }

        #endregion

        #region Comandos

        public ICommand TestConnectionCommand { get; }

        #endregion

        #region Métodos

        /// <summary>
        /// Prueba la conexión a la base de datos
        /// </summary>
        private async Task TestConnectionAsync()
        {
            if (_databaseService == null)
            {
                ConnectionStatus = "❌ Servicio de base de datos no disponible";
                return;
            }

            IsTesting = true;
            ConnectionStatus = "🔄 Probando conexión...";

            try
            {
                // Verificar conexión
                var canConnect = await _databaseService.TestConnectionAsync();
                if (!canConnect)
                {
                    ConnectionStatus = "❌ No se puede conectar a la base de datos";
                    IsConnected = false;
                    return;
                }

                // Verificar si la base de datos existe
                var databaseExists = await _databaseService.DatabaseExistsAsync();
                if (!databaseExists)
                {
                    ConnectionStatus = "❌ La base de datos no existe";
                    IsConnected = false;
                    return;
                }

                // Obtener estado completo
                var status = await _databaseService.GetDatabaseStatusAsync();
                ConnectionStatus = status;
                IsConnected = true;
            }
            catch (Exception ex)
            {
                ConnectionStatus = $"❌ Error: {ex.Message}";
                IsConnected = false;
            }
            finally
            {
                IsTesting = false;
            }
        }

        #endregion
    }
}
