namespace ManagementBusiness.Services.Validation
{
    /// <summary>
    /// Representa un error de validación individual
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Nombre de la propiedad que falló la validación
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Mensaje de error descriptivo
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Código de error (opcional)
        /// </summary>
        public string? ErrorCode { get; }

        /// <summary>
        /// Valor que causó el error (opcional)
        /// </summary>
        public object? AttemptedValue { get; }

        /// <summary>
        /// Crea una nueva instancia de ValidationError
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <param name="errorMessage">Mensaje de error</param>
        public ValidationError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        }

        /// <summary>
        /// Crea una nueva instancia de ValidationError con código de error
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <param name="errorMessage">Mensaje de error</param>
        /// <param name="errorCode">Código de error</param>
        public ValidationError(string propertyName, string errorMessage, string errorCode)
            : this(propertyName, errorMessage)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Crea una nueva instancia de ValidationError con valor intentado
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <param name="errorMessage">Mensaje de error</param>
        /// <param name="attemptedValue">Valor que causó el error</param>
        public ValidationError(string propertyName, string errorMessage, object attemptedValue)
            : this(propertyName, errorMessage)
        {
            AttemptedValue = attemptedValue;
        }

        /// <summary>
        /// Crea una nueva instancia de ValidationError completa
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <param name="errorMessage">Mensaje de error</param>
        /// <param name="errorCode">Código de error</param>
        /// <param name="attemptedValue">Valor que causó el error</param>
        public ValidationError(string propertyName, string errorMessage, string errorCode, object attemptedValue)
            : this(propertyName, errorMessage, errorCode)
        {
            AttemptedValue = attemptedValue;
        }

        /// <summary>
        /// Representación en cadena del error
        /// </summary>
        /// <returns>Cadena representando el error</returns>
        public override string ToString()
        {
            return $"{PropertyName}: {ErrorMessage}";
        }
    }
}
