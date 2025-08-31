using System.Collections.Generic;

namespace ManagementBusiness.Services.Validation
{
    /// <summary>
    /// Interfaz base para todos los validadores
    /// </summary>
    /// <typeparam name="T">Tipo de entidad a validar</typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Valida una entidad completa
        /// </summary>
        /// <param name="entity">Entidad a validar</param>
        /// <returns>Resultado de la validación</returns>
        ValidationResult Validate(T entity);

        /// <summary>
        /// Valida una entidad de forma asíncrona
        /// </summary>
        /// <param name="entity">Entidad a validar</param>
        /// <returns>Resultado de la validación</returns>
        Task<ValidationResult> ValidateAsync(T entity);

        /// <summary>
        /// Valida una propiedad específica
        /// </summary>
        /// <param name="entity">Entidad a validar</param>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <returns>Resultado de la validación</returns>
        ValidationResult ValidateProperty(T entity, string propertyName);

        /// <summary>
        /// Obtiene todas las reglas de validación
        /// </summary>
        /// <returns>Lista de reglas de validación</returns>
        IEnumerable<IValidationRule<T>> GetValidationRules();
    }
}
