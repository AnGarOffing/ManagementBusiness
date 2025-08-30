using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Interfaz base para repositorios genéricos que proporciona operaciones CRUD básicas
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que maneja el repositorio</typeparam>
    public interface IRepository<T> where T : class
    {
        #region Operaciones CRUD Básicas

        /// <summary>
        /// Obtiene una entidad por su ID
        /// </summary>
        /// <param name="id">ID de la entidad</param>
        /// <returns>La entidad encontrada o null si no existe</returns>
        T GetById(object id);

        /// <summary>
        /// Obtiene una entidad por su ID de forma asíncrona
        /// </summary>
        /// <param name="id">ID de la entidad</param>
        /// <returns>La entidad encontrada o null si no existe</returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// Obtiene todas las entidades
        /// </summary>
        /// <returns>Colección de todas las entidades</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Obtiene todas las entidades de forma asíncrona
        /// </summary>
        /// <returns>Colección de todas las entidades</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Obtiene entidades que cumplen con un predicado
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <returns>Colección de entidades que cumplen el predicado</returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene entidades que cumplen con un predicado de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <returns>Colección de entidades que cumplen el predicado</returns>
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene la primera entidad que cumple con un predicado
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <returns>La primera entidad que cumple el predicado o null si no existe</returns>
        T GetFirst(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene la primera entidad que cumple con un predicado de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <returns>La primera entidad que cumple el predicado o null si no existe</returns>
        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene la primera entidad que cumple con un predicado o una entidad por defecto
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <param name="defaultValue">Valor por defecto si no se encuentra ninguna entidad</param>
        /// <returns>La primera entidad que cumple el predicado o el valor por defecto</returns>
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate, T? defaultValue = default);

        /// <summary>
        /// Obtiene la primera entidad que cumple con un predicado o una entidad por defecto de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <param name="defaultValue">Valor por defecto si no se encuentra ninguna entidad</param>
        /// <returns>La primera entidad que cumple el predicado o el valor por defecto</returns>
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, T? defaultValue = default);

        /// <summary>
        /// Agrega una nueva entidad
        /// </summary>
        /// <param name="entity">Entidad a agregar</param>
        void Add(T entity);

        /// <summary>
        /// Agrega una nueva entidad de forma asíncrona
        /// </summary>
        /// <param name="entity">Entidad a agregar</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Agrega múltiples entidades
        /// </summary>
        /// <param name="entities">Colección de entidades a agregar</param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Agrega múltiples entidades de forma asíncrona
        /// </summary>
        /// <param name="entities">Colección de entidades a agregar</param>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Actualiza una entidad existente
        /// </summary>
        /// <param name="entity">Entidad a actualizar</param>
        void Update(T entity);

        /// <summary>
        /// Actualiza una entidad existente de forma asíncrona
        /// </summary>
        /// <param name="entity">Entidad a actualizar</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Actualiza múltiples entidades
        /// </summary>
        /// <param name="entities">Colección de entidades a actualizar</param>
        void UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// Actualiza múltiples entidades de forma asíncrona
        /// </summary>
        /// <param name="entities">Colección de entidades a actualizar</param>
        Task UpdateRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Elimina una entidad
        /// </summary>
        /// <param name="entity">Entidad a eliminar</param>
        void Remove(T entity);

        /// <summary>
        /// Elimina una entidad de forma asíncrona
        /// </summary>
        /// <param name="entity">Entidad a eliminar</param>
        Task RemoveAsync(T entity);

        /// <summary>
        /// Elimina múltiples entidades
        /// </summary>
        /// <param name="entities">Colección de entidades a eliminar</param>
        void RemoveRange(IEnumerable<T> entities);

        /// <summary>
        /// Elimina múltiples entidades de forma asíncrona
        /// </summary>
        /// <param name="entities">Colección de entidades a eliminar</param>
        Task RemoveRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Elimina entidades que cumplen con un predicado
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades a eliminar</param>
        void RemoveWhere(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Elimina entidades que cumplen con un predicado de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades a eliminar</param>
        Task RemoveWhereAsync(Expression<Func<T, bool>> predicate);

        #endregion

        #region Operaciones de Consulta Avanzadas

        /// <summary>
        /// Obtiene entidades con paginación
        /// </summary>
        /// <param name="pageNumber">Número de página (base 1)</param>
        /// <param name="pageSize">Tamaño de la página</param>
        /// <returns>Colección paginada de entidades</returns>
        IEnumerable<T> GetPaged(int pageNumber, int pageSize);

        /// <summary>
        /// Obtiene entidades con paginación de forma asíncrona
        /// </summary>
        /// <param name="pageNumber">Número de página (base 1)</param>
        /// <param name="pageSize">Tamaño de la página</param>
        /// <returns>Colección paginada de entidades</returns>
        Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Obtiene entidades ordenadas
        /// </summary>
        /// <typeparam name="TKey">Tipo de la clave de ordenamiento</typeparam>
        /// <param name="keySelector">Expresión lambda para seleccionar la clave de ordenamiento</param>
        /// <param name="ascending">True para ordenamiento ascendente, false para descendente</param>
        /// <returns>Colección ordenada de entidades</returns>
        IEnumerable<T> GetOrdered<TKey>(Expression<Func<T, TKey>> keySelector, bool ascending = true);

        /// <summary>
        /// Obtiene entidades ordenadas de forma asíncrona
        /// </summary>
        /// <typeparam name="TKey">Tipo de la clave de ordenamiento</typeparam>
        /// <param name="keySelector">Expresión lambda para seleccionar la clave de ordenamiento</param>
        /// <param name="ascending">True para ordenamiento ascendente, false para descendente</param>
        /// <returns>Colección ordenada de entidades</returns>
        Task<IEnumerable<T>> GetOrderedAsync<TKey>(Expression<Func<T, TKey>> keySelector, bool ascending = true);

        /// <summary>
        /// Obtiene entidades filtradas, ordenadas y paginadas
        /// </summary>
        /// <typeparam name="TKey">Tipo de la clave de ordenamiento</typeparam>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <param name="keySelector">Expresión lambda para seleccionar la clave de ordenamiento</param>
        /// <param name="ascending">True para ordenamiento ascendente, false para descendente</param>
        /// <param name="pageNumber">Número de página (base 1)</param>
        /// <param name="pageSize">Tamaño de la página</param>
        /// <returns>Colección filtrada, ordenada y paginada de entidades</returns>
        IEnumerable<T> GetFilteredOrderedPaged<TKey>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TKey>> keySelector,
            bool ascending = true,
            int pageNumber = 1,
            int pageSize = 20);

        /// <summary>
        /// Obtiene entidades filtradas, ordenadas y paginadas de forma asíncrona
        /// </summary>
        /// <typeparam name="TKey">Tipo de la clave de ordenamiento</typeparam>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <param name="keySelector">Expresión lambda para seleccionar la clave de ordenamiento</param>
        /// <param name="ascending">True para ordenamiento ascendente, false para descendente</param>
        /// <param name="pageNumber">Número de página (base 1)</param>
        /// <param name="pageSize">Tamaño de la página</param>
        /// <returns>Colección filtrada, ordenada y paginada de entidades</returns>
        Task<IEnumerable<T>> GetFilteredOrderedPagedAsync<TKey>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TKey>> keySelector,
            bool ascending = true,
            int pageNumber = 1,
            int pageSize = 20);

        #endregion

        #region Operaciones de Conteo y Verificación

        /// <summary>
        /// Cuenta el total de entidades
        /// </summary>
        /// <returns>Número total de entidades</returns>
        int Count();

        /// <summary>
        /// Cuenta el total de entidades de forma asíncrona
        /// </summary>
        /// <returns>Número total de entidades</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Cuenta entidades que cumplen con un predicado
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <returns>Número de entidades que cumplen el predicado</returns>
        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Cuenta entidades que cumplen con un predicado de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <returns>Número de entidades que cumplen el predicado</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Verifica si existe alguna entidad
        /// </summary>
        /// <returns>True si existe al menos una entidad, false en caso contrario</returns>
        bool Any();

        /// <summary>
        /// Verifica si existe alguna entidad de forma asíncrona
        /// </summary>
        /// <returns>True si existe al menos una entidad, false en caso contrario</returns>
        Task<bool> AnyAsync();

        /// <summary>
        /// Verifica si existe alguna entidad que cumpla con un predicado
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <returns>True si existe al menos una entidad que cumpla el predicado, false en caso contrario</returns>
        bool Any(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Verifica si existe alguna entidad que cumpla con un predicado de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <returns>True si existe al menos una entidad que cumpla el predicado, false en caso contrario</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        #endregion

        #region Operaciones de Consulta con Inclusión

        /// <summary>
        /// Obtiene entidades incluyendo propiedades de navegación específicas
        /// </summary>
        /// <param name="includes">Expresiones lambda para incluir propiedades de navegación</param>
        /// <returns>Colección de entidades con propiedades de navegación incluidas</returns>
        IEnumerable<T> GetWithIncludes(params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Obtiene entidades incluyendo propiedades de navegación específicas de forma asíncrona
        /// </summary>
        /// <param name="includes">Expresiones lambda para incluir propiedades de navegación</param>
        /// <returns>Colección de entidades con propiedades de navegación incluidas</returns>
        Task<IEnumerable<T>> GetWithIncludesAsync(params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Obtiene entidades filtradas incluyendo propiedades de navegación específicas
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <param name="includes">Expresiones lambda para incluir propiedades de navegación</param>
        /// <returns>Colección filtrada de entidades con propiedades de navegación incluidas</returns>
        IEnumerable<T> GetWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Obtiene entidades filtradas incluyendo propiedades de navegación específicas de forma asíncrona
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <param name="includes">Expresiones lambda para incluir propiedades de navegación</param>
        /// <returns>Colección filtrada de entidades con propiedades de navegación incluidas</returns>
        Task<IEnumerable<T>> GetWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        #endregion

        #region Operaciones de Consulta con Proyección

        /// <summary>
        /// Obtiene entidades proyectadas a un tipo específico
        /// </summary>
        /// <typeparam name="TResult">Tipo de resultado de la proyección</typeparam>
        /// <param name="selector">Expresión lambda para proyectar entidades</param>
        /// <returns>Colección de resultados proyectados</returns>
        IEnumerable<TResult> GetProjected<TResult>(Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Obtiene entidades proyectadas a un tipo específico de forma asíncrona
        /// </summary>
        /// <typeparam name="TResult">Tipo de resultado de la proyección</typeparam>
        /// <param name="selector">Expresión lambda para proyectar entidades</param>
        /// <returns>Colección de resultados proyectados</returns>
        Task<IEnumerable<TResult>> GetProjectedAsync<TResult>(Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Obtiene entidades filtradas y proyectadas a un tipo específico
        /// </summary>
        /// <typeparam name="TResult">Tipo de resultado de la proyección</typeparam>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <param name="selector">Expresión lambda para proyectar entidades</param>
        /// <returns>Colección filtrada y proyectada de resultados</returns>
        IEnumerable<TResult> GetProjected<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Obtiene entidades filtradas y proyectadas a un tipo específico de forma asíncrona
        /// </summary>
        /// <typeparam name="TResult">Tipo de resultado de la proyección</typeparam>
        /// <param name="predicate">Expresión lambda para filtrar entidades</param>
        /// <param name="selector">Expresión lambda para proyectar entidades</param>
        /// <returns>Colección filtrada y proyectada de resultados</returns>
        Task<IEnumerable<TResult>> GetProjectedAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);

        #endregion
    }
}
