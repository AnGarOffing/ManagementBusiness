using System;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using ManagementBusiness.Services;

namespace ManagementBusiness
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            try
            {
                // Configurar servicios de inyección de dependencias
                ServiceContainer.ConfigureServices();

                // Verificar conexión a la base de datos
                await VerifyDatabaseConnectionAsync();

                // Continuar con el startup normal
                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar la aplicación: {ex.Message}", 
                    "Error de Inicialización", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                
                // Cerrar la aplicación si hay un error crítico
                Shutdown(1);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Liberar recursos del contenedor de servicios
            ServiceContainer.Dispose();
            base.OnExit(e);
        }

        /// <summary>
        /// Verifica la conexión a la base de datos
        /// </summary>
        private async Task VerifyDatabaseConnectionAsync()
        {
            try
            {
                var databaseService = ServiceContainer.GetRequiredService<IDatabaseService>();

                // Verificar conexión
                var canConnect = await databaseService.TestConnectionAsync();
                if (!canConnect)
                {
                    throw new InvalidOperationException("No se puede conectar a la base de datos. Verifica la cadena de conexión.");
                }

                // Verificar si la base de datos existe
                var databaseExists = await databaseService.DatabaseExistsAsync();
                if (!databaseExists)
                {
                    throw new InvalidOperationException("La base de datos no existe. Verifica que se haya creado correctamente.");
                }

                // Aplicar migraciones pendientes
                var migrationsApplied = await databaseService.ApplyMigrationsAsync();
                if (!migrationsApplied)
                {
                    throw new InvalidOperationException("Error al aplicar las migraciones de Entity Framework.");
                }

                // Obtener estado de la base de datos
                var status = await databaseService.GetDatabaseStatusAsync();
                
                // Mostrar información del estado (opcional, solo en desarrollo)
                #if DEBUG
                MessageBox.Show($"Estado de la base de datos:\n{status}", 
                    "Conexión Exitosa", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
                #endif
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al verificar la base de datos: {ex.Message}", ex);
            }
        }
    }
}
