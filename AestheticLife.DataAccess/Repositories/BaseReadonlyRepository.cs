using AestheticsLife.DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AestheticLife.DataAccess;

public class BaseReadonlyRepository<TEntity> : IBaseReadonlyRepository<TEntity>
{
    public BaseReadonlyRepository(DbContext context)
    {
        
    }
}