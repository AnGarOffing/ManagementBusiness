using System.Globalization;

namespace ManagementBusiness.Services.Helpers
{
    /// <summary>
    /// Helper específico para formateo de fechas y tiempos
    /// </summary>
    public static class DateFormatHelper
    {
        /// <summary>
        /// Formatea una fecha en formato corto estándar
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Fecha formateada en formato corto</returns>
        public static string FormatDate(DateTime date, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            return date.ToString("d", culture);
        }

        /// <summary>
        /// Formatea una fecha en formato largo estándar
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Fecha formateada en formato largo</returns>
        public static string FormatDateLong(DateTime date, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            return date.ToString("D", culture);
        }

        /// <summary>
        /// Formatea una fecha y hora en formato estándar
        /// </summary>
        /// <param name="dateTime">Fecha y hora a formatear</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Fecha y hora formateada</returns>
        public static string FormatDateTime(DateTime dateTime, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            return dateTime.ToString("g", culture);
        }

        /// <summary>
        /// Formatea una fecha y hora en formato completo
        /// </summary>
        /// <param name="dateTime">Fecha y hora a formatear</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Fecha y hora formateada en formato completo</returns>
        public static string FormatDateTimeFull(DateTime dateTime, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            return dateTime.ToString("F", culture);
        }

        /// <summary>
        /// Formatea solo la hora en formato estándar
        /// </summary>
        /// <param name="dateTime">Fecha y hora a formatear</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Hora formateada</returns>
        public static string FormatTime(DateTime dateTime, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            return dateTime.ToString("t", culture);
        }

        /// <summary>
        /// Formatea solo la hora en formato completo
        /// </summary>
        /// <param name="dateTime">Fecha y hora a formatear</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Hora formateada en formato completo</returns>
        public static string FormatTimeFull(DateTime dateTime, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            return dateTime.ToString("T", culture);
        }

        /// <summary>
        /// Formatea una fecha en formato ISO 8601
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <returns>Fecha en formato ISO 8601</returns>
        public static string FormatIso8601(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Formatea una fecha y hora en formato ISO 8601
        /// </summary>
        /// <param name="dateTime">Fecha y hora a formatear</param>
        /// <returns>Fecha y hora en formato ISO 8601</returns>
        public static string FormatIso8601DateTime(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        /// <summary>
        /// Formatea una fecha en formato personalizado
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <param name="format">Formato personalizado</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Fecha formateada según el formato especificado</returns>
        public static string FormatCustom(DateTime date, string format, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            return date.ToString(format, culture);
        }

        /// <summary>
        /// Formatea una fecha relativa (hace 2 días, ayer, hoy, etc.)
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Fecha formateada de forma relativa</returns>
        public static string FormatRelative(DateTime date, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            var now = DateTime.Now;
            var diff = now - date;

            if (diff.TotalDays >= 7)
            {
                return date.ToString("d", culture);
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

        /// <summary>
        /// Formatea una fecha relativa en inglés
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <returns>Fecha formateada de forma relativa en inglés</returns>
        public static string FormatRelativeEnglish(DateTime date)
        {
            var now = DateTime.Now;
            var diff = now - date;

            if (diff.TotalDays >= 7)
            {
                return date.ToString("d", CultureInfo.InvariantCulture);
            }
            else if (diff.TotalDays >= 2)
            {
                var days = (int)diff.TotalDays;
                return $"{days} days ago";
            }
            else if (diff.TotalDays >= 1)
            {
                return "yesterday";
            }
            else if (diff.TotalHours >= 1)
            {
                var hours = (int)diff.TotalHours;
                return $"{hours} hours ago";
            }
            else if (diff.TotalMinutes >= 1)
            {
                var minutes = (int)diff.TotalMinutes;
                return $"{minutes} minutes ago";
            }
            else
            {
                return "just now";
            }
        }

        /// <summary>
        /// Formatea un rango de fechas
        /// </summary>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha de fin</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Rango de fechas formateado</returns>
        public static string FormatDateRange(DateTime startDate, DateTime endDate, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;

            if (startDate.Date == endDate.Date)
            {
                return $"{startDate.ToString("d", culture)}";
            }
            else if (startDate.Year == endDate.Year)
            {
                if (startDate.Month == endDate.Month)
                {
                    return $"{startDate.Day} - {endDate.ToString("d", culture)}";
                }
                else
                {
                    return $"{startDate.ToString("d", culture)} - {endDate.ToString("d", culture)}";
                }
            }
            else
            {
                return $"{startDate.ToString("d", culture)} - {endDate.ToString("d", culture)}";
            }
        }

        /// <summary>
        /// Formatea una duración entre dos fechas
        /// </summary>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha de fin</param>
        /// <returns>Duración formateada</returns>
        public static string FormatDuration(DateTime startDate, DateTime endDate)
        {
            var diff = endDate - startDate;

            if (diff.TotalDays >= 365)
            {
                var years = (int)(diff.TotalDays / 365);
                var remainingDays = (int)(diff.TotalDays % 365);
                if (remainingDays > 0)
                    return $"{years} años, {remainingDays} días";
                return $"{years} años";
            }
            else if (diff.TotalDays >= 30)
            {
                var months = (int)(diff.TotalDays / 30);
                var remainingDays = (int)(diff.TotalDays % 30);
                if (remainingDays > 0)
                    return $"{months} meses, {remainingDays} días";
                return $"{months} meses";
            }
            else if (diff.TotalDays >= 1)
            {
                var days = (int)diff.TotalDays;
                var remainingHours = (int)(diff.TotalHours % 24);
                if (remainingHours > 0)
                    return $"{days} días, {remainingHours} horas";
                return $"{days} días";
            }
            else if (diff.TotalHours >= 1)
            {
                var hours = (int)diff.TotalHours;
                var remainingMinutes = (int)(diff.TotalMinutes % 60);
                if (remainingMinutes > 0)
                    return $"{hours} horas, {remainingMinutes} minutos";
                return $"{hours} horas";
            }
            else if (diff.TotalMinutes >= 1)
            {
                var minutes = (int)diff.TotalMinutes;
                var remainingSeconds = (int)(diff.TotalSeconds % 60);
                if (remainingSeconds > 0)
                    return $"{minutes} minutos, {remainingSeconds} segundos";
                return $"{minutes} minutos";
            }
            else
            {
                var seconds = (int)diff.TotalSeconds;
                return $"{seconds} segundos";
            }
        }

        /// <summary>
        /// Formatea una fecha para mostrar la edad
        /// </summary>
        /// <param name="birthDate">Fecha de nacimiento</param>
        /// <returns>Edad formateada</returns>
        public static string FormatAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
                age--;

            if (age == 0)
            {
                var months = (today.Year - birthDate.Year) * 12 + today.Month - birthDate.Month;
                if (birthDate.Day > today.Day)
                    months--;

                if (months == 0)
                {
                    var days = (today - birthDate).Days;
                    return $"{days} días";
                }
                return $"{months} meses";
            }

            return $"{age} años";
        }

        /// <summary>
        /// Formatea una fecha para mostrar el día de la semana
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Día de la semana formateado</returns>
        public static string FormatDayOfWeek(DateTime date, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            return date.ToString("dddd", culture);
        }

        /// <summary>
        /// Formatea una fecha para mostrar el mes
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Mes formateado</returns>
        public static string FormatMonth(DateTime date, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            return date.ToString("MMMM", culture);
        }

        /// <summary>
        /// Formatea una fecha para mostrar el año
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <returns>Año formateado</returns>
        public static string FormatYear(DateTime date)
        {
            return date.Year.ToString();
        }

        /// <summary>
        /// Formatea una fecha para mostrar el trimestre
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <returns>Trimestre formateado</returns>
        public static string FormatQuarter(DateTime date)
        {
            var quarter = ((date.Month - 1) / 3) + 1;
            return $"Q{quarter} {date.Year}";
        }

        /// <summary>
        /// Formatea una fecha para mostrar la semana del año
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <param name="culture">Cultura para el formateo</param>
        /// <returns>Semana del año formateada</returns>
        public static string FormatWeekOfYear(DateTime date, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            var calendar = culture.Calendar;
            var weekOfYear = calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return $"Semana {weekOfYear} del {date.Year}";
        }
    }
}
