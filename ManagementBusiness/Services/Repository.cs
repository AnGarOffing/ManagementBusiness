using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ManagementBusiness.Data;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Implementación base del repositorio genérico usando Entity Framework Core
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que maneja el repositorio</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ManagementBusinessContext _context;
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Constructor que recibe el contexto de Entity Framework
        /// </summary>
        /// <param name="context">Contexto de base de datos</param>
        public Repository(ManagementBusinessContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        #region Operaciones CRUD Básicas

        public virtual T GetById(object id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidad por ID: {ex.Message}", ex);
            }
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidad por ID: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return _dbSet.ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener todas las entidades: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener todas las entidades: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                return _dbSet.Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades con predicado: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                return await _dbSet.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades con predicado: {ex.Message}", ex);
            }
        }

        public virtual T GetFirst(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                return _dbSet.First(predicate);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener primera entidad con predicado: {ex.Message}", ex);
            }
        }

        public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                return await _dbSet.FirstAsync(predicate);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener primera entidad con predicado: {ex.Message}", ex);
            }
        }

        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> predicate, T? defaultValue = default)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                return _dbSet.FirstOrDefault(predicate) ?? defaultValue;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener primera entidad o valor por defecto: {ex.Message}", ex);
            }
        }

        public virtual async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, T? defaultValue = default)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                return await _dbSet.FirstOrDefaultAsync(predicate) ?? defaultValue;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener primera entidad o valor por defecto: {ex.Message}", ex);
            }
        }

        public virtual void Add(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al agregar entidad: {ex.Message}", ex);
            }
        }

        public virtual async Task AddAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                await _dbSet.AddAsync(entity);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al agregar entidad: {ex.Message}", ex);
            }
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                _dbSet.AddRange(entities);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al agregar rango de entidades: {ex.Message}", ex);
            }
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                await _dbSet.AddRangeAsync(entities);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al agregar rango de entidades: {ex.Message}", ex);
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al actualizar entidad: {ex.Message}", ex);
            }
        }

        public virtual Task UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _dbSet.Update(entity);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al actualizar entidad: {ex.Message}", ex);
            }
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                _dbSet.UpdateRange(entities);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al actualizar rango de entidades: {ex.Message}", ex);
            }
        }

        public virtual Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                _dbSet.UpdateRange(entities);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al actualizar rango de entidades: {ex.Message}", ex);
            }
        }

        public virtual void Remove(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al eliminar entidad: {ex.Message}", ex);
            }
        }

        public virtual Task RemoveAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _dbSet.Remove(entity);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al eliminar entidad: {ex.Message}", ex);
            }
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                _dbSet.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al eliminar rango de entidades: {ex.Message}", ex);
            }
        }

        public virtual Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                _dbSet.RemoveRange(entities);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al eliminar rango de entidades: {ex.Message}", ex);
            }
        }

        public virtual void RemoveWhere(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var entitiesToRemove = _dbSet.Where(predicate);
                _dbSet.RemoveRange(entitiesToRemove);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al eliminar entidades con predicado: {ex.Message}", ex);
            }
        }

        public virtual Task RemoveWhereAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var entitiesToRemove = _dbSet.Where(predicate);
                _dbSet.RemoveRange(entitiesToRemove);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al eliminar entidades con predicado: {ex.Message}", ex);
            }
        }

        #endregion

        #region Operaciones de Consulta Avanzadas

        public virtual IEnumerable<T> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber < 1)
                    throw new ArgumentException("El número de página debe ser mayor a 0", nameof(pageNumber));
                if (pageSize < 1)
                    throw new ArgumentException("El tamaño de página debe ser mayor a 0", nameof(pageSize));

                return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades paginadas: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber < 1)
                    throw new ArgumentException("El número de página debe ser mayor a 0", nameof(pageNumber));
                if (pageSize < 1)
                    throw new ArgumentException("El tamaño de página debe ser mayor a 0", nameof(pageSize));

                return await _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades paginadas: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetOrdered<TKey>(Expression<Func<T, TKey>> keySelector, bool ascending = true)
        {
            try
            {
                if (keySelector == null)
                    throw new ArgumentNullException(nameof(keySelector));

                return ascending 
                    ? _dbSet.OrderBy(keySelector).ToList()
                    : _dbSet.OrderByDescending(keySelector).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades ordenadas: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetOrderedAsync<TKey>(Expression<Func<T, TKey>> keySelector, bool ascending = true)
        {
            try
            {
                if (keySelector == null)
                    throw new ArgumentNullException(nameof(keySelector));

                return ascending 
                    ? await _dbSet.OrderBy(keySelector).ToListAsync()
                    : await _dbSet.OrderByDescending(keySelector).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades ordenadas: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetFilteredOrderedPaged<TKey>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TKey>> keySelector,
            bool ascending = true,
            int pageNumber = 1,
            int pageSize = 20)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));
                if (keySelector == null)
                    throw new ArgumentNullException(nameof(keySelector));
                if (pageNumber < 1)
                    throw new ArgumentException("El número de página debe ser mayor a 0", nameof(pageNumber));
                if (pageSize < 1)
                    throw new ArgumentException("El tamaño de página debe ser mayor a 0", nameof(pageSize));

                var query = _dbSet.Where(predicate);
                query = ascending ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);
                return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades filtradas, ordenadas y paginadas: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetFilteredOrderedPagedAsync<TKey>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TKey>> keySelector,
            bool ascending = true,
            int pageNumber = 1,
            int pageSize = 20)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));
                if (keySelector == null)
                    throw new ArgumentNullException(nameof(keySelector));
                if (pageNumber < 1)
                    throw new ArgumentException("El número de página debe ser mayor a 0", nameof(pageNumber));
                if (pageSize < 1)
                    throw new ArgumentException("El tamaño de página debe ser mayor a 0", nameof(pageSize));

                var query = _dbSet.Where(predicate);
                query = ascending ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);
                return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades filtradas, ordenadas y paginadas: {ex.Message}", ex);
            }
        }

        #endregion

        #region Operaciones de Conteo y Verificación

        public virtual int Count()
        {
            try
            {
                return _dbSet.Count();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al contar entidades: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> CountAsync()
        {
            try
            {
                return await _dbSet.CountAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al contar entidades: {ex.Message}", ex);
            }
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                return _dbSet.Count(predicate);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al contar entidades con predicado: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                return await _dbSet.CountAsync(predicate);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al contar entidades con predicado: {ex.Message}", ex);
            }
        }

        public virtual bool Any()
        {
            try
            {
                return _dbSet.Any();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al verificar existencia de entidades: {ex.Message}", ex);
            }
        }

        public virtual async Task<bool> AnyAsync()
        {
            try
            {
                return await _dbSet.AnyAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al verificar existencia de entidades: {ex.Message}", ex);
            }
        }

        public virtual bool Any(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                return _dbSet.Any(predicate);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al verificar existencia de entidades con predicado: {ex.Message}", ex);
            }
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                return await _dbSet.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al verificar existencia de entidades con predicado: {ex.Message}", ex);
            }
        }

        #endregion

        #region Operaciones de Consulta con Inclusión

        public virtual IEnumerable<T> GetWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            try
            {
                if (includes == null || includes.Length == 0)
                    return GetAll();

                var query = _dbSet.AsQueryable();
                foreach (var include in includes)
                {
                    if (include != null)
                        query = query.Include(include);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades con includes: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetWithIncludesAsync(params Expression<Func<T, object>>[] includes)
        {
            try
            {
                if (includes == null || includes.Length == 0)
                    return await GetAllAsync();

                var query = _dbSet.AsQueryable();
                foreach (var include in includes)
                {
                    if (include != null)
                        query = query.Include(include);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades con includes: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var query = _dbSet.Where(predicate);
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        if (include != null)
                            query = query.Include(include);
                    }
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades filtradas con includes: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var query = _dbSet.Where(predicate);
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        if (include != null)
                            query = query.Include(include);
                    }
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades filtradas con includes: {ex.Message}", ex);
            }
        }

        #endregion

        #region Operaciones de Consulta con Proyección

        public virtual IEnumerable<TResult> GetProjected<TResult>(Expression<Func<T, TResult>> selector)
        {
            try
            {
                if (selector == null)
                    throw new ArgumentNullException(nameof(selector));

                return _dbSet.Select(selector).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades proyectadas: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<TResult>> GetProjectedAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            try
            {
                if (selector == null)
                    throw new ArgumentNullException(nameof(selector));

                return await _dbSet.Select(selector).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades proyectadas: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<TResult> GetProjected<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));
                if (selector == null)
                    throw new ArgumentNullException(nameof(selector));

                return _dbSet.Where(predicate).Select(selector).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades filtradas y proyectadas: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<TResult>> GetProjectedAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));
                if (selector == null)
                    throw new ArgumentNullException(nameof(selector));

                return await _dbSet.Where(predicate).Select(selector).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades filtradas y proyectadas: {ex.Message}", ex);
            }
        }

        #endregion
    }
}
