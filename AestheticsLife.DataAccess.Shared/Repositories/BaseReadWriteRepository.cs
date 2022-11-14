using AestheticsLife.DataAccess.Shared.Abstractions.Interfaces;
using AestheticsLife.DataAccess.Shared.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AestheticsLife.DataAccess.Shared.Repositories;

internal class BaseReadWriteRepository<TEntity>
    : BaseReadonlyRepository<TEntity>,
      IBaseReadWriteRepository<TEntity> 
    where TEntity : class, IEntity
{
    public BaseReadWriteRepository(DbContext dbContext) 
        : base(dbContext)
    {
    }

    public virtual long Save(TEntity model)
    {
        _dbSet.Add(model);
        _dbContext.SaveChanges();
        return model.Id;
    }

    public virtual async Task<long> SaveAsync(TEntity model)
    {
        await _dbSet.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        
        return model.Id;
    }

    public TEntity Update(TEntity model)
    {
        _dbSet.Update(model);
        _dbContext.SaveChanges();
        return model;
    }

    public async Task<TEntity> UpdateAsync(TEntity model)
    {
        _dbSet.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
        
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public virtual void Remove(TEntity model)
    {
        _dbContext.Remove(model);
        _dbContext.SaveChanges();
    }

    public virtual async Task RemoveAsync(TEntity model)
    {
        _dbContext.Remove(model);
        await _dbContext.SaveChangesAsync();
    }
        
    public virtual void Remove(long id)
    {
        var model = _dbSet.SingleOrDefault(x => x.Id == id);
           
        Remove(model);
    }
        
    public virtual async Task RemoveAsync(long id)
    {
        var model = await _dbSet.SingleOrDefaultAsync(x => x.Id == id);

        await RemoveAsync(model);
    }
}