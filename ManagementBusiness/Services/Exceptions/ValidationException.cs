using System;

namespace ManagementBusiness.Services.Exceptions
{
    /// <summary>
    /// Excepción que se lanza cuando falla la validación de datos
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de ValidationException
        /// </summary>
        public ValidationException() : base() { }

        /// <summary>
        /// Inicializa una nueva instancia de ValidationException con un mensaje
        /// </summary>
        /// <param name="message">Mensaje de error</param>
        public ValidationException(string message) : base(message) { }

        /// <summary>
        /// Inicializa una nueva instancia de ValidationException con un mensaje y una excepción interna
        /// </summary>
        /// <param name="message">Mensaje de error</param>
        /// <param name="innerException">Excepción interna</param>
        public ValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
