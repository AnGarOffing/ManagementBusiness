using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ManagementBusiness.Data;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Contenedor de servicios para inyección de dependencias
    /// </summary>
    public static class ServiceContainer
    {
        private static IServiceProvider _serviceProvider;

        /// <summary>
        /// Configura los servicios de la aplicación
        /// </summary>
        public static void ConfigureServices()
        {
            var services = new ServiceCollection();

            // Configurar configuración
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            // Configurar Entity Framework
            services.AddDbContext<ManagementBusinessContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            // Configurar servicios de base de datos
            services.AddScoped<IDatabaseService, DatabaseService>();

            // Configurar repositorios
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepositoryWithId<,>), typeof(RepositoryWithId<,>));
            services.AddScoped(typeof(IRepositoryWithAudit<>), typeof(RepositoryWithAudit<>));
            services.AddScoped(typeof(IRepositoryWithSoftDelete<>), typeof(RepositoryWithSoftDelete<>));

            // Configurar Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Configurar servicio de logging
            services.AddSingleton<ILoggingService, LoggingService>();

            // Configurar servicios de navegación
            services.AddScoped<INavigationService, NavigationService>();

            // Construir el proveedor de servicios
            _serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Obtiene un servicio del contenedor
        /// </summary>
        public static T GetService<T>() where T : class
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException("Los servicios no han sido configurados. Llama a ConfigureServices() primero.");
            }

            return _serviceProvider.GetService<T>() 
                ?? throw new InvalidOperationException($"No se pudo resolver el servicio de tipo {typeof(T).Name}");
        }

        /// <summary>
        /// Obtiene un servicio requerido del contenedor
        /// </summary>
        public static T GetRequiredService<T>() where T : class
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException("Los servicios no han sido configurados. Llama a ConfigureServices() primero.");
            }

            return _serviceProvider.GetRequiredService<T>();
        }

        /// <summary>
        /// Libera los recursos del contenedor
        /// </summary>
        public static void Dispose()
        {
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
