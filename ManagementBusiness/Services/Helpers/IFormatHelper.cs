namespace ManagementBusiness.Services.Helpers
{
    /// <summary>
    /// Interfaz base para helpers de formateo de datos
    /// </summary>
    public interface IFormatHelper
    {
        /// <summary>
        /// Formatea un valor decimal como moneda
        /// </summary>
        /// <param name="value">Valor a formatear</param>
        /// <param name="currencyCode">Código de moneda (opcional, por defecto "ARS")</param>
        /// <returns>Valor formateado como moneda</returns>
        string FormatCurrency(decimal value, string currencyCode = "ARS");

        /// <summary>
        /// Formatea un valor decimal como moneda con símbolo
        /// </summary>
        /// <param name="value">Valor a formatear</param>
        /// <param name="showSymbol">Indica si mostrar el símbolo de moneda</param>
        /// <returns>Valor formateado como moneda</returns>
        string FormatCurrency(decimal value, bool showSymbol);

        /// <summary>
        /// Formatea un valor decimal como porcentaje
        /// </summary>
        /// <param name="value">Valor a formatear (0.15 = 15%)</param>
        /// <param name="decimalPlaces">Número de decimales</param>
        /// <returns>Valor formateado como porcentaje</returns>
        string FormatPercentage(decimal value, int decimalPlaces = 2);

        /// <summary>
        /// Formatea un valor decimal como número
        /// </summary>
        /// <param name="value">Valor a formatear</param>
        /// <param name="decimalPlaces">Número de decimales</param>
        /// <returns>Valor formateado como número</returns>
        string FormatNumber(decimal value, int decimalPlaces = 2);

        /// <summary>
        /// Formatea un valor entero con separadores de miles
        /// </summary>
        /// <param name="value">Valor a formatear</param>
        /// <returns>Valor formateado con separadores de miles</returns>
        string FormatInteger(int value);

        /// <summary>
        /// Formatea una fecha en formato corto
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <returns>Fecha formateada en formato corto</returns>
        string FormatDate(DateTime date);

        /// <summary>
        /// Formatea una fecha en formato largo
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <returns>Fecha formateada en formato largo</returns>
        string FormatDateLong(DateTime date);

        /// <summary>
        /// Formatea una fecha y hora
        /// </summary>
        /// <param name="dateTime">Fecha y hora a formatear</param>
        /// <returns>Fecha y hora formateada</returns>
        string FormatDateTime(DateTime dateTime);

        /// <summary>
        /// Formatea una fecha relativa (hace 2 días, ayer, hoy, etc.)
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <returns>Fecha formateada de forma relativa</returns>
        string FormatDateRelative(DateTime date);

        /// <summary>
        /// Formatea un número de teléfono
        /// </summary>
        /// <param name="phoneNumber">Número de teléfono a formatear</param>
        /// <returns>Número de teléfono formateado</returns>
        string FormatPhoneNumber(string phoneNumber);

        /// <summary>
        /// Formatea un DNI/CUIT
        /// </summary>
        /// <param name="dniCuit">DNI/CUIT a formatear</param>
        /// <returns>DNI/CUIT formateado</returns>
        string FormatDniCuit(string dniCuit);

        /// <summary>
        /// Formatea un código de barras
        /// </summary>
        /// <param name="barcode">Código de barras a formatear</param>
        /// <returns>Código de barras formateado</returns>
        string FormatBarcode(string barcode);

        /// <summary>
        /// Formatea un texto con capitalización de título
        /// </summary>
        /// <param name="text">Texto a formatear</param>
        /// <returns>Texto con capitalización de título</returns>
        string FormatTitleCase(string text);

        /// <summary>
        /// Formatea un texto truncando si es muy largo
        /// </summary>
        /// <param name="text">Texto a formatear</param>
        /// <param name="maxLength">Longitud máxima</param>
        /// <param name="suffix">Sufijo para texto truncado</param>
        /// <returns>Texto truncado si es necesario</returns>
        string FormatTruncatedText(string text, int maxLength, string suffix = "...");

        /// <summary>
        /// Formatea un tamaño de archivo en bytes a formato legible
        /// </summary>
        /// <param name="bytes">Tamaño en bytes</param>
        /// <returns>Tamaño formateado (KB, MB, GB, etc.)</returns>
        string FormatFileSize(long bytes);

        /// <summary>
        /// Formatea una duración en segundos a formato legible
        /// </summary>
        /// <param name="seconds">Duración en segundos</param>
        /// <returns>Duración formateada (HH:MM:SS)</returns>
        string FormatDuration(int seconds);

        /// <summary>
        /// Formatea un valor booleano a texto legible
        /// </summary>
        /// <param name="value">Valor booleano</param>
        /// <param name="trueText">Texto para valor verdadero</param>
        /// <param name="falseText">Texto para valor falso</param>
        /// <returns>Texto formateado</returns>
        string FormatBoolean(bool value, string trueText = "Sí", string falseText = "No");
    }
}
