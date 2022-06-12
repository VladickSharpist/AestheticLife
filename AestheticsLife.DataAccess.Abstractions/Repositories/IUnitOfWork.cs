using AestheticLife.DataAccess.Domain.Abstractions.Interfaces;
using AestheticLife.DataAccess.Domain.Models;

namespace AestheticsLife.DataAccess.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IBaseReadonlyRepository<TEntity> GetReadonlyRepository<TEntity>() 
        where TEntity : class;
        
    IBaseReadWriteRepository<TEntity> GetReadWriteRepository<TEntity>() 
        where TEntity : class, IEntity;

    TIRepository GetCustomRepository<TEntity, TIRepository>()
        where TEntity : class
        where TIRepository : class, IBaseReadonlyRepository<TEntity>;

    int SaveChanges();
        
    Task<int> SaveChangesAsync();

    int ExecuteSqlCommand(
        string sql, 
        params object[] parameters);

    IQueryable<TEntity> FromSql<TEntity>(
        string sql, 
        params object[] parameters) 
        where TEntity : class;
}