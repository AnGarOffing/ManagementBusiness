using System.Diagnostics;
using System.Text;
using System.IO;
using Microsoft.Data.SqlClient;

namespace ManagementBusiness.Services.Helpers
{
    /// <summary>
    /// Helper para manejo centralizado de errores y excepciones
    /// </summary>
    public static class ErrorHandler
    {
        /// <summary>
        /// Maneja una excepción de forma segura y registra información de depuración
        /// </summary>
        /// <param name="ex">Excepción a manejar</param>
        /// <param name="context">Contexto donde ocurrió el error</param>
        /// <param name="additionalInfo">Información adicional opcional</param>
        /// <returns>Mensaje de error formateado para el usuario</returns>
        public static string HandleException(Exception ex, string context, string? additionalInfo = null)
        {
            try
            {
                // Registrar información de depuración
                LogExceptionDetails(ex, context, additionalInfo);

                // Retornar mensaje amigable para el usuario
                return GetUserFriendlyMessage(ex);
            }
            catch
            {
                // Si falla el manejo de errores, retornar mensaje genérico
                return "Ha ocurrido un error inesperado. Por favor, contacte al soporte técnico.";
            }
        }

        /// <summary>
        /// Maneja una excepción de forma asíncrona
        /// </summary>
        /// <param name="ex">Excepción a manejar</param>
        /// <param name="context">Contexto donde ocurrió el error</param>
        /// <param name="additionalInfo">Información adicional opcional</param>
        /// <returns>Task con el mensaje de error formateado</returns>
        public static async Task<string> HandleExceptionAsync(Exception ex, string context, string? additionalInfo = null)
        {
            return await Task.Run(() => HandleException(ex, context, additionalInfo));
        }

        /// <summary>
        /// Ejecuta una acción de forma segura con manejo de errores
        /// </summary>
        /// <param name="action">Acción a ejecutar</param>
        /// <param name="context">Contexto de la operación</param>
        /// <param name="defaultValue">Valor por defecto en caso de error</param>
        /// <returns>Resultado de la acción o valor por defecto</returns>
        public static T ExecuteSafely<T>(Func<T> action, string context, T defaultValue)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                HandleException(ex, context);
                return defaultValue;
            }
        }

        /// <summary>
        /// Ejecuta una acción de forma segura con manejo de errores (sin valor de retorno)
        /// </summary>
        /// <param name="action">Acción a ejecutar</param>
        /// <param name="context">Contexto de la operación</param>
        /// <returns>True si la acción se ejecutó correctamente, false en caso contrario</returns>
        public static bool ExecuteSafely(Action action, string context)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex, context);
                return false;
            }
        }

        /// <summary>
        /// Ejecuta una acción de forma segura de forma asíncrona
        /// </summary>
        /// <param name="action">Acción asíncrona a ejecutar</param>
        /// <param name="context">Contexto de la operación</param>
        /// <param name="defaultValue">Valor por defecto en caso de error</param>
        /// <returns>Task con el resultado de la acción o valor por defecto</returns>
        public static async Task<T> ExecuteSafelyAsync<T>(Func<Task<T>> action, string context, T defaultValue)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, context);
                return defaultValue;
            }
        }

        /// <summary>
        /// Ejecuta una acción de forma segura de forma asíncrona (sin valor de retorno)
        /// </summary>
        /// <param name="action">Acción asíncrona a ejecutar</param>
        /// <param name="context">Contexto de la operación</param>
        /// <returns>Task con true si la acción se ejecutó correctamente, false en caso contrario</returns>
        public static async Task<bool> ExecuteSafelyAsync(Func<Task> action, string context)
        {
            try
            {
                await action();
                return true;
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, context);
                return false;
            }
        }

        /// <summary>
        /// Valida si una excepción es crítica y requiere atención inmediata
        /// </summary>
        /// <param name="ex">Excepción a evaluar</param>
        /// <returns>True si la excepción es crítica</returns>
        public static bool IsCriticalException(Exception ex)
        {
            return ex is OutOfMemoryException ||
                   ex is StackOverflowException ||
                   ex is ThreadAbortException ||
                   ex is AccessViolationException;
        }

        /// <summary>
        /// Obtiene el tipo de excepción en formato legible
        /// </summary>
        /// <param name="ex">Excepción a analizar</param>
        /// <returns>Tipo de excepción en formato legible</returns>
        public static string GetExceptionType(Exception ex)
        {
            return ex.GetType().Name.Replace("Exception", "");
        }

        /// <summary>
        /// Obtiene la información completa de la excepción para depuración
        /// </summary>
        /// <param name="ex">Excepción a analizar</param>
        /// <returns>Información completa de la excepción</returns>
        public static string GetExceptionDetails(Exception ex)
        {
            var details = new StringBuilder();
            
            details.AppendLine($"Tipo de Excepción: {ex.GetType().FullName}");
            details.AppendLine($"Mensaje: {ex.Message}");
            details.AppendLine($"StackTrace: {ex.StackTrace}");

            if (ex.InnerException != null)
            {
                details.AppendLine("Excepción Interna:");
                details.AppendLine(GetExceptionDetails(ex.InnerException));
            }

            return details.ToString();
        }

        /// <summary>
        /// Obtiene información del contexto de ejecución actual
        /// </summary>
        /// <returns>Información del contexto de ejecución</returns>
        public static string GetExecutionContextInfo()
        {
            var info = new StringBuilder();
            
            try
            {
                var process = Process.GetCurrentProcess();
                info.AppendLine($"Proceso: {process.ProcessName} (PID: {process.Id})");
                info.AppendLine($"Memoria: {process.WorkingSet64 / 1024 / 1024} MB");
                info.AppendLine($"Tiempo de CPU: {process.TotalProcessorTime.TotalSeconds:F2} segundos");
            }
            catch
            {
                info.AppendLine("No se pudo obtener información del proceso");
            }

            try
            {
                info.AppendLine($"Directorio de trabajo: {Environment.CurrentDirectory}");
                info.AppendLine($"Usuario: {Environment.UserName}");
                info.AppendLine($"Máquina: {Environment.MachineName}");
                info.AppendLine($"Versión .NET: {Environment.Version}");
                info.AppendLine($"Sistema Operativo: {Environment.OSVersion}");
            }
            catch
            {
                info.AppendLine("No se pudo obtener información del entorno");
            }

            return info.ToString();
        }

        /// <summary>
        /// Registra los detalles de la excepción para depuración
        /// </summary>
        /// <param name="ex">Excepción a registrar</param>
        /// <param name="context">Contexto donde ocurrió el error</param>
        /// <param name="additionalInfo">Información adicional opcional</param>
        private static void LogExceptionDetails(Exception ex, string context, string? additionalInfo)
        {
            try
            {
                var logMessage = new StringBuilder();
                logMessage.AppendLine($"=== ERROR EN {context.ToUpper()} ===");
                logMessage.AppendLine($"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                logMessage.AppendLine($"Tipo: {GetExceptionType(ex)}");
                logMessage.AppendLine($"Mensaje: {ex.Message}");
                
                if (!string.IsNullOrWhiteSpace(additionalInfo))
                {
                    logMessage.AppendLine($"Información Adicional: {additionalInfo}");
                }

                logMessage.AppendLine("Detalles Completos:");
                logMessage.AppendLine(GetExceptionDetails(ex));
                
                logMessage.AppendLine("Contexto de Ejecución:");
                logMessage.AppendLine(GetExecutionContextInfo());
                logMessage.AppendLine("==========================================");

                // Aquí se podría integrar con el sistema de logging
                Debug.WriteLine(logMessage.ToString());
                
                // También se podría escribir a un archivo de log
                // File.AppendAllText("error.log", logMessage.ToString());
            }
            catch
            {
                // Si falla el logging, no hacer nada para evitar bucles infinitos
            }
        }

        /// <summary>
        /// Obtiene un mensaje amigable para el usuario basado en el tipo de excepción
        /// </summary>
        /// <param name="ex">Excepción a analizar</param>
        /// <returns>Mensaje amigable para el usuario</returns>
        private static string GetUserFriendlyMessage(Exception ex)
        {
            return ex switch
            {
                ArgumentNullException => "Faltan datos requeridos para completar la operación.",
                ArgumentException => "Los datos proporcionados no son válidos.",
                InvalidOperationException => "La operación no se puede realizar en este momento.",
                UnauthorizedAccessException => "No tiene permisos para realizar esta operación.",
                FileNotFoundException => "No se encontró el archivo solicitado.",
                DirectoryNotFoundException => "No se encontró el directorio solicitado.",
                IOException => "Error al acceder al archivo o directorio.",
                SqlException => "Error en la base de datos. Por favor, intente nuevamente.",
                TimeoutException => "La operación tardó demasiado en completarse.",
                NotSupportedException => "Esta operación no está soportada.",
                FormatException => "El formato de los datos no es válido.",
                OverflowException => "Los datos exceden el límite permitido.",
                DivideByZeroException => "Error en el cálculo matemático.",
                IndexOutOfRangeException => "Error al acceder a los datos.",
                NullReferenceException => "Error interno del sistema.",
                OutOfMemoryException => "Memoria insuficiente para completar la operación.",
                StackOverflowException => "Error interno del sistema.",
                ThreadAbortException => "La operación fue cancelada.",
                AccessViolationException => "Error de acceso a memoria.",
                _ => "Ha ocurrido un error inesperado. Por favor, intente nuevamente."
            };
        }

        /// <summary>
        /// Valida si una excepción es recuperable
        /// </summary>
        /// <param name="ex">Excepción a evaluar</param>
        /// <returns>True si la excepción es recuperable</returns>
        public static bool IsRecoverableException(Exception ex)
        {
            return !IsCriticalException(ex) && 
                   ex is not OutOfMemoryException &&
                   ex is not StackOverflowException &&
                   ex is not ThreadAbortException &&
                   ex is not AccessViolationException;
        }

        /// <summary>
        /// Obtiene sugerencias de resolución basadas en el tipo de excepción
        /// </summary>
        /// <param name="ex">Excepción a analizar</param>
        /// <returns>Lista de sugerencias de resolución</returns>
        public static List<string> GetResolutionSuggestions(Exception ex)
        {
            var suggestions = new List<string>();

            switch (ex)
            {
                case ArgumentNullException:
                    suggestions.Add("Verifique que todos los campos requeridos estén completos.");
                    suggestions.Add("Asegúrese de que los datos no estén vacíos.");
                    break;

                case ArgumentException:
                    suggestions.Add("Verifique el formato de los datos ingresados.");
                    suggestions.Add("Asegúrese de que los valores estén dentro del rango permitido.");
                    break;

                case InvalidOperationException:
                    suggestions.Add("Verifique que la operación sea válida en el estado actual.");
                    suggestions.Add("Intente cerrar y abrir nuevamente la aplicación.");
                    break;

                case UnauthorizedAccessException:
                    suggestions.Add("Verifique que tenga los permisos necesarios.");
                    suggestions.Add("Contacte al administrador del sistema.");
                    break;

                case FileNotFoundException:
                case DirectoryNotFoundException:
                    suggestions.Add("Verifique que el archivo o directorio exista.");
                    suggestions.Add("Asegúrese de que la ruta sea correcta.");
                    break;

                case IOException:
                    suggestions.Add("Verifique que el archivo no esté siendo usado por otra aplicación.");
                    suggestions.Add("Asegúrese de tener permisos de escritura.");
                    break;

                case SqlException:
                    suggestions.Add("Verifique la conexión a la base de datos.");
                    suggestions.Add("Intente nuevamente en unos momentos.");
                    break;

                case TimeoutException:
                    suggestions.Add("Verifique su conexión a internet.");
                    suggestions.Add("Intente nuevamente en unos momentos.");
                    break;

                default:
                    suggestions.Add("Reinicie la aplicación.");
                    suggestions.Add("Contacte al soporte técnico si el problema persiste.");
                    break;
            }

            return suggestions;
        }
    }
}
