using System.Globalization;

namespace ManagementBusiness.Services.Helpers
{
    /// <summary>
    /// Helper específico para formateo de números y operaciones matemáticas
    /// </summary>
    public static class NumberFormatHelper
    {
        /// <summary>
        /// Formatea un número con separadores de miles y decimales
        /// </summary>
        /// <param name="value">Valor a formatear</param>
        /// <param name="decimalPlaces">Número de decimales</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Número formateado</returns>
        public static string FormatNumber(decimal value, int decimalPlaces = 2, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            var format = $"N{decimalPlaces}";
            return value.ToString(format, culture);
        }

        /// <summary>
        /// Formatea un número entero con separadores de miles
        /// </summary>
        /// <param name="value">Valor a formatear</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Número entero formateado</returns>
        public static string FormatInteger(int value, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            return value.ToString("N0", culture);
        }

        /// <summary>
        /// Formatea un número como porcentaje
        /// </summary>
        /// <param name="value">Valor a formatear (0.15 = 15%)</param>
        /// <param name="decimalPlaces">Número de decimales</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Porcentaje formateado</returns>
        public static string FormatPercentage(decimal value, int decimalPlaces = 2, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            var format = $"P{decimalPlaces}";
            return value.ToString(format, culture);
        }

        /// <summary>
        /// Formatea un número en notación científica
        /// </summary>
        /// <param name="value">Valor a formatear</param>
        /// <param name="decimalPlaces">Número de decimales</param>
        /// <returns>Número en notación científica</returns>
        public static string FormatScientific(decimal value, int decimalPlaces = 2)
        {
            var format = $"E{decimalPlaces}";
            return value.ToString(format);
        }

        /// <summary>
        /// Formatea un número en formato compacto (1K, 1M, 1B, etc.)
        /// </summary>
        /// <param name="value">Valor a formatear</param>
        /// <param name="decimalPlaces">Número de decimales</param>
        /// <returns>Número en formato compacto</returns>
        public static string FormatCompact(decimal value, int decimalPlaces = 1)
        {
            var absValue = Math.Abs(value);
            var sign = value < 0 ? "-" : "";

            if (absValue >= 1000000000000)
            {
                var formatted = (absValue / 1000000000000).ToString($"N{decimalPlaces}");
                return $"{sign}{formatted}T";
            }
            else if (absValue >= 1000000000)
            {
                var formatted = (absValue / 1000000000).ToString($"N{decimalPlaces}");
                return $"{sign}{formatted}B";
            }
            else if (absValue >= 1000000)
            {
                var formatted = (absValue / 1000000).ToString($"N{decimalPlaces}");
                return $"{sign}{formatted}M";
            }
            else if (absValue >= 1000)
            {
                var formatted = (absValue / 1000).ToString($"N{decimalPlaces}");
                return $"{sign}{formatted}K";
            }
            else
            {
                return value.ToString($"N{decimalPlaces}");
            }
        }

        /// <summary>
        /// Formatea un número en formato compacto con símbolo de moneda
        /// </summary>
        /// <param name="value">Valor a formatear</param>
        /// <param name="symbol">Símbolo de moneda</param>
        /// <param name="decimalPlaces">Número de decimales</param>
        /// <returns>Número en formato compacto con símbolo</returns>
        public static string FormatCompactCurrency(decimal value, string symbol, int decimalPlaces = 1)
        {
            var compact = FormatCompact(value, decimalPlaces);
            return $"{symbol}{compact}";
        }

        /// <summary>
        /// Formatea un número de teléfono con formato estándar
        /// </summary>
        /// <param name="phoneNumber">Número de teléfono</param>
        /// <returns>Número de teléfono formateado</returns>
        public static string FormatPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return phoneNumber;

            // Remover todos los caracteres no numéricos
            var digits = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (digits.Length == 10)
            {
                // Formato: (011) 1234-5678
                return $"({digits.Substring(0, 3)}) {digits.Substring(3, 4)}-{digits.Substring(7, 4)}";
            }
            else if (digits.Length == 11)
            {
                // Formato: +54 11 1234-5678
                return $"+{digits.Substring(0, 2)} {digits.Substring(2, 2)} {digits.Substring(4, 4)}-{digits.Substring(8, 4)}";
            }
            else if (digits.Length == 8)
            {
                // Formato: 1234-5678
                return $"{digits.Substring(0, 4)}-{digits.Substring(4, 4)}";
            }

            return phoneNumber;
        }

        /// <summary>
        /// Formatea un DNI/CUIT con separadores
        /// </summary>
        /// <param name="dniCuit">DNI/CUIT a formatear</param>
        /// <returns>DNI/CUIT formateado</returns>
        public static string FormatDniCuit(string dniCuit)
        {
            if (string.IsNullOrWhiteSpace(dniCuit))
                return dniCuit;

            // Remover todos los caracteres no numéricos
            var digits = new string(dniCuit.Where(char.IsDigit).ToArray());

            if (digits.Length == 7)
            {
                // Formato: 12.345.678
                return $"{digits.Substring(0, 2)}.{digits.Substring(2, 3)}.{digits.Substring(5, 3)}";
            }
            else if (digits.Length == 8)
            {
                // Formato: 12-3456789-0
                return $"{digits.Substring(0, 2)}-{digits.Substring(2, 6)}-{digits.Substring(8, 1)}";
            }
            else if (digits.Length == 11)
            {
                // Formato: 20-12345678-9
                return $"{digits.Substring(0, 2)}-{digits.Substring(2, 8)}-{digits.Substring(10, 1)}";
            }

            return dniCuit;
        }

        /// <summary>
        /// Formatea un código de barras con separadores
        /// </summary>
        /// <param name="barcode">Código de barras a formatear</param>
        /// <returns>Código de barras formateado</returns>
        public static string FormatBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                return barcode;

            // Remover espacios y guiones
            var clean = new string(barcode.Where(c => c != ' ' && c != '-').ToArray());

            if (clean.Length == 8)
            {
                // Formato: 12345678
                return clean;
            }
            else if (clean.Length == 12)
            {
                // Formato: 123456789012
                return clean;
            }
            else if (clean.Length == 13)
            {
                // Formato: 123-456789012-3
                return $"{clean.Substring(0, 3)}-{clean.Substring(3, 9)}-{clean.Substring(12, 1)}";
            }
            else if (clean.Length == 14)
            {
                // Formato: 1234-567890123-4
                return $"{clean.Substring(0, 4)}-{clean.Substring(4, 9)}-{clean.Substring(13, 1)}";
            }

            return barcode;
        }

        /// <summary>
        /// Formatea un número de tarjeta de crédito ocultando números intermedios
        /// </summary>
        /// <param name="cardNumber">Número de tarjeta</param>
        /// <param name="maskChar">Carácter para ocultar</param>
        /// <returns>Número de tarjeta formateado y oculto</returns>
        public static string FormatMaskedCardNumber(string cardNumber, char maskChar = '*')
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return cardNumber;

            // Remover todos los caracteres no numéricos
            var digits = new string(cardNumber.Where(char.IsDigit).ToArray());

            if (digits.Length >= 13 && digits.Length <= 19)
            {
                var visibleDigits = 4;
                var maskedLength = digits.Length - (visibleDigits * 2);
                var masked = new string(maskChar, maskedLength);
                
                return $"{digits.Substring(0, visibleDigits)} {masked} {digits.Substring(digits.Length - visibleDigits)}";
            }

            return cardNumber;
        }

        /// <summary>
        /// Formatea un número de cuenta bancaria con separadores
        /// </summary>
        /// <param name="accountNumber">Número de cuenta</param>
        /// <param name="groupSize">Tamaño de cada grupo</param>
        /// <returns>Número de cuenta formateado</returns>
        public static string FormatAccountNumber(string accountNumber, int groupSize = 4)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                return accountNumber;

            // Remover todos los caracteres no numéricos
            var digits = new string(accountNumber.Where(char.IsDigit).ToArray());

            if (digits.Length <= groupSize)
                return digits;

            var result = "";
            for (int i = 0; i < digits.Length; i++)
            {
                if (i > 0 && i % groupSize == 0)
                    result += " ";
                result += digits[i];
            }

            return result;
        }

        /// <summary>
        /// Formatea un número de serie con separadores
        /// </summary>
        /// <param name="serialNumber">Número de serie</param>
        /// <param name="groupSize">Tamaño de cada grupo</param>
        /// <param name="separator">Separador entre grupos</param>
        /// <returns>Número de serie formateado</returns>
        public static string FormatSerialNumber(string serialNumber, int groupSize = 4, string separator = "-")
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
                return serialNumber;

            // Remover todos los caracteres no alfanuméricos
            var chars = new string(serialNumber.Where(char.IsLetterOrDigit).ToArray());

            if (chars.Length <= groupSize)
                return chars;

            var result = "";
            for (int i = 0; i < chars.Length; i++)
            {
                if (i > 0 && i % groupSize == 0)
                    result += separator;
                result += chars[i];
            }

            return result;
        }

        /// <summary>
        /// Formatea un número de versión (semver)
        /// </summary>
        /// <param name="major">Número mayor</param>
        /// <param name="minor">Número menor</param>
        /// <param name="patch">Número de parche</param>
        /// <param name="prerelease">Pre-release (opcional)</param>
        /// <returns>Número de versión formateado</returns>
        public static string FormatVersion(int major, int minor, int patch, string? prerelease = null)
        {
            var version = $"{major}.{minor}.{patch}";
            if (!string.IsNullOrWhiteSpace(prerelease))
                version += $"-{prerelease}";
            return version;
        }

        /// <summary>
        /// Formatea un número de versión desde string
        /// </summary>
        /// <param name="versionString">String de versión</param>
        /// <returns>Número de versión formateado</returns>
        public static string FormatVersion(string versionString)
        {
            if (string.IsNullOrWhiteSpace(versionString))
                return versionString;

            // Intentar parsear como versión
            if (Version.TryParse(versionString, out var version))
            {
                return version.ToString();
            }

            return versionString;
        }
    }
}
