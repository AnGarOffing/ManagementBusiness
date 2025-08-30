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
    /// Implementación del repositorio especializado para entidades con auditoría
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que maneja el repositorio</typeparam>
    public class RepositoryWithAudit<T> : Repository<T>, IRepositoryWithAudit<T> where T : class
    {
        /// <summary>
        /// Constructor que recibe el contexto de Entity Framework
        /// </summary>
        /// <param name="context">Contexto de base de datos</param>
        public RepositoryWithAudit(ManagementBusinessContext context) : base(context)
        {
        }

        #region Operaciones de Auditoría por Fecha

        public virtual IEnumerable<T> GetByCreatedDate(DateTime date)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var createdDateProperty = Expression.Property(parameter, "CreatedDate");
                var dateOnly = date.Date;
                var nextDay = dateOnly.AddDays(1);
                
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(createdDateProperty, Expression.Constant(dateOnly));
                var lessThan = Expression.LessThan(createdDateProperty, Expression.Constant(nextDay));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades por fecha de creación: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetByCreatedDateAsync(DateTime date)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var createdDateProperty = Expression.Property(parameter, "CreatedDate");
                var dateOnly = date.Date;
                var nextDay = dateOnly.AddDays(1);
                
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(createdDateProperty, Expression.Constant(dateOnly));
                var lessThan = Expression.LessThan(createdDateProperty, Expression.Constant(nextDay));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades por fecha de creación: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetByCreatedDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var createdDateProperty = Expression.Property(parameter, "CreatedDate");
                var startDateOnly = startDate.Date;
                var endDateOnly = endDate.Date.AddDays(1); // Incluir todo el día final
                
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(createdDateProperty, Expression.Constant(startDateOnly));
                var lessThan = Expression.LessThan(createdDateProperty, Expression.Constant(endDateOnly));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades por rango de fecha de creación: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var createdDateProperty = Expression.Property(parameter, "CreatedDate");
                var startDateOnly = startDate.Date;
                var endDateOnly = endDate.Date.AddDays(1); // Incluir todo el día final
                
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(createdDateProperty, Expression.Constant(startDateOnly));
                var lessThan = Expression.LessThan(createdDateProperty, Expression.Constant(endDateOnly));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades por rango de fecha de creación: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetByModifiedDate(DateTime date)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var modifiedDateProperty = Expression.Property(parameter, "ModifiedDate");
                var dateOnly = date.Date;
                var nextDay = dateOnly.AddDays(1);
                
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(modifiedDateProperty, Expression.Constant(dateOnly));
                var lessThan = Expression.LessThan(modifiedDateProperty, Expression.Constant(nextDay));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades por fecha de modificación: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetByModifiedDateAsync(DateTime date)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var modifiedDateProperty = Expression.Property(parameter, "ModifiedDate");
                var dateOnly = date.Date;
                var nextDay = dateOnly.AddDays(1);
                
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(modifiedDateProperty, Expression.Constant(dateOnly));
                var lessThan = Expression.LessThan(modifiedDateProperty, Expression.Constant(nextDay));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades por fecha de modificación: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetByModifiedDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var modifiedDateProperty = Expression.Property(parameter, "ModifiedDate");
                var startDateOnly = startDate.Date;
                var endDateOnly = endDate.Date.AddDays(1); // Incluir todo el día final
                
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(modifiedDateProperty, Expression.Constant(startDateOnly));
                var lessThan = Expression.LessThan(modifiedDateProperty, Expression.Constant(endDateOnly));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades por rango de fecha de modificación: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetByModifiedDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var modifiedDateProperty = Expression.Property(parameter, "ModifiedDate");
                var startDateOnly = startDate.Date;
                var endDateOnly = endDate.Date.AddDays(1); // Incluir todo el día final
                
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(modifiedDateProperty, Expression.Constant(startDateOnly));
                var lessThan = Expression.LessThan(modifiedDateProperty, Expression.Constant(endDateOnly));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades por rango de fecha de modificación: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetCreatedToday()
        {
            return GetByCreatedDate(DateTime.Today);
        }

        public virtual async Task<IEnumerable<T>> GetCreatedTodayAsync()
        {
            return await GetByCreatedDateAsync(DateTime.Today);
        }

        public virtual IEnumerable<T> GetModifiedToday()
        {
            return GetByModifiedDate(DateTime.Today);
        }

        public virtual async Task<IEnumerable<T>> GetModifiedTodayAsync()
        {
            return await GetByModifiedDateAsync(DateTime.Today);
        }

        public virtual IEnumerable<T> GetCreatedLastWeek()
        {
            var endDate = DateTime.Today;
            var startDate = endDate.AddDays(-7);
            return GetByCreatedDateRange(startDate, endDate);
        }

        public virtual async Task<IEnumerable<T>> GetCreatedLastWeekAsync()
        {
            var endDate = DateTime.Today;
            var startDate = endDate.AddDays(-7);
            return await GetByCreatedDateRangeAsync(startDate, endDate);
        }

        public virtual IEnumerable<T> GetModifiedLastWeek()
        {
            var endDate = DateTime.Today;
            var startDate = endDate.AddDays(-7);
            return GetByModifiedDateRange(startDate, endDate);
        }

        public virtual async Task<IEnumerable<T>> GetModifiedLastWeekAsync()
        {
            var endDate = DateTime.Today;
            var startDate = endDate.AddDays(-7);
            return await GetByModifiedDateRangeAsync(startDate, endDate);
        }

        public virtual IEnumerable<T> GetCreatedLastMonth()
        {
            var endDate = DateTime.Today;
            var startDate = endDate.AddMonths(-1);
            return GetByCreatedDateRange(startDate, endDate);
        }

        public virtual async Task<IEnumerable<T>> GetCreatedLastMonthAsync()
        {
            var endDate = DateTime.Today;
            var startDate = endDate.AddMonths(-1);
            return await GetByCreatedDateRangeAsync(startDate, endDate);
        }

        public virtual IEnumerable<T> GetModifiedLastMonth()
        {
            var endDate = DateTime.Today;
            var startDate = endDate.AddMonths(-1);
            return GetByModifiedDateRange(startDate, endDate);
        }

        public virtual async Task<IEnumerable<T>> GetModifiedLastMonthAsync()
        {
            var endDate = DateTime.Today;
            var startDate = endDate.AddMonths(-1);
            return await GetByModifiedDateRangeAsync(startDate, endDate);
        }

        #endregion

        #region Operaciones de Auditoría por Usuario

        public virtual IEnumerable<T> GetByCreatedBy(string createdBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(createdBy))
                    throw new ArgumentException("El usuario creador no puede estar vacío", nameof(createdBy));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var createdByProperty = Expression.Property(parameter, "CreatedBy");
                var constant = Expression.Constant(createdBy.Trim());
                var equals = Expression.Equal(createdByProperty, constant);
                var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades por usuario creador: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetByCreatedByAsync(string createdBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(createdBy))
                    throw new ArgumentException("El usuario creador no puede estar vacío", nameof(createdBy));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var createdByProperty = Expression.Property(parameter, "CreatedBy");
                var constant = Expression.Constant(createdBy.Trim());
                var equals = Expression.Equal(createdByProperty, constant);
                var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades por usuario creador: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetByModifiedBy(string modifiedBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(modifiedBy))
                    throw new ArgumentException("El usuario modificador no puede estar vacío", nameof(modifiedBy));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var modifiedByProperty = Expression.Property(parameter, "ModifiedBy");
                var constant = Expression.Constant(modifiedBy.Trim());
                var equals = Expression.Equal(modifiedByProperty, constant);
                var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades por usuario modificador: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetByModifiedByAsync(string modifiedBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(modifiedBy))
                    throw new ArgumentException("El usuario modificador no puede estar vacío", nameof(modifiedBy));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var modifiedByProperty = Expression.Property(parameter, "ModifiedBy");
                var constant = Expression.Constant(modifiedBy.Trim());
                var equals = Expression.Equal(modifiedByProperty, constant);
                var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades por usuario modificador: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetByCreatedByAndDateRange(string createdBy, DateTime startDate, DateTime endDate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(createdBy))
                    throw new ArgumentException("El usuario creador no puede estar vacío", nameof(createdBy));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var createdByProperty = Expression.Property(parameter, "CreatedBy");
                var createdDateProperty = Expression.Property(parameter, "CreatedDate");
                
                var startDateOnly = startDate.Date;
                var endDateOnly = endDate.Date.AddDays(1); // Incluir todo el día final
                
                var createdByEquals = Expression.Equal(createdByProperty, Expression.Constant(createdBy.Trim()));
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(createdDateProperty, Expression.Constant(startDateOnly));
                var lessThan = Expression.LessThan(createdDateProperty, Expression.Constant(endDateOnly));
                var dateRange = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var final = Expression.AndAlso(createdByEquals, dateRange);
                var lambda = Expression.Lambda<Func<T, bool>>(final, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades por usuario creador y rango de fechas: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetByCreatedByAndDateRangeAsync(string createdBy, DateTime startDate, DateTime endDate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(createdBy))
                    throw new ArgumentException("El usuario creador no puede estar vacío", nameof(createdBy));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var createdByProperty = Expression.Property(parameter, "CreatedBy");
                var createdDateProperty = Expression.Property(parameter, "CreatedDate");
                
                var startDateOnly = startDate.Date;
                var endDateOnly = endDate.Date.AddDays(1); // Incluir todo el día final
                
                var createdByEquals = Expression.Equal(createdByProperty, Expression.Constant(createdBy.Trim()));
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(createdDateProperty, Expression.Constant(startDateOnly));
                var lessThan = Expression.LessThan(createdDateProperty, Expression.Constant(endDateOnly));
                var dateRange = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var final = Expression.AndAlso(createdByEquals, dateRange);
                var lambda = Expression.Lambda<Func<T, bool>>(final, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades por usuario creador y rango de fechas: {ex.Message}", ex);
            }
        }

        #endregion

        #region Operaciones de Conteo de Auditoría

        public virtual int CountByCreatedDate(DateTime date)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var createdDateProperty = Expression.Property(parameter, "CreatedDate");
                var dateOnly = date.Date;
                var nextDay = dateOnly.AddDays(1);
                
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(createdDateProperty, Expression.Constant(dateOnly));
                var lessThan = Expression.LessThan(createdDateProperty, Expression.Constant(nextDay));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return _dbSet.Count(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al contar entidades por fecha de creación: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> CountByCreatedDateAsync(DateTime date)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var createdDateProperty = Expression.Property(parameter, "CreatedDate");
                var dateOnly = date.Date;
                var nextDay = dateOnly.AddDays(1);
                
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(createdDateProperty, Expression.Constant(dateOnly));
                var lessThan = Expression.LessThan(createdDateProperty, Expression.Constant(nextDay));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return await _dbSet.CountAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al contar entidades por fecha de creación: {ex.Message}", ex);
            }
        }

        public virtual int CountByCreatedDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var createdDateProperty = Expression.Property(parameter, "CreatedDate");
                var startDateOnly = startDate.Date;
                var endDateOnly = endDate.Date.AddDays(1); // Incluir todo el día final
                
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(createdDateProperty, Expression.Constant(startDateOnly));
                var lessThan = Expression.LessThan(createdDateProperty, Expression.Constant(endDateOnly));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return _dbSet.Count(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al contar entidades por rango de fecha de creación: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> CountByCreatedDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var createdDateProperty = Expression.Property(parameter, "CreatedDate");
                var startDateOnly = startDate.Date;
                var endDateOnly = endDate.Date.AddDays(1); // Incluir todo el día final
                
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(createdDateProperty, Expression.Constant(startDateOnly));
                var lessThan = Expression.LessThan(createdDateProperty, Expression.Constant(endDateOnly));
                var and = Expression.AndAlso(greaterThanOrEqual, lessThan);
                var lambda = Expression.Lambda<Func<T, bool>>(and, parameter);

                return await _dbSet.CountAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al contar entidades por rango de fecha de creación: {ex.Message}", ex);
            }
        }

        public virtual int CountByCreatedBy(string createdBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(createdBy))
                    throw new ArgumentException("El usuario creador no puede estar vacío", nameof(createdBy));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var createdByProperty = Expression.Property(parameter, "CreatedBy");
                var constant = Expression.Constant(createdBy.Trim());
                var equals = Expression.Equal(createdByProperty, constant);
                var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

                return _dbSet.Count(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al contar entidades por usuario creador: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> CountByCreatedByAsync(string createdBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(createdBy))
                    throw new ArgumentException("El usuario creador no puede estar vacío", nameof(createdBy));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var createdByProperty = Expression.Property(parameter, "CreatedBy");
                var constant = Expression.Constant(createdBy.Trim());
                var equals = Expression.Equal(createdByProperty, constant);
                var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

                return await _dbSet.CountAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al contar entidades por usuario creador: {ex.Message}", ex);
            }
        }

        public virtual int CountByModifiedBy(string modifiedBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(modifiedBy))
                    throw new ArgumentException("El usuario modificador no puede estar vacío", nameof(modifiedBy));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var modifiedByProperty = Expression.Property(parameter, "ModifiedBy");
                var constant = Expression.Constant(modifiedBy.Trim());
                var equals = Expression.Equal(modifiedByProperty, constant);
                var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

                return _dbSet.Count(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al contar entidades por usuario modificador: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> CountByModifiedByAsync(string modifiedBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(modifiedBy))
                    throw new ArgumentException("El usuario modificador no puede estar vacío", nameof(modifiedBy));

                var parameter = Expression.Parameter(typeof(T), "entity");
                var modifiedByProperty = Expression.Property(parameter, "ModifiedBy");
                var constant = Expression.Constant(modifiedBy.Trim());
                var equals = Expression.Equal(modifiedByProperty, constant);
                var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

                return await _dbSet.CountAsync(lambda);
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al contar entidades por usuario modificador: {ex.Message}", ex);
            }
        }

        #endregion

        #region Operaciones de Auditoría Avanzadas

        public virtual IEnumerable<T> GetChangeHistory(object entityId)
        {
            try
            {
                if (entityId == null)
                    throw new ArgumentNullException(nameof(entityId));

                // Esta implementación básica retorna la entidad actual
                // En un sistema más avanzado, esto podría consultar una tabla de historial
                var entity = _dbSet.Find(entityId);
                if (entity == null)
                    return Enumerable.Empty<T>();

                return new List<T> { entity };
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener historial de cambios: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetChangeHistoryAsync(object entityId)
        {
            try
            {
                if (entityId == null)
                    throw new ArgumentNullException(nameof(entityId));

                // Esta implementación básica retorna la entidad actual
                var entity = await _dbSet.FindAsync(entityId);
                if (entity == null)
                    return Enumerable.Empty<T>();

                return new List<T> { entity };
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener historial de cambios: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetNotModifiedSince(DateTime date)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var modifiedDateProperty = Expression.Property(parameter, "ModifiedDate");
                var dateOnly = date.Date;
                
                var lessThan = Expression.LessThan(modifiedDateProperty, Expression.Constant(dateOnly));
                var lambda = Expression.Lambda<Func<T, bool>>(lessThan, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades no modificadas desde fecha: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetNotModifiedSinceAsync(DateTime date)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var modifiedDateProperty = Expression.Property(parameter, "ModifiedDate");
                var dateOnly = date.Date;
                
                var lessThan = Expression.LessThan(modifiedDateProperty, Expression.Constant(dateOnly));
                var lambda = Expression.Lambda<Func<T, bool>>(lessThan, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades no modificadas desde fecha: {ex.Message}", ex);
            }
        }

        public virtual IEnumerable<T> GetCreatedButNeverModified()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var modifiedDateProperty = Expression.Property(parameter, "ModifiedDate");
                
                var isNull = Expression.Equal(modifiedDateProperty, Expression.Constant(null, typeof(DateTime?)));
                var lambda = Expression.Lambda<Func<T, bool>>(isNull, parameter);

                return _dbSet.Where(lambda).ToList();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error al obtener entidades creadas pero nunca modificadas: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetCreatedButNeverModifiedAsync()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "entity");
                var modifiedDateProperty = Expression.Property(parameter, "ModifiedDate");
                
                var isNull = Expression.Equal(modifiedDateProperty, Expression.Constant(null, typeof(DateTime?)));
                var lambda = Expression.Lambda<Func<T, bool>>(isNull, parameter);

                return await _dbSet.Where(lambda).ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implementar logging de errores
                throw new InvalidOperationException($"Error asíncrono al obtener entidades creadas pero nunca modificadas: {ex.Message}", ex);
            }
        }

        #endregion
    }
}
