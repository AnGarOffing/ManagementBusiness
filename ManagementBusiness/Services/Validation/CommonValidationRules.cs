using System.Text.RegularExpressions;

namespace ManagementBusiness.Services.Validation
{
    /// <summary>
    /// Regla de validación para campos requeridos
    /// </summary>
    public class RequiredValidationRule<T> : ValidationRuleBase<T>
    {
        public RequiredValidationRule(string propertyName, string? errorMessage = null)
            : base(propertyName, errorMessage ?? $"El campo {propertyName} es obligatorio")
        {
        }

        public override bool IsValid(T entity)
        {
            var value = GetPropertyValueAsString(entity, PropertyName);
            return !string.IsNullOrWhiteSpace(value);
        }
    }

    /// <summary>
    /// Regla de validación para longitud mínima
    /// </summary>
    public class MinLengthValidationRule<T> : ValidationRuleBase<T>
    {
        private readonly int _minLength;

        public MinLengthValidationRule(string propertyName, int minLength, string? errorMessage = null)
            : base(propertyName, errorMessage ?? $"El campo {propertyName} debe tener al menos {minLength} caracteres")
        {
            _minLength = minLength;
        }

        public override bool IsValid(T entity)
        {
            var value = GetPropertyValueAsString(entity, PropertyName);
            if (string.IsNullOrEmpty(value))
                return true; // Los campos vacíos son manejados por RequiredValidationRule

            return value.Length >= _minLength;
        }
    }

    /// <summary>
    /// Regla de validación para longitud máxima
    /// </summary>
    public class MaxLengthValidationRule<T> : ValidationRuleBase<T>
    {
        private readonly int _maxLength;

        public MaxLengthValidationRule(string propertyName, int maxLength, string? errorMessage = null)
            : base(propertyName, errorMessage ?? $"El campo {propertyName} no puede exceder {maxLength} caracteres")
        {
            _maxLength = maxLength;
        }

        public override bool IsValid(T entity)
        {
            var value = GetPropertyValueAsString(entity, PropertyName);
            if (string.IsNullOrEmpty(value))
                return true; // Los campos vacíos son manejados por RequiredValidationRule

            return value.Length <= _maxLength;
        }
    }

    /// <summary>
    /// Regla de validación para rango de longitud
    /// </summary>
    public class LengthRangeValidationRule<T> : ValidationRuleBase<T>
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public LengthRangeValidationRule(string propertyName, int minLength, int maxLength, string? errorMessage = null)
            : base(propertyName, errorMessage ?? $"El campo {propertyName} debe tener entre {minLength} y {maxLength} caracteres")
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public override bool IsValid(T entity)
        {
            var value = GetPropertyValueAsString(entity, PropertyName);
            if (string.IsNullOrEmpty(value))
                return true; // Los campos vacíos son manejados por RequiredValidationRule

            return value.Length >= _minLength && value.Length <= _maxLength;
        }
    }

    /// <summary>
    /// Regla de validación para formato de email
    /// </summary>
    public class EmailValidationRule<T> : ValidationRuleBase<T>
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public EmailValidationRule(string propertyName, string? errorMessage = null)
            : base(propertyName, errorMessage ?? $"El campo {propertyName} debe tener un formato de email válido")
        {
        }

        public override bool IsValid(T entity)
        {
            var value = GetPropertyValueAsString(entity, PropertyName);
            if (string.IsNullOrEmpty(value))
                return true; // Los campos vacíos son manejados por RequiredValidationRule

            return EmailRegex.IsMatch(value);
        }
    }

    /// <summary>
    /// Regla de validación para valores numéricos positivos
    /// </summary>
    public class PositiveNumberValidationRule<T> : ValidationRuleBase<T>
    {
        public PositiveNumberValidationRule(string propertyName, string? errorMessage = null)
            : base(propertyName, errorMessage ?? $"El campo {propertyName} debe ser un número positivo")
        {
        }

        public override bool IsValid(T entity)
        {
            var value = GetPropertyValueAsDecimal(entity, PropertyName);
            return value.HasValue && value.Value > 0;
        }
    }

    /// <summary>
    /// Regla de validación para valores numéricos no negativos
    /// </summary>
    public class NonNegativeNumberValidationRule<T> : ValidationRuleBase<T>
    {
        public NonNegativeNumberValidationRule(string propertyName, string? errorMessage = null)
            : base(propertyName, errorMessage ?? $"El campo {propertyName} debe ser un número no negativo")
        {
        }

        public override bool IsValid(T entity)
        {
            var value = GetPropertyValueAsDecimal(entity, PropertyName);
            return value.HasValue && value.Value >= 0;
        }
    }

    /// <summary>
    /// Regla de validación para rango de valores numéricos
    /// </summary>
    public class NumberRangeValidationRule<T> : ValidationRuleBase<T>
    {
        private readonly decimal _minValue;
        private readonly decimal _maxValue;

        public NumberRangeValidationRule(string propertyName, decimal minValue, decimal maxValue, string? errorMessage = null)
            : base(propertyName, errorMessage ?? $"El campo {propertyName} debe estar entre {minValue} y {maxValue}")
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public override bool IsValid(T entity)
        {
            var value = GetPropertyValueAsDecimal(entity, PropertyName);
            return value.HasValue && value.Value >= _minValue && value.Value <= _maxValue;
        }
    }

    /// <summary>
    /// Regla de validación para fechas futuras
    /// </summary>
    public class FutureDateValidationRule<T> : ValidationRuleBase<T>
    {
        public FutureDateValidationRule(string propertyName, string? errorMessage = null)
            : base(propertyName, errorMessage ?? $"El campo {propertyName} debe ser una fecha futura")
        {
        }

        public override bool IsValid(T entity)
        {
            var value = GetPropertyValueAsDateTime(entity, PropertyName);
            return value.HasValue && value.Value > DateTime.Now;
        }
    }

    /// <summary>
    /// Regla de validación para fechas pasadas
    /// </summary>
    public class PastDateValidationRule<T> : ValidationRuleBase<T>
    {
        public PastDateValidationRule(string propertyName, string? errorMessage = null)
            : base(propertyName, errorMessage ?? $"El campo {propertyName} debe ser una fecha pasada")
        {
        }

        public override bool IsValid(T entity)
        {
            var value = GetPropertyValueAsDateTime(entity, PropertyName);
            return value.HasValue && value.Value < DateTime.Now;
        }
    }

    /// <summary>
    /// Regla de validación para formato de teléfono
    /// </summary>
    public class PhoneValidationRule<T> : ValidationRuleBase<T>
    {
        private static readonly Regex PhoneRegex = new Regex(
            @"^[\+]?[0-9\s\-\(\)]{7,}$",
            RegexOptions.Compiled);

        public PhoneValidationRule(string propertyName, string? errorMessage = null)
            : base(propertyName, errorMessage ?? $"El campo {propertyName} debe tener un formato de teléfono válido")
        {
        }

        public override bool IsValid(T entity)
        {
            var value = GetPropertyValueAsString(entity, PropertyName);
            if (string.IsNullOrEmpty(value))
                return true; // Los campos vacíos son manejados por RequiredValidationRule

            return PhoneRegex.IsMatch(value);
        }
    }
}
