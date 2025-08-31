using ManagementBusiness.Models;

namespace ManagementBusiness.Services.Validation
{
    /// <summary>
    /// Validador para la entidad Producto
    /// </summary>
    public class ProductoValidator : ValidatorBase<Producto>
    {
        protected override void ConfigureValidationRules()
        {
            // Reglas básicas de validación
            AddRule(new RequiredValidationRule<Producto>("Nombre", "El nombre del producto es obligatorio"));
            AddRule(new RequiredValidationRule<Producto>("Descripcion", "La descripción del producto es obligatoria"));
            AddRule(new RequiredValidationRule<Producto>("Precio", "El precio del producto es obligatorio"));
            
            // Validaciones de longitud
            AddRule(new MinLengthValidationRule<Producto>("Nombre", 3, "El nombre debe tener al menos 3 caracteres"));
            AddRule(new MaxLengthValidationRule<Producto>("Nombre", 100, "El nombre no puede exceder 100 caracteres"));
            AddRule(new MinLengthValidationRule<Producto>("Descripcion", 10, "La descripción debe tener al menos 10 caracteres"));
            AddRule(new MaxLengthValidationRule<Producto>("Descripcion", 500, "La descripción no puede exceder 500 caracteres"));
            
            // Validaciones numéricas
            AddRule(new PositiveNumberValidationRule<Producto>("Precio", "El precio debe ser mayor a 0"));
            AddRule(new NonNegativeNumberValidationRule<Producto>("Stock", "El stock no puede ser negativo"));
            AddRule(new NonNegativeNumberValidationRule<Producto>("StockMinimo", "El stock mínimo no puede ser negativo"));
            
            // Validaciones de rango
            AddRule(new NumberRangeValidationRule<Producto>("Precio", 0.01m, 999999.99m, "El precio debe estar entre $0.01 y $999,999.99"));
            AddRule(new NumberRangeValidationRule<Producto>("Stock", 0, 999999, "El stock debe estar entre 0 y 999,999"));
            AddRule(new NumberRangeValidationRule<Producto>("StockMinimo", 0, 999999, "El stock mínimo debe estar entre 0 y 999,999"));
            
            // Validaciones opcionales
            AddRule(new MaxLengthValidationRule<Producto>("Codigo", 50, "El código no puede exceder 50 caracteres"));
            AddRule(new MaxLengthValidationRule<Producto>("Marca", 100, "La marca no puede exceder 100 caracteres"));
            AddRule(new MaxLengthValidationRule<Producto>("Modelo", 100, "El modelo no puede exceder 100 caracteres"));
            
            // Validación personalizada para código de barras
            AddRule(new CodigoBarrasValidationRule());
        }
    }

    /// <summary>
    /// Regla de validación personalizada para código de barras
    /// </summary>
    public class CodigoBarrasValidationRule : ValidationRuleBase<Producto>
    {
        public CodigoBarrasValidationRule() 
            : base("CodigoBarras", "El código de barras debe tener un formato válido")
        {
        }

        public override bool IsValid(Producto entity)
        {
            var codigoBarras = GetPropertyValueAsString(entity, PropertyName);
            if (string.IsNullOrEmpty(codigoBarras))
                return true; // Campo opcional

            // Validar formato básico (solo números, entre 8 y 13 dígitos)
            if (!System.Text.RegularExpressions.Regex.IsMatch(codigoBarras, @"^\d{8,13}$"))
                return false;

            return true;
        }
    }
}
