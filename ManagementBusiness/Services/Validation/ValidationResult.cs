using System.Collections.Generic;
using System.Linq;

namespace ManagementBusiness.Services.Validation
{
    /// <summary>
    /// Resultado de una validación
    /// </summary>
    public class ValidationResult
    {
        private readonly List<ValidationError> _errors;

        public ValidationResult()
        {
            _errors = new List<ValidationError>();
        }

        /// <summary>
        /// Indica si la validación fue exitosa
        /// </summary>
        public bool IsValid => _errors.Count == 0;

        /// <summary>
        /// Lista de errores de validación
        /// </summary>
        public IReadOnlyList<ValidationError> Errors => _errors.AsReadOnly();

        /// <summary>
        /// Agrega un error de validación
        /// </summary>
        /// <param name="error">Error a agregar</param>
        public void AddError(ValidationError error)
        {
            if (error != null)
                _errors.Add(error);
        }

        /// <summary>
        /// Agrega un error de validación
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <param name="errorMessage">Mensaje de error</param>
        public void AddError(string propertyName, string errorMessage)
        {
            AddError(new ValidationError(propertyName, errorMessage));
        }

        /// <summary>
        /// Agrega múltiples errores de validación
        /// </summary>
        /// <param name="errors">Errores a agregar</param>
        public void AddErrors(IEnumerable<ValidationError> errors)
        {
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    AddError(error);
                }
            }
        }

        /// <summary>
        /// Combina este resultado con otro
        /// </summary>
        /// <param name="other">Otro resultado de validación</param>
        public void Merge(ValidationResult other)
        {
            if (other != null)
            {
                AddErrors(other.Errors);
            }
        }

        /// <summary>
        /// Limpia todos los errores
        /// </summary>
        public void Clear()
        {
            _errors.Clear();
        }

        /// <summary>
        /// Obtiene errores para una propiedad específica
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <returns>Lista de errores para la propiedad</returns>
        public IEnumerable<ValidationError> GetErrorsForProperty(string propertyName)
        {
            return _errors.Where(e => e.PropertyName == propertyName);
        }

        /// <summary>
        /// Obtiene el primer error para una propiedad específica
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <returns>Primer error encontrado o null</returns>
        public ValidationError? GetFirstErrorForProperty(string propertyName)
        {
            return _errors.FirstOrDefault(e => e.PropertyName == propertyName);
        }

        /// <summary>
        /// Obtiene todos los mensajes de error como una sola cadena
        /// </summary>
        /// <param name="separator">Separador entre mensajes</param>
        /// <returns>Cadena con todos los mensajes de error</returns>
        public string GetAllErrorMessages(string separator = "; ")
        {
            return string.Join(separator, _errors.Select(e => e.ErrorMessage));
        }
    }
}
