using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ManagementBusiness.Data;

namespace ManagementBusiness.Services
{
    /// <summary>
    /// Implementación del patrón Unit of Work que coordina múltiples repositorios
    /// y gestiona transacciones de base de datos
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ManagementBusinessContext _context;
        private readonly IServiceProvider _serviceProvider;
        private IDbContextTransaction _currentTransaction;
        private readonly Dictionary<Type, object> _repositories;
        private bool _disposed = false;

        public UnitOfWork(ManagementBusinessContext context, IServiceProvider serviceProvider)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _repositories = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Obtiene el repositorio genérico para una entidad específica
        /// </summary>
        public IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<T>);
                var repository = Activator.CreateInstance(repositoryType, _context);
                _repositories[type] = repository;
            }

            return (IRepository<T>)_repositories[type];
        }

        /// <summary>
        /// Obtiene el repositorio con ID para una entidad específica
        /// </summary>
        public IRepositoryWithId<T, TId> GetRepositoryWithId<T, TId>() where T : class
        {
            var type = typeof(T);
            var key = $"{type.Name}_{typeof(TId).Name}";
            
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryWithId<T, TId>);
                var repository = Activator.CreateInstance(repositoryType, _context);
                _repositories[type] = repository;
            }

            return (IRepositoryWithId<T, TId>)_repositories[type];
        }

        /// <summary>
        /// Obtiene el repositorio con auditoría para una entidad específica
        /// </summary>
        public IRepositoryWithAudit<T> GetRepositoryWithAudit<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryWithAudit<T>);
                var repository = Activator.CreateInstance(repositoryType, _context);
                _repositories[type] = repository;
            }

            return (IRepositoryWithAudit<T>)_repositories[type];
        }

        /// <summary>
        /// Obtiene el repositorio con eliminación suave para una entidad específica
        /// </summary>
        public IRepositoryWithSoftDelete<T> GetRepositoryWithSoftDelete<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryWithSoftDelete<T>);
                var repository = Activator.CreateInstance(repositoryType, _context);
                _repositories[type] = repository;
            }

            return (IRepositoryWithSoftDelete<T>)_repositories[type];
        }

        /// <summary>
        /// Guarda todos los cambios pendientes en la base de datos
        /// </summary>
        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log del error (se implementará cuando se agregue el servicio de logging)
                throw new InvalidOperationException("Error al guardar cambios en la base de datos", ex);
            }
        }

        /// <summary>
        /// Guarda todos los cambios pendientes en la base de datos de forma asíncrona
        /// </summary>
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log del error (se implementará cuando se agregue el servicio de logging)
                throw new InvalidOperationException("Error al guardar cambios en la base de datos", ex);
            }
        }

        /// <summary>
        /// Inicia una nueva transacción de base de datos
        /// </summary>
        public IDbTransaction BeginTransaction()
        {
            if (_currentTransaction != null)
            {
                throw new InvalidOperationException("Ya existe una transacción activa");
            }

            _currentTransaction = _context.Database.BeginTransaction();
            return new DbTransactionWrapper(_currentTransaction);
        }

        /// <summary>
        /// Inicia una nueva transacción de base de datos de forma asíncrona
        /// </summary>
        public async Task<IDbTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                throw new InvalidOperationException("Ya existe una transacción activa");
            }

            _currentTransaction = await _context.Database.BeginTransactionAsync();
            return new DbTransactionWrapper(_currentTransaction);
        }

        /// <summary>
        /// Confirma la transacción actual
        /// </summary>
        public void CommitTransaction()
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("No hay transacción activa para confirmar");
            }

            try
            {
                _currentTransaction.Commit();
            }
            finally
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        /// <summary>
        /// Revierte la transacción actual
        /// </summary>
        public void RollbackTransaction()
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("No hay transacción activa para revertir");
            }

            try
            {
                _currentTransaction.Rollback();
            }
            finally
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        /// <summary>
        /// Ejecuta una operación dentro de una transacción
        /// </summary>
        public T ExecuteInTransaction<T>(Func<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            using var transaction = BeginTransaction();
            try
            {
                var result = action();
                SaveChanges();
                transaction.Commit();
                return result;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Ejecuta una operación dentro de una transacción de forma asíncrona
        /// </summary>
        public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            using var transaction = await BeginTransactionAsync();
            try
            {
                var result = await action();
                await SaveChangesAsync();
                transaction.Commit();
                return result;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Ejecuta una acción dentro de una transacción
        /// </summary>
        public void ExecuteInTransaction(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            using var transaction = BeginTransaction();
            try
            {
                action();
                SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Ejecuta una acción dentro de una transacción de forma asíncrona
        /// </summary>
        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            using var transaction = await BeginTransactionAsync();
            try
            {
                await action();
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Libera los recursos utilizados por la unidad de trabajo
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Libera los recursos utilizados por la unidad de trabajo
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _currentTransaction?.Dispose();
                _context?.Dispose();
                _disposed = true;
            }
        }
    }

    /// <summary>
    /// Wrapper para transacciones de Entity Framework que implementa IDbTransaction
    /// </summary>
    public class DbTransactionWrapper : IDbTransaction
    {
        private readonly IDbContextTransaction _transaction;

        public DbTransactionWrapper(IDbContextTransaction transaction)
        {
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }
}
