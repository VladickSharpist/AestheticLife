using AestheticsLife.DataAccess.Shared.Repositories;
using DataAccess.Auth.Abstractions.Models;
using DataAccess.Auth.Abstractions.Repositories;

namespace DataAccess.Auth.Repositories;

internal class UserRoleRepository : BaseReadonlyRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(AuthDbContext dbContext) : base(dbContext)
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