using ManagementBusiness.Models;

namespace ManagementBusiness.Services.Validation
{
    /// <summary>
    /// Validador para la entidad Cliente
    /// </summary>
    public class ClienteValidator : ValidatorBase<Cliente>
    {
        protected override void ConfigureValidationRules()
        {
            // Reglas básicas de validación
            AddRule(new RequiredValidationRule<Cliente>("Nombre", "El nombre del cliente es obligatorio"));
            AddRule(new RequiredValidationRule<Cliente>("Apellido", "El apellido del cliente es obligatorio"));
            AddRule(new RequiredValidationRule<Cliente>("Email", "El email del cliente es obligatorio"));
            
            // Validaciones de longitud
            AddRule(new MinLengthValidationRule<Cliente>("Nombre", 2, "El nombre debe tener al menos 2 caracteres"));
            AddRule(new MaxLengthValidationRule<Cliente>("Nombre", 50, "El nombre no puede exceder 50 caracteres"));
            AddRule(new MinLengthValidationRule<Cliente>("Apellido", 2, "El apellido debe tener al menos 2 caracteres"));
            AddRule(new MaxLengthValidationRule<Cliente>("Apellido", 50, "El apellido no puede exceder 50 caracteres"));
            
            // Validación de formato de email
            AddRule(new EmailValidationRule<Cliente>("Email"));
            
            // Validaciones opcionales (solo si tienen valor)
            AddRule(new PhoneValidationRule<Cliente>("Telefono"));
            
            // Validaciones de longitud para campos opcionales
            AddRule(new MaxLengthValidationRule<Cliente>("Direccion", 200, "La dirección no puede exceder 200 caracteres"));
            AddRule(new MaxLengthValidationRule<Cliente>("Ciudad", 100, "La ciudad no puede exceder 100 caracteres"));
            AddRule(new MaxLengthValidationRule<Cliente>("CodigoPostal", 10, "El código postal no puede exceder 10 caracteres"));
            
            // Validación personalizada para DNI/CUIT
            AddRule(new DniCuitValidationRule());
        }
    }

    /// <summary>
    /// Regla de validación personalizada para DNI/CUIT
    /// </summary>
    public class DniCuitValidationRule : ValidationRuleBase<Cliente>
    {
        public DniCuitValidationRule() 
            : base("DniCuit", "El DNI/CUIT debe tener un formato válido")
        {
        }

        public override bool IsValid(Cliente entity)
        {
            var dniCuit = GetPropertyValueAsString(entity, PropertyName);
            if (string.IsNullOrEmpty(dniCuit))
                return true; // Campo opcional

            // Validar formato básico (solo números, entre 7 y 11 dígitos)
            if (!System.Text.RegularExpressions.Regex.IsMatch(dniCuit, @"^\d{7,11}$"))
                return false;

            return true;
        }
    }
}
