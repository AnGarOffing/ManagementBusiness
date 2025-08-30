using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Interfaz para repositorios que manejan entidades con auditoría
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que maneja el repositorio</typeparam>
    public interface IRepositoryWithAudit<T> : IRepository<T> where T : class
    {
        #region Operaciones de Auditoría por Fecha

        /// <summary>
        /// Obtiene entidades creadas en una fecha específica
        /// </summary>
        /// <param name="date">Fecha de creación</param>
        /// <returns>Colección de entidades creadas en la fecha especificada</returns>
        IEnumerable<T> GetByCreatedDate(DateTime date);

        /// <summary>
        /// Obtiene entidades creadas en una fecha específica de forma asíncrona
        /// </summary>
        /// <param name="date">Fecha de creación</param>
        /// <returns>Colección de entidades creadas en la fecha especificada</returns>
        Task<IEnumerable<T>> GetByCreatedDateAsync(DateTime date);

        /// <summary>
        /// Obtiene entidades creadas en un rango de fechas
        /// </summary>
        /// <param name="startDate">Fecha de inicio del rango (inclusive)</param>
        /// <param name="endDate">Fecha de fin del rango (inclusive)</param>
        /// <returns>Colección de entidades creadas en el rango de fechas</returns>
        IEnumerable<T> GetByCreatedDateRange(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Obtiene entidades creadas en un rango de fechas de forma asíncrona
        /// </summary>
        /// <param name="startDate">Fecha de inicio del rango (inclusive)</param>
        /// <param name="endDate">Fecha de fin del rango (inclusive)</param>
        /// <returns>Colección de entidades creadas en el rango de fechas</returns>
        Task<IEnumerable<T>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Obtiene entidades modificadas en una fecha específica
        /// </summary>
        /// <param name="date">Fecha de modificación</param>
        /// <returns>Colección de entidades modificadas en la fecha especificada</returns>
        IEnumerable<T> GetByModifiedDate(DateTime date);

        /// <summary>
        /// Obtiene entidades modificadas en una fecha específica de forma asíncrona
        /// </summary>
        /// <param name="date">Fecha de modificación</param>
        /// <returns>Colección de entidades modificadas en la fecha especificada</returns>
        Task<IEnumerable<T>> GetByModifiedDateAsync(DateTime date);

        /// <summary>
        /// Obtiene entidades modificadas en un rango de fechas
        /// </summary>
        /// <param name="startDate">Fecha de inicio del rango (inclusive)</param>
        /// <param name="endDate">Fecha de fin del rango (inclusive)</param>
        /// <returns>Colección de entidades modificadas en el rango de fechas</returns>
        IEnumerable<T> GetByModifiedDateRange(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Obtiene entidades modificadas en un rango de fechas de forma asíncrona
        /// </summary>
        /// <param name="startDate">Fecha de inicio del rango (inclusive)</param>
        /// <param name="endDate">Fecha de fin del rango (inclusive)</param>
        /// <returns>Colección de entidades modificadas en el rango de fechas</returns>
        Task<IEnumerable<T>> GetByModifiedDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Obtiene entidades creadas hoy
        /// </summary>
        /// <returns>Colección de entidades creadas hoy</returns>
        IEnumerable<T> GetCreatedToday();

        /// <summary>
        /// Obtiene entidades creadas hoy de forma asíncrona
        /// </summary>
        /// <returns>Colección de entidades creadas hoy</returns>
        Task<IEnumerable<T>> GetCreatedTodayAsync();

        /// <summary>
        /// Obtiene entidades modificadas hoy
        /// </summary>
        /// <returns>Colección de entidades modificadas hoy</returns>
        IEnumerable<T> GetModifiedToday();

        /// <summary>
        /// Obtiene entidades modificadas hoy de forma asíncrona
        /// </summary>
        /// <returns>Colección de entidades modificadas hoy</returns>
        Task<IEnumerable<T>> GetModifiedTodayAsync();

        /// <summary>
        /// Obtiene entidades creadas en la última semana
        /// </summary>
        /// <returns>Colección de entidades creadas en la última semana</returns>
        IEnumerable<T> GetCreatedLastWeek();

        /// <summary>
        /// Obtiene entidades creadas en la última semana de forma asíncrona
        /// </summary>
        /// <returns>Colección de entidades creadas en la última semana</returns>
        Task<IEnumerable<T>> GetCreatedLastWeekAsync();

        /// <summary>
        /// Obtiene entidades modificadas en la última semana
        /// </summary>
        /// <returns>Colección de entidades modificadas en la última semana</returns>
        IEnumerable<T> GetModifiedLastWeek();

        /// <summary>
        /// Obtiene entidades modificadas en la última semana de forma asíncrona
        /// </summary>
        /// <returns>Colección de entidades modificadas en la última semana</returns>
        Task<IEnumerable<T>> GetModifiedLastWeekAsync();

        /// <summary>
        /// Obtiene entidades creadas en el último mes
        /// </summary>
        /// <returns>Colección de entidades creadas en el último mes</returns>
        IEnumerable<T> GetCreatedLastMonth();

        /// <summary>
        /// Obtiene entidades creadas en el último mes de forma asíncrona
        /// </summary>
        /// <returns>Colección de entidades creadas en el último mes</returns>
        Task<IEnumerable<T>> GetCreatedLastMonthAsync();

        /// <summary>
        /// Obtiene entidades modificadas en el último mes
        /// </summary>
        /// <returns>Colección de entidades modificadas en el último mes</returns>
        IEnumerable<T> GetModifiedLastMonth();

        /// <summary>
        /// Obtiene entidades modificadas en el último mes de forma asíncrona
        /// </summary>
        /// <returns>Colección de entidades modificadas en el último mes</returns>
        Task<IEnumerable<T>> GetModifiedLastMonthAsync();

        #endregion

        #region Operaciones de Auditoría por Usuario

        /// <summary>
        /// Obtiene entidades creadas por un usuario específico
        /// </summary>
        /// <param name="createdBy">Usuario que creó las entidades</param>
        /// <returns>Colección de entidades creadas por el usuario especificado</returns>
        IEnumerable<T> GetByCreatedBy(string createdBy);

        /// <summary>
        /// Obtiene entidades creadas por un usuario específico de forma asíncrona
        /// </summary>
        /// <param name="createdBy">Usuario que creó las entidades</param>
        /// <returns>Colección de entidades creadas por el usuario especificado</returns>
        Task<IEnumerable<T>> GetByCreatedByAsync(string createdBy);

        /// <summary>
        /// Obtiene entidades modificadas por un usuario específico
        /// </summary>
        /// <param name="modifiedBy">Usuario que modificó las entidades</param>
        /// <returns>Colección de entidades modificadas por el usuario especificado</returns>
        IEnumerable<T> GetByModifiedBy(string modifiedBy);

        /// <summary>
        /// Obtiene entidades modificadas por un usuario específico de forma asíncrona
        /// </summary>
        /// <param name="modifiedBy">Usuario que modificó las entidades</param>
        /// <returns>Colección de entidades modificadas por el usuario especificado</returns>
        Task<IEnumerable<T>> GetByModifiedByAsync(string modifiedBy);

        /// <summary>
        /// Obtiene entidades creadas por un usuario en un rango de fechas
        /// </summary>
        /// <param name="createdBy">Usuario que creó las entidades</param>
        /// <param name="startDate">Fecha de inicio del rango (inclusive)</param>
        /// <param name="endDate">Fecha de fin del rango (inclusive)</param>
        /// <returns>Colección de entidades creadas por el usuario en el rango de fechas</returns>
        IEnumerable<T> GetByCreatedByAndDateRange(string createdBy, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Obtiene entidades creadas por un usuario en un rango de fechas de forma asíncrona
        /// </summary>
        /// <param name="createdBy">Usuario que creó las entidades</param>
        /// <param name="startDate">Fecha de inicio del rango (inclusive)</param>
        /// <param name="endDate">Fecha de fin del rango (inclusive)</param>
        /// <returns>Colección de entidades creadas por el usuario en el rango de fechas</returns>
        Task<IEnumerable<T>> GetByCreatedByAndDateRangeAsync(string createdBy, DateTime startDate, DateTime endDate);

        #endregion

        #region Operaciones de Conteo de Auditoría

        /// <summary>
        /// Cuenta entidades creadas en una fecha específica
        /// </summary>
        /// <param name="date">Fecha de creación</param>
        /// <returns>Número de entidades creadas en la fecha especificada</returns>
        int CountByCreatedDate(DateTime date);

        /// <summary>
        /// Cuenta entidades creadas en una fecha específica de forma asíncrona
        /// </summary>
        /// <param name="date">Fecha de creación</param>
        /// <returns>Número de entidades creadas en la fecha especificada</returns>
        Task<int> CountByCreatedDateAsync(DateTime date);

        /// <summary>
        /// Cuenta entidades creadas en un rango de fechas
        /// </summary>
        /// <param name="startDate">Fecha de inicio del rango (inclusive)</param>
        /// <param name="endDate">Fecha de fin del rango (inclusive)</param>
        /// <returns>Número de entidades creadas en el rango de fechas</returns>
        int CountByCreatedDateRange(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Cuenta entidades creadas en un rango de fechas de forma asíncrona
        /// </summary>
        /// <param name="startDate">Fecha de inicio del rango (inclusive)</param>
        /// <param name="endDate">Fecha de fin del rango (inclusive)</param>
        /// <returns>Número de entidades creadas en el rango de fechas</returns>
        Task<int> CountByCreatedDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Cuenta entidades creadas por un usuario específico
        /// </summary>
        /// <param name="createdBy">Usuario que creó las entidades</param>
        /// <returns>Número de entidades creadas por el usuario especificado</returns>
        int CountByCreatedBy(string createdBy);

        /// <summary>
        /// Cuenta entidades creadas por un usuario específico de forma asíncrona
        /// </summary>
        /// <param name="createdBy">Usuario que creó las entidades</param>
        /// <returns>Número de entidades creadas por el usuario especificado</returns>
        Task<int> CountByCreatedByAsync(string createdBy);

        /// <summary>
        /// Cuenta entidades modificadas por un usuario específico
        /// </summary>
        /// <param name="modifiedBy">Usuario que modificó las entidades</param>
        /// <returns>Número de entidades modificadas por el usuario especificado</returns>
        int CountByModifiedBy(string modifiedBy);

        /// <summary>
        /// Cuenta entidades modificadas por un usuario específico de forma asíncrona
        /// </summary>
        /// <param name="modifiedBy">Usuario que modificó las entidades</param>
        /// <returns>Número de entidades modificadas por el usuario especificado</returns>
        Task<int> CountByModifiedByAsync(string modifiedBy);

        #endregion

        #region Operaciones de Auditoría Avanzadas

        /// <summary>
        /// Obtiene el historial de cambios de una entidad específica
        /// </summary>
        /// <param name="entityId">ID de la entidad</param>
        /// <returns>Historial de cambios de la entidad</returns>
        IEnumerable<T> GetChangeHistory(object entityId);

        /// <summary>
        /// Obtiene el historial de cambios de una entidad específica de forma asíncrona
        /// </summary>
        /// <param name="entityId">ID de la entidad</param>
        /// <returns>Historial de cambios de la entidad</returns>
        Task<IEnumerable<T>> GetChangeHistoryAsync(object entityId);

        /// <summary>
        /// Obtiene entidades que no han sido modificadas desde una fecha específica
        /// </summary>
        /// <param name="date">Fecha de referencia</param>
        /// <returns>Colección de entidades no modificadas desde la fecha especificada</returns>
        IEnumerable<T> GetNotModifiedSince(DateTime date);

        /// <summary>
        /// Obtiene entidades que no han sido modificadas desde una fecha específica de forma asíncrona
        /// </summary>
        /// <param name="date">Fecha de referencia</param>
        /// <returns>Colección de entidades no modificadas desde la fecha especificada</returns>
        Task<IEnumerable<T>> GetNotModifiedSinceAsync(DateTime date);

        /// <summary>
        /// Obtiene entidades creadas pero nunca modificadas
        /// </summary>
        /// <returns>Colección de entidades creadas pero nunca modificadas</returns>
        IEnumerable<T> GetCreatedButNeverModified();

        /// <summary>
        /// Obtiene entidades creadas pero nunca modificadas de forma asíncrona
        /// </summary>
        /// <returns>Colección de entidades creadas pero nunca modificadas</returns>
        Task<IEnumerable<T>> GetCreatedButNeverModifiedAsync();

        #endregion
    }
}
