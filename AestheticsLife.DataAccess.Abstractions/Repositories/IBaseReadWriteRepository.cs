using AestheticLife.DataAccess.Domain.Abstractions.Interfaces;

namespace AestheticsLife.DataAccess.Abstractions;

public interface IBaseReadWriteRepository<TEntity> 
    : IBaseReadonlyRepository<TEntity>
    where TEntity : class, IEntity
{
    void Save(TEntity model);

    Task SaveAsync(TEntity model);

    void SaveChanges();

    Task SaveChangesAsync();

    void Remove(TEntity model);
        
    Task RemoveAsync(TEntity model);

    void Remove(long id);
        
    Task RemoveAsync(long id);
}