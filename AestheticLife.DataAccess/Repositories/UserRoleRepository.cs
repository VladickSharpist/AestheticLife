using AestheticLife.DataAccess.Domain.Models;
using AestheticsLife.DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AestheticLife.DataAccess;

internal class UserRoleRepository : BaseReadonlyRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(AestheticLifeDbContext dbContext) : base(dbContext)
    {
    }

    public async Task SaveAsync(UserRole model)
    {
        await _dbSet.AddAsync(model);
    }
    
    public virtual async Task RemoveAsync(UserRole model)
    {
        _dbContext.Remove(model);
        await _dbContext.SaveChangesAsync();
    }

}