using System.Text.RegularExpressions;

namespace ManagementBusiness.Services.Helpers
{
    /// <summary>
    /// Helper para validaciones comunes de datos
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Valida si un string no está vacío o es null
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si el valor es válido</returns>
        public static bool IsNotNullOrEmpty(string? value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Valida si un string no está vacío, es null o solo contiene espacios en blanco
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si el valor es válido</returns>
        public static bool IsNotNullOrWhiteSpace(string? value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Valida si un string tiene una longitud mínima
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <param name="minLength">Longitud mínima requerida</param>
        /// <returns>True si el valor es válido</returns>
        public static bool HasMinLength(string? value, int minLength)
        {
            return value?.Length >= minLength;
        }

        /// <summary>
        /// Valida si un string tiene una longitud máxima
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <param name="maxLength">Longitud máxima permitida</param>
        /// <returns>True si el valor es válido</returns>
        public static bool HasMaxLength(string? value, int maxLength)
        {
            return value?.Length <= maxLength;
        }

        /// <summary>
        /// Valida si un string tiene una longitud específica
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <param name="exactLength">Longitud exacta requerida</param>
        /// <returns>True si el valor es válido</returns>
        public static bool HasExactLength(string? value, int exactLength)
        {
            return value?.Length == exactLength;
        }

        /// <summary>
        /// Valida si un string tiene una longitud dentro de un rango
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <param name="minLength">Longitud mínima</param>
        /// <param name="maxLength">Longitud máxima</param>
        /// <returns>True si el valor es válido</returns>
        public static bool HasLengthInRange(string? value, int minLength, int maxLength)
        {
            if (value == null) return false;
            return value.Length >= minLength && value.Length <= maxLength;
        }

        /// <summary>
        /// Valida si un string es un email válido
        /// </summary>
        /// <param name="email">Email a validar</param>
        /// <returns>True si el email es válido</returns>
        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Valida si un string es una URL válida
        /// </summary>
        /// <param name="url">URL a validar</param>
        /// <returns>True si la URL es válida</returns>
        public static bool IsValidUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }

        /// <summary>
        /// Valida si un string es un número de teléfono válido
        /// </summary>
        /// <param name="phoneNumber">Número de teléfono a validar</param>
        /// <returns>True si el número es válido</returns>
        public static bool IsValidPhoneNumber(string? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Remover todos los caracteres no numéricos
            var digits = Regex.Replace(phoneNumber, @"[^\d]", "");
            
            // Validar que tenga entre 8 y 15 dígitos
            return digits.Length >= 8 && digits.Length <= 15;
        }

        /// <summary>
        /// Valida si un string es un DNI válido (7 dígitos)
        /// </summary>
        /// <param name="dni">DNI a validar</param>
        /// <returns>True si el DNI es válido</returns>
        public static bool IsValidDni(string? dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return false;

            var digits = Regex.Replace(dni, @"[^\d]", "");
            return digits.Length == 7;
        }

        /// <summary>
        /// Valida si un string es un CUIT válido (11 dígitos)
        /// </summary>
        /// <param name="cuit">CUIT a validar</param>
        /// <returns>True si el CUIT es válido</returns>
        public static bool IsValidCuit(string? cuit)
        {
            if (string.IsNullOrWhiteSpace(cuit))
                return false;

            var digits = Regex.Replace(cuit, @"[^\d]", "");
            return digits.Length == 11;
        }

        /// <summary>
        /// Valida si un string es un DNI o CUIT válido
        /// </summary>
        /// <param name="dniCuit">DNI o CUIT a validar</param>
        /// <returns>True si es válido</returns>
        public static bool IsValidDniCuit(string? dniCuit)
        {
            return IsValidDni(dniCuit) || IsValidCuit(dniCuit);
        }

        /// <summary>
        /// Valida si un string es un código de barras válido
        /// </summary>
        /// <param name="barcode">Código de barras a validar</param>
        /// <returns>True si el código es válido</returns>
        public static bool IsValidBarcode(string? barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                return false;

            var clean = Regex.Replace(barcode, @"[\s\-]", "");
            
            // Validar códigos de barras comunes: 8, 12, 13, 14 dígitos
            return clean.Length == 8 || clean.Length == 12 || clean.Length == 13 || clean.Length == 14;
        }

        /// <summary>
        /// Valida si un string es un número de tarjeta de crédito válido
        /// </summary>
        /// <param name="cardNumber">Número de tarjeta a validar</param>
        /// <returns>True si el número es válido</returns>
        public static bool IsValidCreditCard(string? cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return false;

            var digits = Regex.Replace(cardNumber, @"[^\d]", "");
            
            // Validar que tenga entre 13 y 19 dígitos
            if (digits.Length < 13 || digits.Length > 19)
                return false;

            // Algoritmo de Luhn para validar tarjeta de crédito
            var sum = 0;
            var isEven = false;

            for (int i = digits.Length - 1; i >= 0; i--)
            {
                var digit = int.Parse(digits[i].ToString());

                if (isEven)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }

                sum += digit;
                isEven = !isEven;
            }

            return sum % 10 == 0;
        }

        /// <summary>
        /// Valida si un string es un número de cuenta bancaria válido
        /// </summary>
        /// <param name="accountNumber">Número de cuenta a validar</param>
        /// <returns>True si el número es válido</returns>
        public static bool IsValidBankAccount(string? accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                return false;

            var digits = Regex.Replace(accountNumber, @"[^\d]", "");
            
            // Validar que tenga entre 8 y 20 dígitos
            return digits.Length >= 8 && digits.Length <= 20;
        }

        /// <summary>
        /// Valida si un string es un código postal válido
        /// </summary>
        /// <param name="postalCode">Código postal a validar</param>
        /// <returns>True si el código es válido</returns>
        public static bool IsValidPostalCode(string? postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                return false;

            var digits = Regex.Replace(postalCode, @"[^\d]", "");
            
            // Códigos postales argentinos tienen 4 dígitos
            return digits.Length == 4;
        }

        /// <summary>
        /// Valida si un string es un número de versión válido
        /// </summary>
        /// <param name="version">Número de versión a validar</param>
        /// <returns>True si el número es válido</returns>
        public static bool IsValidVersion(string? version)
        {
            if (string.IsNullOrWhiteSpace(version))
                return false;

            return Version.TryParse(version, out _);
        }

        /// <summary>
        /// Valida si un string es un color hexadecimal válido
        /// </summary>
        /// <param name="hexColor">Color hexadecimal a validar</param>
        /// <returns>True si el color es válido</returns>
        public static bool IsValidHexColor(string? hexColor)
        {
            if (string.IsNullOrWhiteSpace(hexColor))
                return false;

            var regex = new Regex(@"^#?([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");
            return regex.IsMatch(hexColor);
        }

        /// <summary>
        /// Valida si un string es un código de color RGB válido
        /// </summary>
        /// <param name="rgbColor">Color RGB a validar</param>
        /// <returns>True si el color es válido</returns>
        public static bool IsValidRgbColor(string? rgbColor)
        {
            if (string.IsNullOrWhiteSpace(rgbColor))
                return false;

            var regex = new Regex(@"^rgb\(\s*\d+\s*,\s*\d+\s*,\s*\d+\s*\)$");
            return regex.IsMatch(rgbColor);
        }

        /// <summary>
        /// Valida si un string contiene solo letras
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si solo contiene letras</returns>
        public static bool ContainsOnlyLetters(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return value.All(char.IsLetter);
        }

        /// <summary>
        /// Valida si un string contiene solo números
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si solo contiene números</returns>
        public static bool ContainsOnlyDigits(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return value.All(char.IsDigit);
        }

        /// <summary>
        /// Valida si un string contiene solo letras y números
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si solo contiene letras y números</returns>
        public static bool ContainsOnlyLettersAndDigits(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return value.All(char.IsLetterOrDigit);
        }

        /// <summary>
        /// Valida si un string contiene al menos una letra mayúscula
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si contiene al menos una mayúscula</returns>
        public static bool ContainsUppercase(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return value.Any(char.IsUpper);
        }

        /// <summary>
        /// Valida si un string contiene al menos una letra minúscula
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si contiene al menos una minúscula</returns>
        public static bool ContainsLowercase(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return value.Any(char.IsLower);
        }

        /// <summary>
        /// Valida si un string contiene al menos un número
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si contiene al menos un número</returns>
        public static bool ContainsDigit(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return value.Any(char.IsDigit);
        }

        /// <summary>
        /// Valida si un string contiene al menos un carácter especial
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si contiene al menos un carácter especial</returns>
        public static bool ContainsSpecialCharacter(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return value.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));
        }

        /// <summary>
        /// Valida si un string es un número entero válido
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si es un entero válido</returns>
        public static bool IsValidInteger(string? value)
        {
            return int.TryParse(value, out _);
        }

        /// <summary>
        /// Valida si un string es un número decimal válido
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si es un decimal válido</returns>
        public static bool IsValidDecimal(string? value)
        {
            return decimal.TryParse(value, out _);
        }

        /// <summary>
        /// Valida si un string es un número double válido
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si es un double válido</returns>
        public static bool IsValidDouble(string? value)
        {
            return double.TryParse(value, out _);
        }

        /// <summary>
        /// Valida si un string es una fecha válida
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si es una fecha válida</returns>
        public static bool IsValidDate(string? value)
        {
            return DateTime.TryParse(value, out _);
        }

        /// <summary>
        /// Valida si un string es un GUID válido
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si es un GUID válido</returns>
        public static bool IsValidGuid(string? value)
        {
            return Guid.TryParse(value, out _);
        }

        /// <summary>
        /// Valida si un string es un número de teléfono móvil válido
        /// </summary>
        /// <param name="mobileNumber">Número móvil a validar</param>
        /// <returns>True si el número es válido</returns>
        public static bool IsValidMobileNumber(string? mobileNumber)
        {
            if (string.IsNullOrWhiteSpace(mobileNumber))
                return false;

            var digits = Regex.Replace(mobileNumber, @"[^\d]", "");
            
            // Números móviles argentinos empiezan con 9 y tienen 10 dígitos
            return digits.Length == 10 && digits.StartsWith("9");
        }

        /// <summary>
        /// Valida si un string es un nombre válido (solo letras y espacios)
        /// </summary>
        /// <param name="name">Nombre a validar</param>
        /// <returns>True si el nombre es válido</returns>
        public static bool IsValidName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            // Solo letras, espacios y algunos caracteres especiales comunes
            var regex = new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-']+$");
            return regex.IsMatch(name);
        }

        /// <summary>
        /// Valida si un string es una dirección válida
        /// </summary>
        /// <param name="address">Dirección a validar</param>
        /// <returns>True si la dirección es válida</returns>
        public static bool IsValidAddress(string? address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return false;

            // Debe tener al menos 10 caracteres y contener números y letras
            return address.Length >= 10 && 
                   address.Any(char.IsDigit) && 
                   address.Any(char.IsLetter);
        }
    }
}
