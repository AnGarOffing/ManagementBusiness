using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ManagementBusiness.Services.Helpers
{
    /// <summary>
    /// Implementación de helpers para formateo de datos
    /// </summary>
    public class FormatHelper : IFormatHelper
    {
        private readonly CultureInfo _cultureInfo;

        public FormatHelper()
        {
            // Usar cultura argentina por defecto
            _cultureInfo = new CultureInfo("es-AR");
        }

        public FormatHelper(string cultureName)
        {
            _cultureInfo = new CultureInfo(cultureName);
        }

        public string FormatCurrency(decimal value, string currencyCode = "ARS")
        {
            return value.ToString("C2", _cultureInfo);
        }

        public string FormatCurrency(decimal value, bool showSymbol)
        {
            if (showSymbol)
                return value.ToString("C2", _cultureInfo);
            else
                return value.ToString("N2", _cultureInfo);
        }

        public string FormatPercentage(decimal value, int decimalPlaces = 2)
        {
            var format = $"P{decimalPlaces}";
            return value.ToString(format, _cultureInfo);
        }

        public string FormatNumber(decimal value, int decimalPlaces = 2)
        {
            var format = $"N{decimalPlaces}";
            return value.ToString(format, _cultureInfo);
        }

        public string FormatInteger(int value)
        {
            return value.ToString("N0", _cultureInfo);
        }

        public string FormatDate(DateTime date)
        {
            return date.ToString("d", _cultureInfo);
        }

        public string FormatDateLong(DateTime date)
        {
            return date.ToString("D", _cultureInfo);
        }

        public string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString("g", _cultureInfo);
        }

        public string FormatDateRelative(DateTime date)
        {
            var now = DateTime.Now;
            var diff = now - date;

            if (diff.TotalDays >= 7)
            {
                return date.ToString("d", _cultureInfo);
            }
            else if (diff.TotalDays >= 2)
            {
                var days = (int)diff.TotalDays;
                return $"hace {days} días";
            }
            else if (diff.TotalDays >= 1)
            {
                return "ayer";
            }
            else if (diff.TotalHours >= 1)
            {
                var hours = (int)diff.TotalHours;
                return $"hace {hours} horas";
            }
            else if (diff.TotalMinutes >= 1)
            {
                var minutes = (int)diff.TotalMinutes;
                return $"hace {minutes} minutos";
            }
            else
            {
                return "hace un momento";
            }
        }

        public string FormatPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return phoneNumber;

            // Remover todos los caracteres no numéricos
            var digits = Regex.Replace(phoneNumber, @"[^\d]", "");

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

            return phoneNumber; // Retornar original si no coincide con ningún formato
        }

        public string FormatDniCuit(string dniCuit)
        {
            if (string.IsNullOrWhiteSpace(dniCuit))
                return dniCuit;

            // Remover todos los caracteres no numéricos
            var digits = Regex.Replace(dniCuit, @"[^\d]", "");

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

            return dniCuit; // Retornar original si no coincide con ningún formato
        }

        public string FormatBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                return barcode;

            // Remover espacios y guiones
            var clean = Regex.Replace(barcode, @"[\s\-]", "");

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

            return barcode; // Retornar original si no coincide con ningún formato
        }

        public string FormatTitleCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var words = text.Split(' ');
            var result = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    // Capitalizar primera letra, resto en minúsculas
                    result.Append(char.ToUpper(words[i][0]));
                    if (words[i].Length > 1)
                        result.Append(words[i].Substring(1).ToLower());

                    if (i < words.Length - 1)
                        result.Append(' ');
                }
            }

            return result.ToString();
        }

        public string FormatTruncatedText(string text, int maxLength, string suffix = "...")
        {
            if (string.IsNullOrWhiteSpace(text) || text.Length <= maxLength)
                return text;

            if (maxLength <= suffix.Length)
                return suffix;

            return text.Substring(0, maxLength - suffix.Length) + suffix;
        }

        public string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }

        public string FormatDuration(int seconds)
        {
            if (seconds < 0)
                return "0:00:00";

            var hours = seconds / 3600;
            var minutes = (seconds % 3600) / 60;
            var secs = seconds % 60;

            if (hours > 0)
                return $"{hours:D2}:{minutes:D2}:{secs:D2}";
            else
                return $"{minutes:D2}:{secs:D2}";
        }

        public string FormatBoolean(bool value, string trueText = "Sí", string falseText = "No")
        {
            return value ? trueText : falseText;
        }

        /// <summary>
        /// Formatea un valor decimal como moneda con símbolo personalizado
        /// </summary>
        /// <param name="value">Valor a formatear</param>
        /// <param name="symbol">Símbolo de moneda</param>
        /// <returns>Valor formateado con símbolo personalizado</returns>
        public string FormatCurrencyWithSymbol(decimal value, string symbol)
        {
            return $"{symbol}{value:N2}";
        }

        /// <summary>
        /// Formatea un valor decimal como moneda sin símbolo
        /// </summary>
        /// <param name="value">Valor a formatear</param>
        /// <returns>Valor formateado sin símbolo de moneda</returns>
        public string FormatCurrencyWithoutSymbol(decimal value)
        {
            return value.ToString("N2", _cultureInfo);
        }

        /// <summary>
        /// Formatea un valor decimal como moneda compacta (1.5K, 1.2M, etc.)
        /// </summary>
        /// <param name="value">Valor a formatear</param>
        /// <returns>Valor formateado de forma compacta</returns>
        public string FormatCurrencyCompact(decimal value)
        {
            if (Math.Abs(value) >= 1000000)
                return $"{(value / 1000000):N1}M";
            else if (Math.Abs(value) >= 1000)
                return $"{(value / 1000):N1}K";
            else
                return value.ToString("N0");
        }

        /// <summary>
        /// Formatea un valor decimal como moneda compacta con símbolo
        /// </summary>
        /// <param name="value">Valor a formatear</param>
        /// <param name="symbol">Símbolo de moneda</param>
        /// <returns>Valor formateado de forma compacta con símbolo</returns>
        public string FormatCurrencyCompactWithSymbol(decimal value, string symbol)
        {
            var compact = FormatCurrencyCompact(value);
            return $"{symbol}{compact}";
        }
    }
}
