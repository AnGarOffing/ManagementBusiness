using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ManagementBusiness.Data;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Implementación del servicio de base de datos
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'");
        }

        /// <summary>
        /// Verifica la conexión a la base de datos
        /// </summary>
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                using var context = CreateContext();
                await context.Database.CanConnectAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica si la base de datos existe
        /// </summary>
        public async Task<bool> DatabaseExistsAsync()
        {
            try
            {
                using var context = CreateContext();
                return await context.Database.CanConnectAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Ejecuta las migraciones pendientes
        /// </summary>
        public async Task<bool> ApplyMigrationsAsync()
        {
            try
            {
                using var context = CreateContext();
                await context.Database.MigrateAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene información del estado de la base de datos
        /// </summary>
        public async Task<string> GetDatabaseStatusAsync()
        {
            try
            {
                using var context = CreateContext();
                
                if (await context.Database.CanConnectAsync())
                {
                    var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                    var appliedMigrations = await context.Database.GetAppliedMigrationsAsync();
                    
                    return $"✅ Base de datos conectada correctamente\n" +
                           $"📊 Migraciones aplicadas: {appliedMigrations.Count()}\n" +
                           $"⏳ Migraciones pendientes: {pendingMigrations.Count()}";
                }
                else
                {
                    return "❌ No se puede conectar a la base de datos";
                }
            }
            catch (Exception ex)
            {
                return $"❌ Error al verificar estado: {ex.Message}";
            }
        }

        /// <summary>
        /// Crea un contexto de base de datos
        /// </summary>
        private ManagementBusinessContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ManagementBusinessContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            return new ManagementBusinessContext(optionsBuilder.Options);
        }
    }
}
