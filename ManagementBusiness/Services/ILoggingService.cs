using System;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Interfaz para el servicio de logging de la aplicación
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Registra un mensaje de información
        /// </summary>
        /// <param name="message">Mensaje a registrar</param>
        /// <param name="source">Fuente del mensaje (opcional)</param>
        void LogInformation(string message, string? source = null);

        /// <summary>
        /// Registra un mensaje de advertencia
        /// </summary>
        /// <param name="message">Mensaje a registrar</param>
        /// <param name="source">Fuente del mensaje (opcional)</param>
        void LogWarning(string message, string? source = null);

        /// <summary>
        /// Registra un mensaje de error
        /// </summary>
        /// <param name="message">Mensaje a registrar</param>
        /// <param name="source">Fuente del mensaje (opcional)</param>
        void LogError(string message, string? source = null);

        /// <summary>
        /// Registra un mensaje de error con excepción
        /// </summary>
        /// <param name="message">Mensaje a registrar</param>
        /// <param name="exception">Excepción que causó el error</param>
        /// <param name="source">Fuente del mensaje (opcional)</param>
        void LogError(string message, Exception exception, string? source = null);

        /// <summary>
        /// Registra un mensaje de debug (solo en modo DEBUG)
        /// </summary>
        /// <param name="message">Mensaje a registrar</param>
        /// <param name="source">Fuente del mensaje (opcional)</param>
        void LogDebug(string message, string? source = null);

        /// <summary>
        /// Registra un mensaje de trace (solo en modo DEBUG)
        /// </summary>
        /// <param name="message">Mensaje a registrar</param>
        /// <param name="source">Fuente del mensaje (opcional)</param>
        void LogTrace(string message, string? source = null);
    }
}
