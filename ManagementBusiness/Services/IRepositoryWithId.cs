using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Interfaz especializada para repositorios que manejan entidades con ID específico
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que maneja el repositorio</typeparam>
    /// <typeparam name="TId">Tipo del ID de la entidad</typeparam>
    public interface IRepositoryWithId<T, TId> : IRepository<T> where T : class
    {
        #region Operaciones Específicas con ID

        /// <summary>
        /// Obtiene una entidad por su ID tipado
        /// </summary>
        /// <param name="id">ID tipado de la entidad</param>
        /// <returns>La entidad encontrada o null si no existe</returns>
        T GetById(TId id);

        /// <summary>
        /// Obtiene una entidad por su ID tipado de forma asíncrona
        /// </summary>
        /// <param name="id">ID tipado de la entidad</param>
        /// <returns>La entidad encontrada o null si no existe</returns>
        Task<T> GetByIdAsync(TId id);

        /// <summary>
        /// Obtiene múltiples entidades por sus IDs
        /// </summary>
        /// <param name="ids">Colección de IDs de las entidades</param>
        /// <returns>Colección de entidades encontradas</returns>
        IEnumerable<T> GetByIds(IEnumerable<TId> ids);

        /// <summary>
        /// Obtiene múltiples entidades por sus IDs de forma asíncrona
        /// </summary>
        /// <param name="ids">Colección de IDs de las entidades</param>
        /// <returns>Colección de entidades encontradas</returns>
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<TId> ids);

        /// <summary>
        /// Verifica si existe una entidad con el ID especificado
        /// </summary>
        /// <param name="id">ID de la entidad a verificar</param>
        /// <returns>True si existe la entidad, false en caso contrario</returns>
        bool Exists(TId id);

        /// <summary>
        /// Verifica si existe una entidad con el ID especificado de forma asíncrona
        /// </summary>
        /// <param name="id">ID de la entidad a verificar</param>
        /// <returns>True si existe la entidad, false en caso contrario</returns>
        Task<bool> ExistsAsync(TId id);

        /// <summary>
        /// Verifica si existen entidades con los IDs especificados
        /// </summary>
        /// <param name="ids">Colección de IDs a verificar</param>
        /// <returns>True si existen todas las entidades, false en caso contrario</returns>
        bool ExistsAll(IEnumerable<TId> ids);

        /// <summary>
        /// Verifica si existen entidades con los IDs especificados de forma asíncrona
        /// </summary>
        /// <param name="ids">Colección de IDs a verificar</param>
        /// <returns>True si existen todas las entidades, false en caso contrario</returns>
        Task<bool> ExistsAllAsync(IEnumerable<TId> ids);

        /// <summary>
        /// Verifica si existe al menos una entidad con los IDs especificados
        /// </summary>
        /// <param name="ids">Colección de IDs a verificar</param>
        /// <returns>True si existe al menos una entidad, false en caso contrario</returns>
        bool ExistsAny(IEnumerable<TId> ids);

        /// <summary>
        /// Verifica si existe al menos una entidad con los IDs especificados de forma asíncrona
        /// </summary>
        /// <param name="ids">Colección de IDs a verificar</param>
        /// <returns>True si existe al menos una entidad, false en caso contrario</returns>
        Task<bool> ExistsAnyAsync(IEnumerable<TId> ids);

        /// <summary>
        /// Elimina una entidad por su ID
        /// </summary>
        /// <param name="id">ID de la entidad a eliminar</param>
        /// <returns>True si se eliminó correctamente, false si no se encontró</returns>
        bool RemoveById(TId id);

        /// <summary>
        /// Elimina una entidad por su ID de forma asíncrona
        /// </summary>
        /// <param name="id">ID de la entidad a eliminar</param>
        /// <returns>True si se eliminó correctamente, false si no se encontró</returns>
        Task<bool> RemoveByIdAsync(TId id);

        /// <summary>
        /// Elimina múltiples entidades por sus IDs
        /// </summary>
        /// <param name="ids">Colección de IDs de las entidades a eliminar</param>
        /// <returns>Número de entidades eliminadas</returns>
        int RemoveByIds(IEnumerable<TId> ids);

        /// <summary>
        /// Elimina múltiples entidades por sus IDs de forma asíncrona
        /// </summary>
        /// <param name="ids">Colección de IDs de las entidades a eliminar</param>
        /// <returns>Número de entidades eliminadas</returns>
        Task<int> RemoveByIdsAsync(IEnumerable<TId> ids);

        /// <summary>
        /// Obtiene el siguiente ID disponible (útil para entidades con ID autoincremental)
        /// </summary>
        /// <returns>El siguiente ID disponible</returns>
        TId GetNextId();

        /// <summary>
        /// Obtiene el siguiente ID disponible de forma asíncrona
        /// </summary>
        /// <returns>El siguiente ID disponible</returns>
        Task<TId> GetNextIdAsync();

        /// <summary>
        /// Obtiene el ID máximo actual
        /// </summary>
        /// <returns>El ID máximo actual</returns>
        TId GetMaxId();

        /// <summary>
        /// Obtiene el ID máximo actual de forma asíncrona
        /// </summary>
        /// <returns>El ID máximo actual</returns>
        Task<TId> GetMaxIdAsync();

        #endregion

        #region Operaciones de Búsqueda por ID

        /// <summary>
        /// Obtiene entidades cuyo ID está en un rango específico
        /// </summary>
        /// <param name="startId">ID de inicio del rango (inclusive)</param>
        /// <param name="endId">ID de fin del rango (inclusive)</param>
        /// <returns>Colección de entidades en el rango de IDs</returns>
        IEnumerable<T> GetByIdRange(TId startId, TId endId);

        /// <summary>
        /// Obtiene entidades cuyo ID está en un rango específico de forma asíncrona
        /// </summary>
        /// <param name="startId">ID de inicio del rango (inclusive)</param>
        /// <param name="endId">ID de fin del rango (inclusive)</param>
        /// <returns>Colección de entidades en el rango de IDs</returns>
        Task<IEnumerable<T>> GetByIdRangeAsync(TId startId, TId endId);

        /// <summary>
        /// Obtiene entidades cuyo ID es mayor que el valor especificado
        /// </summary>
        /// <param name="id">ID de referencia</param>
        /// <returns>Colección de entidades con ID mayor al especificado</returns>
        IEnumerable<T> GetByIdGreaterThan(TId id);

        /// <summary>
        /// Obtiene entidades cuyo ID es mayor que el valor especificado de forma asíncrona
        /// </summary>
        /// <param name="id">ID de referencia</param>
        /// <returns>Colección de entidades con ID mayor al especificado</returns>
        Task<IEnumerable<T>> GetByIdGreaterThanAsync(TId id);

        /// <summary>
        /// Obtiene entidades cuyo ID es menor que el valor especificado
        /// </summary>
        /// <param name="id">ID de referencia</param>
        /// <returns>Colección de entidades con ID menor al especificado</returns>
        IEnumerable<T> GetByIdLessThan(TId id);

        /// <summary>
        /// Obtiene entidades cuyo ID es menor que el valor especificado de forma asíncrona
        /// </summary>
        /// <param name="id">ID de referencia</param>
        /// <returns>Colección de entidades con ID menor al especificado</returns>
        Task<IEnumerable<T>> GetByIdLessThanAsync(TId id);

        #endregion

        #region Operaciones de Conteo por ID

        /// <summary>
        /// Cuenta entidades cuyo ID está en un rango específico
        /// </summary>
        /// <param name="startId">ID de inicio del rango (inclusive)</param>
        /// <param name="endId">ID de fin del rango (inclusive)</param>
        /// <returns>Número de entidades en el rango de IDs</returns>
        int CountByIdRange(TId startId, TId endId);

        /// <summary>
        /// Cuenta entidades cuyo ID está en un rango específico de forma asíncrona
        /// </summary>
        /// <param name="startId">ID de inicio del rango (inclusive)</param>
        /// <param name="endId">ID de fin del rango (inclusive)</param>
        /// <returns>Número de entidades en el rango de IDs</returns>
        Task<int> CountByIdRangeAsync(TId startId, TId endId);

        /// <summary>
        /// Cuenta entidades cuyo ID es mayor que el valor especificado
        /// </summary>
        /// <param name="id">ID de referencia</param>
        /// <returns>Número de entidades con ID mayor al especificado</returns>
        int CountByIdGreaterThan(TId id);

        /// <summary>
        /// Cuenta entidades cuyo ID es mayor que el valor especificado de forma asíncrona
        /// </summary>
        /// <param name="id">ID de referencia</param>
        /// <returns>Número de entidades con ID mayor al especificado</returns>
        Task<int> CountByIdGreaterThanAsync(TId id);

        /// <summary>
        /// Cuenta entidades cuyo ID es menor que el valor especificado
        /// </summary>
        /// <param name="id">ID de referencia</param>
        /// <returns>Número de entidades con ID menor al especificado</returns>
        int CountByIdLessThan(TId id);

        /// <summary>
        /// Cuenta entidades cuyo ID es menor que el valor especificado de forma asíncrona
        /// </summary>
        /// <param name="id">ID de referencia</param>
        /// <returns>Número de entidades con ID menor al especificado</returns>
        Task<int> CountByIdLessThanAsync(TId id);

        #endregion
    }
}
