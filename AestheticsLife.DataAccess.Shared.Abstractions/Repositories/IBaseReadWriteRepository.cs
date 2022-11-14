using AestheticsLife.DataAccess.Shared.Abstractions.Interfaces;

namespace AestheticsLife.DataAccess.Shared.Abstractions.Repositories;

public interface IBaseReadWriteRepository<TEntity> 
    : IBaseReadonlyRepository<TEntity>
    where TEntity : class, IEntity
{
    long Save(TEntity model);

    Task<long> SaveAsync(TEntity model);
    
    TEntity Update(TEntity model);

    Task<TEntity> UpdateAsync(TEntity model);

    void SaveChanges();

    Task SaveChangesAsync();

    void Remove(TEntity model);
        
    Task RemoveAsync(TEntity model);

    void Remove(long id);
        
    Task RemoveAsync(long id);
}