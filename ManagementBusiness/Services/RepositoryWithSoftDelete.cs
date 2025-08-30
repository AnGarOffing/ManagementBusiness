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
    /// Implementación del repositorio especializado para entidades con soft delete
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que maneja el repositorio</typeparam>
    public class RepositoryWithSoftDelete<T> : Repository<T>, IRepositoryWithSoftDelete<T> where T : class
    {
        /// <summary>
        /// Constructor que recibe el contexto de Entity Framework
        /// </summary>
        /// <param name="context">Contexto de base de datos</param>
        public RepositoryWithSoftDelete(ManagementBusinessContext context) : base(context)
        {
        }

        #region Operaciones de Soft Delete

        public virtual void SoftDelete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                // Marcar la entidad como eliminada
                var isDeletedProperty = Expression.Property(Expression.Parameter(typeof(T), "e"), "IsDeleted");
                var isDeletedPropertyInfo = typeof(T).GetProperty("IsDeleted");
                
                if (isDeletedPropertyInfo != null)
                {
                    isDeletedPropertyInfo.SetValue(entity, true);
                }

                // Establecer fecha de eliminación si existe la propiedad
                var deletedDatePropertyInfo = typeof(T).GetProperty("DeletedDate");
                if (deletedDatePropertyInfo != null)
                {
                    deletedDatePropertyInfo.SetValue(entity, DateTime.UtcNow);
                }

                // Establecer usuario que eliminó si existe la propiedad
                var deletedByPropertyInfo = typeof(T).GetProperty("DeletedBy");
                if (deletedByPropertyInfo != null)
                {
                    // TODO: Obtener usuario actual del contexto de seguridad
                    deletedByPropertyInfo.SetValue(entity, "System");
                }

                // Marcar como modificado para que Entity Framework detecte el cambio
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al realizar soft delete de entidad: {ex.Message}", ex);
            }
        }

        public virtual async Task SoftDeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                SoftDelete(entity);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al realizar soft delete de entidad: {ex.Message}", ex);
            }
        }

        public virtual void SoftDeleteRange(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                {
                    SoftDelete(entity);
                }
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al realizar soft delete de rango de entidades: {ex.Message}", ex);
            }
        }

        public virtual async Task SoftDeleteRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                SoftDeleteRange(entities);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al realizar soft delete de rango de entidades: {ex.Message}", ex);
            }
        }

        public virtual void SoftDeleteWhere(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var entitiesToSoftDelete = _dbSet.Where(predicate).ToList();
                SoftDeleteRange(entitiesToSoftDelete);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al realizar soft delete de entidades con predicado: {ex.Message}", ex);
            }
        }

        public virtual async Task SoftDeleteWhereAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var entitiesToSoftDelete = await _dbSet.Where(predicate).ToListAsync();
                SoftDeleteRange(entitiesToSoftDelete);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al realizar soft delete de entidades con predicado: {ex.Message}", ex);
            }
        }

        public virtual void Restore(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                // Marcar la entidad como no eliminada
                var isDeletedPropertyInfo = typeof(T).GetProperty("IsDeleted");
                if (isDeletedPropertyInfo != null)
                {
                    isDeletedPropertyInfo.SetValue(entity, false);
                }

                // Limpiar fecha de eliminación si existe la propiedad
                var deletedDatePropertyInfo = typeof(T).GetProperty("DeletedDate");
                if (deletedDatePropertyInfo != null)
                {
                    deletedDatePropertyInfo.SetValue(entity, null);
                }

                // Limpiar usuario que eliminó si existe la propiedad
                var deletedByPropertyInfo = typeof(T).GetProperty("DeletedBy");
                if (deletedByPropertyInfo != null)
                {
                    deletedByPropertyInfo.SetValue(entity, null);
                }

                // Marcar como modificado para que Entity Framework detecte el cambio
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al restaurar entidad: {ex.Message}", ex);
            }
        }

        public virtual async Task RestoreAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Restore(entity);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al restaurar entidad: {ex.Message}", ex);
            }
        }

        public virtual void RestoreRange(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                {
                    Restore(entity);
                }
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al restaurar rango de entidades: {ex.Message}", ex);
            }
        }

        public virtual async Task RestoreRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                RestoreRange(entities);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al restaurar rango de entidades: {ex.Message}", ex);
            }
        }

        public virtual void RestoreWhere(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var entitiesToRestore = _dbSet.Where(predicate).ToList();
                RestoreRange(entitiesToRestore);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al restaurar entidades con predicado: {ex.Message}", ex);
            }
        }

        public virtual async Task RestoreWhereAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var entitiesToRestore = await _dbSet.Where(predicate).ToListAsync();
                RestoreRange(entitiesToRestore);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al restaurar entidades con predicado: {ex.Message}", ex);
            }
        }

        #endregion

        #region Consultas con Soft Delete

        public virtual IEnumerable<T> GetActive()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                var lambda = Expression.Lambda<Func<T, bool>>(isFalse, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades activas: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetActiveAsync()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                var lambda = Expression.Lambda<Func<T, bool>>(isFalse, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades activas: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetDeleted()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isTrue = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                var lambda = Expression.Lambda<Func<T, bool>>(isTrue, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades eliminadas: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetDeletedAsync()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isTrue = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                var lambda = Expression.Lambda<Func<T, bool>>(isTrue, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades eliminadas: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetActive(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                
                // Combinar el predicado original con la condición de no eliminado
                var combined = Expression.AndAlso(predicate, isFalse);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades activas con predicado: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetActiveAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                
                // Combinar el predicado original con la condición de no eliminado
                var combined = Expression.AndAlso(predicate, isFalse);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades activas con predicado: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetDeleted(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isTrue = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                
                // Combinar el predicado original con la condición de eliminado
                var combined = Expression.AndAlso(predicate, isTrue);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades eliminadas con predicado: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetDeletedAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isTrue = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                
                // Combinar el predicado original con la condición de eliminado
                var combined = Expression.AndAlso(predicate, isTrue);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades eliminadas con predicado: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetActivePaged(int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber < 1)
                    throw new ArgumentException("El número de página debe ser mayor a 0", nameof(pageNumber));
                if (pageSize < 1)
                    throw new ArgumentException("El tamaño de página debe ser mayor a 0", nameof(pageSize));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                var lambda = Expression.Lambda<Func<T, bool>>(isFalse, parameter);

                return _dbSet.Where(lambda).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades activas paginadas: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetActivePagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber < 1)
                    throw new ArgumentException("El número de página debe ser mayor a 0", nameof(pageNumber));
                if (pageSize < 1)
                    throw new ArgumentException("El tamaño de página debe ser mayor a 0", nameof(pageSize));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                var lambda = Expression.Lambda<Func<T, bool>>(isFalse, parameter);

                return await _dbSet.Where(lambda).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades activas paginadas: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetDeletedPaged(int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber < 1)
                    throw new ArgumentException("El número de página debe ser mayor a 0", nameof(pageNumber));
                if (pageSize < 1)
                    throw new ArgumentException("El tamaño de página debe ser mayor a 0", nameof(pageSize));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isTrue = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                var lambda = Expression.Lambda<Func<T, bool>>(isTrue, parameter);

                return _dbSet.Where(lambda).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades eliminadas paginadas: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetDeletedPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber < 1)
                    throw new ArgumentException("El número de página debe ser mayor a 0", nameof(pageNumber));
                if (pageSize < 1)
                    throw new ArgumentException("El tamaño de página debe ser mayor a 0", nameof(pageSize));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isTrue = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                var lambda = Expression.Lambda<Func<T, bool>>(isTrue, parameter);

                return await _dbSet.Where(lambda).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades eliminadas paginadas: {ex.Message}", ex);
            }
        }

        #endregion

        #region Conteo con Soft Delete

        public virtual int CountActive()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                var lambda = Expression.Lambda<Func<T, bool>>(isFalse, parameter);

                return _dbSet.Count(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al contar entidades activas: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> CountActiveAsync()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                var lambda = Expression.Lambda<Func<T, bool>>(isFalse, parameter);

                return await _dbSet.CountAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al contar entidades activas: {ex.Message}", ex);
            }
        }

        public virtual int CountDeleted()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isTrue = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                var lambda = Expression.Lambda<Func<T, bool>>(isTrue, parameter);

                return _dbSet.Count(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al contar entidades eliminadas: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> CountDeletedAsync()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isTrue = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                var lambda = Expression.Lambda<Func<T, bool>>(isTrue, parameter);

                return await _dbSet.CountAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al contar entidades eliminadas: {ex.Message}", ex);
            }
        }

        public virtual int CountActive(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                
                var combined = Expression.AndAlso(predicate, isFalse);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return _dbSet.Count(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al contar entidades activas con predicado: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> CountActiveAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                
                var combined = Expression.AndAlso(predicate, isFalse);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return await _dbSet.CountAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al contar entidades activas con predicado: {ex.Message}", ex);
            }
        }

        public virtual int CountDeleted(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isTrue = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                
                var combined = Expression.AndAlso(predicate, isTrue);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return _dbSet.Count(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al contar entidades eliminadas con predicado: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> CountDeletedAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isTrue = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                
                var combined = Expression.AndAlso(predicate, isTrue);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return await _dbSet.CountAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al contar entidades eliminadas con predicado: {ex.Message}", ex);
            }
        }

        #endregion

        #region Verificación con Soft Delete

        public virtual bool AnyActive()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                var lambda = Expression.Lambda<Func<T, bool>>(isFalse, parameter);

                return _dbSet.Any(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al verificar existencia de entidades activas: {ex.Message}", ex);
            }
        }

        public virtual async Task<bool> AnyActiveAsync()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                var lambda = Expression.Lambda<Func<T, bool>>(isFalse, parameter);

                return await _dbSet.AnyAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al verificar existencia de entidades activas: {ex.Message}", ex);
            }
        }

        public virtual bool AnyDeleted()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isTrue = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                var lambda = Expression.Lambda<Func<T, bool>>(isTrue, parameter);

                return _dbSet.Any(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al verificar existencia de entidades eliminadas: {ex.Message}", ex);
            }
        }

        public virtual async Task<bool> AnyDeletedAsync()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isTrue = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                var lambda = Expression.Lambda<Func<T, bool>>(isTrue, parameter);

                return await _dbSet.AnyAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al verificar existencia de entidades eliminadas: {ex.Message}", ex);
            }
        }

        public virtual bool AnyActive(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                
                var combined = Expression.AndAlso(predicate, isFalse);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return _dbSet.Any(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al verificar existencia de entidades activas con predicado: {ex.Message}", ex);
            }
        }

        public virtual async Task<bool> AnyActiveAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                
                var combined = Expression.AndAlso(predicate, isFalse);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return await _dbSet.AnyAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al verificar existencia de entidades activas con predicado: {ex.Message}", ex);
            }
        }

        public virtual bool AnyDeleted(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isTrue = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                
                var combined = Expression.AndAlso(predicate, isTrue);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return _dbSet.Any(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al verificar existencia de entidades eliminadas con predicado: {ex.Message}", ex);
            }
        }

        public virtual async Task<bool> AnyDeletedAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isTrue = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                
                var combined = Expression.AndAlso(predicate, isTrue);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return await _dbSet.AnyAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al verificar existencia de entidades eliminadas con predicado: {ex.Message}", ex);
            }
        }

        #endregion

        #region Operaciones de Limpieza

        public virtual void PermanentlyDelete(IEnumerable<T> entities)
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
                throw new InvalidOperationException($"Error al eliminar permanentemente entidades: {ex.Message}", ex);
            }
        }

        public virtual async Task PermanentlyDeleteAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                PermanentlyDelete(entities);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al eliminar permanentemente entidades: {ex.Message}", ex);
            }
        }

        public virtual void PermanentlyDeleteWhere(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var entitiesToDelete = _dbSet.Where(predicate).ToList();
                PermanentlyDelete(entitiesToDelete);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al eliminar permanentemente entidades con predicado: {ex.Message}", ex);
            }
        }

        public virtual async Task PermanentlyDeleteWhereAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                var entitiesToDelete = await _dbSet.Where(predicate).ToListAsync();
                PermanentlyDelete(entitiesToDelete);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al eliminar permanentemente entidades con predicado: {ex.Message}", ex);
            }
        }

        public virtual int CleanupDeletedOlderThan(DateTime cutoffDate)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var deletedDateProperty = Expression.Property(parameter, "DeletedDate");
                
                var isDeleted = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                var olderThan = Expression.LessThan(deletedDateProperty, Expression.Constant(cutoffDate));
                var combined = Expression.AndAlso(isDeleted, olderThan);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                var entitiesToDelete = _dbSet.Where(lambda).ToList();
                PermanentlyDelete(entitiesToDelete);
                return entitiesToDelete.Count;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al limpiar entidades eliminadas más antiguas que fecha: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> CleanupDeletedOlderThanAsync(DateTime cutoffDate)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var deletedDateProperty = Expression.Property(parameter, "DeletedDate");
                
                var isDeleted = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                var olderThan = Expression.LessThan(deletedDateProperty, Expression.Constant(cutoffDate));
                var combined = Expression.AndAlso(isDeleted, olderThan);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                var entitiesToDelete = await _dbSet.Where(lambda).ToListAsync();
                PermanentlyDelete(entitiesToDelete);
                return entitiesToDelete.Count;
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al limpiar entidades eliminadas más antiguas que fecha: {ex.Message}", ex);
            }
        }

        #endregion

        #region Operaciones de Auditoría de Soft Delete

        public virtual IEnumerable<T> GetDeletedOnDate(DateTime date)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var deletedDateProperty = Expression.Property(parameter, "DeletedDate");
                
                var dateOnly = date.Date;
                var nextDay = dateOnly.AddDays(1);
                
                var isDeleted = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(deletedDateProperty, Expression.Constant(dateOnly));
                var lessThan = Expression.LessThan(deletedDateProperty, Expression.Constant(nextDay));
                var dateRange = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var combined = Expression.AndAlso(isDeleted, dateRange);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades eliminadas en fecha: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetDeletedOnDateAsync(DateTime date)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var deletedDateProperty = Expression.Property(parameter, "DeletedDate");
                
                var dateOnly = date.Date;
                var nextDay = dateOnly.AddDays(1);
                
                var isDeleted = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(deletedDateProperty, Expression.Constant(dateOnly));
                var lessThan = Expression.LessThan(deletedDateProperty, Expression.Constant(nextDay));
                var dateRange = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var combined = Expression.AndAlso(isDeleted, dateRange);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades eliminadas en fecha: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetRestoredOnDate(DateTime date)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var restoredDateProperty = Expression.Property(parameter, "RestoredDate");
                
                var dateOnly = date.Date;
                var nextDay = dateOnly.AddDays(1);
                
                var isNotDeleted = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(restoredDateProperty, Expression.Constant(dateOnly));
                var lessThan = Expression.LessThan(restoredDateProperty, Expression.Constant(nextDay));
                var dateRange = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var combined = Expression.AndAlso(isNotDeleted, dateRange);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades restauradas en fecha: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetRestoredOnDateAsync(DateTime date)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var restoredDateProperty = Expression.Property(parameter, "RestoredDate");
                
                var dateOnly = date.Date;
                var nextDay = dateOnly.AddDays(1);
                
                var isNotDeleted = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(restoredDateProperty, Expression.Constant(dateOnly));
                var lessThan = Expression.LessThan(restoredDateProperty, Expression.Constant(nextDay));
                var dateRange = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var combined = Expression.AndAlso(isNotDeleted, dateRange);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades restauradas en fecha: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetDeletedBy(string deletedBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(deletedBy))
                    throw new ArgumentException("El usuario que eliminó no puede estar vacío", nameof(deletedBy));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var deletedByProperty = Expression.Property(parameter, "DeletedBy");
                
                var isDeleted = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                var deletedByEquals = Expression.Equal(deletedByProperty, Expression.Constant(deletedBy.Trim()));
                var combined = Expression.AndAlso(isDeleted, deletedByEquals);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades eliminadas por usuario: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetDeletedByAsync(string deletedBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(deletedBy))
                    throw new ArgumentException("El usuario que eliminó no puede estar vacío", nameof(deletedBy));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var deletedByProperty = Expression.Property(parameter, "DeletedBy");
                
                var isDeleted = Expression.Equal(isDeletedProperty, Expression.Constant(true));
                var deletedByEquals = Expression.Equal(deletedByProperty, Expression.Constant(deletedBy.Trim()));
                var combined = Expression.AndAlso(isDeleted, deletedByEquals);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades eliminadas por usuario: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetRestoredBy(string restoredBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(restoredBy))
                    throw new ArgumentException("El usuario que restauró no puede estar vacío", nameof(restoredBy));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var restoredByProperty = Expression.Property(parameter, "RestoredBy");
                
                var isNotDeleted = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                var restoredByEquals = Expression.Equal(restoredByProperty, Expression.Constant(restoredBy.Trim()));
                var combined = Expression.AndAlso(isNotDeleted, restoredByEquals);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades restauradas por usuario: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetRestoredByAsync(string restoredBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(restoredBy))
                    throw new ArgumentException("El usuario que restauró no puede estar vacío", nameof(restoredBy));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var restoredByProperty = Expression.Property(parameter, "RestoredBy");
                
                var isNotDeleted = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                var restoredByEquals = Expression.Equal(restoredByProperty, Expression.Constant(restoredBy.Trim()));
                var combined = Expression.AndAlso(isNotDeleted, restoredByEquals);
                var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades restauradas por usuario: {ex.Message}", ex);
            }
        }

        #endregion
    }
}
