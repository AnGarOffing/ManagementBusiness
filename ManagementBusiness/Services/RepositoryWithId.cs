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
    /// Implementación del repositorio especializado para entidades con ID tipado
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que maneja el repositorio</typeparam>
    /// <typeparam name="TId">Tipo del ID de la entidad</typeparam>
    public class RepositoryWithId<T, TId> : Repository<T>, IRepositoryWithId<T, TId> where T : class
    {
        /// <summary>
        /// Constructor que recibe el contexto de Entity Framework
        /// </summary>
        /// <param name="context">Contexto de base de datos</param>
        public RepositoryWithId(ManagementBusinessContext context) : base(context)
        {
        }

        #region Operaciones Específicas con ID

        public virtual T GetById(TId id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidad por ID tipado: {ex.Message}", ex);
            }
        }

        public virtual async Task<T> GetByIdAsync(TId id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidad por ID tipado: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetByIds(IEnumerable<TId> ids)
        {
            try
            {
                if (ids == null)
                    throw new ArgumentNullException(nameof(ids));

                var idList = ids.ToList();
                if (!idList.Any())
                    return Enumerable.Empty<T>();

                // Usar Contains para obtener múltiples entidades por ID
                // Esto requiere que la entidad tenga una propiedad Id que sea del tipo TId
                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var containsMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TId));
                var containsCall = Expression.Call(containsMethod, Expression.Constant(idList), property);
                var lambda = Expression.Lambda<Func<T, bool>>(containsCall, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades por IDs: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<TId> ids)
        {
            try
            {
                if (ids == null)
                    throw new ArgumentNullException(nameof(ids));

                var idList = ids.ToList();
                if (!idList.Any())
                    return Enumerable.Empty<T>();

                // Usar Contains para obtener múltiples entidades por ID
                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var containsMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TId));
                var containsCall = Expression.Call(containsMethod, Expression.Constant(idList), property);
                var lambda = Expression.Lambda<Func<T, bool>>(containsCall, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades por IDs: {ex.Message}", ex);
            }
        }

        public virtual bool Exists(TId id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return _dbSet.Find(id) != null;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al verificar existencia de entidad por ID: {ex.Message}", ex);
            }
        }

        public virtual async Task<bool> ExistsAsync(TId id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return await _dbSet.FindAsync(id) != null;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al verificar existencia de entidad por ID: {ex.Message}", ex);
            }
        }

        public virtual bool ExistsAll(IEnumerable<TId> ids)
        {
            try
            {
                if (ids == null)
                    throw new ArgumentNullException(nameof(ids));

                var idList = ids.ToList();
                if (!idList.Any())
                    return true;

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var containsMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TId));
                var containsCall = Expression.Call(containsMethod, Expression.Constant(idList), property);
                var lambda = Expression.Lambda<Func<T, bool>>(containsCall, parameter);

                var count = _dbSet.Count(lambda);
                return count == idList.Count;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al verificar existencia de todas las entidades por IDs: {ex.Message}", ex);
            }
        }

        public virtual async Task<bool> ExistsAllAsync(IEnumerable<TId> ids)
        {
            try
            {
                if (ids == null)
                    throw new ArgumentNullException(nameof(ids));

                var idList = ids.ToList();
                if (!idList.Any())
                    return true;

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var containsMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TId));
                var containsCall = Expression.Call(containsMethod, Expression.Constant(idList), property);
                var lambda = Expression.Lambda<Func<T, bool>>(containsCall, parameter);

                var count = await _dbSet.CountAsync(lambda);
                return count == idList.Count;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al verificar existencia de todas las entidades por IDs: {ex.Message}", ex);
            }
        }

        public virtual bool ExistsAny(IEnumerable<TId> ids)
        {
            try
            {
                if (ids == null)
                    throw new ArgumentNullException(nameof(ids));

                var idList = ids.ToList();
                if (!idList.Any())
                    return false;

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var containsMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TId));
                var containsCall = Expression.Call(containsMethod, Expression.Constant(idList), property);
                var lambda = Expression.Lambda<Func<T, bool>>(containsCall, parameter);

                return _dbSet.Any(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al verificar existencia de alguna entidad por IDs: {ex.Message}", ex);
            }
        }

        public virtual async Task<bool> ExistsAnyAsync(IEnumerable<TId> ids)
        {
            try
            {
                if (ids == null)
                    throw new ArgumentNullException(nameof(ids));

                var idList = ids.ToList();
                if (!idList.Any())
                    return false;

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var containsMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TId));
                var containsCall = Expression.Call(containsMethod, Expression.Constant(idList), property);
                var lambda = Expression.Lambda<Func<T, bool>>(containsCall, parameter);

                return await _dbSet.AnyAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al verificar existencia de alguna entidad por IDs: {ex.Message}", ex);
            }
        }

        public virtual bool RemoveById(TId id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                var entity = _dbSet.Find(id);
                if (entity == null)
                    return false;

                _dbSet.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al eliminar entidad por ID: {ex.Message}", ex);
            }
        }

        public virtual async Task<bool> RemoveByIdAsync(TId id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    return false;

                _dbSet.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al eliminar entidad por ID: {ex.Message}", ex);
            }
        }

        public virtual int RemoveByIds(IEnumerable<TId> ids)
        {
            try
            {
                if (ids == null)
                    throw new ArgumentNullException(nameof(ids));

                var idList = ids.ToList();
                if (!idList.Any())
                    return 0;

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var containsMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TId));
                var containsCall = Expression.Call(containsMethod, Expression.Constant(idList), property);
                var lambda = Expression.Lambda<Func<T, bool>>(containsCall, parameter);

                var entitiesToRemove = _dbSet.Where(lambda).ToList();
                _dbSet.RemoveRange(entitiesToRemove);
                return entitiesToRemove.Count;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al eliminar entidades por IDs: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> RemoveByIdsAsync(IEnumerable<TId> ids)
        {
            try
            {
                if (ids == null)
                    throw new ArgumentNullException(nameof(ids));

                var idList = ids.ToList();
                if (!idList.Any())
                    return 0;

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var containsMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TId));
                var containsCall = Expression.Call(containsMethod, Expression.Constant(idList), property);
                var lambda = Expression.Lambda<Func<T, bool>>(containsCall, parameter);

                var entitiesToRemove = await _dbSet.Where(lambda).ToListAsync();
                _dbSet.RemoveRange(entitiesToRemove);
                return entitiesToRemove.Count;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al eliminar entidades por IDs: {ex.Message}", ex);
            }
        }

        public virtual TId GetNextId()
        {
            try
            {
                // Obtener el ID máximo y sumar 1
                var maxId = GetMaxId();
                if (maxId == null)
                    return default(TId);

                // Intentar convertir a numérico para sumar 1
                if (maxId is int intId)
                    return (TId)(object)(intId + 1);
                if (maxId is long longId)
                    return (TId)(object)(longId + 1);
                if (maxId is decimal decimalId)
                    return (TId)(object)(decimalId + 1);

                // Si no es numérico, retornar el valor por defecto
                return default(TId);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener siguiente ID: {ex.Message}", ex);
            }
        }

        public virtual async Task<TId> GetNextIdAsync()
        {
            try
            {
                var maxId = await GetMaxIdAsync();
                if (maxId == null)
                    return default(TId);

                // Intentar convertir a numérico para sumar 1
                if (maxId is int intId)
                    return (TId)(object)(intId + 1);
                if (maxId is long longId)
                    return (TId)(object)(longId + 1);
                if (maxId is decimal decimalId)
                    return (TId)(object)(decimalId + 1);

                return default(TId);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener siguiente ID: {ex.Message}", ex);
            }
        }

        public virtual TId GetMaxId()
        {
            try
            {
                // Obtener el ID máximo usando la propiedad Id
                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var lambda = Expression.Lambda<Func<T, TId>>(property, parameter);

                return _dbSet.Max(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener ID máximo: {ex.Message}", ex);
            }
        }

        public virtual async Task<TId> GetMaxIdAsync()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var lambda = Expression.Lambda<Func<T, TId>>(property, parameter);

                return await _dbSet.MaxAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener ID máximo: {ex.Message}", ex);
            }
        }

        #endregion

        #region Operaciones de Búsqueda por ID

        public virtual IEnumerable<T> GetByIdRange(TId startId, TId endId)
        {
            try
            {
                if (startId == null)
                    throw new ArgumentNullException(nameof(startId));
                if (endId == null)
                    throw new ArgumentNullException(nameof(endId));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(property, Expression.Constant(startId));
                var lessThanOrEqual = Expression.LessThanOrEqual(property, Expression.Constant(endId));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades por rango de ID: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetByIdRangeAsync(TId startId, TId endId)
        {
            try
            {
                if (startId == null)
                    throw new ArgumentNullException(nameof(startId));
                if (endId == null)
                    throw new ArgumentNullException(nameof(endId));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(property, Expression.Constant(startId));
                var lessThanOrEqual = Expression.LessThanOrEqual(property, Expression.Constant(endId));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades por rango de ID: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetByIdGreaterThan(TId id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var greaterThan = Expression.GreaterThan(property, Expression.Constant(id));
                var lambda = Expression.Lambda<Func<T, bool>>(greaterThan, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades con ID mayor que: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetByIdGreaterThanAsync(TId id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var greaterThan = Expression.GreaterThan(property, Expression.Constant(id));
                var lambda = Expression.Lambda<Func<T, bool>>(greaterThan, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades con ID mayor que: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetByIdLessThan(TId id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var lessThan = Expression.LessThan(property, Expression.Constant(id));
                var lambda = Expression.Lambda<Func<T, bool>>(lessThan, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades con ID menor que: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetByIdLessThanAsync(TId id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var lessThan = Expression.LessThan(property, Expression.Constant(id));
                var lambda = Expression.Lambda<Func<T, bool>>(lessThan, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades con ID menor que: {ex.Message}", ex);
            }
        }

        #endregion

        #region Operaciones de Conteo por ID

        public virtual int CountByIdRange(TId startId, TId endId)
        {
            try
            {
                if (startId == null)
                    throw new ArgumentNullException(nameof(startId));
                if (endId == null)
                    throw new ArgumentNullException(nameof(endId));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(property, Expression.Constant(startId));
                var lessThanOrEqual = Expression.LessThanOrEqual(property, Expression.Constant(endId));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return _dbSet.Count(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al contar entidades por rango de ID: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> CountByIdRangeAsync(TId startId, TId endId)
        {
            try
            {
                if (startId == null)
                    throw new ArgumentNullException(nameof(startId));
                if (endId == null)
                    throw new ArgumentNullException(nameof(endId));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(property, Expression.Constant(startId));
                var lessThanOrEqual = Expression.LessThanOrEqual(property, Expression.Constant(endId));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return await _dbSet.CountAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al contar entidades por rango de ID: {ex.Message}", ex);
            }
        }

        public virtual int CountByIdGreaterThan(TId id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var greaterThan = Expression.GreaterThan(property, Expression.Constant(id));
                var lambda = Expression.Lambda<Func<T, bool>>(greaterThan, parameter);

                return _dbSet.Count(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al contar entidades con ID mayor que: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> CountByIdGreaterThanAsync(TId id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var greaterThan = Expression.GreaterThan(property, Expression.Constant(id));
                var lambda = Expression.Lambda<Func<T, bool>>(greaterThan, parameter);

                return await _dbSet.CountAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al contar entidades con ID mayor que: {ex.Message}", ex);
            }
        }

        public virtual int CountByIdLessThan(TId id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var lessThan = Expression.LessThan(property, Expression.Constant(id));
                var lambda = Expression.Lambda<Func<T, bool>>(lessThan, parameter);

                return _dbSet.Count(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al contar entidades con ID menor que: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> CountByIdLessThanAsync(TId id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var property = Expression.Property(parameter, "Id");
                var lessThan = Expression.LessThan(property, Expression.Constant(id));
                var lambda = Expression.Lambda<Func<T, bool>>(lessThan, parameter);

                return await _dbSet.CountAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al contar entidades con ID menor que: {ex.Message}", ex);
            }
        }

        #endregion
    }
}
