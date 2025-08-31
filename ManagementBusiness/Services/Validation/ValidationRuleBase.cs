namespace ManagementBusiness.Services.Validation
{
    /// <summary>
    /// Clase base para reglas de validación individuales
    /// </summary>
    /// <typeparam name="T">Tipo de entidad a validar</typeparam>
    public abstract class ValidationRuleBase<T> : IValidationRule<T>
    {
        public string RuleName { get; }
        public string ErrorMessage { get; }
        public string PropertyName { get; }

        protected ValidationRuleBase(string propertyName, string errorMessage, string? ruleName = null)
        {
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
            RuleName = ruleName ?? GetType().Name;
        }

        public abstract bool IsValid(T entity);

        public virtual Task<bool> IsValidAsync(T entity)
        {
            return Task.FromResult(IsValid(entity));
        }

        /// <summary>
        /// Obtiene el valor de una propiedad usando reflexión
        /// </summary>
        /// <param name="entity">Entidad</param>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <returns>Valor de la propiedad</returns>
        protected object? GetPropertyValue(T entity, string propertyName)
        {
            if (entity == null || string.IsNullOrEmpty(propertyName))
                return null;

            var property = typeof(T).GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            return property?.GetValue(entity);
        }

        /// <summary>
        /// Obtiene el valor de una propiedad como string
        /// </summary>
        /// <param name="entity">Entidad</param>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <returns>Valor de la propiedad como string</returns>
        protected string? GetPropertyValueAsString(T entity, string propertyName)
        {
            var value = GetPropertyValue(entity, propertyName);
            return value?.ToString();
        }

        /// <summary>
        /// Obtiene el valor de una propiedad como int
        /// </summary>
        /// <param name="entity">Entidad</param>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <returns>Valor de la propiedad como int</returns>
        protected int? GetPropertyValueAsInt(T entity, string propertyName)
        {
            var value = GetPropertyValue(entity, propertyName);
            if (value is int intValue)
                return intValue;
            if (int.TryParse(value?.ToString(), out var parsedValue))
                return parsedValue;
            return null;
        }

        /// <summary>
        /// Obtiene el valor de una propiedad como decimal
        /// </summary>
        /// <param name="entity">Entidad</param>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <returns>Valor de la propiedad como decimal</returns>
        protected decimal? GetPropertyValueAsDecimal(T entity, string propertyName)
        {
            var value = GetPropertyValue(entity, propertyName);
            if (value is decimal decimalValue)
                return decimalValue;
            if (decimal.TryParse(value?.ToString(), out var parsedValue))
                return parsedValue;
            return null;
        }

        /// <summary>
        /// Obtiene el valor de una propiedad como DateTime
        /// </summary>
        /// <param name="entity">Entidad</param>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <returns>Valor de la propiedad como DateTime</returns>
        protected DateTime? GetPropertyValueAsDateTime(T entity, string propertyName)
        {
            var value = GetPropertyValue(entity, propertyName);
            if (value is DateTime dateTimeValue)
                return dateTimeValue;
            if (DateTime.TryParse(value?.ToString(), out var parsedValue))
                return parsedValue;
            return null;
        }
    }
}
