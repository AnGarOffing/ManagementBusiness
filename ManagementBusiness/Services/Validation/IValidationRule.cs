namespace ManagementBusiness.Services.Validation
{
    /// <summary>
    /// Interfaz para reglas de validación individuales
    /// </summary>
    /// <typeparam name="T">Tipo de entidad a validar</typeparam>
    public interface IValidationRule<T>
    {
        /// <summary>
        /// Nombre de la regla de validación
        /// </summary>
        string RuleName { get; }

        /// <summary>
        /// Mensaje de error para esta regla
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// Propiedad que se valida
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// Valida la entidad según esta regla
        /// </summary>
        /// <param name="entity">Entidad a validar</param>
        /// <returns>True si la validación es exitosa, false en caso contrario</returns>
        bool IsValid(T entity);

        /// <summary>
        /// Valida la entidad de forma asíncrona según esta regla
        /// </summary>
        /// <param name="entity">Entidad a validar</param>
        /// <returns>True si la validación es exitosa, false en caso contrario</returns>
        Task<bool> IsValidAsync(T entity);
    }
}
