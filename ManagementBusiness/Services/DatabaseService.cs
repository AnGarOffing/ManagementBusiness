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
                
                // Verificar si hay migraciones pendientes
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                if (!pendingMigrations.Any())
                {
                    // No hay migraciones pendientes, la base de datos está actualizada
                    return true;
                }
                
                // Aplicar migraciones pendientes
                await context.Database.MigrateAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log del error para debugging
                System.Diagnostics.Debug.WriteLine($"Error al aplicar migraciones: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// Fuerza la aplicación de migraciones (para desarrollo)
        /// </summary>
        public async Task<bool> ForceApplyMigrationsAsync()
        {
            try
            {
                using var context = CreateContext();
                
                // Verificar si la base de datos existe, si no, crearla
                if (!await context.Database.CanConnectAsync())
                {
                    await context.Database.EnsureCreatedAsync();
                    return true;
                }
                
                // Aplicar migraciones pendientes
                await context.Database.MigrateAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al forzar migraciones: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// Sincroniza EF con una base de datos existente (para desarrollo)
        /// </summary>
        public async Task<bool> SyncWithExistingDatabaseAsync()
        {
            try
            {
                using var context = CreateContext();
                
                // Verificar si la base de datos existe y tiene tablas
                if (!await context.Database.CanConnectAsync())
                {
                    return false;
                }
                
                // Verificar si ya hay migraciones aplicadas
                var appliedMigrations = await context.Database.GetAppliedMigrationsAsync();
                if (appliedMigrations.Any())
                {
                    // Ya hay migraciones aplicadas, no hacer nada
                    return true;
                }
                
                // Marcar la migración inicial como aplicada (ya que las tablas existen)
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Contains("20250827001135_InitialCreate"))
                {
                    // Crear la tabla __EFMigrationsHistory manualmente
                    var sql = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '__EFMigrationsHistory')
                        BEGIN
                            CREATE TABLE [__EFMigrationsHistory] (
                                [MigrationId] nvarchar(150) NOT NULL,
                                [ProductVersion] nvarchar(32) NOT NULL,
                                CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
                            );
                        END
                        
                        IF NOT EXISTS (SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = '20250827001135_InitialCreate')
                        BEGIN
                            INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion]) 
                            VALUES ('20250827001135_InitialCreate', '9.0.8');
                        END";
                    
                    await context.Database.ExecuteSqlRawAsync(sql);
                    return true;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al sincronizar con BD existente: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
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
                    
                    var status = $"✅ Base de datos conectada correctamente\n" +
                               $"📊 Migraciones aplicadas: {appliedMigrations.Count()}\n" +
                               $"⏳ Migraciones pendientes: {pendingMigrations.Count()}";
                    
                    // Agregar información detallada de migraciones si hay problemas
                    if (pendingMigrations.Any())
                    {
                        status += $"\n\n📋 Migraciones pendientes:\n";
                        foreach (var migration in pendingMigrations)
                        {
                            status += $"   • {migration}\n";
                        }
                    }
                    
                    return status;
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
        /// Verifica si las migraciones están sincronizadas
        /// </summary>
        public async Task<bool> AreMigrationsInSyncAsync()
        {
            try
            {
                using var context = CreateContext();
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                return !pendingMigrations.Any();
            }
            catch (Exception)
            {
                return false;
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
