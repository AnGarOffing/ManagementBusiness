using System;
using System.Threading.Tasks;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Interfaz para el servicio de base de datos
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// Verifica la conexión a la base de datos
        /// </summary>
        /// <returns>True si la conexión es exitosa, false en caso contrario</returns>
        Task<bool> TestConnectionAsync();

        /// <summary>
        /// Verifica si la base de datos existe
        /// </summary>
        /// <returns>True si la base de datos existe, false en caso contrario</returns>
        Task<bool> DatabaseExistsAsync();

        /// <summary>
        /// Ejecuta las migraciones pendientes
        /// </summary>
        /// <returns>True si las migraciones se ejecutaron correctamente</returns>
        Task<bool> ApplyMigrationsAsync();

        /// <summary>
        /// Obtiene información del estado de la base de datos
        /// </summary>
        /// <returns>Información del estado de la base de datos</returns>
        Task<string> GetDatabaseStatusAsync();

        /// <summary>
        /// Fuerza la aplicación de migraciones (para desarrollo)
        /// </summary>
        /// <returns>True si las migraciones se ejecutaron correctamente</returns>
        Task<bool> ForceApplyMigrationsAsync();

        /// <summary>
        /// Sincroniza EF con una base de datos existente (para desarrollo)
        /// </summary>
        /// <returns>True si la sincronización fue exitosa</returns>
        Task<bool> SyncWithExistingDatabaseAsync();
    }
}
