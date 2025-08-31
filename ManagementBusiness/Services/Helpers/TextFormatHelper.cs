using System.Text;
using System.Text.RegularExpressions;

namespace ManagementBusiness.Services.Helpers
{
    /// <summary>
    /// Helper específico para formateo de texto
    /// </summary>
    public static class TextFormatHelper
    {
        /// <summary>
        /// Convierte un texto a formato camelCase
        /// </summary>
        /// <param name="text">Texto a convertir</param>
        /// <returns>Texto en formato camelCase</returns>
        public static string ToCamelCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var words = text.Split(' ', '-', '_');
            var result = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    if (i == 0)
                    {
                        // Primera palabra en minúsculas
                        result.Append(words[i].ToLower());
                    }
                    else
                    {
                        // Resto de palabras con primera letra mayúscula
                        result.Append(char.ToUpper(words[i][0]));
                        if (words[i].Length > 1)
                            result.Append(words[i].Substring(1).ToLower());
                    }
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Convierte un texto a formato PascalCase
        /// </summary>
        /// <param name="text">Texto a convertir</param>
        /// <returns>Texto en formato PascalCase</returns>
        public static string ToPascalCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var words = text.Split(' ', '-', '_');
            var result = new StringBuilder();

            foreach (var word in words)
            {
                if (word.Length > 0)
                {
                    result.Append(char.ToUpper(word[0]));
                    if (word.Length > 1)
                        result.Append(word.Substring(1).ToLower());
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Convierte un texto a formato snake_case
        /// </summary>
        /// <param name="text">Texto a convertir</param>
        /// <returns>Texto en formato snake_case</returns>
        public static string ToSnakeCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            // Insertar guiones bajos antes de mayúsculas y convertir a minúsculas
            var result = Regex.Replace(text, @"([A-Z])", "_$1").ToLower();
            
            // Remover guión bajo inicial si existe
            if (result.StartsWith("_"))
                result = result.Substring(1);

            return result;
        }

        /// <summary>
        /// Convierte un texto a formato kebab-case
        /// </summary>
        /// <param name="text">Texto a convertir</param>
        /// <returns>Texto en formato kebab-case</returns>
        public static string ToKebabCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            // Insertar guiones antes de mayúsculas y convertir a minúsculas
            var result = Regex.Replace(text, @"([A-Z])", "-$1").ToLower();
            
            // Remover guión inicial si existe
            if (result.StartsWith("-"))
                result = result.Substring(1);

            return result;
        }

        /// <summary>
        /// Capitaliza la primera letra de cada palabra
        /// </summary>
        /// <param name="text">Texto a capitalizar</param>
        /// <returns>Texto con primera letra de cada palabra en mayúscula</returns>
        public static string CapitalizeWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var words = text.Split(' ');
            var result = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    result.Append(char.ToUpper(words[i][0]));
                    if (words[i].Length > 1)
                        result.Append(words[i].Substring(1).ToLower());

                    if (i < words.Length - 1)
                        result.Append(' ');
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Invierte el orden de las palabras en un texto
        /// </summary>
        /// <param name="text">Texto a invertir</param>
        /// <returns>Texto con palabras invertidas</returns>
        public static string ReverseWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var words = text.Split(' ');
            Array.Reverse(words);
            return string.Join(" ", words);
        }

        /// <summary>
        /// Invierte el orden de los caracteres en un texto
        /// </summary>
        /// <param name="text">Texto a invertir</param>
        /// <returns>Texto con caracteres invertidos</returns>
        public static string ReverseText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var chars = text.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// Alterna entre mayúsculas y minúsculas en un texto
        /// </summary>
        /// <param name="text">Texto a alternar</param>
        /// <returns>Texto con alternancia de mayúsculas y minúsculas</returns>
        public static string AlternatingCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var result = new StringBuilder();
            var upper = true;

            foreach (var c in text)
            {
                if (char.IsLetter(c))
                {
                    result.Append(upper ? char.ToUpper(c) : char.ToLower(c));
                    upper = !upper;
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Convierte un texto a formato de acrónimo (USA, NASA, etc.)
        /// </summary>
        /// <param name="text">Texto a convertir</param>
        /// <returns>Acrónimo del texto</returns>
        public static string ToAcronym(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var words = text.Split(' ', '-', '_');
            var result = new StringBuilder();

            foreach (var word in words)
            {
                if (word.Length > 0)
                {
                    result.Append(char.ToUpper(word[0]));
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Formatea un texto para que sea legible en consola (con saltos de línea)
        /// </summary>
        /// <param name="text">Texto a formatear</param>
        /// <param name="maxLineLength">Longitud máxima de línea</param>
        /// <returns>Texto formateado para consola</returns>
        public static string FormatForConsole(string text, int maxLineLength = 80)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var words = text.Split(' ');
            var result = new StringBuilder();
            var currentLineLength = 0;

            foreach (var word in words)
            {
                if (currentLineLength + word.Length + 1 > maxLineLength)
                {
                    result.AppendLine();
                    result.Append(word);
                    currentLineLength = word.Length;
                }
                else
                {
                    if (currentLineLength > 0)
                    {
                        result.Append(' ');
                        currentLineLength++;
                    }
                    result.Append(word);
                    currentLineLength += word.Length;
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Genera un slug URL-friendly a partir de un texto
        /// </summary>
        /// <param name="text">Texto a convertir</param>
        /// <returns>Slug URL-friendly</returns>
        public static string ToSlug(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            // Convertir a minúsculas
            var result = text.ToLower();

            // Reemplazar caracteres especiales
            result = Regex.Replace(result, @"[áàäâã]", "a");
            result = Regex.Replace(result, @"[éèëê]", "e");
            result = Regex.Replace(result, @"[íìïî]", "i");
            result = Regex.Replace(result, @"[óòöôõ]", "o");
            result = Regex.Replace(result, @"[úùüû]", "u");
            result = Regex.Replace(result, @"[ñ]", "n");

            // Reemplazar espacios y caracteres especiales con guiones
            result = Regex.Replace(result, @"[^a-z0-9\s-]", "");
            result = Regex.Replace(result, @"[\s-]+", "-");

            // Remover guiones al inicio y final
            result = result.Trim('-');

            return result;
        }
    }
}
