using System;
using System.Threading.Tasks;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Interfaz para el patrón Unit of Work que coordina múltiples repositorios
    /// y gestiona transacciones de base de datos
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Obtiene el repositorio genérico para una entidad específica
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <returns>Repositorio genérico</returns>
        IRepository<T> GetRepository<T>() where T : class;

        /// <summary>
        /// Obtiene el repositorio con ID para una entidad específica
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <typeparam name="TId">Tipo del ID de la entidad</typeparam>
        /// <returns>Repositorio con ID</returns>
        IRepositoryWithId<T, TId> GetRepositoryWithId<T, TId>() where T : class;

        /// <summary>
        /// Obtiene el repositorio con auditoría para una entidad específica
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <returns>Repositorio con auditoría</returns>
        IRepositoryWithAudit<T> GetRepositoryWithAudit<T>() where T : class;

        /// <summary>
        /// Obtiene el repositorio con eliminación suave para una entidad específica
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <returns>Repositorio con eliminación suave</returns>
        IRepositoryWithSoftDelete<T> GetRepositoryWithSoftDelete<T>() where T : class;

        /// <summary>
        /// Guarda todos los cambios pendientes en la base de datos
        /// </summary>
        /// <returns>Número de entidades afectadas</returns>
        int SaveChanges();

        /// <summary>
        /// Guarda todos los cambios pendientes en la base de datos de forma asíncrona
        /// </summary>
        /// <returns>Número de entidades afectadas</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Inicia una nueva transacción de base de datos
        /// </summary>
        /// <returns>Transacción de base de datos</returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// Inicia una nueva transacción de base de datos de forma asíncrona
        /// </summary>
        /// <returns>Transacción de base de datos</returns>
        Task<IDbTransaction> BeginTransactionAsync();

        /// <summary>
        /// Confirma la transacción actual
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Revierte la transacción actual
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Ejecuta una operación dentro de una transacción
        /// </summary>
        /// <param name="action">Acción a ejecutar</param>
        /// <returns>Resultado de la operación</returns>
        T ExecuteInTransaction<T>(Func<T> action);

        /// <summary>
        /// Ejecuta una operación dentro de una transacción de forma asíncrona
        /// </summary>
        /// <param name="action">Acción asíncrona a ejecutar</param>
        /// <returns>Resultado de la operación</returns>
        Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action);

        /// <summary>
        /// Ejecuta una acción dentro de una transacción
        /// </summary>
        /// <param name="action">Acción a ejecutar</param>
        void ExecuteInTransaction(Action action);

        /// <summary>
        /// Ejecuta una acción dentro de una transacción de forma asíncrona
        /// </summary>
        /// <param name="action">Acción asíncrona a ejecutar</param>
        /// <returns>Tarea completada</returns>
        Task ExecuteInTransactionAsync(Func<Task> action);
    }

    /// <summary>
    /// Interfaz para transacciones de base de datos
    /// </summary>
    public interface IDbTransaction : IDisposable
    {
        /// <summary>
        /// Confirma la transacción
        /// </summary>
        void Commit();

        /// <summary>
        /// Revierte la transacción
        /// </summary>
        void Rollback();
    }
}
