using System.Collections.Generic;
using System.Reflection;

namespace ManagementBusiness.Services.Validation
{
    /// <summary>
    /// Clase base abstracta para todos los validadores
    /// </summary>
    /// <typeparam name="T">Tipo de entidad a validar</typeparam>
    public abstract class ValidatorBase<T> : IValidator<T>
    {
        protected readonly List<IValidationRule<T>> _validationRules;

        protected ValidatorBase()
        {
            _validationRules = new List<IValidationRule<T>>();
            ConfigureValidationRules();
        }

        /// <summary>
        /// Configura las reglas de validación para este validador
        /// </summary>
        protected abstract void ConfigureValidationRules();

        /// <summary>
        /// Agrega una regla de validación
        /// </summary>
        /// <param name="rule">Regla a agregar</param>
        protected void AddRule(IValidationRule<T> rule)
        {
            if (rule != null)
                _validationRules.Add(rule);
        }

        /// <summary>
        /// Agrega múltiples reglas de validación
        /// </summary>
        /// <param name="rules">Reglas a agregar</param>
        protected void AddRules(IEnumerable<IValidationRule<T>> rules)
        {
            if (rules != null)
            {
                foreach (var rule in rules)
                {
                    AddRule(rule);
                }
            }
        }

        public virtual ValidationResult Validate(T entity)
        {
            var result = new ValidationResult();

            if (entity == null)
            {
                result.AddError("Entity", "La entidad no puede ser nula");
                return result;
            }

            foreach (var rule in _validationRules)
            {
                if (!rule.IsValid(entity))
                {
                    result.AddError(rule.PropertyName, rule.ErrorMessage);
                }
            }

            return result;
        }

        public virtual async Task<ValidationResult> ValidateAsync(T entity)
        {
            var result = new ValidationResult();

            if (entity == null)
            {
                result.AddError("Entity", "La entidad no puede ser nula");
                return result;
            }

            foreach (var rule in _validationRules)
            {
                if (!await rule.IsValidAsync(entity))
                {
                    result.AddError(rule.PropertyName, rule.ErrorMessage);
                }
            }

            return result;
        }

        public virtual ValidationResult ValidateProperty(T entity, string propertyName)
        {
            var result = new ValidationResult();

            if (entity == null)
            {
                result.AddError("Entity", "La entidad no puede ser nula");
                return result;
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                result.AddError("PropertyName", "El nombre de la propiedad no puede estar vacío");
                return result;
            }

            var rulesForProperty = _validationRules.Where(r => r.PropertyName == propertyName);
            foreach (var rule in rulesForProperty)
            {
                if (!rule.IsValid(entity))
                {
                    result.AddError(rule.PropertyName, rule.ErrorMessage);
                }
            }

            return result;
        }

        public virtual IEnumerable<IValidationRule<T>> GetValidationRules()
        {
            return _validationRules.AsReadOnly();
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

            var property = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
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
    }
}
