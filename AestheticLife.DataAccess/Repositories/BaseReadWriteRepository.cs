using AestheticLife.DataAccess.Domain.Abstractions.Interfaces;
using AestheticsLife.DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AestheticLife.DataAccess;

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
        if (model.Id > 0)
        {
            _dbSet.Update(model);
        }
        else
        {
            _dbSet.Add(model);
        }
            
        _dbContext.SaveChanges();
        return model.Id;
    }

    public virtual async Task<long> SaveAsync(TEntity model)
    {
        if (model.Id > 0)
        {
            _dbSet.Update(model);
        }
        else
        {
            await _dbSet.AddAsync(model);
        }

        await _dbContext.SaveChangesAsync();
        
        return model.Id;
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