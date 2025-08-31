using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Implementación del servicio de logging
    /// </summary>
    public class LoggingService : ILoggingService
    {
        private readonly string _logDirectory;
        private readonly string _logFilePath;
        private readonly object _lockObject = new object();

        public LoggingService()
        {
            // Crear directorio de logs en la carpeta de la aplicación
            _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(_logDirectory);

            // Crear archivo de log con fecha actual
            var fileName = $"ManagementBusiness_{DateTime.Now:yyyyMMdd}.log";
            _logFilePath = Path.Combine(_logDirectory, fileName);
        }

        public void LogInformation(string message, string? source = null)
        {
            WriteLog("INFO", message, source);
        }

        public void LogWarning(string message, string? source = null)
        {
            WriteLog("WARN", message, source);
        }

        public void LogError(string message, string? source = null)
        {
            WriteLog("ERROR", message, source);
        }

        public void LogError(string message, Exception exception, string? source = null)
        {
            var fullMessage = $"{message}\nExcepción: {exception.Message}\nStackTrace: {exception.StackTrace}";
            WriteLog("ERROR", fullMessage, source);
        }

        public void LogDebug(string message, string? source = null)
        {
#if DEBUG
            WriteLog("DEBUG", message, source);
#endif
        }

        public void LogTrace(string message, string? source = null)
        {
#if DEBUG
            WriteLog("TRACE", message, source);
#endif
        }

        /// <summary>
        /// Escribe el log tanto en consola como en archivo
        /// </summary>
        private void WriteLog(string level, string message, string? source)
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var sourceInfo = !string.IsNullOrEmpty(source) ? $"[{source}]" : "";
            var logEntry = $"{timestamp} [{level}] {sourceInfo} {message}";

            // Escribir en consola de debug
            Debug.WriteLine(logEntry);

            // Escribir en archivo de log
            try
            {
                lock (_lockObject)
                {
                    File.AppendAllText(_logFilePath, logEntry + Environment.NewLine, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                // Si falla la escritura al archivo, solo escribir en consola
                Debug.WriteLine($"Error al escribir en archivo de log: {ex.Message}");
            }
        }

        /// <summary>
        /// Limpia archivos de log antiguos (más de 30 días)
        /// </summary>
        public void CleanupOldLogs()
        {
            try
            {
                var cutoffDate = DateTime.Now.AddDays(-30);
                var logFiles = Directory.GetFiles(_logDirectory, "ManagementBusiness_*.log");

                foreach (var logFile in logFiles)
                {
                    var fileInfo = new FileInfo(logFile);
                    if (fileInfo.CreationTime < cutoffDate)
                    {
                        File.Delete(logFile);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al limpiar logs antiguos: {ex.Message}");
            }
        }
    }
}
