using AestheticLife.DataAccess.Domain.Abstractions.Interfaces;
using AestheticLife.DataAccess.Domain.Models;
using AestheticsLife.DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AestheticLife.DataAccess;

internal class UnitOfWork<TContext> 
        : IUnitOfWork
        where TContext : DbContext
    {
        private bool _disposed;
        private IDictionary<Type, object> _readonlyRepositories;
        private IDictionary<Type, object> _readwriteRepositories;
        public TContext DbContext { get; }

        public UnitOfWork(TContext context)
        {
            DbContext = context 
               ?? throw new ArgumentNullException(nameof(context));
        }

        public IBaseReadonlyRepository<TEntity> GetReadonlyRepository<TEntity>()
            where TEntity : class
        {
            if (_readonlyRepositories == null)
            {
                _readonlyRepositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_readonlyRepositories.ContainsKey(type))
            {
                _readonlyRepositories[type] = new BaseReadonlyRepository<TEntity>(DbContext);
            }

            return (IBaseReadonlyRepository<TEntity>) _readonlyRepositories[type];
        }

        public IBaseReadWriteRepository<TEntity> GetReadWriteRepository<TEntity>()
            where TEntity : class, IEntity
        {
            if (_readwriteRepositories == null)
            {
                _readwriteRepositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_readwriteRepositories.ContainsKey(type))
            {
                _readwriteRepositories[type] = new BaseReadWriteRepository<TEntity>(DbContext);
            }

            return (IBaseReadWriteRepository<TEntity>) _readwriteRepositories[type];
        }

        public TIRepository GetCustomRepository<TEntity, TIRepository>()
            where TEntity : class
            where TIRepository : class, IBaseReadonlyRepository<TEntity>
        {
            var customRepo = DbContext.GetService<TIRepository>();
            if (customRepo != null)
            {
                return customRepo;
            }

            return null;
        }

        public int ExecuteSqlCommand(
            string sql, 
            params object[] parameters) 
            => DbContext.Database.ExecuteSqlRaw(sql, parameters);

        public IQueryable<TEntity> FromSql<TEntity>(
            string sql, 
            params object[] parameters) 
            where TEntity : class 
            => DbContext.Set<TEntity>().FromSqlRaw(sql, parameters);

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _readonlyRepositories?.Clear();
                    _readwriteRepositories?.Clear();
                    
                    DbContext.Dispose();
                }
            }

            _disposed = true;
        }
    }