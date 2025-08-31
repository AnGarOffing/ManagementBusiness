using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ManagementBusiness.Services.Helpers
{
    /// <summary>
    /// Extensiones útiles para Entity Framework
    /// </summary>
    public static class EntityFrameworkExtensions
    {
        /// <summary>
        /// Incluye entidades relacionadas solo si la condición es verdadera
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <typeparam name="TProperty">Tipo de propiedad de navegación</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="condition">Condición para incluir</param>
        /// <param name="navigationPropertyPath">Propiedad de navegación a incluir</param>
        /// <returns>Query con include condicional</returns>
        public static IQueryable<T> IncludeIf<T, TProperty>(
            this IQueryable<T> query,
            bool condition,
            Expression<Func<T, TProperty>> navigationPropertyPath) where T : class
        {
            return condition ? query.Include(navigationPropertyPath) : query;
        }

        /// <summary>
        /// Incluye múltiples entidades relacionadas solo si la condición es verdadera
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="condition">Condición para incluir</param>
        /// <param name="navigationPropertyPaths">Propiedades de navegación a incluir</param>
        /// <returns>Query con includes condicionales</returns>
        public static IQueryable<T> IncludeIf<T>(
            this IQueryable<T> query,
            bool condition,
            params Expression<Func<T, object>>[] navigationPropertyPaths) where T : class
        {
            if (!condition) return query;

            foreach (var navigationPropertyPath in navigationPropertyPaths)
            {
                query = query.Include(navigationPropertyPath);
            }

            return query;
        }

        /// <summary>
        /// Aplica un filtro solo si la condición es verdadera
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="condition">Condición para aplicar el filtro</param>
        /// <param name="predicate">Predicado del filtro</param>
        /// <returns>Query con filtro condicional</returns>
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> query,
            bool condition,
            Expression<Func<T, bool>> predicate) where T : class
        {
            return condition ? query.Where(predicate) : query;
        }

        /// <summary>
        /// Aplica un filtro de búsqueda de texto solo si el término de búsqueda no está vacío
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="searchTerm">Término de búsqueda</param>
        /// <param name="predicate">Predicado de búsqueda</param>
        /// <returns>Query con filtro de búsqueda</returns>
        public static IQueryable<T> SearchIf<T>(
            this IQueryable<T> query,
            string? searchTerm,
            Expression<Func<T, bool>> predicate) where T : class
        {
            return !string.IsNullOrWhiteSpace(searchTerm) ? query.Where(predicate) : query;
        }

        /// <summary>
        /// Aplica ordenamiento solo si la condición es verdadera
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <typeparam name="TKey">Tipo de clave de ordenamiento</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="condition">Condición para aplicar el ordenamiento</param>
        /// <param name="keySelector">Selector de clave</param>
        /// <param name="ascending">True para ordenamiento ascendente, false para descendente</param>
        /// <returns>Query con ordenamiento condicional</returns>
        public static IQueryable<T> OrderByIf<T, TKey>(
            this IQueryable<T> query,
            bool condition,
            Expression<Func<T, TKey>> keySelector,
            bool ascending = true) where T : class
        {
            if (!condition) return query;

            return ascending ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);
        }

        /// <summary>
        /// Aplica paginación a la query
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="pageNumber">Número de página (base 1)</param>
        /// <param name="pageSize">Tamaño de página</param>
        /// <returns>Query paginada</returns>
        public static IQueryable<T> Paginate<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize) where T : class
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Aplica paginación solo si se especifica
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="pageNumber">Número de página (base 1)</param>
        /// <param name="pageSize">Tamaño de página</param>
        /// <returns>Query paginada si se especifica</returns>
        public static IQueryable<T> PaginateIf<T>(
            this IQueryable<T> query,
            int? pageNumber,
            int? pageSize) where T : class
        {
            if (!pageNumber.HasValue || !pageSize.HasValue || pageNumber.Value < 1 || pageSize.Value < 1)
                return query;

            return query.Paginate(pageNumber.Value, pageSize.Value);
        }

        /// <summary>
        /// Aplica un filtro de rango de fechas
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha de fin</param>
        /// <param name="dateSelector">Selector de fecha</param>
        /// <returns>Query con filtro de rango de fechas</returns>
        public static IQueryable<T> WhereDateRange<T>(
            this IQueryable<T> query,
            DateTime? startDate,
            DateTime? endDate,
            Expression<Func<T, DateTime>> dateSelector) where T : class
        {
            if (startDate.HasValue)
            {
                query = query.Where(entity => dateSelector.Compile()(entity) >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(entity => dateSelector.Compile()(entity) <= endDate.Value);
            }

            return query;
        }

        /// <summary>
        /// Aplica un filtro de rango de fechas usando expresiones
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha de fin</param>
        /// <param name="dateSelector">Selector de fecha</param>
        /// <returns>Query con filtro de rango de fechas</returns>
        public static IQueryable<T> WhereDateRangeExpression<T>(
            this IQueryable<T> query,
            DateTime? startDate,
            DateTime? endDate,
            Expression<Func<T, DateTime>> dateSelector) where T : class
        {
            if (startDate.HasValue)
            {
                var startDateParam = Expression.Constant(startDate.Value);
                var startDateComparison = Expression.GreaterThanOrEqual(dateSelector, startDateParam);
                var startDateLambda = Expression.Lambda<Func<T, bool>>(startDateComparison, dateSelector.Parameters);
                query = query.Where(startDateLambda);
            }

            if (endDate.HasValue)
            {
                var endDateParam = Expression.Constant(endDate.Value);
                var endDateComparison = Expression.LessThanOrEqual(dateSelector, endDateParam);
                var endDateLambda = Expression.Lambda<Func<T, bool>>(endDateComparison, dateSelector.Parameters);
                query = query.Where(endDateLambda);
            }

            return query;
        }

        /// <summary>
        /// Aplica un filtro de búsqueda en múltiples propiedades de texto
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="searchTerm">Término de búsqueda</param>
        /// <param name="stringProperties">Propiedades de string a buscar</param>
        /// <returns>Query con filtro de búsqueda en múltiples propiedades</returns>
        public static IQueryable<T> SearchInProperties<T>(
            this IQueryable<T> query,
            string? searchTerm,
            params Expression<Func<T, string>>[] stringProperties) where T : class
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || !stringProperties.Any())
                return query;

            var parameter = Expression.Parameter(typeof(T), "entity");
            var searchTermConstant = Expression.Constant(searchTerm.ToLower());

            var propertyChecks = stringProperties.Select(propertySelector =>
            {
                var property = propertySelector.Body;
                var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                var toLowerCall = Expression.Call(property, toLowerMethod);
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var containsCall = Expression.Call(toLowerCall, containsMethod, searchTermConstant);
                return containsCall;
            });

            var propertyChecksArray = propertyChecks.ToArray();
            Expression combinedChecks = propertyChecksArray[0];
            for (int i = 1; i < propertyChecksArray.Length; i++)
            {
                combinedChecks = Expression.OrElse(combinedChecks, propertyChecksArray[i]);
            }

            var lambda = Expression.Lambda<Func<T, bool>>(combinedChecks, parameter);
            return query.Where(lambda);
        }

        /// <summary>
        /// Aplica un filtro de estado activo/inactivo
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="isActive">Estado activo a filtrar</param>
        /// <param name="activePropertySelector">Selector de la propiedad de estado activo</param>
        /// <returns>Query con filtro de estado activo</returns>
        public static IQueryable<T> WhereActive<T>(
            this IQueryable<T> query,
            bool isActive,
            Expression<Func<T, bool>> activePropertySelector) where T : class
        {
            return query.Where(activePropertySelector);
        }

        /// <summary>
        /// Aplica un filtro de soft delete
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="deletedPropertySelector">Selector de la propiedad de eliminado</param>
        /// <returns>Query que excluye entidades eliminadas</returns>
        public static IQueryable<T> WhereNotDeleted<T>(
            this IQueryable<T> query,
            Expression<Func<T, bool>> deletedPropertySelector) where T : class
        {
            var parameter = Expression.Parameter(typeof(T), "entity");
            var deletedProperty = deletedPropertySelector.Body;
            var falseConstant = Expression.Constant(false);
            var notDeletedCheck = Expression.Equal(deletedProperty, falseConstant);
            var lambda = Expression.Lambda<Func<T, bool>>(notDeletedCheck, parameter);
            
            return query.Where(lambda);
        }

        /// <summary>
        /// Aplica un filtro de auditoría por usuario
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="userId">ID del usuario</param>
        /// <param name="createdByPropertySelector">Selector de la propiedad de creado por</param>
        /// <returns>Query filtrada por usuario creador</returns>
        public static IQueryable<T> WhereCreatedBy<T>(
            this IQueryable<T> query,
            string userId,
            Expression<Func<T, string>> createdByPropertySelector) where T : class
        {
            var parameter = Expression.Parameter(typeof(T), "entity");
            var createdByProperty = createdByPropertySelector.Body;
            var userIdConstant = Expression.Constant(userId);
            var createdByCheck = Expression.Equal(createdByProperty, userIdConstant);
            var lambda = Expression.Lambda<Func<T, bool>>(createdByCheck, parameter);
            
            return query.Where(lambda);
        }

        /// <summary>
        /// Aplica un filtro de rango numérico
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <typeparam name="TValue">Tipo de valor numérico</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="minValue">Valor mínimo</param>
        /// <param name="maxValue">Valor máximo</param>
        /// <param name="valueSelector">Selector de valor</param>
        /// <returns>Query con filtro de rango numérico</returns>
        public static IQueryable<T> WhereNumericRange<T, TValue>(
            this IQueryable<T> query,
            TValue? minValue,
            TValue? maxValue,
            Expression<Func<T, TValue>> valueSelector) where T : class where TValue : struct, IComparable<TValue>
        {
            if (minValue.HasValue)
            {
                var minValueConstant = Expression.Constant(minValue.Value);
                var minValueComparison = Expression.GreaterThanOrEqual(valueSelector.Body, minValueConstant);
                var minValueLambda = Expression.Lambda<Func<T, bool>>(minValueComparison, valueSelector.Parameters);
                query = query.Where(minValueLambda);
            }

            if (maxValue.HasValue)
            {
                var maxValueConstant = Expression.Constant(maxValue.Value);
                var maxValueComparison = Expression.LessThanOrEqual(valueSelector.Body, maxValueConstant);
                var maxValueLambda = Expression.Lambda<Func<T, bool>>(maxValueComparison, valueSelector.Parameters);
                query = query.Where(maxValueLambda);
            }

            return query;
        }

        /// <summary>
        /// Aplica un filtro de lista de valores
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <typeparam name="TValue">Tipo de valor</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="values">Lista de valores a filtrar</param>
        /// <param name="valueSelector">Selector de valor</param>
        /// <returns>Query con filtro de lista de valores</returns>
        public static IQueryable<T> WhereIn<T, TValue>(
            this IQueryable<T> query,
            IEnumerable<TValue> values,
            Expression<Func<T, TValue>> valueSelector) where T : class
        {
            if (values == null || !values.Any())
                return query;

            var parameter = Expression.Parameter(typeof(T), "entity");
            var property = valueSelector.Body;
            var valuesConstant = Expression.Constant(values);
            var containsMethod = typeof(Enumerable).GetMethods()
                .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(TValue));
            var containsCall = Expression.Call(containsMethod, valuesConstant, property);
            var lambda = Expression.Lambda<Func<T, bool>>(containsCall, parameter);
            
            return query.Where(lambda);
        }

        /// <summary>
        /// Aplica un filtro de texto que comience con un prefijo
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="prefix">Prefijo a buscar</param>
        /// <param name="stringPropertySelector">Selector de propiedad de string</param>
        /// <returns>Query con filtro de prefijo</returns>
        public static IQueryable<T> WhereStartsWith<T>(
            this IQueryable<T> query,
            string? prefix,
            Expression<Func<T, string>> stringPropertySelector) where T : class
        {
            if (string.IsNullOrWhiteSpace(prefix))
                return query;

            var parameter = Expression.Parameter(typeof(T), "entity");
            var property = stringPropertySelector.Body;
            var prefixConstant = Expression.Constant(prefix);
            var startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
            var startsWithCall = Expression.Call(property, startsWithMethod, prefixConstant);
            var lambda = Expression.Lambda<Func<T, bool>>(startsWithCall, parameter);
            
            return query.Where(lambda);
        }

        /// <summary>
        /// Aplica un filtro de texto que termine con un sufijo
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Query base</param>
        /// <param name="suffix">Sufijo a buscar</param>
        /// <param name="stringPropertySelector">Selector de propiedad de string</param>
        /// <returns>Query con filtro de sufijo</returns>
        public static IQueryable<T> WhereEndsWith<T>(
            this IQueryable<T> query,
            string? suffix,
            Expression<Func<T, string>> stringPropertySelector) where T : class
        {
            if (string.IsNullOrWhiteSpace(suffix))
                return query;

            var parameter = Expression.Parameter(typeof(T), "entity");
            var property = stringPropertySelector.Body;
            var suffixConstant = Expression.Constant(suffix);
            var endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
            var endsWithCall = Expression.Call(property, endsWithMethod, suffixConstant);
            var lambda = Expression.Lambda<Func<T, bool>>(endsWithCall, parameter);
            
            return query.Where(lambda);
        }
    }
}
