using AestheticsLife.DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AestheticLife.DataAccess;

public class BaseReadWriteRepository<TEntity>
    :   BaseReadonlyRepository<TEntity>,
        IBaseReadWriteRepository<TEntity>
{
    public BaseReadWriteRepository(DbContext context) : base(context)
    {
        
    }
}