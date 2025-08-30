using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Interfaz para repositorios que manejan entidades con soft delete (eliminación lógica)
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que maneja el repositorio</typeparam>
    public interface IRepositoryWithSoftDelete<T> : IRepository<T> where T : class
    {
        #region Operaciones de Soft Delete

        /// <summary>
        /// Realiza soft delete de una entidad (marca como eliminada sin borrarla físicamente)
        /// </summary>
        /// <param name="entity">Entidad a marcar como eliminada</param>
        void SoftDelete(T entity);

        /// <summary>
        /// Realiza soft delete de una entidad de forma asíncrona
        /// </summary>
        /// <param name="entity">Entidad a marcar como eliminada</param>
        Task SoftDeleteAsync(T entity);

        /// <summary>
        /// Realiza soft delete de múltiples entidades
        /// </summary>
        /// <param name="entities">Colección de entidades a marcar como eliminadas</param>
        void SoftDeleteRange(IEnumerable<T> entities);

        /// <summary>
        /// Realiza soft delete de múltiples entidades de forma asíncrona
        /// </summary>
        /// <param name="entities">Colección de entidades a marcar como eliminadas</param>
        Task SoftDeleteRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Realiza soft delete de entidades que cumplen con un predicado
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades a marcar como eliminadas</param>
        void SoftDeleteWhere(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Realiza soft delete de entidades que cumplen con un predicado de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades a marcar como eliminadas</param>
        Task SoftDeleteWhereAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Restaura una entidad marcada como eliminada
        /// </summary>
        /// <param name="entity">Entidad a restaurar</param>
        void Restore(T entity);

        /// <summary>
        /// Restaura una entidad marcada como eliminada de forma asíncrona
        /// </summary>
        /// <param name="entity">Entidad a restaurar</param>
        Task RestoreAsync(T entity);

        /// <summary>
        /// Restaura múltiples entidades marcadas como eliminadas
        /// </summary>
        /// <param name="entities">Colección de entidades a restaurar</param>
        void RestoreRange(IEnumerable<T> entities);

        /// <summary>
        /// Restaura múltiples entidades marcadas como eliminadas de forma asíncrona
        /// </summary>
        /// <param name="entities">Colección de entidades a restaurar</param>
        Task RestoreRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Restaura entidades que cumplen con un predicado
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades a restaurar</param>
        void RestoreWhere(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Restaura entidades que cumplen con un predicado de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades a restaurar</param>
        Task RestoreWhereAsync(Expression<Func<T, bool>> predicate);

        #endregion

        #region Consultas con Soft Delete

        /// <summary>
        /// Obtiene solo entidades activas (no eliminadas)
        /// </summary>
        /// <returns>Colección de entidades activas</returns>
        IEnumerable<T> GetActive();

        /// <summary>
        /// Obtiene solo entidades activas de forma asíncrona
        /// </summary>
        /// <returns>Colección de entidades activas</returns>
        Task<IEnumerable<T>> GetActiveAsync();

        /// <summary>
        /// Obtiene solo entidades eliminadas (soft deleted)
        /// </summary>
        /// <returns>Colección de entidades eliminadas</returns>
        IEnumerable<T> GetDeleted();

        /// <summary>
        /// Obtiene solo entidades eliminadas de forma asíncrona
        /// </summary>
        /// <returns>Colección de entidades eliminadas</returns>
        Task<IEnumerable<T>> GetDeletedAsync();

        /// <summary>
        /// Obtiene entidades activas que cumplen con un predicado
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades activas</param>
        /// <returns>Colección de entidades activas que cumplen el predicado</returns>
        IEnumerable<T> GetActive(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene entidades activas que cumplen con un predicado de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades activas</param>
        /// <returns>Colección de entidades activas que cumplen el predicado</returns>
        Task<IEnumerable<T>> GetActiveAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene entidades eliminadas que cumplen con un predicado
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades eliminadas</param>
        /// <returns>Colección de entidades eliminadas que cumplen el predicado</returns>
        IEnumerable<T> GetDeleted(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene entidades eliminadas que cumplen con un predicado de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades eliminadas</param>
        /// <returns>Colección de entidades eliminadas que cumplen el predicado</returns>
        Task<IEnumerable<T>> GetDeletedAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene entidades activas con paginación
        /// </summary>
        /// <param name="pageNumber">Número de página (base 1)</param>
        /// <param name="pageSize">Tamaño de la página</param>
        /// <returns>Colección paginada de entidades activas</returns>
        IEnumerable<T> GetActivePaged(int pageNumber, int pageSize);

        /// <summary>
        /// Obtiene entidades activas con paginación de forma asíncrona
        /// </summary>
        /// <param name="pageNumber">Número de página (base 1)</param>
        /// <param name="pageSize">Tamaño de la página</param>
        /// <returns>Colección paginada de entidades activas</returns>
        Task<IEnumerable<T>> GetActivePagedAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Obtiene entidades eliminadas con paginación
        /// </summary>
        /// <param name="pageNumber">Número de página (base 1)</param>
        /// <param name="pageSize">Tamaño de la página</param>
        /// <returns>Colección paginada de entidades eliminadas</returns>
        IEnumerable<T> GetDeletedPaged(int pageNumber, int pageSize);

        /// <summary>
        /// Obtiene entidades eliminadas con paginación de forma asíncrona
        /// </summary>
        /// <param name="pageNumber">Número de página (base 1)</param>
        /// <param name="pageSize">Tamaño de la página</param>
        /// <returns>Colección paginada de entidades eliminadas</returns>
        Task<IEnumerable<T>> GetDeletedPagedAsync(int pageNumber, int pageSize);

        #endregion

        #region Conteo con Soft Delete

        /// <summary>
        /// Cuenta entidades activas
        /// </summary>
        /// <returns>Número de entidades activas</returns>
        int CountActive();

        /// <summary>
        /// Cuenta entidades activas de forma asíncrona
        /// </summary>
        /// <returns>Número de entidades activas</returns>
        Task<int> CountActiveAsync();

        /// <summary>
        /// Cuenta entidades eliminadas
        /// </summary>
        /// <returns>Número de entidades eliminadas</returns>
        int CountDeleted();

        /// <summary>
        /// Cuenta entidades eliminadas de forma asíncrona
        /// </summary>
        /// <returns>Número de entidades eliminadas</returns>
        Task<int> CountDeletedAsync();

        /// <summary>
        /// Cuenta entidades activas que cumplen con un predicado
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades activas</param>
        /// <returns>Número de entidades activas que cumplen el predicado</returns>
        int CountActive(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Cuenta entidades activas que cumplen con un predicado de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades activas</param>
        /// <returns>Número de entidades activas que cumplen el predicado</returns>
        Task<int> CountActiveAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Cuenta entidades eliminadas que cumplen con un predicado
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades eliminadas</param>
        /// <returns>Número de entidades eliminadas que cumplen el predicado</returns>
        int CountDeleted(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Cuenta entidades eliminadas que cumplen con un predicado de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades eliminadas</param>
        /// <returns>Número de entidades eliminadas que cumplen el predicado</returns>
        Task<int> CountDeletedAsync(Expression<Func<T, bool>> predicate);

        #endregion

        #region Verificación con Soft Delete

        /// <summary>
        /// Verifica si existe alguna entidad activa
        /// </summary>
        /// <returns>True si existe al menos una entidad activa, false en caso contrario</returns>
        bool AnyActive();

        /// <summary>
        /// Verifica si existe alguna entidad activa de forma asíncrona
        /// </summary>
        /// <returns>True si existe al menos una entidad activa, false en caso contrario</returns>
        Task<bool> AnyActiveAsync();

        /// <summary>
        /// Verifica si existe alguna entidad eliminada
        /// </summary>
        /// <returns>True si existe al menos una entidad eliminada, false en caso contrario</returns>
        bool AnyDeleted();

        /// <summary>
        /// Verifica si existe alguna entidad eliminada de forma asíncrona
        /// </summary>
        /// <returns>True si existe al menos una entidad eliminada, false en caso contrario</returns>
        Task<bool> AnyDeletedAsync();

        /// <summary>
        /// Verifica si existe alguna entidad activa que cumpla con un predicado
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades activas</param>
        /// <returns>True si existe al menos una entidad activa que cumpla el predicado, false en caso contrario</returns>
        bool AnyActive(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Verifica si existe alguna entidad activa que cumpla con un predicado de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades activas</param>
        /// <returns>True si existe al menos una entidad activa que cumpla el predicado, false en caso contrario</returns>
        Task<bool> AnyActiveAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Verifica si existe alguna entidad eliminada que cumpla con un predicado
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades eliminadas</param>
        /// <returns>True si existe al menos una entidad eliminada que cumpla el predicado, false en caso contrario</returns>
        bool AnyDeleted(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Verifica si existe alguna entidad eliminada que cumpla con un predicado de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades eliminadas</param>
        /// <returns>True si existe al menos una entidad eliminada que cumpla el predicado, false en caso contrario</returns>
        Task<bool> AnyDeletedAsync(Expression<Func<T, bool>> predicate);

        #endregion

        #region Operaciones de Limpieza

        /// <summary>
        /// Elimina permanentemente entidades marcadas como eliminadas (hard delete)
        /// </summary>
        /// <param name="entities">Colección de entidades a eliminar permanentemente</param>
        void PermanentlyDelete(IEnumerable<T> entities);

        /// <summary>
        /// Elimina permanentemente entidades marcadas como eliminadas de forma asíncrona
        /// </summary>
        /// <param name="entities">Colección de entidades a eliminar permanentemente</param>
        Task PermanentlyDeleteAsync(IEnumerable<T> entities);

        /// <summary>
        /// Elimina permanentemente entidades marcadas como eliminadas que cumplen con un predicado
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades a eliminar permanentemente</param>
        void PermanentlyDeleteWhere(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Elimina permanentemente entidades marcadas como eliminadas que cumplen con un predicado de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades a eliminar permanentemente</param>
        Task PermanentlyDeleteWhereAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Limpia entidades eliminadas más antiguas que una fecha específica
        /// </summary>
        /// <param name="cutoffDate">Fecha límite para la limpieza</param>
        /// <returns>Número de entidades eliminadas permanentemente</returns>
        int CleanupDeletedOlderThan(DateTime cutoffDate);

        /// <summary>
        /// Limpia entidades eliminadas más antiguas que una fecha específica de forma asíncrona
        /// </summary>
        /// <param name="cutoffDate">Fecha límite para la limpieza</param>
        /// <returns>Número de entidades eliminadas permanentemente</returns>
        Task<int> CleanupDeletedOlderThanAsync(DateTime cutoffDate);

        #endregion

        #region Operaciones de Auditoría de Soft Delete

        /// <summary>
        /// Obtiene entidades eliminadas en una fecha específica
        /// </summary>
        /// <param name="date">Fecha de eliminación</param>
        /// <returns>Colección de entidades eliminadas en la fecha especificada</returns>
        IEnumerable<T> GetDeletedOnDate(DateTime date);

        /// <summary>
        /// Obtiene entidades eliminadas en una fecha específica de forma asíncrona
        /// </summary>
        /// <param name="date">Fecha de eliminación</param>
        /// <returns>Colección de entidades eliminadas en la fecha especificada</returns>
        Task<IEnumerable<T>> GetDeletedOnDateAsync(DateTime date);

        /// <summary>
        /// Obtiene entidades restauradas en una fecha específica
        /// </summary>
        /// <param name="date">Fecha de restauración</param>
        /// <returns>Colección de entidades restauradas en la fecha especificada</returns>
        IEnumerable<T> GetRestoredOnDate(DateTime date);

        /// <summary>
        /// Obtiene entidades restauradas en una fecha específica de forma asíncrona
        /// </summary>
        /// <param name="date">Fecha de restauración</param>
        /// <returns>Colección de entidades restauradas en la fecha especificada</returns>
        Task<IEnumerable<T>> GetRestoredOnDateAsync(DateTime date);

        /// <summary>
        /// Obtiene entidades eliminadas por un usuario específico
        /// </summary>
        /// <param name="deletedBy">Usuario que eliminó las entidades</param>
        /// <returns>Colección de entidades eliminadas por el usuario especificado</returns>
        IEnumerable<T> GetDeletedBy(string deletedBy);

        /// <summary>
        /// Obtiene entidades eliminadas por un usuario específico de forma asíncrona
        /// </summary>
        /// <param name="deletedBy">Usuario que eliminó las entidades</param>
        /// <returns>Colección de entidades eliminadas por el usuario especificado</returns>
        Task<IEnumerable<T>> GetDeletedByAsync(string deletedBy);

        /// <summary>
        /// Obtiene entidades restauradas por un usuario específico
        /// </summary>
        /// <param name="restoredBy">Usuario que restauró las entidades</param>
        /// <returns>Colección de entidades restauradas por el usuario especificado</returns>
        IEnumerable<T> GetRestoredBy(string restoredBy);

        /// <summary>
        /// Obtiene entidades restauradas por un usuario específico de forma asíncrona
        /// </summary>
        /// <param name="restoredBy">Usuario que restauró las entidades</param>
        /// <returns>Colección de entidades restauradas por el usuario especificado</returns>
        Task<IEnumerable<T>> GetRestoredByAsync(string restoredBy);

        #endregion
    }
}
